/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace BoatColors
{
    [HarmonyPatch(typeof(SaveLoadManager))]
    internal class SaveLoadPatches
    {
        [HarmonyPatch("LoadModData")]
        [HarmonyPostfix]
        public static void LoadColors(SaveLoadManager __instance)
        {
            __instance.gameObject.AddComponent<ModRefs>();
        }
    }
}
*/