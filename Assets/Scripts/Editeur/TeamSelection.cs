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
            team_selector.SelectItem += OnSelectItem;
            team_selector.DeployOrTuck += OnDeployOrTuck;
            unit_selector.SelectItem += OnSelectItem;

            // A SUPPRIMER
            team_selector.Elements = new string[]
            {
                "Equipe 1",
                "Equipe 2",
                "Equipe 3",
                "Equipe 4",
                "Equipe 5",
                "Equipe 6",
                "Equipe 7",
                "Equipe 8",
                "Equipe 9",
                "Equipe 10",
                "Equipe 11",
                "Equipe 12",
                "Equipe 13"
            };
            unit_selector.Elements = new string[]
            {
                "Unité 1",
                "Unité 2",
                "Unité 3",
                "Unité 4",
                "Unité 5",
                "Unité 6",
                "Unité 7",
                "Unité 8",
                "Unité 9",
                "Unité 10",
                "Unité 11",
                "Unité 12",
                "Unité 13"
            };
        }

        /// <summary>
        /// Appelé lorsque qu'un élément est sélectionné dans un sélecteur
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="args"></param>
        void OnSelectItem(Widget widget, object args)
        {
            string selection = (string)args;
            if (widget.ID == this.team_selector.ID)
            {
                //Sélection de l'équipe
                
            }
            else
            {
                //Sélection de l'unité

            }
        }

        /// <summary>
        /// Appelé lorsque qu'un sélecteur est déployé
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="args"></param>
        void OnDeployOrTuck(Widget widget, object args)
        {
            bool state = (bool)args;
            if (widget.ID == this.team_selector.ID)
            {
                //Sélection de l'équipe
                this.unit_selector.Active = !state;
            }
        }

    }

}
