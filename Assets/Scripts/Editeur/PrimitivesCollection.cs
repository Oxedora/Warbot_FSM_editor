using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class PrimitivesCollection : Container
    {

        private static readonly Color BACKGROUND_COLOR = new Color((float)0xb5 / 255, (float)0xcf / 255, (float)0xd8 / 255); //#b5cfd8

        public PrimitivesCollection() : base(new Rect(0, Screen.height * TeamSelection.DIM_HEIGHT, Screen.width * TeamSelection.DIM_WIDTH, Screen.height * (1 - TeamSelection.DIM_HEIGHT)))
        {
            this.AllowScrollbar = true;
            this.Background = BACKGROUND_COLOR;
        }

    }

}