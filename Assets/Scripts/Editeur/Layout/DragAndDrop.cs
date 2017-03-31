using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class DragAndDrop : Widget
    {

        private static readonly Vector2 PRIMITIVE_CURSOR_DEC = new Vector2(Primitive.DIM_WIDTH / 2, Primitive.DIM_TITLE_HEIGHT / 2);

        private PrimitivesCollection primitives;
        private BehaviorEditor editor;

        private Vector2 cursor = new Vector2(), saved_position, saved_cursor;

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

        private void OnSelectItem(Widget widget, object args)
        {
            this.AddChild(new Primitive(cursor - PRIMITIVE_CURSOR_DEC, (string)args));
            this.saved_position = this.childs[0].GlobalPosition;
            this.saved_cursor = cursor;
        }

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
