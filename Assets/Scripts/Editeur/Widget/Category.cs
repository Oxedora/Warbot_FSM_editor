using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Sélecteur sous forme de catégories
    /// </summary>
    public class Category : Widget
    {


        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/


        /// <summary>
        /// Hauteur d'un élément
        /// </summary>
        public static readonly int DIM_ELEMENT_HEIGHT = 22;

        /// <summary>
        /// Marge du titre de la catégorie
        /// </summary>
        private static readonly int DIM_MARGIN = 20;

        /// <summary>
        /// Marge d'un élément
        /// </summary>
        private static readonly int DIM_ELEMENT_MARGIN = 30;

        /// <summary>
        /// Hauteur du texte
        /// </summary>
        private static readonly int DIM_TEXT_SIZE = 14;

        /// <summary>
        /// Dimension du triangle
        /// </summary>
        private static readonly float DIM_TRIANGLE_SIZE = 0.5f;

        /// <summary>
        /// Style du titre de la catégorie
        /// </summary>
        private static readonly FontStyle LABEL_FONTSTYLE = FontStyle.Bold;


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        /// <summary>
        /// Titre de la catégorie
        /// </summary>
        protected Label label;

        /// <summary>
        /// Sélection actuelle
        /// </summary>
        protected int selection = -1;

        /// <summary>
        /// Conteneur des éléments
        /// </summary>
        protected Container container;


        /************************************
         ****** EVENEMENTS SPECIFIQUES ******
         ************************************/


        /// <summary>
        /// Appelé lors de la sélection du item
        /// </summary>
        public event Widget.EventDelegate SelectItem = null;

        /// <summary>
        /// Appelé lors du déploiement ou du repli du sélecteur
        /// </summary>
        public event Widget.EventDelegate DeployOrTuck = null;


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Liste des éléments du sélecteur
        /// </summary>
        public string[] Elements
        {
            get
            {
                int size = this.container.Childs.Length;
                string[] res = new string[size];
                for (int i = 0; i < size; i++)
                {
                    res[i] = ((Label)this.container.Childs[i]).Text;
                }
                return res;
            }
            set
            {
                this.container.RemoveAllChilds();
                for (int i = 0; i < value.Length; i++)
                {
                    Label label = new Label(new Rect(0, i * DIM_ELEMENT_HEIGHT, this.area.width - Scrollbar.DIM_WIDTH, DIM_ELEMENT_HEIGHT), value[i]);
                    label.TextAlign = TextAnchor.MiddleLeft;
                    label.Margin = DIM_ELEMENT_MARGIN;
                    label.Clic += OnSelect;
                    this.container.AddChild(label);
                }
                if (value.Length == 0) this.container.LocalArea = new Rect(0, this.area.height, this.area.width, DIM_ELEMENT_HEIGHT);
                else this.container.LocalArea = new Rect(0, this.area.height, this.area.width, value.Length * DIM_ELEMENT_HEIGHT + DIM_TEXT_SIZE);

                if (value.Length == 0) this.selection = -1;
                else this.selection = 0;
            }
        }

        /// <summary>
        /// Sélection actuelle
        /// </summary>
        public string Selection
        {
            get
            {
                if (this.selection == -1)
                    return "";
                else
                    return this.Elements[this.selection];
            }
            set
            {
                int size = this.childs[0].Childs.Length;
                for (int i = 0; i < size; i++)
                {
                    if (((Label)this.container.Childs[i]).Text == value)
                    {
                        this.selection = i;
                        return;
                    }
                }
                this.selection = -1;
            }
        }


        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base d'une catégorie
        /// </summary>
        /// <param name="name">nom à afficher</param>
        /// <param name="pos">coordonnées locales</param>
        /// <param name="width">largeur</param>
        public Category(string name, Vector2 pos, int width)
        {
            this.area = new Rect(pos.x, pos.y, width, DIM_ELEMENT_HEIGHT);
            this.container = new Container(new Rect(0, this.area.height, this.area.width, 0));
            this.AddChild(this.container);
            this.label = new Label(new Rect(0, 0, this.area.width, this.area.height), name);
            this.AddChild(this.label);

            this.container.Parent = this;
            this.container.Active = false;

            this.label.Parent = this;
            this.label.Margin = DIM_MARGIN;
            this.label.TextAlign = TextAnchor.MiddleLeft;
            this.label.TextSize = DIM_TEXT_SIZE;
            this.label.TextStyle = LABEL_FONTSTYLE;
            this.label.Clic += OnLabelClic;
        }

        /// <summary>
        /// Appelée lorsque l'on clique sur le label principal
        /// </summary>
        /// <param name="widget">widget</param>
        /// <param name="button">bouton de la souris</param>
        /// <param name="x">coordonées en x</param>
        /// <param name="y">coordonées en y</param>
        protected void OnLabelClic(Widget widget, int button, int x, int y)
        {
            this.Deploy(!this.container.Active);
        }

        /// <summary>
        /// Appelée lors du clic sur un élément du sélecteur
        /// </summary>
        /// <param name="widget">widget</param>
        /// <param name="button">bouton de la souris</param>
        /// <param name="x">coordonées en x</param>
        /// <param name="y">coordonées en y</param>
        protected void OnSelect(Widget widget, int button, int x, int y)
        {
            this.Selection = ((Label)widget).Text;
            if (this.SelectItem != null)
                SelectItem(this, this.Selection);
        }

        /// <summary>
        /// Déploie le sélecteur
        /// </summary>
        public void Deploy()
        {
            if (this.container.Active == true) return;
            this.container.Active = true;
            if (this.DeployOrTuck != null)
                this.DeployOrTuck(this, true);
            this.LocalArea = new Rect(this.area.x, this.area.y, this.area.width, this.area.height + this.container.LocalArea.height);
        }

        /// <summary>
        /// Déploie ou replie le sélecteur
        /// </summary>
        /// <param name="state">état du sélecteur</param>
        public void Deploy(bool state)
        {
            if (this.container.Active == state) return;
            if (state)
                this.Deploy();
            else
                this.Tuck();
        }

        /// <summary>
        /// Repli le sélecteur
        /// </summary>
        public void Tuck()
        {
            if (this.container.Active == false) return;
            this.container.Active = false;
            if (this.DeployOrTuck != null)
                this.DeployOrTuck(this, false);
            this.LocalArea = new Rect(this.area.x, this.area.y, this.area.width, this.label.LocalArea.height);
        }

        public override void OnDrawWithGL()
        {
            base.OnDrawWithGL();

            Rect rect = this.label.GlobalArea;
            GL.Begin(GL.TRIANGLES);
            GL.Color(Color.black);
            if (this.container.Active)
            {
                //Flèche "bas"
                rect.width = DIM_TRIANGLE_SIZE * this.label.LocalArea.height;
                rect.height = 0.7f * rect.width;
                rect.x += (this.label.LocalArea.height - rect.width) / 2;
                rect.y += (this.label.LocalArea.height - rect.height) / 2;
                GL.Vertex3(rect.xMin, rect.yMin, 0);
                GL.Vertex3((rect.xMin + rect.xMax) / 2, rect.yMax, 0);
                GL.Vertex3(rect.xMax, rect.yMin, 0);
            }
            else
            {
                //Flèche "droite"
                rect.height = DIM_TRIANGLE_SIZE * this.label.LocalArea.height;
                rect.width = 0.7f * rect.height;
                rect.x += (this.label.LocalArea.height - rect.width) / 2;
                rect.y += (this.label.LocalArea.height - rect.height) / 2;
                GL.Vertex3(rect.xMin, rect.yMin, 0);
                GL.Vertex3(rect.xMax, (rect.yMin + rect.yMax) / 2, 0);
                GL.Vertex3(rect.xMin, rect.yMax, 0);
            }
            GL.End();
        }

    }

}
