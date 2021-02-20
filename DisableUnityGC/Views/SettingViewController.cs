using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using DisableUnityGC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace DisableUnityGC.Views
{
    [HotReload]
    internal class SettingViewController : BSMLAutomaticViewController, IInitializable
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);

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
