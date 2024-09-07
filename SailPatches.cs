using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BoatColors
{
    internal static class SailColorPatcher
    {
        public static PrefabsDirectory prefabsDirectory;
        public static void UpdateCustomSailColor(int index, Color color)
        {
            prefabsDirectory.sailColors[index] = color;
            ShipyardUI.instance.RefreshButtons();
        }
        public static void UpdateCustomSailColors()
        {
            SailColorPatcher.UpdateCustomSailColor(24, Plugin.customSailColor.Value);
            SailColorPatcher.UpdateCustomSailColor(25, Plugin.customSailColor1.Value);
        }
    }

    [HarmonyPatch(typeof(SaveLoadManager))]
    internal class SailPatches
    {
        /*[HarmonyPatch("LoadModData")]
        [HarmonyPostfix]
        public static void LoadColors(SaveLoadManager __instance)
        {
            __instance.gameObject.AddComponent<ModRefs>();
        }*/


        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void AddColors(SaveLoadManager __instance, ref PrefabsDirectory ___prefabsDirectory)
        {
            if (!Plugin.useCustomSailColors.Value) { return; }
            ___prefabsDirectory.sailColors = ___prefabsDirectory.sailColors.AddRangeToArray(new Color[]{ Color.white, Color.black });
            SailColorPatcher.prefabsDirectory = ___prefabsDirectory;
            //___prefabsDirectory.sailColors.Append(new Color(0, 0, 0));
            //___prefabsDirectory.sailColors[8] = Plugin.customSail.Value;
        }
    }



}
