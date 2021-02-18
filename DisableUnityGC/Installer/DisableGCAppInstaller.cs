using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using SiraUtil;
using UnityEngine.Scripting;
using UnityEngine.SceneManagement;

namespace DisableUnityGC.Installer
{
    public class DisableGCAppInstaller : MonoInstaller
    {
        [Inject]
        GameScenesManager _gameScenesManager;

        public override void InstallBindings()
        {
            
        }

        public override void Start()
        {
            base.Start();
            this._gameScenesManager.transitionDidFinishEvent += this._gameScenesManager_transitionDidFinishEvent;
            GarbageCollector.GCModeChanged += this.GarbageCollector_GCModeChanged;
        }

        private void OnDestroy()
        {
            this._gameScenesManager.transitionDidFinishEvent -= this._gameScenesManager_transitionDidFinishEvent;
            GarbageCollector.GCModeChanged -= this.GarbageCollector_GCModeChanged;
        }

        private void _gameScenesManager_transitionDidFinishEvent(ScenesTransitionSetupDataSO arg1, DiContainer arg2)
        {
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
