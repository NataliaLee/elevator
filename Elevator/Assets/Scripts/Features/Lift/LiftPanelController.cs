using Assets.Scripts.Core;
using Assets.Scripts.Features.Windows.Buttons;
using Assets.Scripts.Logs;
using Assets.Scripts.ResourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features.Lift
{
    public class LiftPanelController : MonoBehaviour
    {
        [Inject]
        private ICustomLogger _logger;
        [Inject]
        private Instantiator _instantiator;
        [Inject]
        private LiftService _liftService;

        [SerializeField]
        private Transform _buttonsHolder;
        [SerializeField]
        private Text _currenLiftPosition;
        [SerializeField]
        private Text _currentDirection;
        [SerializeField]
        private Button _stopButton;
        private LiftButtonData[] _buttonsData;
        private TextButton[] _buttons;

        public void Setup(int floorsNumber)
        {
            AddButtons(floorsNumber);
            _currenLiftPosition.text = 0.ToString();
            _currentDirection.text = Direction.None.ToString();

            _liftService.OnFloorUpdate += UpdateFloorView;
            _liftService.OnArrivedOnFloor += (floor, direction) =>
            {
                UpdateFloorView(floor,direction);
                ResetButton(floor);
            };
            _liftService.OnChangeDirection += (direction) => { _currentDirection.text = direction.ToString(); };
            _stopButton.onClick.AddListener(_liftService.StopLift);
        }

        private void UpdateFloorView(int floor, Direction direction)
        {
            _currenLiftPosition.text = floor.ToString();
            _currentDirection.text = direction.ToString();
        }

        private void AddButtons(int floorsNumber)
        {
            _buttonsData = new LiftButtonData[floorsNumber];
            _buttons = new TextButton[floorsNumber];
            var buttonPrefab = Resources.Load<GameObject>(ResourceLocator.FLOOR_BUTTON);
            for (int i = 0; i < floorsNumber; i++)
            {
                var floor = i;
                _buttonsData[i] = new LiftButtonData(i);
                var button = _instantiator.LoadGameObjectFromReference(buttonPrefab, _buttonsHolder);
                _buttons[i] = button.GetComponent<TextButton>();
                _buttons[i].SetHighlighted(_buttonsData[i].IsPushed);
                _buttons[i].SetText(floor.ToString());
                _buttons[i].SetOnClickAction(() =>
                {
                    OnFloorButtonClicked(floor);
                });
            }
        }

        private void ResetButton(int floor)
        {
            if (floor >= _buttonsData.Length)
                return;
            _buttonsData[floor].Reset(); 
            _buttons[floor].SetHighlighted(_buttonsData[floor].IsPushed);
        }

        private void OnFloorButtonClicked(int floor)
        {
            if (floor >= _buttonsData.Length)
            {
                Debug.LogError($"error in floor amount. clicked on {floor}");
                return;
            }
            _logger.Log($"clicked on floor button {floor}. id selected {_buttonsData[floor].IsPushed}");
            _buttonsData[floor].OnClicked();
            _buttons[floor].SetHighlighted(_buttonsData[floor].IsPushed);
            if (_buttonsData[floor].IsPushed)
            {
                _liftService.AddFloor(Direction.None, floor);
            }
            else
            {
                _liftService.RemoveFloor(Direction.None, floor);
            }
        }
    }
}
