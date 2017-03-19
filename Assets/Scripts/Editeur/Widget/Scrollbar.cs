using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Scollbar verticale
    /// </summary>
    public class Scrollbar : Widget
    {

        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/


        private static readonly Color COLOR_1 = new Color((float)0x34 / 255, (float)0x95 / 255, (float)0x5e / 255);
        private static readonly Color COLOR_2 = new Color((float)0x2c / 255, (float)0x3e / 255, (float)0x50 / 255);
        private static readonly Color COLOR_3 = new Color((float)0xbd / 255, (float)0xc3 / 255, (float)0xc7 / 255);
        private static readonly Color COLOR_4 = new Color((float)0xec / 255, (float)0xf0 / 255, (float)0xf1 / 255);


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        private float scroll_height;

        private float actual_value = 0;

        private event EventDelegate on_changevalue;

        //ATTRIBUTS D'EVENEMENTS

        private bool is_hover = false, is_clicked = false;

        private Vector2 clic_position = new Vector2();


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Hauteur totale du conteneur
        /// </summary>
        public float ScrollHeight
        {
            get
            {
                return scroll_height;
            }
            set
            {
                if (value < 0)
                    scroll_height = 0;
                else
                    scroll_height = value;
                if (scroll_height - parent.Area.height < actual_value)
                    actual_value = scroll_height - parent.Area.height;
                if (actual_value < 0)
                    actual_value = 0;
            }
        }

        /// <summary>
        /// Position verticale actuelle
        /// </summary>
        public float CurrentValue
        {
            get
            {
                return actual_value;
            }
            set
            {
                actual_value = value;
                if (scroll_height - parent.Area.height < actual_value)
                    actual_value = scroll_height - parent.Area.height;
                if (actual_value < 0)
                    actual_value = 0;
            }
        }


        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base de la scrollbar
        /// </summary>
        /// <param name="area">coordonnées et dimensions</param>
        /// <param name="scroll_height">hauteur totale du conteneur</param>
        /// <param name="on_changevalue">évènement provoqué d'un changement de scrolling</param>
        public Scrollbar(Rect area, float scroll_height, EventDelegate on_changevalue)
        {
            this.area = area;
            this.scroll_height = scroll_height;
            this.on_changevalue = on_changevalue;
        }


        /***********************************
         ****** METHODES D'EVENEMENTS ******
         ***********************************/


        public override void OnDrawWithGL()
        {
            GL.Begin(GL.QUADS);

            //Contour
            if (this.is_hover)
                GL.Color(Scrollbar.COLOR_1);
            else
                GL.Color(Scrollbar.COLOR_2);
            MainLayout.print(Scrollbar.COLOR_1);
            GL.Vertex3(this.area.xMin, this.area.yMin, 0);
            GL.Vertex3(this.area.xMax, this.area.yMin, 0);
            GL.Vertex3(this.area.xMax, this.area.yMax, 0);
            GL.Vertex3(this.area.xMin, this.area.yMax, 0);

            //Bouton "haut"
            Rect rect = new Rect(this.area.xMin + 1, this.area.yMin + 1, this.area.xMax - 1, this.area.yMax - 1);
            GL.Vertex3(rect.xMin, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMin, 0);
            GL.Vertex3(rect.xMax, rect.yMax, 0);
            GL.Vertex3(rect.xMin, rect.yMax, 0);

            GL.End();
        }

        public override void OnMouseEvent(int button, bool pressed, int x, int y)
        {
            
        }

        public override void OnMotionEvent(int x, int y)
        {
            
        }

        public override void OnScrollEvent(int delta)
        {
            
        }

    }

}
