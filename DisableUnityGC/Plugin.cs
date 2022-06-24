using DisableUnityGC.Installer;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace DisableUnityGC
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        private static Harmony s_harmony;
        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, Config conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Info("DisableUnityGC initialized.");
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
            zenjector.Install<DisableGCAppInstaller>(Location.App);
            zenjector.Install<DisableGCMenuInstaller>(Location.Menu);
        }
        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");
        }

        [OnEnable]
        public void OnEnable()
        {
            try {
                s_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e) {
                Log.Error(e);
            }
        }

        [OnDisable]
        public void OnDisable()
        {
            try {
                s_harmony?.UnpatchSelf();
            }
            catch (Exception e) {
                Log.Error(e);
            }
        }
    }
}
