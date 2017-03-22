using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class Primitive : Widget
    {

        public static readonly int DIM_WIDTH = 150;
        private static readonly int DIM_TRIANGLE_SIZE = 20;
        public static readonly int DIM_TITLE_HEIGHT = 20;

        public static readonly string NAME_PRIMITIVE_BEGIN = "DEBUT";

        private static readonly Color COLOR_1 = new Color((float)0xcc / 255, (float)0xcc / 255, (float)0xff / 255);
        private static readonly Color COLOR_2 = new Color((float)0x00 / 255, (float)0x00 / 255, (float)0x00 / 255);

        protected Label title;

        protected List<Primitive> inner = new List<Primitive>();

        protected List<Primitive> outer = new List<Primitive>();

        protected Primitive next = null;

        protected Vector2 cursor = new Vector2(), saved_cursor, saved_position;

        protected bool is_clicked = false;

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
                }
            }
        }

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

        public Primitive(Vector2 position, string name)
        {
            if (name == NAME_PRIMITIVE_BEGIN)
                this.LocalArea = new Rect(position.x, position.y, DIM_WIDTH, DIM_TITLE_HEIGHT);
            else
                this.LocalArea = new Rect(position.x, position.y, DIM_WIDTH, 100);

            this.title = new Label(new Rect(0, 0, this.area.width, DIM_TITLE_HEIGHT), name);
            this.title.Color = COLOR_2;
            this.AddChild(this.title);
        }

        public void PushPrimitive(Primitive primitive, Vector2 cursor)
        {
            if (this.GlobalArea.Contains(cursor))
            {
                this.Next = primitive;
            }
            else
            {
                foreach (Widget w in this.childs)
                {
                    if (typeof(Primitive).Equals(w.GetType()))
                        ((Primitive)w).PushPrimitive(primitive, cursor);
                }
            }
        }

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
            if (pressed && this.title.Text != NAME_PRIMITIVE_BEGIN)
            {
                if (this.GlobalArea.Contains(new Vector2(x, y)))
                {
                    this.is_clicked = true;
                    this.saved_cursor = new Vector2(x, y);
                    this.saved_position = this.LocalPosition;
                }
            }
            else
            {
                if (this.is_clicked)
                {
                    ((Primitive)this.Parent).Next = null;
                    this.Parent = null;
                    BehaviorEditor.Actual.First.PushPrimitive(this, this.cursor);
                }
                this.is_clicked = false;
            }
        }

        public override void OnMotionEvent(int x, int y)
        {
            base.OnMotionEvent(x, y);
            this.cursor = new Vector2(x, y);
            if (this.is_clicked)
            {
                this.LocalPosition = this.saved_position + (this.cursor - this.saved_cursor);
            }
        }

    }

}