using Assets.Scripts.Logs;
using Assets.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.Floors
{
    public class FloorsPanelController: MonoBehaviour
    {
        [Inject]
        private ICustomLogger _logger;

        [SerializeField]
        private List _floorsList;
        [SerializeField]
        private ListItemBase _floorItem;
        private FloorData[] _floorsData;

        //todo on click
        public void Setup(int floorsAmount)
        {
            _floorsData = new FloorData[floorsAmount];
            for (int i = 0; i < floorsAmount; i++)
            {
                _floorsData[i] = new FloorData(i);
            }
            _floorsList.onItemLoaded += OnItemLoaded;
            _floorsList.Create(floorsAmount, _floorItem);
        }

        private void OnItemLoaded(ListItemBase item)
        {
            var floorItem = (FloorListItem)item;
            floorItem.Setup(_floorsData[floorItem.Index],OnUpLiftButtonPressed, OnDownLiftButtonPressed);
        }

        private void OnUpLiftButtonPressed(int floorNumber)
        {
            if (floorNumber >= _floorsData.Length)
            {
                _logger.LogError($"clicked on non existed floor {floorNumber}");
                return;
            }
            var floorData = _floorsData[floorNumber];
            _logger.Log($"clicked UP on floor {floorNumber}. button is on: {floorData.UpIsPushed}");
        }

        private void OnDownLiftButtonPressed(int floorNumber)
        {
            if (floorNumber >= _floorsData.Length)
            {
                _logger.LogError($"clicked on non existed floor {floorNumber}");
                return;
            }
            var floorData = _floorsData[floorNumber];
            _logger.Log($"clicked DOWN on floor {floorNumber}. button is on: {floorData.DownIsPushed}");
        }
    }
}
