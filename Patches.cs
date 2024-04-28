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
                Plugin.AddConfigEntries();
                Plugin.UpdateConfigsFromBoat();
                //ShipColors.UpdateColorsFromConfig();
            }
            [HarmonyPatch("SaveModData")]
            [HarmonyPrefix]
            public static void SaveModData()
            {
                ShipColors.SaveColors();
                //Plugin.AddConfigEntries();
                //ShipColors.UpdateColorsFromConfig();
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
        /*        [HarmonyPatch(typeof(PurchasableBoat), "LoadAsPurchased")]
                public static class LoadPatch
                {
                    [HarmonyPostfix]
                    public static void Postfix()
                    {
                        ShipColors.LoadColors();
                    }
                }*/

        /*        [HarmonyPatch(typeof(StartMenu), "Start")]
                public static class StartMenuPatch
                {
                    [HarmonyPostfix]
                    public static void StartPatch()
                    {
                        ShipColors.UpdateColors();
                    }
                }*/
        /*
                [HarmonyPatch(typeof(BoatPerformanceSwitcher), "Update")]
                public static class BoatPerformanceSwitcherPatch
                {
                    [HarmonyPostfix]
                    public static void Postfix(BoatPerformanceSwitcher __instance)
                    {
                        ShipColors.UpdateShipColors(__instance.transform);
                        //Debug.Log("BPS update");
                    }

                }*/
    }
}
