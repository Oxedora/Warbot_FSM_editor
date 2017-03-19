using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;

namespace WarBotEngine.Editeur
{
	/// <summary>
	/// Class qui implementerait un drop down pour visualiser la sélection d'équipes
	/// et des unités.
	/// </summary>
	public class Selector : Widget {

		protected List<Label> options = new List<Label>();

		protected int selected_option = -1;
		protected bool dropped = false;

		private static readonly float dim_scale_width_p = 0.25f;

		private static readonly int dim_margin_top = 20;
		private static readonly int dim_padding = 5;

		private static readonly float dim_scale_width = 0.8f;
		private static readonly int dim_pixel_height = 30;

		private static readonly int dim_triangle_size = 20;

		/// <summary>
		/// Initializes a new instance of the <see cref="WarBotEngine.Editeur.Selector"/> class.
		/// </summary>
		/// <param name="opt">La liste des options que proposera le selector</param>
		public Selector(List<string> opt)
		{
			Rect zone = new Rect(0, 0, Screen.width * dim_scale_width_p, Screen.height);
			float width = zone.width * dim_scale_width;
			this.area = new Rect((zone.width - width) / 2, dim_margin_top, width, dim_pixel_height);

			int i = 0;
			foreach (string o in opt) 
			{
				options.Add(new Label(new Rect(this.area.xMin,
					this.area.yMin + i * dim_pixel_height,
					this.area.width,
					dim_pixel_height),
					o));
				i++;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WarBotEngine.Editeur.Selector"/> class.
		/// Constructeur par copie
		/// </summary>
		/// <param name="sel">Selector a copier.</param>
		public Selector(Selector sel)
		{
			this.area = sel.Area;
			this.options = sel.options;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WarBotEngine.Editeur.Selector"/> class.
		/// </summary>
		public Selector()
		{
			Rect zone = new Rect(0, 0, Screen.width * dim_scale_width_p, Screen.height);
			float width = zone.width * dim_scale_width;
			this.area = new Rect((zone.width - width) / 2, dim_margin_top, width, dim_pixel_height);
		}

		public Selector(string path)
		{
			Rect zone = new Rect(0, 0, Screen.width * dim_scale_width_p, Screen.height);
			float width = zone.width * dim_scale_width;
			this.area = new Rect((zone.width - width) / 2, dim_margin_top, width, dim_pixel_height);

			string pattern = @"(^(.*)\\)|(.meta)"; // @".." ne tient pas compte des séquences d'échappement

			int i = 0;
			foreach (string file in Directory.GetFiles(path)) // Pour chacun des fichiers du chemin spécifié
			{ 
				string teamName = Regex.Replace(file, pattern, string.Empty); // Récupère seulement le nom de l'équipe dans le chemin donné

				options.Add(new Label(new Rect(this.area.xMin,
					this.area.yMin + i * dim_pixel_height,
					this.area.width,
					dim_pixel_height),
					teamName)); // ajoute le nom de l'équipe au menu défilant
				i++;
			}
		}

		/// <summary>
		/// Options proposées par le selector
		/// </summary>
		public Label[] Options { get { return options.ToArray(); } }

		public Label SelectedOption()
		{
			Label defaut = (new Label(new Rect(this.area.xMin,
				this.area.yMin,
				this.area.width,
				dim_pixel_height),
				"Aucune équipe sélectionnée"));
			return (selected_option != -1 ? options [selected_option] : defaut);
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
				left = (float)(this.area.xMax - dim_padding - dim_triangle_size);
				right = (float)(this.area.xMax - dim_padding);
				bottom = (float)(this.area.yMin + dim_padding);
				top = (float)(this.area.yMin + dim_padding + dim_triangle_size);
				GL.Begin (GL.TRIANGLES);
				GL.Color (Color.black);
				GL.Vertex3 (left, bottom, 0);
				GL.Vertex3 ((left + right) / 2, top, 0);
				GL.Vertex3 (right, bottom, 0);
				GL.End ();
			} 
			else 
			{
				// Traçage du rectangle de fond
				float left = (float)this.area.x, 
				right = (float)this.area.xMax, 
				bottom = (float)(this.area.yMin), 
				top = (float)(this.area.yMax + this.area.height * (options.Count - 1));
				GL.Begin (GL.QUADS);
				GL.Color (Color.white);
				GL.Vertex3 (left, top, 0);
				GL.Vertex3 (right, top, 0);
				GL.Vertex3 (right, bottom, 0);
				GL.Vertex3 (left, bottom, 0);
				GL.End ();

				//Traçage du triangle cliquable
				left = (float)(this.area.xMax - dim_padding - dim_triangle_size);
				right = (float)(this.area.xMax - dim_padding);
				bottom = (float)(this.area.yMin + dim_padding);
				top = (float)(this.area.yMin + dim_padding + dim_triangle_size);
				GL.Begin (GL.TRIANGLES);
				GL.Color (Color.black);
				GL.Vertex3 (left, top, 0);
				GL.Vertex3 ((left + right) / 2, bottom, 0);
				GL.Vertex3 (right, top, 0);
				GL.End ();
				
			}
		}

		public override void OnDrawWithoutGL()
		{
			if (!dropped) {
				SelectedOption().OnDrawWithoutGL();
			} 
			else 
			{
				for (int i = 0; i < options.Count; i++) 
				{
					options[i].OnDrawWithoutGL();
				}
			}
		}

		public override void OnMouseEvent(int button, bool pressed, int x, int y)
		{
			if (pressed && button == 0) 
			{
				Vector2 cursor = new Vector2 (x, y);
				Rect triangle = new Rect (this.area.xMax - dim_padding - dim_triangle_size,
					                this.area.yMin + dim_padding,
					                dim_triangle_size,
					                dim_triangle_size);

				// Si on a cliqué sur le triangle on déploie ou remballe
				if (triangle.Contains (cursor)) {
					dropped = !dropped;	
				}
			}
		}
	}
}