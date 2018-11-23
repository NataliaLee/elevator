using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zenject;
using DG.Tweening;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Assets.Scripts.Logs;
using Assets.Scripts.ResourceHandlers;
using Assets.Scripts.Features.Tools;

namespace Assets.Scripts.Core
{
    public class LiftService
    {
        private const float LIFT_SPEED = 0.5f;
        public int FloorsAmount { get; set; }

        public Direction CurrentDirection { get; private set; }

        public int CurrentFloor
        {
            get
            {
                return (int)_currentPosition;
            }
        }

        public event Action<int, Direction> OnArrivedOnFloor;
        public event Action<int, Direction> OnFloorUpdate;
        public event Action<Direction> OnChangeDirection;

        private ICustomLogger _logger;
        private Instantiator _instantiator;
        private float _currentPosition;
        private int _currentTargetFloor;
        private FloorsToGo _floorsToGo;
        private bool _stopped;
        private TweenerCore<float, float, FloatOptions> _currentMovement;

        public LiftService(ICustomLogger logger, Instantiator instantiator)
        {
            _logger = logger;
            _instantiator = instantiator;
            _floorsToGo = new FloorsToGo(_logger);
        }

        public void AddFloor(Direction direction, int floor)
        {
            if (CurrentFloor == floor)
            {
                OnArrivedOnFloor(floor, CurrentDirection);
                //todo open doors
                return;
            }
            _floorsToGo.AddFloor(floor,direction);
            switch (CurrentDirection)
            {
                case Direction.None:
                    if (CurrentFloor > floor)
                        CurrentDirection = Direction.Down;
                    else
                        CurrentDirection = Direction.Up;
                    _currentTargetFloor = floor;
                    GoToFloor(GetNextFloorTo(CurrentFloor, CurrentDirection));
                    break;
                case Direction.Up:
                    if (direction != Direction.Down && floor<_currentTargetFloor && floor>CurrentFloor)
                    {
                        _currentTargetFloor = floor;//останавливаемся по пути к текущей цели
                    }
                    break;
                case Direction.Down:
                    if (direction != Direction.Up && floor > _currentTargetFloor && floor < CurrentFloor)
                    {
                        _currentTargetFloor = floor;//останавливаемся по пути к текущей цели
                    }
                    break;
            }
        }

        public void RemoveFloor(Direction direction, int floor)
        {
            _floorsToGo.RemoveFloor(direction,floor);
        }

        public void RemoveFloor(int floor)
        {
            _floorsToGo.RemoveFloor(floor);
        }

        public void StopLift()
        {
            if (_currentMovement != null)
                _currentMovement.Kill();
            CurrentDirection = Direction.None;
            OnChangeDirection(CurrentDirection);
        }

        private void GoToFloor(int nextFloor)
        {
            _logger.Log($"GoToFloor {nextFloor}");
            OnChangeDirection(CurrentDirection);
            var duration = (Math.Abs(nextFloor - _currentPosition)) / LIFT_SPEED;
            _currentMovement = DOTween.To(() => _currentPosition, x => _currentPosition = x, nextFloor, duration);
            _currentMovement.onComplete = OnCompleteMovement;
         }

        private void OnCompleteMovement()
        {
            _logger.Log($"lift arrived on floor {CurrentFloor}. position is {_currentPosition}");
            if (_currentTargetFloor != CurrentFloor)
            {
                if (CurrentDirection == Direction.None)
                    return;
                OnFloorUpdate(CurrentFloor,CurrentDirection);
                GoToFloor(GetNextFloorTo(CurrentFloor, CurrentDirection));
            }
            else
            {
                _logger.Log($"<color=blue>lift arrived on target floor {CurrentFloor}</color>");
                var message=_instantiator.LoadGameObjectOnScene<MessageTooltip>(ResourceLocator.MESSAGE_TOOLTIP);
                message.Show("Open doors");
                OnArrivedOnFloor(CurrentFloor, CurrentDirection);
                RemoveFloor(CurrentFloor);
                GoToNextFloor();
            }
        }

        private void GoToNextFloor()
        {
            if (CurrentDirection == Direction.None)
                return;
            var nextFloor = _floorsToGo.GetNextFloor(CurrentFloor, CurrentDirection);
            _logger.Log($"Go to next floor. Current is {CurrentFloor}. next is {nextFloor}");
            if (nextFloor == CurrentFloor)
            {
                CurrentDirection = Direction.None;
                OnChangeDirection(CurrentDirection);
                return;
            }
            _currentTargetFloor = nextFloor;
            if (_currentTargetFloor > CurrentFloor)
                CurrentDirection = Direction.Up;
            else
            {
                CurrentDirection = Direction.Down;
            }
            GoToFloor(GetNextFloorTo(CurrentFloor,CurrentDirection));
        }

        private int GetNextFloorTo(int floor, Direction direction)
        {
            switch (CurrentDirection)
            {
                case Direction.Up:
                    if (floor + 1 >= FloorsAmount)
                    {
                        return 0;
                    }
                    else
                        return floor + 1;
                case Direction.Down:
                    if (floor == 0)
                        return 0;
                    return floor - 1;
                default:
                    return 0;
            }
        }
    }
}
