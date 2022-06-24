using DisableUnityGC.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace DisableUnityGC.HarmonyPatches
{
    [HarmonyPatch(typeof(DisableGCWhileEnabled))]
    internal class DisableGCWhileEnabledPatch
    {
        private static string s_activeSceneName = "";
        private static bool s_isGameCore = false;

        static DisableGCWhileEnabledPatch()
        {
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private static void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            s_activeSceneName = arg1.name;
            s_isGameCore = arg1.name == "GameCore";
        }

        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(DisableGCWhileEnabled), nameof(DisableGCWhileEnabled.OnEnable));
            yield return AccessTools.Method(typeof(DisableGCWhileEnabled), nameof(DisableGCWhileEnabled.OnDisable));
        }

        [HarmonyPostfix]
        public static void Postfix()
        {
            Plugin.Log.Debug($"scene:{s_activeSceneName}, GC Mode:{GarbageCollector.GCMode}");
            if (PluginConfig.Instance.Enable && s_isGameCore) {
                GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
                Plugin.Log.Info($"GC Disable!");
            }
        }
    }
}
