using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class Container : Widget
    {

        protected Color background_color = Color.clear;

        protected Scrollbar scrollbar;

        protected MotionScroll motionscroll;

        public Color Background { get { return background_color; } set { background_color = value; } }

        public bool AllowScrollbar { get { return scrollbar.Active; } set { scrollbar.Active = value; } }

        public bool AllowMotionScroll { get { return motionscroll.Active; } set { motionscroll.Active = value; } }

        public Container(Rect area)
        {
            this.area = area;
            this.scrollbar = new Scrollbar(new Rect(area.width - Scrollbar.DIM_WIDTH, 0, Scrollbar.DIM_WIDTH, area.height), area.height, OnScrollingEvent, this);
            this.scrollbar.Active = false;
            this.motionscroll = new MotionScroll(area.width, OnScrollingEvent, this);
            this.motionscroll.Active = false;
        }

        protected void OnScrollingEvent(Widget widget, object args)
        {
            
        }

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

    }

}
