using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoatColors
{
    internal class ShipColors
    {
        private static char keyValSep = '=';
        private static char entrySep = ',';

        public static Dictionary<string, Material> boatMats = new Dictionary<string, Material>();
        public static Dictionary<string, Color> defaultColors = new Dictionary<string, Color>();


        public static void Cache3(Transform ship)
        {
            // ----- kakam -----
            if (ship.name.Contains("junk small"))
            {
                {
                    var structure = ship.Find("junk small").Find("structure");
                    for (int i = 0; i < structure.childCount; i++)
                    {
                        var child = structure.GetChild(i);
                        if (child.name.Contains("trim"))
                        {
                            if (!boatMats.ContainsKey("kakamTrim")) boatMats.Add("kakamTrim", child.GetComponent<MeshRenderer>().material);
                            child.GetComponent<MeshRenderer>().material = boatMats["kakamTrim"];
                        }
                        if (child.name == "hull")
                        {
                            if (!boatMats.ContainsKey("kakamHull")) boatMats.Add("kakamHull", child.GetComponent<MeshRenderer>().materials[1]);
                            child.GetComponent<MeshRenderer>().materials[1] = boatMats["kakamHull"];

                        }
                        if (child.name.Contains("_roof_"))
                        {
                            var subChild = child.GetChild(0).GetComponent<MeshRenderer>();
                            if (!boatMats.ContainsKey("kakamCabin")) boatMats.Add("kakamCabin", subChild.material);
                            subChild.material = boatMats["kakamCabin"];

                        }
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

                        if (!boatMats.ContainsKey("cogHull")) 
                            boatMats.Add("cogHull", child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats["cogHull"];
                    }
                    if (child.name == "structure")
                    {
                        for (int j = 0;j < child.childCount;j++)
                        {
                            var subChild = child.GetChild(j);
                            if (subChild.name.Contains("trim"))
                            {
                                if (!boatMats.ContainsKey("cogTrim")) 
                                    boatMats.Add("cogTrim", subChild.GetComponent<MeshRenderer>().material);
                                subChild.GetComponent<MeshRenderer>().material = boatMats["cogTrim"];
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
                                if (!boatMats.ContainsKey("cogTrim")) boatMats.Add("cogTrim", subChild.GetComponent<MeshRenderer>().material);
                                subChild.GetComponent<MeshRenderer>().material = boatMats["cogTrim"];
                            }
                            if (subChild.name.Contains("hull"))
                            {
                                if (!boatMats.ContainsKey("cogCabin")) boatMats.Add("cogCabin", subChild.GetComponent<MeshRenderer>().material);
                                subChild.GetComponent<MeshRenderer>().material = boatMats["cogCabin"];

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
                                if (!boatMats.ContainsKey("cogCabin")) boatMats.Add("cogCabin", subChild.GetComponent<MeshRenderer>().material);
                                subChild.GetComponent<MeshRenderer>().material = boatMats["cogCabin"];

                            }
                            else if (subChild.name.Contains("trim"))
                            {
                                if (!boatMats.ContainsKey("cogTrim")) boatMats.Add("cogTrim", subChild.GetComponent<MeshRenderer>().material);
                                subChild.GetComponent<MeshRenderer>().material = boatMats["cogTrim"];
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
                        if (!boatMats.ContainsKey("dhowHull"))
                            boatMats.Add("dhowHull", child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats["dhowHull"];
                    }
                    if (child.name.Contains("trim"))
                    {
                        if (!boatMats.ContainsKey("dhowTrim"))
                            boatMats.Add("dhowTrim", child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["dhowTrim"];
                    }
                    if (child.name == "roof_hard")
                    {
                        if (!boatMats.ContainsKey("dhowCabin"))
                            boatMats.Add("dhowCabin", child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["dhowCabin"];
                    }
                    if (child.name == "roof_cloth_posts")
                    {
                        var subChild = child.GetChild(0);
                        if (!boatMats.ContainsKey("dhowCabin"))
                            boatMats.Add("dhowCabin", subChild.GetComponent<SkinnedMeshRenderer>().material);
                        subChild.GetComponent<SkinnedMeshRenderer>().material = boatMats["dhowCabin"];
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
                    Debug.Log("junk child=" + child.name);
                    if (child.name == "hull")
                    {
                        if (!boatMats.ContainsKey("junkHull"))
                            boatMats.Add("junkHull", child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats["junkHull"];

                    }
                    if (child.name.Contains("trim"))
                    {
                        if (!boatMats.ContainsKey("junkTrim")) 
                            boatMats.Add("junkTrim", child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["junkTrim"];
                        Debug.Log("boatcolors: junk trim=" +  child.name);
                    }
                    if (child.name == "struct_cabin_full")
                    {
                        for (int j = 0; j < child.childCount; j++)
                        {
                            var subChild = child.GetChild(j);
                            if (subChild.name == "trim_000")
                            {
                                if (!boatMats.ContainsKey("junkCabin")) 
                                    boatMats.Add("junkCabin", subChild.GetComponent<MeshRenderer>().material);
                                subChild.GetComponent<MeshRenderer>().material = boatMats["junkCabin"];

                            }
                            if (subChild.name == "trim_013" || subChild.name == "Cube_028" || subChild.name == "Cube_028")
                            {
                                if (!boatMats.ContainsKey("junkTrim"))
                                    boatMats.Add("junkTrim", subChild.GetComponent<MeshRenderer>().material);

                                subChild.GetComponent<MeshRenderer>().material = boatMats["junkTrim"];

                            }
                        }
                    }

                }
                var roof = ship.Find("junk medium (actual)").Find("roof_on");
                if (!boatMats.ContainsKey("junkCabin")) boatMats.Add("junkCabin", roof.GetComponent<Renderer>().material);
                roof.Find("trim_004").GetComponent<MeshRenderer>().material = boatMats["junkCabin"];              
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
                        if (!boatMats.ContainsKey("brigHull")) boatMats.Add("brigHull", child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats["brigHull"];

                    }
                    if (child.name.Contains("trim") || child.name.Contains("Cube"))
                    {
                        if (!boatMats.ContainsKey("brigTrim")) boatMats.Add("brigTrim", child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["brigTrim"];

                    }
                    if (child.name.Contains("railing"))
                    {
                        if (!boatMats.ContainsKey("brigRailing")) boatMats.Add("brigRailing", child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["brigRailing"];

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
                        if (!boatMats.ContainsKey("sanbuqHull")) boatMats.Add("sanbuqHull", child.GetComponent<MeshRenderer>().materials[1]);
                        child.GetComponent<MeshRenderer>().materials[1] = boatMats["sanbuqHull"];

                    }
                    if (child.name.Contains("Cube"))
                    {
                        if (!boatMats.ContainsKey("sanbuqTrim")) boatMats.Add("sanbuqTrim", child.GetComponent<MeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["sanbuqTrim"];

                    }
                    if (child.name == "part_cabin_cloth")
                    {
                        boatMats.Add("sanbuqCabin", child.Find("canopy").GetComponent<SkinnedMeshRenderer>().material);
                        child.GetComponent<MeshRenderer>().material = boatMats["sanbuqCabin"];
                    }
                }
            }

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
                var strings1 = data.Split(entrySep);
                //Debug.Log("strings1= " + strings1);
                foreach (string s in strings1)
                {
                    var subs = s.Split(keyValSep);
                    ColorUtility.TryParseHtmlString(subs[1], out Color color);
                    if (color != null && boatMats.ContainsKey(subs[0])) boatMats[subs[0]].color = color;
                    //Debug.Log(subs[0] + " -is- " + subs[1]);

                }
            }
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


    }
}
