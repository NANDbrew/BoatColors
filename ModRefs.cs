using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BoatColors
{
    internal class ModRefs : MonoBehaviour
    {
        public Dictionary<string, string> modData;
        public Dictionary<string, object> meshRenderers;
        public Dictionary<string, Material> boatMats; 
        private void Start()
        {
            modData = GameState.modData;
            meshRenderers = ShipColors.meshRenderers;
            boatMats = ShipColors.boatMats;
            //var key = modData.First().Key;
            //modData.Remove(key);
        }

        public void SaveColors()
        {
            ShipColors.SaveColors();
        }
        public void LoadColors()
        {
            ShipColors.LoadColors();
        }
    }
}
