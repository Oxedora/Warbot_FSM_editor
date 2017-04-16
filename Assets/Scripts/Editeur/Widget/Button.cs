using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{
    public class Button : Widget
    {

        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/

        private static readonly float VERTICAL_MARGIN = 1f;

        private static readonly float HORIZONTAL_MARGIN = 1f;

        /***********************
         ****** ATTRIBUTS ******
         ***********************/

        /// <summary>
        /// Label du bouton
        /// </summary>
        private Label label;

        /// <summary>
        /// Appelé lors de la sélection du item
        /// </summary>
        public event Widget.EventDelegate behavior = null;

        /// <summary>
        /// Constructeur du bouton
        /// </summary>
        /// <param name="r">Dimension du bouton</param>
        /// <param name="s">Label à afficher</param>
        public Button(Rect r, string s, EventMouseDelegate e)
        {
            this.LocalArea = r;
            Rect rectLabel = new Rect(r);
            this.label = new Label(rectLabel, s);
            this.label.Clic += OnClic;
        }

        public void OnClic(Widget widget, int button, int x, int y)
        {

        }
    }
}
