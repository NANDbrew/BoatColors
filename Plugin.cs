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
        /*internal static ConfigEntry<bool> globalColors;

        internal static ConfigEntry<Color> paintCogHull;
        internal static ConfigEntry<Color> paintCogCabin;
        internal static ConfigEntry<Color> paintCogTrim;

        internal static ConfigEntry<Color> paintSanbuqHull;
        internal static ConfigEntry<Color> paintSanbuqCabin;
        internal static ConfigEntry<Color> paintSanbuqTrim;


        internal static ConfigEntry<Color> paintDhowHull;
        internal static ConfigEntry<Color> paintDhowCabin;
        internal static ConfigEntry<Color> paintDhowTrim;

        internal static ConfigEntry<Color> paintKakamHull;
        internal static ConfigEntry<Color> paintKakamCabin;
        internal static ConfigEntry<Color> paintKakamTrim;

        internal static ConfigEntry<Color> paintBrigHull;
        internal static ConfigEntry<Color> paintBrigTrim;
        internal static ConfigEntry<Color> paintbrigCabin;

        internal static ConfigEntry<Color> paintJunkHull;
        internal static ConfigEntry<Color> paintJunkCabin;
        internal static ConfigEntry<Color> paintJunkTrim;
*/
        internal static ConfigEntry<Color> paintCurrentHull;
        internal static ConfigEntry<Color> paintCurrentCabin;
        internal static ConfigEntry<Color> paintCurrentTrim;

        internal static Color hullDefault = new Color(0.5f, 0.5f, 0.5f, 1f);
        internal static Color trimDefault = new Color(0.5f, 0.5f, 0.5f, 1f);
        internal static Color cabinDefault = new Color(0.5f, 0.5f, 0.5f, 1f);

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

            paintCurrentHull = instance.Config.Bind("Current Boat Colors", "Hull", hullDefault, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));
            paintCurrentCabin = instance.Config.Bind("Current Boat Colors", "Roof", cabinDefault, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));
            paintCurrentTrim = instance.Config.Bind("Current Boat Colors", "Trim", trimDefault, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));

            //customSailColor = instance.Config.Bind("Sails", "Custom Sail Color", new Color(1, 1, 1), new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = false }));
            //customSailColor1 = instance.Config.Bind("Sails", "Custom Sail Color 1", new Color(0, 0, 0), new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = false }));


            SetConfigDelegates();

        }
        public static void SetConfigDelegates()
        {

            paintCurrentHull.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.hullName, paintCurrentHull.Value);
            paintCurrentCabin.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.cabinName, paintCurrentCabin.Value);
            paintCurrentTrim.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.trimName, paintCurrentTrim.Value);

            //customSailColor.SettingChanged += (sender, args) => SailColorPatcher.UpdateCustomSailColor(24, customSailColor.Value);
            //customSailColor1.SettingChanged += (sender, args) => SailColorPatcher.UpdateCustomSailColor(25, customSailColor1.Value);

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
