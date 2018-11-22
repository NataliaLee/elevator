using Assets.Scripts.Core;
using Assets.Scripts.Features.Floors;
using Assets.Scripts.Features.Lift;
using Assets.Scripts.Logs;
using Assets.Scripts.ResourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features
{
    public class LiftGameController : MonoBehaviour
    {
        [Inject]
        private ICustomLogger _logger;
        [Inject]
        private LiftService _liftService;
        [Inject]
        private Instantiator _instantiator;

        [SerializeField]
        private Transform _container;

        private FloorsPanelController _floorsPanelController;
        private LiftPanelController _liftPanelController;

        void Awake()
        {
            _floorsPanelController = _instantiator.LoadGameObjectOnScene<FloorsPanelController>(ResourceLocator.FLOORS_PANEL, _container);
            _floorsPanelController.Setup(_liftService.FloorsAmount);

            _liftPanelController = _instantiator.LoadGameObjectOnScene<LiftPanelController>(ResourceLocator.LIFT_PANEL, _container);
            _liftPanelController.Setup(_liftService.FloorsAmount);
        }

    }
}
