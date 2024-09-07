using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoatColors
{
    internal class PaintColorButton : GoPointerButton
    {
        public TargetType target;
        public ButtonType buttonType;
        public Color color;
        public int index;
        public List<PaintColorButton> buttons;
        public TextMesh text;
        private MeshRenderer renderer;
        public Material[] materials;

        public void Awake()
        {
            text = GetComponentInChildren<TextMesh>();
            renderer = GetComponent<MeshRenderer>();
            //UpdateButtonColor();
        }
        public override void OnActivate()
        {
            UISoundPlayer.instance.PlayUISound(UISounds.buttonClick, 1f, 1f);
            if (buttonType.Equals(ButtonType.ChangeTarget))
            {
                index = 1;
                foreach (var button in buttons)
                {
                    if (button != this && button.buttonType.Equals(ButtonType.ChangeTarget))
                    {
                        button.index = 0;
                    }
                    else
                    {
                        button.target = target;
                    }
                    button.UpdateButton();
                }
                //UpdateButton();
            }
            else if (buttonType.Equals(ButtonType.Paint))
            {
                if (target == TargetType.Hull)
                {
                    ShipColors.UpdateColor(GameState.currentShipyard.GetCurrentBoat().name + ShipColors.hullName, color);
                }
                else if (target == TargetType.Trim)
                {
                    ShipColors.UpdateColor(GameState.currentShipyard.GetCurrentBoat().name + ShipColors.trimName, color);
                }
                else if (target == TargetType.Roof)
                {
                    ShipColors.UpdateColor(GameState.currentShipyard.GetCurrentBoat().name + ShipColors.cabinName, color);
                }
            }
            else if (buttonType.Equals(ButtonType.Reset))
            {
                if (target == TargetType.Hull)
                {
                    ShipColors.UpdateColor(GameState.currentShipyard.GetCurrentBoat().name + ShipColors.hullName, ShipColors.defaultColors[GameState.currentShipyard.GetCurrentBoat().name + ShipColors.hullName]);
                }
                else if (target == TargetType.Trim)
                {
                    ShipColors.UpdateColor(GameState.currentShipyard.GetCurrentBoat().name + ShipColors.trimName, ShipColors.defaultColors[GameState.currentShipyard.GetCurrentBoat().name + ShipColors.trimName]);
                }
                else if (target == TargetType.Roof)
                {
                    ShipColors.UpdateColor(GameState.currentShipyard.GetCurrentBoat().name + ShipColors.cabinName, ShipColors.defaultColors[GameState.currentShipyard.GetCurrentBoat().name + ShipColors.cabinName]);
                }

            }
        }

        public void UpdateButton()
        {
            if (buttonType.Equals(ButtonType.Paint))
            {
                color = PrefabsDirectory.instance.sailColors[GameState.currentShipyard.availableSailColors[index]];
                GetComponent<Renderer>().material.color = color;
            }
            else if (buttonType.Equals(ButtonType.ChangeTarget))
            {
                //Debug.Log("durr?");
                text.text = target.ToString() + " Color";
                if (index >= materials.Length) { Debug.Log("ack!!!"); return; }
                renderer.material = materials[index];
            }
            else if (buttonType.Equals(ButtonType.Reset))
            {
                text.text = "Remove\npaint";
            }
        }
    }
    public enum TargetType
    {
        Hull,
        Trim,
        Roof
    }
    public enum ButtonType
    {
        Reset,
        Paint,
        ChangeTarget
    }
}
