using DisableUnityGC.Configuration;
using DisableUnityGC.Models;
using SiraUtil;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using Zenject;

namespace DisableUnityGC.Installer
{
    public class DisableGCAppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<NotifyMemorySize>().FromNewComponentOnNewGameObject().AsCached().NonLazy();
        }
    }
}
