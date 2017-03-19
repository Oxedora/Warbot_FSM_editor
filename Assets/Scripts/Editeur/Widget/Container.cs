using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class Container : Widget
    {

        protected Scrollbar scrollbar;

        protected MotionScroll motionscroll;

        public bool AllowScrollbar { get { return scrollbar.Active; } set { scrollbar.Active = value; } }

        public bool AllowMotionScroll { get { return motionscroll.Active; } set { motionscroll.Active = value; } }

        public Container(Rect area)
        {
            this.area = area;
            this.scrollbar = new Scrollbar(new Rect(area.width - Scrollbar.DIM_WIDTH, 0, Scrollbar.DIM_WIDTH, area.height), area.height * 3, OnScrollbarEvent, this);
            this.scrollbar.Active = false;
            this.motionscroll = new MotionScroll(area.width * 3, OnMotionScroll, this);
            this.motionscroll.Active = false;
        }

        protected void OnScrollbarEvent(object args)
        {
            
        }

        protected void OnMotionScroll(object args)
        {
            
        }

        public override void OnDrawWithGL()
        {
            if (!this.Active) return;
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
