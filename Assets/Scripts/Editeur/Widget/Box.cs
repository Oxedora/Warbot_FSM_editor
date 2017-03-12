using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Widget {

    string text;
    bool entree;

    Box other;

	public Box(Rect zone, string text, bool entree, Box other)
    {
        this.area = zone;
        this.text = text;
        this.entree = entree;
        this.other = other;
    }

    bool sortie_selected = false;
    bool connected = false;
    Vector2 cursor;

    public override void OnDraw()
    {
        float left = (float)this.area.x / (float)Screen.width, right = (float)this.area.xMax / (float)Screen.width, bottom = (float)(Screen.height - this.area.yMin) / (float)Screen.height, top = (float)(Screen.height - this.area.yMax) / (float)Screen.height;
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.Vertex3(left, top, 0);
        GL.Vertex3(right, top, 0);
        GL.Vertex3(right, bottom, 0);
        GL.Vertex3(left, bottom, 0);

        if (sortie_selected)
            GL.Color(Color.red);
        else
            GL.Color(Color.black);
        if (entree)
        {
            left = (float)(this.area.x + 10) / (float)Screen.width;
            right = (float)(this.area.x + 20) / (float)Screen.width;
        }
        else
        {
            left = right + (float)(-10) / (float)Screen.width;
            right = right + (float)(-20) / (float)Screen.width;
        }
        bottom = (bottom + top) / 2 - (10 / (float)Screen.height) / 2;
        top = bottom + (float)(10) / (float)Screen.height;
        GL.Vertex3(left, top, 0);
        GL.Vertex3(right, top, 0);
        GL.Vertex3(right, bottom, 0);
        GL.Vertex3(left, bottom, 0);
        GL.End();
        if (connected)
        {
            left -= (float)5 / (float)Screen.width;
            top -= (float)5 / (float)Screen.height;
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex3(left, top, 0);
            left = (float)(other.area.x + 10) / (float)Screen.width;
            bottom = (float)(Screen.height - (other.area.yMin + other.area.yMax) / 2) / (float)Screen.height;
            GL.Vertex3(left, bottom, 0);
            GL.End();
        }
        else if (sortie_selected)
        {
            left -= (float)5 / (float)Screen.width;
            top -= (float)5 / (float)Screen.height;
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex3(left, top, 0);
            GL.Vertex3((float)cursor.x / (float)Screen.width, (float)(Screen.height - cursor.y) / (float)Screen.height, 0);
            GL.End();
        }
    }

    public override void OnMouseEvent(int button, bool pressed, int x, int y)
    {
        if (entree) return;
        Rect entree_area = new Rect(other.area.x + 10, (other.area.y + other.area.yMax) / 2 - 10 / 2, 10, 10),
            sortie_area = new Rect(this.area.xMax - 20, (this.area.y + this.area.yMax) / 2 - 10 / 2, 10, 10);
        MainLayout.print(sortie_area);
        cursor = new Vector2(x, y);
        if (button == 0)
        {
            if (pressed)
            {
                if (sortie_area.Contains(new Vector2(x, y)))
                {
                    sortie_selected = true;
                    connected = false;
                }
            }
            else
            {
                if (sortie_selected && entree_area.Contains(new Vector2(x, y)))
                    connected = true;
                sortie_selected = false;
            }
        }
    }

    public override void OnMotionEvent(int x, int y)
    {
        cursor = new Vector2(x, y);
    }

}
