using Assets.Scripts.Features.Windows;
using Assets.Scripts.Logs;
using Assets.Scripts.ResourceHandlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {

        public override void InstallBindings()
        {
            Container.Bind<ICustomLogger>().To<CustomLogger>().AsSingle();
            Container.Bind<Instantiator>().AsSingle();
            Container.Bind<LiftService>().AsSingle().NonLazy();
            Container.Bind<WindowsSettings>().FromScriptableObjectResource(ResourceLocator.WINDOWS_SETTINGS).AsSingle();
            Container.BindInterfacesAndSelfTo<WindowService>().AsSingle().NonLazy();
        }
    }
}
