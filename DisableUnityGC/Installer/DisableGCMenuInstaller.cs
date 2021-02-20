using DisableUnityGC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using SiraUtil;

namespace DisableUnityGC.Installer
{
    public class DisableGCMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SettingViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
        }
    }
}
