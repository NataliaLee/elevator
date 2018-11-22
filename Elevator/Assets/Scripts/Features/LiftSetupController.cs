using Assets.Scripts.Core;
using Assets.Scripts.Features.Windows;
using Assets.Scripts.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Features
{
    public class LiftSetupController: MonoBehaviour
    {
        [Inject]
        private LiftService _liftService;
        [Inject]
        private WindowService _windowsService;
        [Inject]
        private ICustomLogger _logger;

        [SerializeField]
        private InputField _inputFloorAmountField;
        [SerializeField]
        private Button _okButton;

        void Awake()
        {
            _okButton.onClick.AddListener(OnOkClicked);
        }

        private void OnOkClicked()
        {
            _logger.Log("clicked on ok "+_inputFloorAmountField.text);
            int floors = 0;
            bool result = int.TryParse(_inputFloorAmountField.text, out floors);
            if (result == true)
            {
                _logger.Log($"floors are:{floors}");
                if (floors <= 0)
                {
                    _logger.LogError("amount of floors cant be <=0");
                    return;
                }
                _liftService.FloorsAmount = floors;
                _windowsService.ChangeWindow(WindowType.LiftGame);
            }
            else
            {
                _logger.LogError($"cant parse {_inputFloorAmountField.text}");
            }
        }
    }
}
