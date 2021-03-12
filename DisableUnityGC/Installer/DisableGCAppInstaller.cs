using DisableUnityGC.Configuration;
using DisableUnityGC.Models;
using SiraUtil;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using Zenject;

namespace DisableUnityGC.Installer
{
    public class DisableGCAppInstaller : MonoInstaller
    {
        [Inject]
        private readonly GameScenesManager _gameScenesManager;

        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<NotifyMemorySize>().FromNewComponentOnNewGameObject(nameof(NotifyMemorySize)).AsCached().NonLazy();
        }

        public override void Start()
        {
            base.Start();
            this._gameScenesManager.transitionDidFinishEvent += this.GameScenesManager_transitionDidFinishEvent;
            GarbageCollector.GCModeChanged += this.GarbageCollector_GCModeChanged;
        }

        private void OnDestroy()
        {
            this._gameScenesManager.transitionDidFinishEvent -= this.GameScenesManager_transitionDidFinishEvent;
            GarbageCollector.GCModeChanged -= this.GarbageCollector_GCModeChanged;
        }

        private void GameScenesManager_transitionDidFinishEvent(ScenesTransitionSetupDataSO arg1, DiContainer arg2)
        {
            if (PluginConfig.Instance?.Enable != true) {
                return;
            }
            if (SceneManager.GetActiveScene().name != "GameCore") {
                GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
            }
            else {
                GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
            }
        }

        private void GarbageCollector_GCModeChanged(GarbageCollector.Mode obj)
        {
            Plugin.Log?.Debug($"Changed GCMode : {obj}");
        }
    }
}
