using Assets.Scripts.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core
{
    public class FloorsToGo
    {
        private ICustomLogger _logger;
        private SortedSet<int> _liftBtnsToGo;
        private SortedSet<int> _upToGo;
        private SortedSet<int> _downToGo;

        public FloorsToGo(ICustomLogger logger)
        {
            _logger = logger;
            _liftBtnsToGo = new SortedSet<int>();
            _upToGo = new SortedSet<int>();
            _downToGo = new SortedSet<int>();
        }

        public void AddFloor(int floor, Direction direction)
        {
            _logger.Log($"<color=yellow>add floor {floor}</color>");
            switch (direction)
            {
                case Direction.Up:
                    _upToGo.Add(floor);
                    break;
                case Direction.Down:
                    _downToGo.Add(floor);
                    break;
                default:
                    _liftBtnsToGo.Add(floor);
                    break;
            }
        }

        public void RemoveFloor(Direction direction, int floor)
        {
            switch (direction)
            {
                case Direction.Up:
                    _upToGo.Remove(floor);
                    break;
                case Direction.Down:
                    _downToGo.Remove(floor);
                    break;
                default:
                    _liftBtnsToGo.Remove(floor);
                    break;
            }
        }

        public void RemoveFloor(int floor)
        {
            _upToGo.Remove(floor);
            _downToGo.Remove(floor);
            _liftBtnsToGo.Remove(floor);
        }

        public int GetNextFloor(int currentFloor, Direction direction)
        {
            _logger.Log($"GetNextFloor {currentFloor}   dir {direction}");
            if (_upToGo.Count == 0 && _downToGo.Count == 0 && _liftBtnsToGo.Count == 0)
                return currentFloor;

            switch (direction)
            {
                case Direction.None:
                    return GetNearest(currentFloor);
                case Direction.Up:
                    {

                        var suitable = _upToGo.Where(_ => _ > currentFloor).ToList();
                        suitable.AddRange(_liftBtnsToGo.Where(_ => _ > currentFloor));
                        if (suitable.Count == 0)
                            return GetNearest(currentFloor);
                        else
                        {
                            return suitable.OrderBy(_ => _).First();
                        }
                    }
                case Direction.Down:
                    {
                        var suitable = _downToGo.Where(_ => _ < currentFloor).ToList();
                        suitable.AddRange(_liftBtnsToGo.Where(_ => _ < currentFloor));
                        if (suitable.Count == 0)
                            return GetNearest(currentFloor);
                        else
                        {
                            return suitable.OrderByDescending(_ => _).First();
                        }
                    }
                default:
                    return currentFloor;
            }
        }
        
        private int GetNearest(int currentFloor)
        {//пока не работает
            _logger.LogError($"GetNearest {currentFloor}");
            if (_liftBtnsToGo.Count > 0)
                return _liftBtnsToGo.First();
            if (_upToGo.Count > 0)
                return _upToGo.First();
            if (_downToGo.Count > 0)
                return _downToGo.First();
            return currentFloor;
        }
    }
}
