using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace DisableUnityGC.Patches
{
    [HarmonyPatch(typeof(GameScenesManager))]
    internal class GameScenesManagerScenesTransitionCoroutinePatch
    {
        internal static void PostFix()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
        }
    }
}
