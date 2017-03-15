using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Class abstraite dont les widgets héritent
    /// </summary>
    public abstract class Widget
    {


        /***********************
         ****** DELEGATES ******
         ***********************/


        /// <summary>
        /// Delegate permettant de gérer les évènements des widgets
        /// </summary>
        /// <param name="args">paramètre quelconque de l'évènement</param>
        public delegate void EventDelegate(object args);


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        protected Widget parent = null;

        protected Rect area = new Rect(new Vector2(0, 0), new Vector2(0, 0));

        protected List<Widget> childs = new List<Widget>();

        protected bool active = true;


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Parent du widget s'il en a un et null sinon
        /// </summary>
        public Widget Parent { get { return parent; } set { parent = value; } }

        /// <summary>
        /// Coordonnées et dimensions du widget
        /// </summary>
        public Rect Area { get { return area; } }

        /// <summary>
        /// Widgets contenus dans le widget actuel
        /// </summary>
        public Widget[] Childs { get { return childs.ToArray(); } }

        /// <summary>
        /// Détermine si le widget est actif ou non
        /// </summary>
        public bool Active { get { return active; } set { active = value; } }


        /***********************************
         ****** METHODES D'EVENEMENTS ******
         ***********************************/


        /// <summary>
        /// Appelée lors de la suppression du widget
        /// </summary>
        public virtual void OnDestroy()
        {
            foreach (Widget w in childs)
            {
                w.OnDestroy();
            }
        }

        /// <summary>
        /// Appelée lors du rendu de la scene avec les fonctions OpenGL
        /// </summary>
        public virtual void OnDrawWithGL() { }

        /// <summary>
        /// Appelée après le rendu de la scene avec les fonctions OpenGL
        /// </summary>
        public virtual void OnDrawWithoutGL() { }

        /// <summary>
        /// Appelée lors de la mise à jour des composants
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// Appelée lors d'un évènement clavier
        /// </summary>
        /// <param name="keycode">touche associée à l'évènement</param>
        /// <param name="state">indique si la touche est appuyée ou non</param>
        public virtual void OnKeyEvent(KeyCode keycode, bool state) { }

        /// <summary>
        /// Appelée lors d'un évènement souris de bouton
        /// </summary>
        /// <param name="button">bouton associé à l'évènement</param>
        /// <param name="pressed">indique si le bouton est appuyé ou non</param>
        /// <param name="x">coordonnée en x de la souris</param>
        /// <param name="y">coordonnée en y de la souris</param>
        public virtual void OnMouseEvent(int button, bool pressed, int x, int y) { }

        /// <summary>
        /// Appelée lors d'un évènement souris de mouvement
        /// </summary>
        /// <param name="x">coordonnée en x de la souris</param>
        /// <param name="y">coordonnée en y de la souris</param>
        public virtual void OnMotionEvent(int x, int y) { }

        /// <summary>
        /// Appelée lors d'un évènement souris de scrolling
        /// </summary>
        /// <param name="delta">valeur de scrolling</param>
        public virtual void OnScrollEvent(int delta) { }


        /*********************************
         ****** METHODES DE GESTION ******
         *********************************/


        /// <summary>
        /// Ajoute un widget enfant au widget actuel
        /// </summary>
        /// <param name="widget">widget à ajouter</param>
        public virtual void AddChild(Widget widget)
        {
            if (!childs.Contains(widget))
            {
                widget.Parent = this;
                childs.Add(widget);
            }
        }

        /// <summary>
        /// Supprime un widget enfant du widget actuel
        /// </summary>
        /// <param name="widget">widget à supprimer</param>
        public virtual void RemoveChild(Widget widget)
        {
            if (childs.Contains(widget))
            {
                widget.Parent = null;
                childs.Remove(widget);
            }
        }

        /// <summary>
        /// Supprime tous les widgets enfants du widget actuel
        /// </summary>
        public virtual void RemoveAllChilds()
        {
            childs.Clear();
        }

    }

}
