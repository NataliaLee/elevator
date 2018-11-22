using Assets.Scripts.Core;
using Assets.Scripts.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.Windows
{
    public class WindowService: IInitializable
    {
        private ICustomLogger _logger;
        private GameObject _currentWindow;
        private WindowType _currentWindowType=WindowType.None;
        private WindowsSettings _windowsSettings;
        private Instantiator _instantiator;

        public WindowService(ICustomLogger customLogger, WindowsSettings windowsSettings, Instantiator instantiator)
        {
            _logger = customLogger;
            _windowsSettings = windowsSettings;
            _instantiator = instantiator;
        }

        public void Initialize()
        {
            _logger.Log("Initialize WindowService");
            ChangeWindow(WindowType.Setup);
        }

        public void ChangeWindow(WindowType newWindow)
        {
            _logger.Log($"try change window type {newWindow}");
            if (newWindow == _currentWindowType)
                return;
            var windowSettings=_windowsSettings.windowItems.FirstOrDefault(_=>_.windowType==newWindow);
            if (windowSettings == null)
            {
                _logger.LogError($"no windows settings for window type {newWindow}");
            }
            else
            {
                if (_currentWindow != null)
                {
                    _logger.Log("destroy previous window");
                    GameObject.Destroy(_currentWindow.gameObject);
                }
                _currentWindow=_instantiator.LoadGameObjectFromReference(windowSettings.windowPrefab);
            }
        }
    }
}
