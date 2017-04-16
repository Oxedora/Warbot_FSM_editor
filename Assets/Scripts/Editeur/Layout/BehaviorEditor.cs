using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Conteneur de droite d'édition des comportements en glissez-dépossez
    /// </summary>
    public class BehaviorEditor : Container
    {


        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/

            
        /// <summary>
        /// Espace minimum à gauche avant la première primitive
        /// </summary>
        private static readonly int DIM_MINIMUM_SPACE = 200;

        /// <summary>
        /// Couleur de fond
        /// </summary>
        private static readonly Color BACKGROUND_COLOR = new Color((float)0xe8 / 255, (float)0xec / 255, (float)0xf1 / 255); //#e8ecf1

        /// <summary>
        /// Editeur actuel
        /// </summary>
        private static BehaviorEditor actual = null;
        

        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Editeur actuel
        /// </summary>
        public static BehaviorEditor Actual { get { return actual; } }

        /// <summary>
        /// Premier élément de l'éditeur
        /// </summary>
        public Primitive First { get { return (Primitive)this.childs[0]; } }

        /// <summary>
        /// Dernier élément de l'éditeur
        /// </summary>
        public Primitive Last { get { return (Primitive)this.childs[this.childs.Count-1]; } }


        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base de l'éditeur
        /// </summary>
        public BehaviorEditor(string teamName, string unitName) : base(new Rect(Screen.width * TeamSelection.DIM_WIDTH, 0, Screen.width * (1 - TeamSelection.DIM_WIDTH), Screen.height))
        {
            BehaviorEditor.actual = this;

            XMLInterpreter interpreter = new XMLInterpreter();
            List<Instruction> instructions = interpreter.xmlToUnitBehavior(teamName, Constants.teamsDirectory, unitName);

            this.AllowScrollbar = true;
            this.AllowMotionScroll = true;
            this.Background = BACKGROUND_COLOR;

			this.AddChild(new Primitive(new Vector2(DIM_MINIMUM_SPACE, 0), Primitive.NAME_PRIMITIVE_BEGIN, null));
            this.First.ExtendHeight += OnExtendHeight;

            for(int i = instructions.Count-1; i > -1; i--)
            {
                Primitive p = new Primitive(new Vector2(0, 0), instructions[i].Type(), instructions[i]);
                this.Last.PushPrimitive(p, this.Last.GlobalPosition);
            }
        }

        /// <summary>
        /// Appelé lors de l'extension des éléments dans l'éditeur
        /// </summary>
        /// <param name="w">widget</param>
        /// <param name="args">arguments</param>
        public void OnExtendHeight(Widget w, object args)
        {
            this.inner_width = this.First.TotalWidth + 2 * DIM_MINIMUM_SPACE;
            this.motionscroll.ScrollWidth = this.inner_width;
            this.inner_height = this.First.TotalHeight + DIM_MINIMUM_SPACE;
            this.scrollbar.ScrollHeight = this.inner_height;
        }

        /// <summary>
        /// Surcharge de la fonction héritée pour l'empêcher d'exécuter le code parent
        /// </summary>
        protected override void RefreshDiplaying() {}


        /*********************************
         ****** METHODES DE GESTION ******
         *********************************/


        public override void AddChild(Widget widget)
        {
            base.AddChild(widget);
            foreach (Widget w in childs)
            {
                w.Active = true;
            }
        }

    }

}
