using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{
	public class Label : Widget {

		protected string label;

		private static readonly int dim_padding = 5;

		/// <summary>
		/// Initializes a new instance of the <see cref="WarBotEngine.Editeur.Label"/> class.
		/// </summary>
		/// <param name="r">Taille du label</param>
		/// <param name="s">Le label a afficher</param>
		public Label(Rect r, string s)
		{
			this.area = r;			
			this.label = s;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WarBotEngine.Editeur.Label"/> class.
		/// </summary>
		/// <param name="r">Taille du label</param>
		/// <param name="s">Label a afficher</param>
		/// <param name="p">Parent du Label</param>
		public Label(Rect r, string s, Widget p)
		{
			this.label = s;
			this.parent = p;
			this.area = r;
		}

		public override void OnDrawWithoutGL()
		{
			Rect rect = new Rect (this.area.xMin + dim_padding, 
				this.area.yMin + dim_padding, 
				this.area.width, 
				this.area.height - dim_padding * 2);
			
			GUI.color = Color.black;
			GUI.Label (rect, label);
		}

	}
}