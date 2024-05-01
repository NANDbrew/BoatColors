using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatColors
{
    internal class ShipColorPatches
    {
        [HarmonyPatch(typeof(PurchasableBoat), "Awake")]
        public static class PurchasableBoatPatch
        {
            [HarmonyPostfix]
            public static void PostFix(PurchasableBoat __instance)
            {
                ShipColors.Cache3(__instance.transform);
            }
        }

        [HarmonyPatch(typeof(SaveLoadManager))]
        public static class SaveLoadPatch
        {
            [HarmonyPatch("LoadModData")]
            [HarmonyPostfix]
            public static void LoadModData()
            {
                ShipColors.LoadColors();
            }
            [HarmonyPatch("SaveModData")]
            [HarmonyPrefix]
            public static void SaveModData()
            {
                ShipColors.SaveColors();
            }
        }

        [HarmonyPatch(typeof(PlayerEmbarkDisembarkTrigger), "EnterBoat")]
        public static class Patch1
        {
            [HarmonyPostfix]
            public static void PostFix()
            {
                Plugin.UpdateConfigsFromBoat();
            }
        }

    }
}
