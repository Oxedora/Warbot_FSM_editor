using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class TeamSelection : Container
    {

        public static readonly float DIM_WIDTH = 0.25f;
        public static readonly float DIM_HEIGHT = 0.25f;

        private static readonly Color BACKGROUND_COLOR = new Color((float)0x73 / 255, (float)0x93 / 255, (float)0xa7 / 255); //#7393a7

        public TeamSelection() : base(new Rect(0, 0, Screen.width * DIM_WIDTH, Screen.height * DIM_HEIGHT))
        {
            this.Background = BACKGROUND_COLOR;
            Label label = new Label(new Rect(100, 100, 150, 50), "Ceci est un texte");
            label.Background = Color.gray;
            label.Color = Color.red;
            this.AddChild(label);
        }

    }

}
