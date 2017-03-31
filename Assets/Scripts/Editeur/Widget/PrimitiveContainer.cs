using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class PrimitiveContainer : Primitive
    {

        protected static readonly int DIM_MINIMUM_SPACE = 20;

        protected bool inner_container;

        protected Container container;

        public Primitive First { get { return (Primitive)this.container.Childs[0]; } }

        public int InnerHeight { get { return (this.container.Active) ? this.First.TotalHeight + DIM_MINIMUM_SPACE : 0; } }

        public PrimitiveContainer(Vector2 pos, string name, Primitive parent, bool inner)
        {
            this.inner_container = inner;
            this.area = new Rect(pos.x, pos.y, Primitive.DIM_WIDTH, Primitive.DIM_TITLE_HEIGHT);
            this.container = new Container(new Rect(0, this.area.height, this.area.width, DIM_TITLE_HEIGHT + DIM_MINIMUM_SPACE));

            this.AddChild(this.container);
            Primitive begin = new Primitive(new Vector2(0, 0), Primitive.NAME_PRIMITIVE_BEGIN);
            this.container.AddChild(begin);
            begin.ExtendHeight += OnInnerExtend;

            this.title = new Label(new Rect(0, 0, this.area.width, this.area.height), name);
            this.title.Color = Color.red;
            this.AddChild(this.title);
        }

        public override void PushPrimitive(Primitive primitive, Vector2 cursor)
        {
            this.First.PushPrimitive(primitive, cursor);
        }

        protected void OnInnerExtend(Widget w, object args)
        {
            this.container.LocalArea = new Rect(0, this.area.height, this.area.width, this.First.TotalHeight + DIM_MINIMUM_SPACE);
            if (this.container.Active)
                this.ExtendPrimitive((int)this.container.LocalArea.height);
            else
                this.ExtendPrimitive(0);
        }

        public override void OnMouseEvent(int button, bool pressed, int x, int y)
        {
            if (!this.Active) return;
            base.OnMouseEvent(button, pressed, x, y);
            this.is_clicked = false;
            this.Next = null;
        }

    }

}
