using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class SelectorPanel : Widget
    {

        private static readonly float dim_scale_width = 0.25f;

        public SelectorPanel()
        {
            this.area = new Rect(0, 0, Screen.width * dim_scale_width, Screen.height);
            MainLayout.Actual.AddWidget(new SelectorTeam(this));
        }

        public override void OnDrawWithGL()
        {
            float right = dim_scale_width, bottom = 0, top = 1;
            GL.Begin(GL.LINES);
            GL.Color(Color.white);
            GL.Vertex3(right, top, 0);
            GL.Vertex3(right, bottom, 0);
            GL.End();
        }

    }

}
