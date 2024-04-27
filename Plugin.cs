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

        //--settings--
        internal static ConfigEntry<bool> saveColors;
        internal static ConfigEntry<bool> loadColors;

        internal static ConfigEntry<Color> paintCogHull;
        internal static ConfigEntry<Color> paintCogCabin;
        internal static ConfigEntry<Color> paintCogTrim;

        internal static ConfigEntry<Color> paintSanbuqHull;
        internal static ConfigEntry<Color> paintSanbuqAwning;
        internal static ConfigEntry<Color> paintSanbuqTrim;


        internal static ConfigEntry<Color> paintDhowHull;
        internal static ConfigEntry<Color> paintDhowAwning;
        internal static ConfigEntry<Color> paintDhowTrim;

        internal static ConfigEntry<Color> paintKakamHull;
        internal static ConfigEntry<Color> paintKakamCabin;
        internal static ConfigEntry<Color> paintKakamTrim;

        internal static ConfigEntry<Color> paintBrigHull;
        internal static ConfigEntry<Color> paintBrigTrim;
        internal static ConfigEntry<Color> paintBrigRailing;

        internal static ConfigEntry<Color> paintJunkHull;
        internal static ConfigEntry<Color> paintJunkCabin;
        internal static ConfigEntry<Color> paintJunkTrim;


        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);
            paintSanbuqHull = Config.Bind("Sanbuq Colors", "Sanbuq hull", new Color(1f, 0.9490196f, 0.8823529f));
            paintSanbuqAwning = Config.Bind("Sanbuq Colors", "Sanbuq roof", new Color(1f, 0.9775419f, 0.9386792f));
            paintSanbuqTrim = Config.Bind("Sanbuq Colors", "Sanbuq trim", new Color(1f, 0.9775419f, 0.9386792f));

            paintJunkHull = Config.Bind("Junk Colors", "Junk hull", new Color(0.5f, 0.5f, 0.5f));
            paintJunkCabin = Config.Bind("Junk Colors", "Junk cabin", new Color(0.5f, 0.5f, 0.5f));
            paintJunkTrim = Config.Bind("Junk Colors", "Junk trim", new Color(0.5f, 0.5f, 0.5f));

            paintKakamHull = Config.Bind("Kakam Colors", "Kakam hull", new Color(0.4339623f, 0.4339623f, 0.4339623f));
            paintKakamCabin = Config.Bind("Kakam Colors", "Kakam cabin", new Color(0.4339623f, 0.4339623f, 0.4339623f));
            paintKakamTrim = Config.Bind("Kakam Colors", "Kakam trim", new Color(0.4339623f, 0.4339623f, 0.4339623f));

            paintDhowHull = Config.Bind("Dhow Colors", "Dhow hull", new Color(0.6132076f, 0.5865498f, 0.5813902f));
            paintDhowAwning = Config.Bind("Dhow Colors", "Dhow roof color", new Color(1f, 0.9617205f, 0.9009434f));
            paintDhowTrim = Config.Bind("Dhow Colors", "Dhow trim", new Color(0.5f, 0.5f, 0.5f));

            paintCogHull = Config.Bind("Cog Colors", "Cog hull", new Color(0.5f, 0.5f, 0.5f));
            paintCogCabin = Config.Bind("Cog Colors", "Cog cabin", new Color(0.5f, 0.5f, 0.5f));
            paintCogTrim = Config.Bind("Cog Colors", "Cog trim", new Color(0.5f, 0.5f, 0.5f));

            paintBrigHull = Config.Bind("Brig Colors", "Brig hull", new Color(0.5f, 0.5f, 0.5f));
            paintBrigTrim = Config.Bind("Brig Colors", "Brig trim", new Color(0.5f, 0.5f, 0.5f));
            paintBrigRailing = Config.Bind("Brig Colors", "Brig Railings", new Color(0.5f, 0.5f, 0.5f));


            saveColors = Config.Bind("Options", "Save colors", false, new ConfigDescription("Write paint colors to save. (otherwise they're game-wide)"));
            //loadColors = Config.Bind("Options", "load colors", false, new ConfigDescription("Write paint colors to save. (otherwise they're game-wide)"));
            SetConfigDelegates();
            //OnSettingsChanged();
        }



        public static void SetConfigDelegates()
        {
            paintKakamHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("kakamHull", paintKakamHull.Value);
            paintKakamCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("kakamCabin", paintKakamCabin.Value);
            paintKakamTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("kakamTrim", paintKakamTrim.Value);

            paintCogHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("cogHull", paintCogHull.Value);
            paintCogCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("cogCabin", paintCogCabin.Value);
            paintCogTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("cogTrim", paintCogTrim.Value);

            paintDhowHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("dhowHull", paintDhowHull.Value);
            paintDhowAwning.SettingChanged += (sender, args) => ShipColors.UpdateColor("dhowCabin", paintDhowAwning.Value);
            paintDhowTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("dhowTrim", paintDhowTrim.Value);

            paintBrigHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("brigHull", paintBrigHull.Value);
            paintBrigTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("brigTrim", paintBrigTrim.Value);
            paintBrigRailing.SettingChanged += (sender, args) => ShipColors.UpdateColor("brigRailing", paintBrigRailing.Value);

            paintSanbuqHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("sanbuqHull", paintSanbuqHull.Value);
            paintSanbuqAwning.SettingChanged += (sender, args) => ShipColors.UpdateColor("sanbuqCabin", paintSanbuqAwning.Value);
            paintSanbuqTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("sanbuqTrim", paintSanbuqTrim.Value);

            paintJunkHull.SettingChanged += (sender, args) => ShipColors.UpdateColor("junkHull", paintJunkHull.Value);
            paintJunkCabin.SettingChanged += (sender, args) => ShipColors.UpdateColor("junkCabin", paintJunkCabin.Value);
            paintJunkTrim.SettingChanged += (sender, args) => ShipColors.UpdateColor("junkTrim", paintJunkTrim.Value);


            //saveColors.SettingChanged += (sender, args) => ShipColors.SaveColors();
            //loadColors.SettingChanged += (sender, args) => ShipColors.LoadColors();
        }
        /*
                public static void OnSettingsChanged()
                {
                    ShipColors.UpdateColors();
                }*/

    }
}
