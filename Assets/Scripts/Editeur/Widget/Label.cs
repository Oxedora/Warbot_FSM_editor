using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Texte affichable
    /// </summary>
	public class Label : Widget
    {


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        protected string text;

        protected int text_size = 14;

        protected FontStyle text_style = FontStyle.Normal;

        protected TextAnchor text_align = TextAnchor.MiddleCenter;

        protected Color text_color = Color.black;

        protected Color background_color = Color.clear;

        protected int margin = 0;


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Contenu du label
        /// </summary>
        public string Text { get { return this.text; } set { this.text = value; } }

        /// <summary>
        /// Hauteur du texte
        /// </summary>
        public int TextSize { get { return this.text_size; } set { this.text_size = value; } }

        /// <summary>
        /// Style du texte
        /// </summary>
        public FontStyle TextStyle { get { return this.text_style; } set { this.text_style = value; } }

        /// <summary>
        /// Alignement du texte
        /// </summary>
        public TextAnchor TextAlign { get { return this.text_align; } set { this.text_align = value; } }

        /// <summary>
        /// Couleur du texte
        /// </summary>
        public Color Color { get { return this.text_color; } set { this.text_color = value; } }

        /// <summary>
        /// Couleur du font
        /// </summary>
        public Color Background { get { return this.background_color; } set { background_color = value; } }

        /// <summary>
        /// Taille de la marge horizontale
        /// </summary>
        public int Margin { get { return this.margin; } set { this.margin = value; } }


        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base du label
        /// </summary>
        /// <param name="r">Taille du label</param>
        /// <param name="s">Le texte à afficher</param>
        public Label(Rect r, string s)
		{
			this.area = r;			
			this.text = s;
		}


        /***********************************
         ****** METHODES D'EVENEMENTS ******
         ***********************************/


        public override void OnDrawWithGL()
        {
            if (!this.active) return;
            if (this.background_color != Color.clear)
            {
                GL.Begin(GL.QUADS);
                GL.Color(this.background_color);
                Rect rect = this.GlobalArea;
                GL.Vertex3(rect.xMin, rect.yMin, 0);
                GL.Vertex3(rect.xMax, rect.yMin, 0);
                GL.Vertex3(rect.xMax, rect.yMax, 0);
                GL.Vertex3(rect.xMin, rect.yMax, 0);
                GL.End();
            }
        }

        public override void OnDrawWithoutGL()
        {
            if (!this.active) return;
            Rect zone = this.GlobalArea;
            zone.x += this.margin;
            zone.width -= 2 * this.margin;
			GUI.color = this.text_color;

            GUIStyle style = GUI.skin.GetStyle("label");
            style.alignment = this.text_align;
            style.fontSize = this.text_size;
            style.fontStyle = this.text_style;
            GUI.Label(zone, text);
		}

	}

}