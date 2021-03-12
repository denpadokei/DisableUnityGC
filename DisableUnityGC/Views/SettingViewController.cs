using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using DisableUnityGC.Configuration;
using Zenject;

namespace DisableUnityGC.Views
{
    [HotReload]
    internal class SettingViewController : BSMLAutomaticViewController, IInitializable
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public string ResourceName => string.Join(".", this.GetType().Namespace, this.GetType().Name);

        [UIValue("disable-gc")]
        public bool Enable
        {
            get => PluginConfig.Instance.Enable;
            set => PluginConfig.Instance.Enable = value;
        }

        [UIValue("enable-memorysize")]
        public bool MemorySize
        {
            get => PluginConfig.Instance.MemorySize;
            set => PluginConfig.Instance.MemorySize = value;
        }

        public void Initialize()
        {
            BSMLSettings.instance.AddSettingsMenu("DisableGCUnity", this.ResourceName, this);
        }
    }
}
