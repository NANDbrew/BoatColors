using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoatColors
{


    public class PaintMenuButton : GoPointerButton
    {
        public Material offMaterial;

        public Material onMaterial;

        public TargetType type;

        public PaintMenuButton[] linkedToggles;

        internal PaintColorButton[] colorButtons;

        public override void OnActivate()
        {
            UISoundPlayer.instance.PlayUISound(UISounds.buttonClick, 1f, 1.4f);
            SetTo(state: true);
            foreach (PaintMenuButton toggle in linkedToggles) toggle.SetTo(state: false);
        }

        public void Initialize()
        {
            GetComponentInChildren<TextMesh>().text = type.ToString();
        }

        public void SetTo(bool state)
        {
            if (state)
            {
                foreach (PaintColorButton button in colorButtons) button.target = type;

                GetComponent<Renderer>().material = onMaterial;
            }
            else
            {
                GetComponent<Renderer>().material = offMaterial;
            }
        }
    }
}
