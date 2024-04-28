using System;
using System.Collections.Generic;
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
        private static char keyValSep = '=';
        private static char[] entrySep = { ',' };

        public static readonly string trimName = "Trim";
        public static readonly string hullName = "Hull";
        public static readonly string cabinName = "Cabin";


        public static Dictionary<string, Material> boatMats = new Dictionary<string, Material>();
        public static Dictionary<string, Color> defaultColors = new Dictionary<string, Color>();

        // stupid copypasta is required to reference materials
        private static void AddMaterialEntry(string key, Transform transform, int index)
        {

            if (!boatMats.ContainsKey(key)) { boatMats.Add(key, transform.GetComponent<Renderer>().materials[index]); }
            transform.GetComponent<Renderer>().materials[index] = boatMats[key];

            if (!defaultColors.ContainsKey(key)) { defaultColors.Add(key, transform.GetComponent<Renderer>().materials[index].color); }
            //Debug.Log("boatcolors method 0: added " + transform.name + " " + key + hullName);

        }
        private static void AddMaterialEntry(string key, Transform transform)
        {
            if (!boatMats.ContainsKey(key)) { boatMats.Add(key, transform.GetComponent<Renderer>().material); }
            transform.GetComponent<Renderer>().material = boatMats[key];
            if (!defaultColors.ContainsKey(key)) { defaultColors.Add(key, transform.GetComponent<Renderer>().material.color); }
            //Debug.Log("boatcolors method 1: added " + transform.name + " " + key + hullName);

        }

        public static void Cache3(Transform ship)
        {
            string boatName = Plugin.GetBoatName(ship.name);
            // ----- kakam -----
            if (ship.name.Contains("junk small"))
            {
                
                var structure = ship.Find("junk small").Find("structure");
                for (int i = 0; i < structure.childCount; i++)
                {
                    var child = structure.GetChild(i);
                    if (child.name.Contains("trim"))
                    {
                        //if (!boatMats.ContainsKey(boatName + trimName)) boatMats.Add(boatName + trimName, child.GetComponent<MeshRenderer>().material);
                        //child.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    if (child.name == "hull")
                    {
                        //if (!boatMats.ContainsKey(boatName + hullName)) boatMats.Add(boatName + hullName, child.GetComponent<MeshRenderer>().materials[1]);
                        //child.GetComponent<MeshRenderer>().materials[1] = boatMats[boatName + hullName];
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    if (child.name.Contains("_roof_"))
                    {
                        //var subChild = child.GetChild(0).GetComponent<MeshRenderer>();
                        //if (!boatMats.ContainsKey(boatName + cabinName)) boatMats.Add(boatName + cabinName, subChild.material);
                        //subChild.material = boatMats[boatName + cabinName];
                        AddMaterialEntry(boatName + cabinName, child.GetChild(0));

                    }
                }
                
            }

            // ----- cog -----
            if (ship.name.Contains("medi small"))
            {
                var container = ship.Find("medi small");

                for (int i = 0; i < container.childCount; i++)
                {
                    var child = container.GetChild(i);
                    if (child.name == ("hull"))
                    {

                        //if (!boatMats.ContainsKey(boatName + hullName)) 
                            //boatMats.Add(boatName + hullName, child.GetComponent<MeshRenderer>().materials[1]);
                        //child.GetComponent<MeshRenderer>().materials[1] = boatMats[boatName + hullName];
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    if (child.name == "structure")
                    {
                        for (int j = 0; j < child.childCount; j++)
                        {
                            var subChild = child.GetChild(j);
                            if (subChild.name.Contains("trim"))
                            {
                                //if (!boatMats.ContainsKey(boatName + trimName)) 
                                    //boatMats.Add(boatName + trimName, subChild.GetComponent<MeshRenderer>().material);
                                //subChild.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];
                                AddMaterialEntry(boatName + trimName, subChild);

                            }

                        }

                    }
                    if (child.name.Contains("struct_var_2__balcony_"))
                    {
                        for (int k = 0;k < child.childCount;k++)
                        {
                            var subChild = child.GetChild(k);
                            if (subChild.name.Contains("trim") || subChild.name.Contains("Cube"))
                            {
                                //if (!boatMats.ContainsKey(boatName + trimName)) boatMats.Add(boatName + trimName, subChild.GetComponent<MeshRenderer>().material);
                                //subChild.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];
                                AddMaterialEntry(boatName + trimName, subChild);

                            }
                            if (subChild.name.Contains("hull"))
                            {
                                //if (!boatMats.ContainsKey(boatName + cabinName)) boatMats.Add(boatName + cabinName, subChild.GetComponent<MeshRenderer>().material);
                                //subChild.GetComponent<MeshRenderer>().material = boatMats[boatName + cabinName];
                                AddMaterialEntry(boatName + cabinName, subChild);

                            }
                        }

                    }
                    if (child.name == "struct_var_1__low_roof_")
                    {
                        for (int l = 0; l < child.childCount; l++)
                        {
                            var subChild = child.GetChild(l);
                            if (subChild.name == "trim_000")
                            {
                                //if (!boatMats.ContainsKey(boatName + cabinName)) boatMats.Add(boatName + cabinName, subChild.GetComponent<MeshRenderer>().material);
                                //subChild.GetComponent<MeshRenderer>().material = boatMats[boatName + cabinName];
                                AddMaterialEntry(boatName + cabinName, subChild);

                            }
                            else if (subChild.name.Contains("trim"))
                            {
                                //if (!boatMats.ContainsKey(boatName + trimName)) boatMats.Add(boatName + trimName, subChild.GetComponent<MeshRenderer>().material);
                                //subChild.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];
                                AddMaterialEntry(boatName + trimName, subChild);

                            }
                        }
                    }
                }
            }

            // ----- dhow -----
            if (ship.name.Contains("dhow small"))
            {

                var container = ship.Find("dhow");
                for (int i = 0; i < container.childCount; i++)
                {
                    var child = container.GetChild(i);
                    if (child.name == "hull")
                    {
                        //if (!boatMats.ContainsKey(boatName + hullName))
                            //boatMats.Add(boatName + hullName, child.GetComponent<MeshRenderer>().materials[1]);
                        //child.GetComponent<MeshRenderer>().materials[1] = boatMats[boatName + hullName];
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    if (child.name.Contains("trim"))
                    {
                        /*if (!boatMats.ContainsKey(boatName + trimName))
                            boatMats.Add(boatName + trimName, child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];*/
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    if (child.name == "roof_hard")
                    {
                        /*if (!boatMats.ContainsKey(boatName + cabinName))
                            boatMats.Add(boatName + cabinName, child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats[boatName + cabinName];*/
                        AddMaterialEntry(boatName + cabinName, child);

                    }
                    if (child.name == "roof_cloth_posts")
                    {
                        /*var subChild = child.GetChild(0);
                        if (!boatMats.ContainsKey(boatName + cabinName))
                            boatMats.Add(boatName + cabinName, subChild.GetComponent<SkinnedMeshRenderer>().material);
                        subChild.GetComponent<SkinnedMeshRenderer>().material = boatMats[boatName + cabinName];*/
                        AddMaterialEntry(boatName + cabinName, child.GetChild(0));

                    }
                }

            }
            // ----- junk -----
            if (ship.name.Contains("junk medium"))
            {
                var structure = ship.Find("junk medium (actual)").Find("structure");
                for (int i = 0; i < structure.childCount; i++)
                {
                    var child = structure.GetChild(i);
                    //Debug.Log("junk child=" + child.name);
                    if (child.name == "hull")
                    {
                        /*if (!boatMats.ContainsKey(boatName + hullName))
                            boatMats.Add(boatName + hullName, child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats[boatName + hullName];*/
                        AddMaterialEntry(boatName + hullName, child, 1);
                        Debug.Log("boatcolors: adding " + child.name + " " + boatName + hullName);
                    }
/*                    if (child.name == "Cube_010")
                    {
                        AddMaterialEntry(boatName + hullName, child);
                        Debug.Log("boatcolors: adding " + child.name + " " + boatName + hullName);

                    }*/
                    if (child.name.Contains("trim") || child.name == "Cube_035" || child.name == "Cube_024" || child.name == "Cube_029")
                    {
                        /*if (!boatMats.ContainsKey(boatName + trimName)) 
                            boatMats.Add(boatName + trimName, child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];
                        Debug.Log("boatcolors: junk trim=" +  child.name);*/
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    if (child.name == "Cube_034" || child.name == "Cube_026")
                    {
                        AddMaterialEntry(boatName + cabinName, child);

                    }
                    if (child.name == "struct_cabin_full")
                    {
                        for (int j = 0; j < child.childCount; j++)
                        {
                            var subChild = child.GetChild(j);
                            if (subChild.name == "trim_000" || subChild.name == "Cube_028")
                            {
                                /*if (!boatMats.ContainsKey(boatName + cabinName)) 
                                    boatMats.Add(boatName + cabinName, subChild.GetComponent<MeshRenderer>().material);
                                subChild.GetComponent<MeshRenderer>().material = boatMats[boatName + cabinName];*/
                                AddMaterialEntry(boatName + cabinName, subChild);

                            }
                            if (subChild.name == "trim_013" || subChild.name == "Cube_043" || subChild.name == "Cube_006" || subChild.name == "Cube_038")
                            {
                                /*if (!boatMats.ContainsKey(boatName + trimName))
                                    boatMats.Add(boatName + trimName, subChild.GetComponent<MeshRenderer>().material);

                                subChild.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];*/
                                AddMaterialEntry(boatName + trimName, subChild);

                            }
                        }
                    }

                }
                var roof = ship.Find("junk medium (actual)").Find("roof_on");
                for (int k = 0; k < roof.childCount; k++)
                {
                    var child = roof.GetChild(k);
                    if (child.name.Contains("Cube"))
                    {
                        AddMaterialEntry(boatName + trimName, child);
                    }
                    if (child.name.Contains("trim"))
                    {
                        AddMaterialEntry(boatName + cabinName, child);
                    }
                }
                /*if (!boatMats.ContainsKey(boatName + cabinName)) boatMats.Add(boatName + cabinName, roof.GetComponent<Renderer>().material);
                roof.Find("trim_004").GetComponent<MeshRenderer>().material = boatMats[boatName + cabinName];*/

            }
            // ----- brig -----
            if (ship.name.Contains("medi medium"))
            {
                var structure = ship.Find("medi medium new").Find("structure_container");
                for (int i = 0; i < structure.childCount; i++)
                {
                    var child = structure.GetChild(i);
                    if (child.name.Contains("hull"))
                    {
                        /*if (!boatMats.ContainsKey(boatName + hullName)) boatMats.Add(boatName + hullName, child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats[boatName + hullName];*/
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    if (child.name.Contains("trim") || child.name.Contains("Cube"))
                    {
                        /* if (!boatMats.ContainsKey(boatName + trimName)) boatMats.Add(boatName + trimName, child.GetComponent<MeshRenderer>().material);
                         child.GetComponent<MeshRenderer>().material = boatMats[boatName + trimName];*/
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    if (child.name.Contains("railing"))
                    {
                        /*if (!boatMats.ContainsKey(boatName + cabinName)) boatMats.Add(boatName + cabinName, child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats[boatName + cabinName];*/
                        AddMaterialEntry(boatName + cabinName, child);

                    }
                }
            }

            // ----- sanbuq -----
            if (ship.name.Contains("dhow medium"))
            {
                var structure = ship.Find("dhow medium new").Find("structure");
                for (int i = 0; i < structure.childCount; i++)
                {
                    var child = structure.GetChild(i);
                    if (child.name.Contains("hull"))
                    {
                        /*if (!boatMats.ContainsKey("sanbuqHull")) boatMats.Add("sanbuqHull", child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats["sanbuqHull"];*/
                        AddMaterialEntry(boatName + hullName, child, 1);

                    }
                    if (child.name.Contains("Cube"))
                    {
                        /*if (!boatMats.ContainsKey("sanbuqTrim")) boatMats.Add("sanbuqTrim", child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["sanbuqTrim"];*/
                        AddMaterialEntry(boatName + trimName, child);

                    }
                    if (child.name == "part_cabin_cloth")
                    {
                        /*boatMats.Add("sanbuqCabin", child.Find("canopy").GetComponent<SkinnedMeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["sanbuqCabin"];*/
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
                //Debug.Log("boatColors: " + mat.Key.Split('H')[0]);
                if (GameState.modData.TryGetValue(mat.Key.Split('H')[0], out var hexColor))
                {
                    ColorUtility.TryParseHtmlString(hexColor, out Color color);
                    mat.Value.color = color;
                    GameState.modData.Remove(mat.Key);
                    success = true;
                }
            }
            return success;
        }

        public static void LoadColors()
        {

            if (!GameState.modData.ContainsKey(Plugin.PLUGIN_ID))
            {
                if (LoadOldColors())
                {
                    Debug.Log("boatcolors: loaded old colors"); 
                    return;
                }
            }

            if (GameState.modData.TryGetValue(Plugin.PLUGIN_ID, out string data))
            {
                //Debug.Log("string=" + colorStrings);
                var entries = data.Split(entrySep, StringSplitOptions.RemoveEmptyEntries);
                //Debug.Log("strings1= " + strings1);
                foreach (string entry in entries)
                {
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
