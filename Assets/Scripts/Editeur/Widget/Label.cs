using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{
	public class Label : Widget {

		private static readonly int DIM_PADDING = 5;

        protected string text;

        protected Color text_color = Color.black;

        protected Color background_color = Color.clear;

        public Color Color { get { return this.text_color; } set { this.text_color = value; } }

        public Color Background { get { return this.background_color; } set { background_color = value; } }

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
			Rect rect = new Rect (zone.xMin + DIM_PADDING,
                zone.yMin + DIM_PADDING,
                zone.width,
                zone.height - DIM_PADDING * 2);
			
			GUI.color = this.text_color;
			GUI.Label (rect, text);
		}

	}
}