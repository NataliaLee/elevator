using Assets.Scripts.Features.Windows.Buttons;
using Assets.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Features.Floors
{
    public class FloorListItem : ListItemBase
    {
        [SerializeField]
        private Text _floorNumber;
        [SerializeField]
        private HighlightedButton _upButton;
        [SerializeField]
        private HighlightedButton _downButton;
        private FloorData _currentFloorData;
        private Action<int> _onLiftUpButtonClicked;
        private Action<int> _onLiftDownButtonClicked;

        public void Setup(FloorData floorData, Action<int> onLiftUpButtonClicked, Action<int> onLiftDownButtonClicked)
        {
            _currentFloorData = floorData;
            _onLiftUpButtonClicked = onLiftUpButtonClicked;
            _onLiftDownButtonClicked = onLiftDownButtonClicked;
            _downButton.gameObject.SetActive(floorData.FloorNumber>0);            
            UpdateView();
        }

        public void UpdateView()
        {
            _floorNumber.text = _currentFloorData.FloorNumber.ToString();
            _upButton.SetHighlighted(_currentFloorData.UpIsPushed);
            _downButton.SetHighlighted(_currentFloorData.DownIsPushed);
        }

        private void Awake()
        {
            _upButton.SetOnClickAction(OnUpClicked);
            _downButton.SetOnClickAction(OnDownClicked);
        }

        private void OnDownClicked()
        {
            _currentFloorData.OnDownClicked();
            _downButton.SetHighlighted(_currentFloorData.DownIsPushed);
            _onLiftDownButtonClicked?.Invoke(_currentFloorData.FloorNumber);
        }

        private void OnUpClicked()
        {
            _currentFloorData.OnUpClicked();
            _upButton.SetHighlighted(_currentFloorData.UpIsPushed);
            _onLiftUpButtonClicked?.Invoke(_currentFloorData.FloorNumber);
        }
    }
}
