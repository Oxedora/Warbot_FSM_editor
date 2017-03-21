using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class TeamSelection : Container
    {

        public static readonly float DIM_WIDTH = 0.25f;
        public static readonly float DIM_HEIGHT = 0.25f;

        public static readonly float DIM_SELECTOR_WIDTH = 0.75f;
        public static readonly int DIM_SELECTOR_HEIGHT = 30;

        public static readonly float DIM_SELECTOR_DROP_HEIGHT = 1f;

        private static readonly Color BACKGROUND_COLOR = new Color((float)0x73 / 255, (float)0x93 / 255, (float)0xa7 / 255); //#7393a7

        Selector team_selector, unit_selector;

        public TeamSelection() : base(new Rect(0, 0, Screen.width * DIM_WIDTH, Screen.height * DIM_HEIGHT))
        {
            this.Background = BACKGROUND_COLOR;
            team_selector = new Selector(new Rect(this.area.width * (1 - DIM_SELECTOR_WIDTH) / 2, (this.area.height - 2 * DIM_SELECTOR_HEIGHT) / 3, DIM_SELECTOR_WIDTH * this.area.width, DIM_SELECTOR_HEIGHT), (int)(DIM_SELECTOR_DROP_HEIGHT * this.area.height));
            unit_selector = new Selector(new Rect(this.area.width * (1 - DIM_SELECTOR_WIDTH) / 2, (this.area.height - 2 * DIM_SELECTOR_HEIGHT) * 2 / 3 + DIM_SELECTOR_HEIGHT, DIM_SELECTOR_WIDTH * this.area.width, DIM_SELECTOR_HEIGHT), (int)(DIM_SELECTOR_DROP_HEIGHT * this.area.height));
            this.AddChild(unit_selector);
            this.AddChild(team_selector);
            
            team_selector.Elements = new string[]
            {
                "Element 1",
                "Element 2",
                "Element 3",
                "Element 4",
                "Element 5",
                "Element 6",
                "Element 7",
                "Element 8",
                "Element 9",
                "Element 10",
                "Element 11",
                "Element 12",
                "Element 13"
            };
        }

    }

}
