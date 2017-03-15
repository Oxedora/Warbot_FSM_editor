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

        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        private float scroll_height;

        private float actual_value = 0;

        private event EventDelegate on_changevalue;


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
