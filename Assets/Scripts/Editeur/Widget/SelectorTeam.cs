using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorTeam : Widget {

    private static readonly int dim_margin_top = 20;
    private static readonly int dim_padding = 5;

    private static readonly float dim_scale_width = 0.8f;
    private static readonly int dim_pixel_height = 30;

    private static readonly int dim_triangle_size = 20;

    public SelectorTeam(SelectorPanel parent)
    {
        this.parent = parent;
        float width = parent.area.width * dim_scale_width;
        this.area = new Rect((parent.area.width - width) / 2, dim_margin_top, width, dim_pixel_height);
    }

    public override void OnDraw()
    {
        float left = (float)this.area.x / (float)Screen.width, right = (float)this.area.xMax / (float)Screen.width, bottom = (float)(Screen.height - this.area.yMin) / (float)Screen.height, top = (float)(Screen.height - this.area.yMax) / (float)Screen.height;
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.Vertex3(left, top, 0);
        GL.Vertex3(right, top, 0);
        GL.Vertex3(right, bottom, 0);
        GL.Vertex3(left, bottom, 0);
        GL.End();
        left = (float)(this.area.xMax - dim_padding - dim_triangle_size) / (float)Screen.width;
        right = (float)(this.area.xMax - dim_padding) / (float)Screen.width;
        bottom = (float)(Screen.height - this.area.yMax + dim_padding) / (float)Screen.height;
        top = (float)(Screen.height - this.area.yMax + dim_padding + dim_triangle_size) / (float)Screen.height;
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.black);
        GL.Vertex3(left, top, 0);
        GL.Vertex3((left + right) / 2, bottom, 0);
        GL.Vertex3(right, top, 0);
        GL.End();
    }

    public override void OnText()
    {
        Rect rect = new Rect(this.area.x + dim_padding, this.area.y + dim_padding, this.area.width, this.area.height - dim_padding * 2);
        GUI.color = Color.black;
        GUI.Label(rect, "EQUIPE");
    }

}
