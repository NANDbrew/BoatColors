using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BoatColors
{
    [HarmonyPatch(typeof(Shipyard))]
    internal class ShipyardPatches
    {
        /*[HarmonyPatch("AdmitShip")]
        [HarmonyPostfix]
        public static void AdmitShip(GameObject ship)
        {
            foreach(PaintColorButton button in ShipyardUIPatches.buttons)
            {
                button.UpdateButtonColor();
            }
        }*/

        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void AddColors(Shipyard __instance, ref int[] ___availableSailColors)
        {
            if (!Plugin.useCustomSailColors.Value) { return; }

            ___availableSailColors = ___availableSailColors.AddRangeToArray(new int[] { 24, 25 });
        }
    }
}
