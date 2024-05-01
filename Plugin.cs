using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace BoatColors
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    //[BepInDependency("com.app24.sailwindmoddinghelper", "2.0.3")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.boatcolors";
        public const string PLUGIN_NAME = "Boat Colors";
        public const string PLUGIN_VERSION = "0.2.1";

        internal static Plugin instance;


        //--settings--

        internal static ConfigEntry<Color> paintCurrentHull;
        internal static ConfigEntry<Color> paintCurrentCabin;
        internal static ConfigEntry<Color> paintCurrentTrim;

        internal static ConfigEntry<bool> useCustomSailColors;
        internal static ConfigEntry<Color> customSailColor;
        internal static ConfigEntry<Color> customSailColor1;



        private void Awake()
        {
            instance = this;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);
            //SetConfigDelegates();
            //OnSettingsChanged();
            AddConfigEntries();

        }

        public static void AddConfigEntries()
        {

            paintCurrentHull = instance.Config.Bind("Current Boat Colors", "Hull", Color.gray, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));
            paintCurrentCabin = instance.Config.Bind("Current Boat Colors", "Roof", Color.gray, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));
            paintCurrentTrim = instance.Config.Bind("Current Boat Colors", "Trim", Color.gray, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));

            useCustomSailColors = instance.Config.Bind("Sails", "Use Custom Sail Colors", false, new ConfigDescription("Add custom colors to the sail color menu. (requires restart) \nWARNING! USING THESE WILL MAKE YOUR SAVE DEPENDENT ON THIS MOD!", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            
            if (useCustomSailColors.Value) 
            {
                customSailColor = instance.Config.Bind("Sails", "Custom Sail Color 1", Color.white, new ConfigDescription("Apply any color to any sail to update", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
                customSailColor1 = instance.Config.Bind("Sails", "Custom Sail Color 2", Color.black, new ConfigDescription("Apply any color to any sail to update", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            }


            SetConfigDelegates();

        }
        public static void SetConfigDelegates()
        {

            paintCurrentHull.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.hullName, paintCurrentHull.Value);
            paintCurrentCabin.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.cabinName, paintCurrentCabin.Value);
            paintCurrentTrim.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.trimName, paintCurrentTrim.Value);

            customSailColor.SettingChanged += (sender, args) => SailColorPatcher.UpdateCustomSailColor(24, customSailColor.Value);
            customSailColor1.SettingChanged += (sender, args) => SailColorPatcher.UpdateCustomSailColor(25, customSailColor1.Value);

        }

        public static void OnSettingsChanged(string name, Color color)
        {
            if (!GameState.lastBoat)
            {
                Debug.Log("boatcolors: lastBoat is null");
                return;
            }

            ShipColors.UpdateColor(GameState.lastBoat.name + name, color);
        }
        public static string GetBoatName(string currentBoat)
        {
            if (currentBoat.Contains("medi small"))
            {
                currentBoat = "cog";
            }
            else if (currentBoat.Contains("medi medium"))
            {
                currentBoat = "brig";
            }
            else if (currentBoat.Contains("dhow small"))
            {
                currentBoat = "dhow";
            }
            else if (currentBoat.Contains("dhow medium"))
            {
                currentBoat = "sanbuq";
            }
            else if (currentBoat.Contains("junk small"))
            {
                currentBoat = "kakam";
            }
            else if (currentBoat.Contains("junk medium"))
            {
                currentBoat = "junk";
            }
            return currentBoat;
        }

        public static void UpdateConfigsFromBoat()
        {


            if (!GameState.lastBoat || ShipColors.boatMats == null) return;
            paintCurrentHull.Value = ShipColors.boatMats[GameState.lastBoat.name + ShipColors.hullName].color;
            paintCurrentCabin.Value = ShipColors.boatMats[GameState.lastBoat.name + ShipColors.cabinName].color;
            paintCurrentTrim.Value = ShipColors.boatMats[GameState.lastBoat.name + ShipColors.trimName].color;

        }

    }
}
