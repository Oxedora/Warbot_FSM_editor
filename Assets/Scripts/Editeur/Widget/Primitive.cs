using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Editeur.Interpreter;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Primitive à placer dans l'éditeur
    /// </summary>
    public class Primitive : Widget
    {


        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/


        /// <summary>
        /// Largeur d'une primitive
        /// </summary>
        public static readonly int DIM_WIDTH = 150;

        /// <summary>
        /// Hauteur du titre d'une primitive
        /// </summary>
        public static readonly int DIM_TITLE_HEIGHT = 20;

        /// <summary>
        /// Nom d'une primitive de départ
        /// </summary>
        public static readonly string NAME_PRIMITIVE_BEGIN = "DEBUT";

        /// <summary>
        /// Couleur de fond
        /// </summary>
        private static readonly Color COLOR_1 = new Color((float)0xcc / 255, (float)0xcc / 255, (float)0xff / 255);
        /// <summary>
        /// Couleur de contour
        /// </summary>
        private static readonly Color COLOR_2 = new Color((float)0x00 / 255, (float)0x00 / 255, (float)0x00 / 255);


        /***********************
         ****** ATTRIBUTS ******
         ***********************/

            
        /// <summary>
        /// Titre de la primitive
        /// </summary>
        protected Label title;

        /// <summary>
        /// Hauteur minimale de la primitive
        /// </summary>
        protected int minimum_height = DIM_TITLE_HEIGHT;

        /// <summary>
        /// Primitives en entrée
        /// </summary>
		protected PrimitiveContainer inner = null;

        /// <summary>
        /// Primitives en sortie
        /// </summary>
		protected PrimitiveContainer outer = null;

        /// <summary>
        /// Primitive suivante
        /// </summary>
        protected Primitive next = null;

		/// <summary>
		/// The instruction.
		/// </summary>
		protected Instruction instruction = null;


        /************************************
         ****** EVENEMENTS SPECIFIQUES ******
         ************************************/


        /// <summary>
        /// Appelé lors de l'extension des primitives
        /// </summary>
        public event Widget.EventDelegate ExtendHeight;


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Primitive suivante
        /// </summary>
        public Primitive Next
        {
            get
            {
                return this.next;
            }
            set
            {
                if (this.next != null)
                {
                    this.RemoveChild(this.next);
                    if (value != null)
                        value.Last = this.next;
                }
                this.next = value;
                if (this.next != null)
                {
                    this.AddChild(value);
                    value.LocalPosition = new Vector2(0, this.LocalArea.height);
                    this.ExtendPrimitive(this.Next.TotalHeight);
                }
                else
                    this.ExtendPrimitive(0);
            }
        }

        /// <summary>
        /// Dernière primitive de la colonne
        /// </summary>
        public Primitive Last
        {
            get
            {
                if (this.next != null)
                    return this.next.Last;
                else
                    return this;
            }
            set
            {
                if (this.next != null)
                    this.next.Last = value;
                else
                    this.Next = value;
            }
        }

        /// <summary>
        /// Hauteur totale de l'arbre
        /// </summary>
        public int TotalHeight
        {
            get
            {
                if (this.Next == null)
                    return (int)this.area.height;
                else
                    return (int)this.area.height + this.Next.TotalHeight;
            }
        }

        /// <summary>
        /// Largeur totale de l'arbre
        /// </summary>
        public int TotalWidth
        {
            get
            {
                float res = this.area.width;
				if(this.inner != null)
					res = Mathf.Max(res, this.inner.LocalArea.x + this.inner.First.TotalWidth);
				if(this.outer != null)
					res = Mathf.Max(res, this.outer.LocalArea.x + this.outer.First.TotalWidth);
                if (this.Next != null)
                    res = Mathf.Max(res, this.Next.TotalWidth);
                return (int)res;
            }
        }

		public Label Title{get{ return title;} set{this.title = value;}}

		public Instruction Instruction{ 
			get{
				return this.instruction;
			} 
			set{ this.instruction = value;}
		}

        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur vide d'une primitive
        /// </summary>
        protected Primitive() {}

        /// <summary>
        /// Constructeur de base d'une primitive
        /// </summary>
        /// <param name="position">position de la primitive</param>
        /// <param name="name">nom de la primitive</param>
		public Primitive(Vector2 position, string name, Instruction instruction)
		{
			this.instruction = instruction;
			if (name == NAME_PRIMITIVE_BEGIN)
				this.LocalArea = new Rect (position.x, position.y, DIM_WIDTH, DIM_TITLE_HEIGHT);
			else if (instruction.GetType ().Equals (typeof(When))) {
				When w = (When)instruction;

				this.LocalArea = new Rect (position.x, position.y, DIM_WIDTH, 200);
				this.inner = (new PrimitiveContainer (new Vector2 (this.area.width, 0), "CONDITION", this, true));
				this.AddChild (this.inner);
				for (int i = w.getConditions ().Count - 1; i > -1; i--) {
					Primitive p = new Primitive (new Vector2 (), w.getConditions () [i].Type (), w.getConditions () [i]);
					this.inner.PushPrimitive (p, this.inner.First.GlobalPosition);
				}

				this.outer = (new PrimitiveContainer (new Vector2 (this.area.width, 0), "ALORS", this, false));
				this.AddChild (this.outer);
				for (int i = w.getActions ().Count - 1; i > -1; i--) {
					Primitive p = new Primitive (new Vector2 (), w.getActions () [i].Type (), w.getActions () [i]);
					this.outer.PushPrimitive (p, this.outer.First.GlobalPosition);
				}


				this.inner.ExtendHeight += this.OnPrimitiveContainerExtend;
				this.outer.ExtendHeight += this.OnPrimitiveContainerExtend;
				this.OnPrimitiveContainerExtend(null, null);

			} else {
				this.LocalArea = new Rect (position.x, position.y, DIM_WIDTH, 100);
				this.minimum_height = 100;
			}

			this.title = new Label (new Rect (0, 0, this.area.width, DIM_TITLE_HEIGHT), name);
			this.title.Color = COLOR_2;
			this.AddChild (this.title);

        }

        /// <summary>
        /// Tente de placer la primitive entrée en paramètre
        /// </summary>
        /// <param name="primitive">primitive à placer</param>
        /// <param name="cursor">position du curseur</param>
		public virtual bool PushPrimitive(Primitive primitive, Vector2 cursor)
        {
            if (this.GlobalArea.Contains(cursor))
            {
                this.Next = primitive;
				return true;
            }
            else
            {
                foreach (Widget w in this.childs)
                {
                    if (typeof(Primitive).Equals(w.GetType()) || typeof(PrimitiveContainer).Equals(w.GetType()))
                        ((Primitive)w).PushPrimitive(primitive, cursor);
                }
            }
			return false;
        }

        /// <summary>
        /// Appelée lors de l'extension de la primitive
        /// </summary>
        /// <param name="height"></param>
        public virtual void ExtendPrimitive(int height)
        {
            if (this.Parent != null && typeof(Primitive).Equals(this.Parent.GetType()))
                ((Primitive)this.Parent).ExtendPrimitive((int)this.area.height + height);
            if (this.ExtendHeight != null)
                this.ExtendHeight(this, (int)this.area.height + height);
        }

        /// <summary>
        /// Redimensionne la primitive (appelée lors de l'extension des fils)
        /// </summary>
        /// <param name="w">widget</param>
        /// <param name="args">arguments</param>
        public virtual void OnPrimitiveContainerExtend(Widget w, object args)
        {
            int y_pos = DIM_TITLE_HEIGHT;
            
            this.inner.LocalPosition = new Vector2(this.area.width, y_pos);
			y_pos += (int)this.inner.LocalArea.height + this.inner.InnerHeight;
            
			this.outer.LocalPosition = new Vector2(this.area.width, y_pos);
			y_pos += (int)this.outer.LocalArea.height + this.outer.InnerHeight;
            
            if (y_pos < this.minimum_height)
                this.LocalArea = new Rect(this.LocalArea.x, this.LocalArea.y, this.LocalArea.width, this.minimum_height);
            else
                this.LocalArea = new Rect(this.LocalArea.x, this.LocalArea.y, this.LocalArea.width, y_pos);

            if (this.Next != null)
                this.ExtendPrimitive(this.Next.TotalHeight);
            else
                this.ExtendPrimitive(0);
        }


        /***********************************
         ****** METHODES D'EVENEMENTS ******
         ***********************************/


        public override void OnDrawWithGL()
        {
            GL.Begin(GL.QUADS);
            GL.Color(COLOR_1);
            Rect rect = this.GlobalArea;
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.End();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.End();
        }

        public override void OnMouseEvent(int button, bool pressed, int x, int y)
        {
            base.OnMouseEvent(button, pressed, x, y);
            if (pressed && this.title.Text != NAME_PRIMITIVE_BEGIN && !typeof(PrimitiveContainer).Equals(this.GetType()) && this.GlobalArea.Contains(new Vector2(x, y)))
            {
                this.LocalArea = this.GlobalArea;
                ((Primitive)this.Parent).Next = null;
                MainLayout.Actual.Dropper.AddChild(this);
            }
        }

    }

}