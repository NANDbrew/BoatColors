using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BoatColors
{
    [HarmonyPatch(typeof(ShipyardUI))]
    internal class ShipyardUIPatches
    {
        public static PaintColorButton[] buttons;
        public static ShipyardButton[] sailColorButtons;
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void AwakePatch(GameObject ___ui, GameObject ___newPartsMenu, ShipyardButton[] ___sailColorButtons)
        {
            var source0 = UnityEngine.Object.FindObjectOfType<GPButtonControlToggle>();
            var source1 = UnityEngine.Object.FindObjectOfType<GPButtonSliderVolume>();

            Material on = (Material)Traverse.Create(source0).Field("onMaterial").GetValue();
            Material off = (Material)Traverse.Create(source0).Field("offMaterial").GetValue();
            Material blue = ___ui.transform.Find("panel Current Order").Find("shipyard ui button confirm").GetComponent<Renderer>().sharedMaterial;
            var buttonCont = new GameObject { name = "paintColorButtons" };
            var switchButton = UnityEngine.Object.Instantiate(___ui.transform.Find("panel Current Order").Find("shipyard ui button cancel purchase"), buttonCont.transform).gameObject.AddComponent<PaintColorButton>();
            UnityEngine.Object.DestroyImmediate(switchButton.GetComponent<ShipyardButton>());
            switchButton.name = "switchButton";
            switchButton.materials = switchButton.GetComponent<Renderer>().sharedMaterials.AddToArray(blue);
            switchButton.buttonType = ButtonType.ChangeTarget;
            var swbt = switchButton.transform.GetChild(0);
            //swbt.parent = buttonCont.transform;
            switchButton.transform.localScale = new Vector3(1.6f, 0.7f, 0.26f);
            //swbt.parent = switchButton.transform;
            switchButton.text = swbt.GetComponent<TextMesh>();
            switchButton.transform.localPosition = new Vector3(5.1f, -0.3f, 7.63f);
            switchButton.transform.localEulerAngles = new Vector3(0, 9.6f, 0);
            switchButton.target = TargetType.Roof;
            switchButton.transform.GetChild(0).localScale = new Vector3(0.03f, 0.09f, 0.5f);

            var resetButton = UnityEngine.Object.Instantiate(switchButton.transform, buttonCont.transform).GetComponent<PaintColorButton>();
            UnityEngine.Object.Destroy(resetButton.GetComponent<ShipyardButton>());
            resetButton.name = "resetButton";
            resetButton.buttonType = ButtonType.Reset;
            var rbt = resetButton.transform.GetChild(0);
            //rbt.parent = buttonCont.transform;
            resetButton.transform.localScale = new Vector3(1.6f, 1.1f, 0.26f);
            //rbt.parent = resetButton.transform;
            rbt.localScale = new Vector3(0.035f, 0.0573f, 0.5f);
            resetButton.text = rbt.GetComponent<TextMesh>();
            resetButton.transform.localPosition = new Vector3(5.2f, -4.4f, 7.63f);

            sailColorButtons = ___sailColorButtons;
            List<PaintColorButton> colorButtons = new List<PaintColorButton>();
            buttonCont.transform.SetParent(___newPartsMenu.transform, false);
            for (int i = 0; i < ___sailColorButtons.Length; i++)
            {
                var newButton = UnityEngine.Object.Instantiate(___sailColorButtons[i], buttonCont.transform).gameObject.AddComponent<PaintColorButton>();
                UnityEngine.GameObject.Destroy(newButton.GetComponent<ShipyardButton>());
                newButton.gameObject.SetActive(true);
                newButton.index = i;
                newButton.buttonType = ButtonType.Paint;
                newButton.name = "button paint " + i;
                colorButtons.Add(newButton);
            }

            var switchButton2 = UnityEngine.Object.Instantiate(switchButton.transform, buttonCont.transform).GetComponent<PaintColorButton>();
            switchButton2.name = "switchButton 2";
            switchButton2.transform.localPosition = new Vector3(5.1f, 0.4f, 7.63f);
            switchButton2.target = TargetType.Trim;
            switchButton2.Awake();

            var switchButton3 = UnityEngine.Object.Instantiate(switchButton.transform, buttonCont.transform).GetComponent<PaintColorButton>();
            switchButton3.name = "switchButton 3";
            switchButton3.transform.localPosition = new Vector3(5.1f, 1.1f, 7.63f);
            switchButton3.target = TargetType.Hull;
            switchButton3.Awake();

            colorButtons.Add(resetButton);
            colorButtons.Add(switchButton);
            colorButtons.Add(switchButton2);
            colorButtons.Add(switchButton3);
            switchButton.buttons = colorButtons;
            switchButton2.buttons = colorButtons;
            switchButton3.buttons = colorButtons;

            buttonCont.transform.localPosition = new Vector3(0f, 1f, 0);
            buttons = colorButtons.ToArray();
            colorButtons[0].transform.GetChild(0).gameObject.SetActive(false);

        }

        [HarmonyPatch("RefreshButtons")]
        [HarmonyPostfix]
        public static void RefreshButtonsPatch()
        {
            foreach (var button in buttons)
            {
                button.UpdateButton();
            }
        }

        [HarmonyPatch("ShowUI")]
        [HarmonyPostfix]
        public static void ShowUIPatch()
        {
            SailColorPatcher.UpdateCustomSailColors();
        }
    }
}
