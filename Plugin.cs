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
        public const string PLUGIN_VERSION = "0.2.0";

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

        private void Awake()
        {
            instance = this;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);
            //AddConfigEntries();

     /*       globalColors = Config.Bind("Options", "Global colors", false, new ConfigDescription(""));
            globalColors.SettingChanged += (sender, args) => ShipColors.LoadColors();*/

            //loadColors = Config.Bind("Options", "load colors", false, new ConfigDescription("Write paint colors to save. (otherwise they're game-wide)"));
            //SetConfigDelegates();
            //OnSettingsChanged();
            //AddConfigEntries();
        }



        public static void SetConfigDelegates()
        {
           /* paintKakamHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("kakamHull", paintKakamHull.Value);
            paintKakamCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("kakamCabin", paintKakamCabin.Value);
            paintKakamTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("kakamTrim", paintKakamTrim.Value);

            paintCogHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("cogHull", paintCogHull.Value);
            paintCogCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("cogCabin", paintCogCabin.Value);
            paintCogTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("cogTrim", paintCogTrim.Value);

            paintDhowHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("dhowHull", paintDhowHull.Value);
            paintDhowCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("dhowCabin", paintDhowCabin.Value);
            paintDhowTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("dhowTrim", paintDhowTrim.Value);

            paintBrigHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("brigHull", paintBrigHull.Value);
            paintBrigTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("brigTrim", paintBrigTrim.Value);
            paintbrigCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("brigCabin", paintbrigCabin.Value);

            paintSanbuqHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("sanbuqHull", paintSanbuqHull.Value);
            paintSanbuqCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("sanbuqCabin", paintSanbuqCabin.Value);
            paintSanbuqTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("sanbuqTrim", paintSanbuqTrim.Value);

            paintJunkHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("junkHull", paintJunkHull.Value);
            paintJunkCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("junkCabin", paintJunkCabin.Value);
            paintJunkTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("junkTrim", paintJunkTrim.Value);
*/
            paintCurrentHull.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.hullName, paintCurrentHull.Value);
            paintCurrentCabin.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.cabinName, paintCurrentCabin.Value);
            paintCurrentTrim.SettingChanged += (sender, args) => OnSettingsChanged(ShipColors.trimName, paintCurrentTrim.Value);


        }
        public static void AddConfigEntries()
        {
/*
            paintSanbuqHull = instance.Config.Bind("Sanbuq Colors", "Sanbuq hull", ShipColors.defaultColors["sanbuqHull"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true}));
            paintSanbuqCabin = instance.Config.Bind("Sanbuq Colors", "Sanbuq roof", ShipColors.defaultColors["sanbuqCabin"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintSanbuqTrim = instance.Config.Bind("Sanbuq Colors", "Sanbuq trim", ShipColors.defaultColors["sanbuqTrim"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));

            paintJunkHull = instance.Config.Bind("Junk Colors", "Junk hull", ShipColors.defaultColors["junkHull"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintJunkCabin = instance.Config.Bind("Junk Colors", "Junk cabin", ShipColors.defaultColors["junkCabin"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintJunkTrim = instance.Config.Bind("Junk Colors", "Junk trim", ShipColors.defaultColors["junkTrim"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));

            paintKakamHull = instance.Config.Bind("Kakam Colors", "Kakam hull", ShipColors.defaultColors["kakamHull"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintKakamCabin = instance.Config.Bind("Kakam Colors", "Kakam cabin", ShipColors.defaultColors["kakamCabin"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintKakamTrim = instance.Config.Bind("Kakam Colors", "Kakam trim", ShipColors.defaultColors["kakamTrim"]  , new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));

            paintDhowHull = instance.Config.Bind("Dhow Colors", "Dhow hull", ShipColors.defaultColors["dhowHull"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintDhowCabin = instance.Config.Bind("Dhow Colors", "Dhow roof color", ShipColors.defaultColors["dhowCabin"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintDhowTrim = instance.Config.Bind("Dhow Colors", "Dhow trim", ShipColors.defaultColors["dhowTrim"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));

            paintCogHull = instance.Config.Bind("Cog Colors", "Cog hull",  ShipColors.defaultColors["cogHull"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintCogCabin = instance.Config.Bind("Cog Colors", "Cog cabin", ShipColors.defaultColors["cogCabin"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintCogTrim = instance.Config.Bind("Cog Colors", "Cog trim", ShipColors.defaultColors["cogTrim"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));

            paintBrigHull = instance.Config.Bind("Brig Colors", "Brig hull", ShipColors.defaultColors["brigHull"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintBrigTrim = instance.Config.Bind("Brig Colors", "Brig trim", ShipColors.defaultColors["brigTrim"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
            paintbrigCabin = instance.Config.Bind("Brig Colors", "Brig Railings", ShipColors.defaultColors["brigCabin"], new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true }));
*/
            paintCurrentHull = instance.Config.Bind("Current Boat Colors", "Hull", hullDefault, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));
            paintCurrentCabin = instance.Config.Bind("Current Boat Colors", "Roof", cabinDefault, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));
            paintCurrentTrim = instance.Config.Bind("Current Boat Colors", "Trim", trimDefault, new ConfigDescription("", null, new ConfigurationManagerAttributes { HideDefaultButton = true }));


            SetConfigDelegates();
            UpdateConfigsFromBoat();

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
            /*paintSanbuqHull.Value = ShipColors.boatMats["sanbuqHull"].color;
            paintSanbuqCabin.Value = ShipColors.boatMats["sanbuqCabin"].color;
            paintSanbuqTrim.Value = ShipColors.boatMats["sanbuqTrim"].color;

            paintJunkHull.Value = ShipColors.boatMats["junkHull"].color;
            paintJunkCabin.Value = ShipColors.boatMats["junkCabin"].color;
            paintJunkTrim.Value = ShipColors.boatMats["junkTrim"].color;

            paintKakamHull.Value = ShipColors.boatMats["kakamHull"].color;
            paintKakamCabin.Value = ShipColors.boatMats["kakamCabin"].color;
            paintKakamTrim.Value = ShipColors.boatMats["kakamTrim"].color;

            paintDhowHull.Value = ShipColors.boatMats["dhowHull"].color;
            paintDhowCabin.Value = ShipColors.boatMats["dhowCabin"].color;
            paintDhowTrim.Value = ShipColors.boatMats["dhowTrim"].color;

            paintCogHull.Value = ShipColors.boatMats["cogHull"].color;
            paintCogCabin.Value = ShipColors.boatMats["cogCabin"].color;
            paintCogTrim.Value = ShipColors.boatMats["cogTrim"].color;

            paintBrigHull.Value = ShipColors.boatMats["brigHull"].color;
            paintBrigTrim.Value = ShipColors.boatMats["brigTrim"].color;
            paintbrigCabin.Value = ShipColors.boatMats["brigCabin"].color;*/


            if (!GameState.lastBoat || ShipColors.boatMats == null) return;
            paintCurrentHull.Value = ShipColors.boatMats[GameState.lastBoat.name + ShipColors.hullName].color;
            paintCurrentCabin.Value = ShipColors.boatMats[GameState.lastBoat.name + ShipColors.cabinName].color;
            paintCurrentTrim.Value = ShipColors.boatMats[GameState.lastBoat.name + ShipColors.trimName].color;

        }

    }
}
