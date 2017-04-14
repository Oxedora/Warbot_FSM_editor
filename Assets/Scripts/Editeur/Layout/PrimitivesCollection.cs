using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Editeur.Interpreter;

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
		private List<Instruction>[][] instructionsByUnits;

		/// <summary>
		/// The selected unit.
		/// </summary>
		private int selectedUnit = 0;


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

		public Instruction InstructionSelected(int category, int selection)
		{
			int cpt = 0;
			foreach (Instruction i in instructionsByUnits[selectedUnit][category]) {
				if (cpt == selection)
					return i;
			}
			return null;
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

			instructionsByUnits = new List<Instruction>[nbUnits][];
			for (int i = 0; i < nbUnits; i++) {
				instructionsByUnits [i] = new List<Instruction>[3];
			}

			List<Instruction> Control = new List<Instruction> ();
			Control.Add (new When());
			List<Instruction> Condition = new List<Instruction> ();
			Condition.Add (new IsBagEmpty());
			List<Instruction> Action = new List<Instruction> ();
			Action.Add (new Move());

			instructionsByUnits [0] [0] = Control;
			instructionsByUnits [1] [0] = Control;
			instructionsByUnits [0] [1] = Condition;
			instructionsByUnits [1] [1] = Control;
			instructionsByUnits [0] [2] = Action;
			instructionsByUnits [1] [2] = Control;

			this.AddChild (new Label (new Rect (0, 0, this.area.width, DIM_TITLE_HEIGHT), "Primitives pour " + unitName));

			// Récupération de la place de l'unité dans le tableau.
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
			string[] Elements = new string[instructionsByUnits [selectedUnit] [0].Count];
			int cpt = 0;
			foreach (Instruction i in instructionsByUnits [selectedUnit] [0]) {
				Elements [cpt] = i.GetType().Name;
				cpt++;
			}
			category.Elements = Elements;
			this.AddChild (category);

			//Ajout de la liste des conditions
			category = new Category ("Condition", new Vector2 (0, DIM_TITLE_HEIGHT + Category.DIM_ELEMENT_HEIGHT), (int)this.area.width - Scrollbar.DIM_WIDTH);
			category.Resize += OnCategoryResize;
			Elements = new string[instructionsByUnits [selectedUnit] [1].Count];
			cpt = 0;
			foreach (Instruction i in instructionsByUnits [selectedUnit] [1]) {
				Elements [cpt] = i.GetType().Name;
				cpt++;
			}
			category.Elements = Elements;
			this.AddChild (category);

			//Ajout de la liste des actions
			category = new Category ("Action", new Vector2 (0, DIM_TITLE_HEIGHT + Category.DIM_ELEMENT_HEIGHT*2), (int)this.area.width - Scrollbar.DIM_WIDTH);
			category.Resize += OnCategoryResize;
			Elements = new string[instructionsByUnits [selectedUnit] [2].Count];
			cpt = 0;
			foreach (Instruction i in instructionsByUnits [selectedUnit] [2]) {
				Elements [cpt] = i.GetType().Name;
				cpt++;
			}
			category.Elements = Elements;
			this.AddChild (category);


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