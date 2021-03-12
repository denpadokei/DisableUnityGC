using DisableUnityGC.Views;
using SiraUtil;
using Zenject;

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
