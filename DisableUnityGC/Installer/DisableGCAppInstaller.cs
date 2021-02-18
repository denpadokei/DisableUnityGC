using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using SiraUtil;

namespace DisableUnityGC.Installer
{
    public class DisableGCAppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<DisableUnityGCController>().FromNewComponentOnNewGameObject(nameof(DisableUnityGCController)).AsSingle().NonLazy();
        }
    }
}
