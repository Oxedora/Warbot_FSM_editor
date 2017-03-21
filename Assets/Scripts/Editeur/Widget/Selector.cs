using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;

namespace WarBotEngine.Editeur
{
	/// <summary>
	/// Drop-down
	/// </summary>
	public class Selector : Widget
    {

        private static readonly int DIM_PADDING = 2;
        private static readonly int DIM_ELEMENT_HEIGHT = 22;
        private static readonly int DIM_ELEMENT_MARGIN = 15;
        private static readonly float DIM_TRIANGLE_SIZE = 0.6f;

        private static readonly Color COLOR_1 = new Color((float)0x45 / 255, (float)0x3c / 255, (float)0x38 / 255); //453c38
        private static readonly Color COLOR_2 = new Color((float)0x6a / 255, (float)0x5c / 255, (float)0x55 / 255); //6a5c55
        private static readonly Color COLOR_3 = new Color((float)0xf6 / 255, (float)0x6e / 255, (float)0x00 / 255); //f66e00
        private static readonly Color COLOR_CONTAINER_COLOR = new Color((float)0xeb / 255, (float)0xe9 / 255, (float)0xf6 / 255); //ebe9f6

        private int selection = -1;

        private Container container;

        private int max_height;

        private Label main_label;

        public string[] Elements
        {
            get
            {
                int size = this.childs[0].Childs.Length;
                string[] res = new string[size];
                for (int i = 0; i < size; i++)
                {
                    res[i] = ((Label)this.childs[0].Childs[i]).Text;
                }
                return res;
            }
            set
            {
                this.childs[0].RemoveAllChilds();
                for (int i = 0; i < value.Length; i++)
                {
                    Label label = new Label(new Rect(0, i * DIM_ELEMENT_HEIGHT, this.area.width - Scrollbar.DIM_WIDTH, DIM_ELEMENT_HEIGHT), value[i]);
                    label.TextAlign = TextAnchor.MiddleLeft;
                    label.Clic += OnSelect;
                    label.Margin = DIM_ELEMENT_MARGIN;
                    this.childs[0].AddChild(label);
                }
                if (value.Length == 0) this.container.LocalArea = new Rect(0, this.area.height, this.area.width, DIM_ELEMENT_HEIGHT);
                else if (value.Length * DIM_ELEMENT_HEIGHT > this.max_height) this.container.LocalArea = new Rect(0, this.area.height, this.area.width, this.max_height);
                else this.container.LocalArea = new Rect(0, this.area.height, this.area.width, value.Length * DIM_ELEMENT_HEIGHT);

                if (value.Length == 0) this.selection = -1;
                else this.selection = 0;
                this.main_label.Text = this.Selection;
            }
        }

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
                    if (((Label)this.childs[0].Childs[i]).Text == value)
                    {
                        this.selection = i;
                        this.main_label.Text = this.Selection;
                        return;
                    }
                }
                this.selection = -1;
                this.main_label.Text = "";
            }
        }

		public Selector(Rect area, int height)
        {
            this.area = area;
            this.max_height = height;
            this.container = new Container(new Rect(0, this.area.height, this.area.width, DIM_ELEMENT_HEIGHT));
            this.AddChild(container);

            this.container.Parent = this;
            this.container.AllowScrollbar = true;
            this.container.Background = COLOR_CONTAINER_COLOR;

            this.main_label = new Label(new Rect(DIM_PADDING, DIM_PADDING, this.area.width - this.area.height - DIM_PADDING, this.area.height - 2 * DIM_PADDING), "");
            this.main_label.Parent = this;
            this.main_label.Color = COLOR_3;
            this.main_label.Background = COLOR_2;
            this.main_label.Margin = DIM_ELEMENT_MARGIN;
            this.main_label.TextStyle = FontStyle.Bold;
            this.main_label.TextAlign = TextAnchor.MiddleLeft;

            this.container.Active = false;
        }

        public void OnSelect(Widget widget, int button, int x, int y)
        {
            this.Selection = ((Label)widget).Text;
            this.container.Active = false;
        }

        public override void OnDrawWithGL()
        {
            base.OnDrawWithGL();
            GL.Begin(GL.QUADS);
            GL.Color(COLOR_1);
            Rect rect = this.GlobalArea;
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);
            GL.End();
            this.main_label.OnDrawWithGL();

            rect.x += rect.width - rect.height;
            rect.width = DIM_TRIANGLE_SIZE * rect.height;
            rect.height = 0.7f * rect.width;
            rect.x += (this.area.height - rect.width) / 2;
            rect.y += (this.area.height - rect.height) / 2;
            GL.Begin(GL.TRIANGLES);
            GL.Color(COLOR_2);
            if (this.container.Active)
            {
                //Flèche "bas"
                GL.Vertex3(rect.xMin, rect.yMin, 0);
                GL.Vertex3((rect.xMin + rect.xMax) / 2, rect.yMax, 0);
                GL.Vertex3(rect.xMax, rect.yMin, 0);
            }
            else
            {
                //Flèche "haut"
                GL.Vertex3(rect.xMin, rect.yMax, 0);
                GL.Vertex3((rect.xMin + rect.xMax) / 2, rect.yMin, 0);
                GL.Vertex3(rect.xMax, rect.yMax, 0);
            }
            GL.End();
        }

        public override void OnDrawWithoutGL()
        {
            base.OnDrawWithoutGL();
            this.main_label.OnDrawWithoutGL();
        }

        public override void OnMouseEvent(int button, bool pressed, int x, int y)
        {
            base.OnMouseEvent(button, pressed, x, y);
            if (pressed && this.GlobalArea.Contains(new Vector2(x, y)))
                this.childs[0].Active = !this.childs[0].Active;
        }

    }
}