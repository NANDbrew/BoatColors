using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoatColors
{
    internal class ShipColors
    {
        private static readonly char keyValSep = '=';
        private static readonly char entrySep = ',';

        public static readonly string trimName = " trim";
        public static readonly string hullName = " hull";
        public static readonly string cabinName = " cabin";

        public static Dictionary<string, Material> boatMats = new Dictionary<string, Material>();

        public static Dictionary<string, Color> defaultColors = new Dictionary<string, Color>();

        private static void AddMaterialEntry(string key, Renderer target, int index)
        {
            if (!defaultColors.ContainsKey(key)) { defaultColors.Add(key, target.materials.Last().color); }

            if (!boatMats.ContainsKey(key)) { boatMats.Add(key, target.materials.Last()); }
            target.materials[target.materials.Count() - 1] = boatMats[key];
        }
        private static void AddMaterialEntry(string key, Renderer target)
        {
            if (!defaultColors.ContainsKey(key)) { defaultColors.Add(key, target.material.color); }

            if (!boatMats.ContainsKey(key)) { boatMats.Add(key, target.material); }
            target.material = boatMats[key];
        }

        // stupid copypasta is required to reference materials
        public static void Cache3(Transform ship)
        {
            string boatName = ship.name;
            // ----- kakam -----
            if (ship.GetComponent<SaveableObject>().sceneIndex == 90)
            {
                
                var structure = ship.Find("junk small").Find("structure");
                foreach (Renderer child in structure.GetComponentsInChildren<Renderer>(true))
                {
                    if (child.name.Contains("trim"))
                    {
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    if (child.name == "hull")
                    {
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    if (child.transform.parent != structure && child.transform.parent.name.Contains("_roof_") && child.transform.GetSiblingIndex() == 0)
                    {
                        AddMaterialEntry(boatName + cabinName, child);

                    }
                }
                
            }

            // ----- cog -----
            if (ship.GetComponent<SaveableObject>().sceneIndex == 40)
            {
                var container = ship.Find("medi small");

                foreach (Renderer child in container.GetComponentsInChildren<Renderer>(true))
                {
                    if (child.transform.parent.name == "struct_var_1__low_roof_" && child.name == "trim_000")
                    {
                        AddMaterialEntry(boatName + cabinName, child);
                        continue;
                    }
                    if (child.name.Contains("trim") || child.name.Contains("Cube") || child.name == "struct_var_3__no_cabin_")
                    {
                        AddMaterialEntry(boatName + trimName, child);
                        continue;
                    }
                    if (child.name.Contains("hull"))
                    {
                        if (child.transform.parent.name == "struct_var_2__balcony_")
                        {
                            AddMaterialEntry(boatName + cabinName, child);
                            continue;
                        }
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                }
/*                foreach (Renderer child in container.Find("structure").GetComponentsInChildren<Renderer>(true))
                {
                    if (child.transform.parent.name == "struct_var_1__low_roof_" && child.name == "trim_000")
                    {
                        AddMaterialEntry(boatName + cabinName, child);
                        continue;
                    }
                    if (child.name.Contains("trim") || child.name.Contains("Cube") || child.name == "struct_var_3__no_cabin_")
                    {
                        AddMaterialEntry(boatName + trimName, child);
                        continue;
                    }
                    if (child.name.Contains("hull"))
                    {
                        if (child.transform.parent.name == "struct_var_2_balcony_")
                        {
                            AddMaterialEntry(boatName + cabinName, child);
                            continue;
                        }
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                }*/

            }

            // ----- dhow -----
            if (ship.GetComponent<SaveableObject>().sceneIndex == 10)
            {

                var container = ship.Find("dhow");
                foreach (Renderer child in container.GetComponentsInChildren<Renderer>(true))
                {
                    if (child.transform.parent != container && child.transform.parent.name == "roof_cloth_posts")
                    {
                        if (child.transform.GetSiblingIndex() == 0)
                        {
                            AddMaterialEntry(boatName + cabinName, child);
                        }
                        else if (child.transform.GetSiblingIndex() == 1)
                        {
                            AddMaterialEntry(boatName + trimName, child);
                        }
                    }
                    else if (child.name == "hull")
                    {
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    else if (child.name.Contains("trim"))
                    {
                        AddMaterialEntry(boatName + trimName, child);
                    }
                    else if (child.name == "roof_hard")
                    {
                        AddMaterialEntry(boatName + cabinName, child);
                    }
                }

            }
            // ----- junk -----
            if (ship.GetComponent<SaveableObject>().sceneIndex == 80)
            {
                var structure = ship.Find("junk medium (actual)").Find("structure");
                foreach (Renderer child in structure.GetComponentsInChildren<Renderer>(true))
                {
                    if (child.transform.parent.name.StartsWith("mast"))
                    {
                        continue;
                    }
                    if (child.name == "hull")
                    {
                        AddMaterialEntry(boatName + hullName, child, 1);
                        Debug.Log("boatcolors: adding " + child.name + " " + boatName + hullName);
                    }
                   /* if (child.name == "Cube_010")
                    {
                        AddMaterialEntry(boatName + hullName, child);
                        Debug.Log("boatcolors: adding " + child.name + " " + boatName + hullName);

                    }*/
                    if (child.name.Contains("trim") || child.name == "Cube_035" || child.name == "Cube_024" || child.name == "Cube_029")
                    {
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    else if (child.name == "Cube_034" || child.name == "Cube_026")
                    {
                        AddMaterialEntry(boatName + cabinName, child);

                    }
                    if (child.transform.parent != structure && child.transform.parent.name == "struct_cabin_full")
                    {
                        if (child.name == "trim_000" || child.name == "Cube_028")
                        {
                            AddMaterialEntry(boatName + cabinName, child);

                        }
                        else if (child.name == "trim_013" || child.name == "Cube_043" || child.name == "Cube_006" || child.name == "Cube_038")
                        {
                            AddMaterialEntry(boatName + trimName, child);

                        }

                    }

                }
                var roof = ship.Find("junk medium (actual)").Find("roof_on");
                foreach (Renderer child in roof.GetComponentsInChildren<Renderer>())
                {
                    if (child.name.Contains("Cube"))
                    {
                        AddMaterialEntry(boatName + trimName, child);
                    }
                    else if (child.name.Contains("trim"))
                    {
                        AddMaterialEntry(boatName + cabinName, child);
                    }
                }
                /*if (!boatMats.ContainsKey(boatName + cabinName)) boatMats.Add(boatName + cabinName, roof.GetComponent<Renderer>().material);
                roof.Find("trim_004").GetComponent<MeshRenderer>().material = boatMats[boatName + cabinName];*/

            }
            // ----- brig -----
            if (ship.GetComponent<SaveableObject>().sceneIndex == 50)
            {
                var structure = ship.Find("medi medium new");
                for (int h = 0; h < structure.childCount; h++)
                {
                    var structure2 = structure.GetChild(h);
                    if (structure2.name == "structure_container")
                    {
                        foreach (Renderer child in structure2.GetComponentsInChildren<Renderer>())
                        {
                            if (child.name.Contains("hull"))
                            {
                                AddMaterialEntry(boatName + hullName, child, 1);

                            }
                            if (child.name.Contains("trim") || child.name.Contains("Cube"))
                            {
                                AddMaterialEntry(boatName + trimName, child);

                            }
                            if (child.name.Contains("railing"))
                            {
                                AddMaterialEntry(boatName + cabinName, child);

                            }
                        }

                    }
                    if (structure2.name.Contains("shrouds"))
                    {
                        foreach (Renderer child in structure2.GetComponentsInChildren<Renderer>())
                        {
                            if (child.name.Contains("trim"))
                            {
                                AddMaterialEntry(boatName + cabinName, child);

                            }
                        }

                    }

                }

            }
            // ----- sanbuq -----
            if (ship.GetComponent<SaveableObject>().sceneIndex == 20)
            {
                var structure = ship.Find("dhow medium new").Find("structure");
                foreach (Renderer child in structure.GetComponentsInChildren<Renderer>())
                {
                    if (child.name.Contains("hull"))
                    {
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    if (child.name.Contains("Cube"))
                    {
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    if (child.name == "canopy")
                    {
                        AddMaterialEntry(boatName + cabinName, child);

                    }
                }
            }
            //Plugin.AddConfigEntries();
        }

        private static bool LoadOldColors()
        {
            bool success = false;
            for (int i = 0; i < boatMats.Count; i++)
            {
                KeyValuePair<string, Material> mat = boatMats.ElementAt(i);
                string key = Plugin.GetBoatName(mat.Key);
                //Debug.Log("boatColors: " + mat.Key.Split('H')[0]);
                if (GameState.modData.TryGetValue(key, out var hexColor))
                {
                    ColorUtility.TryParseHtmlString(hexColor, out Color color);
                    mat.Value.color = color;
                    GameState.modData.Remove(key);
                    success = true;
                }
            }
            return success;
        }

        public static void LoadColors()
        {

           /* if (!GameState.modData.ContainsKey(Plugin.PLUGIN_ID))
            {
                if (LoadOldColors())
                {
                    Debug.Log("boatcolors: loaded old colors"); 
                    return;
                }
            }*/

            if (GameState.modData.TryGetValue(Plugin.PLUGIN_ID, out string data))
            {
                //Debug.Log("string=" + colorStrings);
                var entries = data.Split(entrySep);
                //Debug.Log("strings1= " + strings1);
                foreach (string entry in entries)
                {
                    if (entry.Length < 6) continue;
                    string[] keyValue = entry.Split(keyValSep);
                    ColorUtility.TryParseHtmlString(keyValue[1], out Color color);
                    if (color != null && boatMats.ContainsKey(keyValue[0])) boatMats[keyValue[0]].color = color;
                    //Debug.Log(subs[0] + " -is- " + subs[1]);
                }
            }
            //Plugin.AddConfigEntries();
           // Plugin.UpdateConfigsFromSave();
        }

        public static void UpdateColor(string boatName, Color color)
        {
            if (boatMats.TryGetValue(boatName, out Material mat))
            {
                mat.color = color;
            }
        }

        public static void ResetColor(string boatName)
        {
            if (boatMats.TryGetValue(boatName, out Material mat))
            {
                mat.color = defaultColors[boatName];
            }
        }

        public static void SaveColors()
        {
            string data = "";
            for (int i = 0; i < boatMats.Count; i++)
            {
                KeyValuePair<string, Material> mat = boatMats.ElementAt(i);
                string hexColor = "#" + ColorUtility.ToHtmlStringRGBA(mat.Value.color);
                data += mat.Key + keyValSep + hexColor + entrySep;
            }
            if (!GameState.modData.ContainsKey(Plugin.PLUGIN_ID))
            {
                GameState.modData.Add(Plugin.PLUGIN_ID, data);
            }
            else
            {
                GameState.modData[Plugin.PLUGIN_ID] = data;
            }
            Debug.Log(data);
        }

        public static void UpdateColorsFromConfig()
        {
 /*           if (!Plugin.globalColors.Value)
            {
                LoadColors();
                return;
            }
            UpdateColor("kakamHull", Plugin.paintKakamHull.Value);
            UpdateColor("kakamCabin", Plugin.paintKakamCabin.Value);
            UpdateColor("kakamTrim", Plugin.paintKakamTrim.Value);

            UpdateColor("cogHull", Plugin.paintCogHull.Value);
            UpdateColor("cogCabin", Plugin.paintCogCabin.Value);
            UpdateColor("cogTrim", Plugin.paintCogTrim.Value);

            UpdateColor("dhowHull", Plugin.paintDhowHull.Value);
            UpdateColor("dhowCabin", Plugin.paintDhowCabin.Value);
            UpdateColor("dhowTrim", Plugin.paintDhowTrim.Value);

            UpdateColor("brigHull", Plugin.paintBrigHull.Value);
            UpdateColor("brigTrim", Plugin.paintBrigTrim.Value);
            UpdateColor("brigCabin", Plugin.paintbrigCabin.Value);

            UpdateColor("sanbuqHull", Plugin.paintSanbuqHull.Value);
            UpdateColor("sanbuqCabin", Plugin.paintSanbuqCabin.Value);
            UpdateColor("sanbuqTrim", Plugin.paintSanbuqTrim.Value);

            UpdateColor("junkHull", Plugin.paintJunkHull.Value);
            UpdateColor("junkCabin", Plugin.paintJunkCabin.Value);
            UpdateColor("junkTrim", Plugin.paintJunkTrim.Value);*/

        }
    }
}
