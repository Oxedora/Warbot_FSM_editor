using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{
	public class PrimitiveSelector : Widget{

		protected Label titre;
		protected bool dropped = false;

		private static readonly float dim_scale_width_p = 0.25f;

		private static readonly int dim_margin_top = 20;
		private static readonly int dim_padding = 5;

		private static readonly float dim_scale_width = 0.8f;
		private static readonly int dim_pixel_height = 30;

		private static readonly int dim_triangle_size = 20;

		/// <summary>
		/// Initializes a new instance of the <see cref="WarBotEngine.Editeur.PrimitiveSelector"/> class.
		/// </summary>
		/// <param name="r">La taille du widget.</param>
		/// <param name="s">Son label.</param>
		public PrimitiveSelector(Rect r, string s)
		{
			this.area = r;
			this.area.height = dim_pixel_height;
			this.titre = new Label (new Rect (r.xMin + dim_padding + dim_triangle_size,
				r.yMin,
				r.width,
				dim_pixel_height), s);
		}

		public override void OnDrawWithGL()
		{
			if (!dropped) {
				// Traçage du rectangle de fond
				float left = (float)this.area.x, 
				right = (float)this.area.xMax, 
				bottom = (float)(this.area.yMin), 
				top = (float)(this.area.yMax);
				GL.Begin (GL.QUADS);
				GL.Color (Color.white);
				GL.Vertex3 (left, top, 0);
				GL.Vertex3 (right, top, 0);
				GL.Vertex3 (right, bottom, 0);
				GL.Vertex3 (left, bottom, 0);
				GL.End ();

				//Traçage du triangle cliquable
				left = (float)(this.area.xMin + dim_padding);
				right = (float)(this.area.xMin + dim_padding + dim_triangle_size);
				bottom = (float)(this.area.yMin + dim_padding);
				top = (float)(this.area.yMin + dim_padding + dim_triangle_size);
				GL.Begin (GL.TRIANGLES);
				GL.Color (Color.black);
				GL.Vertex3 (left, bottom, 0);
				GL.Vertex3 (right, (top + bottom)/2, 0);
				GL.Vertex3 (left, top, 0);
				GL.End ();
			} 
			else 
			{
				// Traçage du rectangle de fond
				float left = (float)this.area.x, 
				right = (float)this.area.xMax, 
				bottom = (float)(this.area.yMin), 
				top = (float)(this.area.yMax + this.area.height);
				GL.Begin (GL.QUADS);
				GL.Color (Color.white);
				GL.Vertex3 (left, top, 0);
				GL.Vertex3 (right, top, 0);
				GL.Vertex3 (right, bottom, 0);
				GL.Vertex3 (left, bottom, 0);
				GL.End ();

				//Traçage du triangle cliquable
				left = (float)(this.area.xMin + dim_padding);
				right = (float)(this.area.xMin + dim_padding + dim_triangle_size);
				bottom = (float)(this.area.yMin + dim_padding);
				top = (float)(this.area.yMin + dim_padding + dim_triangle_size);
				GL.Begin (GL.TRIANGLES);
				GL.Color (Color.black);
				GL.Vertex3 (left, bottom, 0);
				GL.Vertex3 ((left + right) / 2, top, 0);
				GL.Vertex3 (right, bottom, 0);
				GL.End ();

			}
		}

		public override void OnDrawWithoutGL()
		{
			titre.OnDrawWithoutGL ();
		}
	}
}
