using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Assets.Scripts.Editeur.Interpreter;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Widget permettant le glissez-déposez
    /// </summary>
    public class DragAndDrop : Widget
    {


        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/


        /// <summary>
        /// Décalage de la primitive par rapport au curseur lors de la création
        /// </summary>
        private static readonly Vector2 PRIMITIVE_CURSOR_DEC = new Vector2(Primitive.DIM_WIDTH / 2, Primitive.DIM_TITLE_HEIGHT / 2);


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        /// <summary>
        /// Liste des primitives
        /// </summary>
        private PrimitivesCollection primitives;

        /// <summary>
        /// Editeur de droite
        /// </summary>
        private BehaviorEditor editor;

        /// <summary>
        /// Position du curseur actuelle
        /// </summary>
        private Vector2 cursor = new Vector2();

        /// <summary>
        /// Position de la primitive lors du clic
        /// </summary>
        private Vector2 saved_position;

        /// <summary>
        /// Position du curseur lors du clic
        /// </summary>
        private Vector2 saved_cursor;

		/**********************
		 ***** ACCESSEURS *****
		 **********************/

		public PrimitivesCollection Primitives_collection {
			get{ return this.primitives; } 
			set {
				this.primitives = value; 
				foreach (Category ca in primitives.Categories) {
					ca.SelectItem += OnSelectItem;
				} 
			}
		}
		public BehaviorEditor Editor {	get{return this.editor;} set{this.editor = value;} }

        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base du glissez-déposez
        /// </summary>
        /// <param name="primitives">liste des primitives</param>
        /// <param name="editor">éditeur de droite</param>
        public DragAndDrop(PrimitivesCollection primitives, BehaviorEditor editor)
        {
            this.LocalArea = new Rect(0, 0, Screen.width, Screen.height);
            this.primitives = primitives;
            this.editor = editor;
            foreach (Category ca in primitives.Categories)
            {
                ca.SelectItem += OnSelectItem;
            }
        }

        /// <summary>
        /// Appelée lors de la sélection d'une primitive dans la liste
        /// </summary>
        /// <param name="widget">widget</param>
        /// <param name="args">nom de la primitive</param>
		private void OnSelectItem(Widget widget, object args)
        {
			// Identification de la catégorie
			Category[] cats = this.primitives.Categories;
			string label = (string)args;
			Instruction instruction;

			int categorySelected = -1;
			int categorySelection = -1;
			foreach (Category c in cats) {
				categorySelected++;
				if (c.Selection == label) {
					categorySelection = c.EmplacementSelection; 
					break;
				}
			}

			if (categorySelected != -1) 
				instruction = this.primitives.InstructionSelected (categorySelected, categorySelection);
			else
				instruction = null;

			Debug.Log ("Primitive sélectionnée : "+instruction.Type());

			this.AddChild(new Primitive(cursor - PRIMITIVE_CURSOR_DEC, label, instruction));
            this.saved_position = this.childs[0].GlobalPosition;
            this.saved_cursor = cursor;
        }


        /***********************************
         ****** METHODES D'EVENEMENTS ******
         ***********************************/


        public override void OnMouseEvent(int button, bool pressed, int x, int y)
        {
            if (!pressed && this.childs.Count > 0)
            {
                this.editor.First.PushPrimitive((Primitive)this.childs[0], this.cursor);
                this.RemoveAllChilds();
            }
        }

        public override void OnMotionEvent(int x, int y)
        {
            this.cursor = new Vector2(x, y);
            if (this.childs.Count > 0)
            {
                this.childs[0].GlobalPosition = this.saved_position + (this.cursor - this.saved_cursor);
            }
        }
    }

}
