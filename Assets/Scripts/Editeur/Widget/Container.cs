using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class Container : Widget
    {


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        /// <summary>
        /// Couleur de fond
        /// </summary>
        protected Color background_color = Color.clear;

        /// <summary>
        /// Scrollbar
        /// </summary>
        protected Scrollbar scrollbar;

        /// <summary>
        /// MotionScroll
        /// </summary>
        protected MotionScroll motionscroll;

        /// <summary>
        /// Largeur interne du conteneur
        /// </summary>
        protected int inner_width = 0;

        /// <summary>
        /// Hauteur interne du conteneur
        /// </summary>
        protected int inner_height = 0;

        /// <summary>
        /// Positions initiales des widget
        /// </summary>
        List<Vector2> positions = new List<Vector2>();


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Couleur de fond
        /// </summary>
        public Color Background { get { return background_color; } set { background_color = value; } }

        /// <summary>
        /// Activer la scrollbar
        /// </summary>
        public bool AllowScrollbar { get { return scrollbar.Active; } set { scrollbar.Active = value; } }

        /// <summary>
        /// Acriver le motion-scroll
        /// </summary>
        public bool AllowMotionScroll { get { return motionscroll.Active; } set { motionscroll.Active = value; } }


        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base du conteneur
        /// </summary>
        /// <param name="area">zone du conteneur</param>
        public Container(Rect area)
        {
            this.area = area;
            this.scrollbar = new Scrollbar(new Rect(area.width - Scrollbar.DIM_WIDTH, 0, Scrollbar.DIM_WIDTH, area.height), 0, OnScrollingEvent, this);
            this.scrollbar.Active = false;
            this.motionscroll = new MotionScroll(0, OnScrollingEvent, this);
            this.motionscroll.Active = false;
            this.Resize += OnResize;
        }

        /// <summary>
        /// Appelée lors d'un évènement de scrolling
        /// </summary>
        /// <param name="widget">widget de scroll</param>
        /// <param name="args">niveau de scrolling</param>
        protected void OnScrollingEvent(Widget widget, object args)
        {
            for (int i = 0; i < this.childs.Count; i++)
                this.childs[i].LocalPosition = this.positions[i] - new Vector2(this.motionscroll.CurrentValue, this.scrollbar.CurrentValue);
            RefreshDiplaying();
        }

        /// <summary>
        /// Appelée lors d'un redimensionnement
        /// </summary>
        /// <param name="widget">conteneur</param>
        /// <param name="width">largeur</param>
        /// <param name="height">hauteur</param>
        protected void OnResize(Widget widget, int width, int height)
        {
            this.scrollbar.LocalArea = new Rect(area.width - Scrollbar.DIM_WIDTH, 0, Scrollbar.DIM_WIDTH, area.height);
            RefreshDiplaying();
        }

        /// <summary>
        /// Rafraichit l'affichage des éléments du container
        /// </summary>
        protected void RefreshDiplaying()
        {
            foreach (Widget widget in this.childs)
            {
                if (widget.LocalArea.x < 0 || widget.LocalArea.y < 0 || widget.LocalArea.xMax > this.area.width || widget.LocalArea.yMax > this.area.height)
                    widget.Active = false;
                else
                    widget.Active = true;
            }
        }


        /***********************************
         ****** METHODES D'EVENEMENTS ******
         ***********************************/


        public override void OnDrawWithGL()
        {
            if (!this.Active) return;
            if (this.background_color != Color.clear)
            {
                GL.Begin(GL.QUADS);
                GL.Color(this.background_color);
                Rect rect = this.GlobalArea;
                GL.Vertex3(rect.xMin, rect.yMin, 0);
                GL.Vertex3(rect.xMax, rect.yMin, 0);
                GL.Vertex3(rect.xMax, rect.yMax, 0);
                GL.Vertex3(rect.xMin, rect.yMax, 0);
                GL.End();
            }
            base.OnDrawWithGL();
            this.motionscroll.OnDrawWithGL();
            this.scrollbar.OnDrawWithGL();
        }

        public override void OnMouseEvent(int button, bool pressed, int x, int y)
        {
            if (!this.Active) return;
            base.OnMouseEvent(button, pressed, x, y);
            this.scrollbar.OnMouseEvent(button, pressed, x, y);
        }
        
        public override void OnMotionEvent(int x, int y)
        {
            if (!this.Active) return;
            base.OnMotionEvent(x, y);
            this.scrollbar.OnMotionEvent(x, y);
            this.motionscroll.OnMotionEvent(x, y);
        }

        public override void OnScrollEvent(int delta)
        {
            if (!this.Active) return;
            base.OnScrollEvent(delta);
            this.scrollbar.OnScrollEvent(delta);
            this.motionscroll.OnScrollEvent(delta);
        }


        /*********************************
         ****** METHODES DE GESTION ******
         *********************************/


        public override void AddChild(Widget widget)
        {
            base.AddChild(widget);
            if (widget.LocalArea.xMax > this.inner_width)
            {
                this.inner_width = (int)widget.LocalArea.xMax + 1;
                this.motionscroll.ScrollWidth = this.inner_width;
            }
            if (widget.LocalArea.yMax > this.inner_height)
            {
                this.inner_height = (int)widget.LocalArea.yMax + 1;
                this.scrollbar.ScrollHeight = this.inner_height;
            }
            this.positions.Add(widget.LocalPosition);
            widget.LocalPosition -= new Vector2(this.motionscroll.CurrentValue, this.scrollbar.CurrentValue);

            if (widget.LocalArea.x < 0 || widget.LocalArea.y < 0 || widget.LocalArea.xMax > this.area.width || widget.LocalArea.yMax > this.area.height)
                widget.Active = false;
            else
                widget.Active = true;
        }

        public override void RemoveChild(Widget widget)
        {
            if (this.childs.Contains(widget))
                this.positions.RemoveAt(this.childs.IndexOf(widget));
            base.RemoveChild(widget);
            int x_max = 0, y_max = 0;
            foreach (Widget child in this.childs)
            {
                if (child.LocalArea.width > x_max) x_max = (int)child.LocalArea.xMax + 1;
                if (child.LocalArea.width > y_max) y_max = (int)child.LocalArea.yMax + 1;
            }
            this.inner_width = x_max;
            this.motionscroll.ScrollWidth = this.inner_width;
            this.inner_width = y_max;
            this.scrollbar.ScrollHeight = this.inner_height;
        }

        public override void RemoveAllChilds()
        {
            base.RemoveAllChilds();
            this.inner_width = 0;
            this.inner_height = 0;
            this.motionscroll.ScrollWidth = 0;
            this.scrollbar.ScrollHeight = 0;
            this.positions.Clear();
        }

    }

}
