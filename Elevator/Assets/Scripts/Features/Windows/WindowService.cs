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
        private ICustomLogger _customLogger;
        private GameObject _currentWindow;
        private WindowType _currentWindowType=WindowType.None;
        private WindowsSettings _windowsSettings;
        private Instantiator _instantiator;

        public WindowService(ICustomLogger customLogger, WindowsSettings windowsSettings, Instantiator instantiator)
        {
            _customLogger = customLogger;
            _windowsSettings = windowsSettings;
            _instantiator = instantiator;
        }

        public void Initialize()
        {
            _customLogger.Log("Initialize WindowService");
            ChangeWindow(WindowType.Setup);
        }

        public void ChangeWindow(WindowType newWindow)
        {
            _customLogger.Log($"try change window type {newWindow}");
            if (newWindow == _currentWindowType)
                return;
            var windowSettings=_windowsSettings.windowItems.FirstOrDefault(_=>_.windowType==newWindow);
            if (windowSettings == null)
            {
                _customLogger.LogError($"no windows settings for window type {newWindow}");
            }
            else
            {
                _instantiator.LoadGameObjectFromReference(windowSettings.windowPrefab);
            }
        }
    }
}
