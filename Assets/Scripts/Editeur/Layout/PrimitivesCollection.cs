using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

	/// <summary>
	/// Liste des primitives
	/// </summary>
	public class PrimitivesCollection : Container
	{


		/*********************************
	         ****** ATTRIBUTS STATIQUES ******
	         *********************************/


		/// <summary>
		/// Hauteur du titre de la liste
		/// </summary>
		private static readonly int DIM_TITLE_HEIGHT = 50;

		/// <summary>
		/// Couleur de fond
		/// </summary>
		private static readonly Color BACKGROUND_COLOR = new Color ((float)0xb5 / 255, (float)0xcf / 255, (float)0xd8 / 255);
		//#b5cfd8

		private static readonly int nbUnits = 2;

		/// <summary>
		/// Liste des primitives auquel a accès chaque unité.
		/// </summary>
		private List<Primitive>[][] primitivesByUnits;

		/************************
	         ****** ACCESSEURS ******
	         ************************/


		/// <summary>
		/// Liste des catégories
		/// </summary>
		public Category[] Categories {
			get {
				List<Category> list = new List<Category> ();
				for (int i = 1; i < this.childs.Count; i++) {
					list.Add ((Category)this.childs [i]);
				}
				return list.ToArray ();
			}
		}


		/********************************************
	         ****** METHODES SPECIFIQUES AU WIDGET ******
	         ********************************************/


		/// <summary>
		/// Constructeur de base de la liste des primitives
		/// </summary>
		public PrimitivesCollection (string unitName) : base (new Rect (0, Screen.height * TeamSelection.DIM_HEIGHT, Screen.width * TeamSelection.DIM_WIDTH, Screen.height * (1 - TeamSelection.DIM_HEIGHT)))
		{
			this.AllowScrollbar = true;
			this.Background = BACKGROUND_COLOR;

			primitivesByUnits = new List<Primitive>[nbUnits][];
			for (int i = 0; i < nbUnits; i++) {
				primitivesByUnits [i] = new List<Primitive>[3];
			}

			List<Primitive> Control = new List<Primitive> ();
			Control.Add (new Primitive (new Vector2(), "when"));
			List<Primitive> Condition = new List<Primitive> ();
			Condition.Add (new Primitive (new Vector2(), "isBagEmpty"));
			List<Primitive> Action = new List<Primitive> ();
			Action.Add (new Primitive (new Vector2(), "move"));

			primitivesByUnits [0] [0] = Control;
			primitivesByUnits [1] [0] = Control;
			primitivesByUnits [0] [1] = Condition;
			primitivesByUnits [1] [1] = Control;
			primitivesByUnits [0] [2] = Action;
			primitivesByUnits [1] [2] = Control;

			this.AddChild (new Label (new Rect (0, 0, this.area.width, DIM_TITLE_HEIGHT), "Primitives pour " + unitName));

			// Récupération de la place de l'unité dans le tableau.
			int selectedUnit = 0;
			switch (unitName) {
			case "Unité 1":
				selectedUnit = 0;
				break;

			case "Unité 4":
				selectedUnit = 1;
				break;

			default:
				selectedUnit = 0;
				break;
			}
	           
			//Ajout de la list des controles
			Category category = new Category ("Control", new Vector2 (0, DIM_TITLE_HEIGHT), (int)this.area.width - Scrollbar.DIM_WIDTH);
			category.Resize += OnCategoryResize;
			string[] Elements = new string[primitivesByUnits [selectedUnit] [0].Count];
			int cpt = 0;
			foreach (Primitive p in primitivesByUnits [selectedUnit] [0]) {
				Elements [cpt] = p.Title.Text;
				cpt++;
			}
			category.Elements = Elements;
			this.AddChild (category);

			//Ajout de la liste des conditions
			category = new Category ("Condition", new Vector2 (0, DIM_TITLE_HEIGHT + Category.DIM_ELEMENT_HEIGHT), (int)this.area.width - Scrollbar.DIM_WIDTH);
			category.Resize += OnCategoryResize;
			Elements = new string[primitivesByUnits [selectedUnit] [1].Count];
			cpt = 0;
			foreach (Primitive p in primitivesByUnits [selectedUnit] [1]) {
				Elements [cpt] = p.Title.Text;
				cpt++;
			}
			category.Elements = Elements;
			this.AddChild (category);

			//Ajout de la liste des actions
			category = new Category ("Action", new Vector2 (0, DIM_TITLE_HEIGHT + Category.DIM_ELEMENT_HEIGHT*2), (int)this.area.width - Scrollbar.DIM_WIDTH);
			category.Resize += OnCategoryResize;
			Elements = new string[primitivesByUnits [selectedUnit] [2].Count];
			cpt = 0;
			foreach (Primitive p in primitivesByUnits [selectedUnit] [2]) {
				Elements [cpt] = p.Title.Text;
				cpt++;
			}
			category.Elements = Elements;
			this.AddChild (category);

			//A supprimer
			//			Category category = new Category("Catégorie 1", new Vector2(0, DIM_TITLE_HEIGHT), (int)this.area.width - Scrollbar.DIM_WIDTH);
			//            category.Resize += OnCategoryResize;
			//            category.Elements = new string[]
			//            {
			//                "SI ALORS.. SINON..",
			//                "Primitive 2",
			//                "Primitive 3",
			//                "Primitive 4",
			//                "Primitive 5",
			//                "Primitive 6",
			//                "Primitive 7",
			//                "Primitive 8",
			//                "Primitive 9",
			//                "Primitive 10",
			//                "Primitive 11",
			//                "Primitive 12",
			//                "Primitive 13",
			//                "Primitive 14"
			//            };
			//            this.AddChild(category);
			//
			//            category = new Category("Catégorie 2", new Vector2(0, DIM_TITLE_HEIGHT + Category.DIM_ELEMENT_HEIGHT), (int)this.area.width - Scrollbar.DIM_WIDTH);
			//            category.Resize += OnCategoryResize;
			//            category.Elements = new string[]
			//            {
			//                "Primitive 1",
			//                "Primitive 2",
			//                "Primitive 3",
			//                "Primitive 4",
			//                "Primitive 5",
			//                "Primitive 6"
			//            };
			//            this.AddChild(category);
		}

		/// <summary>
		/// Appelée lorsque qu'une catégorie se redimensionne
		/// </summary>
		/// <param name="widget"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void OnCategoryResize (Widget widget, int  x, int y)
		{
			for (int i = 1; i < this.childs.Count; i++) {
				this.childs [i].LocalPosition = new Vector2 (this.childs [i].LocalPosition.x, this.childs [i - 1].LocalArea.yMax);
				this.positions [i] = this.positions [i - 1] + new Vector2 (0, this.childs [i - 1].LocalArea.height);
			}
			this.scrollbar.ScrollHeight = this.childs [this.childs.Count - 1].LocalArea.yMax;
		}

		/// <summary>
		/// Surcharge de la fonction héritée pour éviter des erreurs
		/// </summary>
		protected override void RefreshDiplaying ()
		{
			foreach (Widget widget in this.childs) {
				widget.Active = true;
			}
		}

	}

}