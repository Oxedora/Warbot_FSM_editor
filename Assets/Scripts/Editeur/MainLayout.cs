using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Gestionnaire des widgets à l'écran
    /// </summary>
    public class MainLayout : MonoBehaviour
    {


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        private static MainLayout actual;

        private List<Widget> widgets;

        private Vector2 mouse_position;

        private static Material lineMaterial;


        /****************************
         ****** OBJETS D'UNITY ******
         ****************************/


        public GameObject MainCanvas;


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Layout actuel
        /// </summary>
        public static MainLayout Actual { get { return actual; } }

        /// <summary>
        /// Liste des widgets
        /// </summary>
        public Widget[] Widgets { get { return widgets.ToArray(); } }


        /****************************
         ****** APPELS D'UNITY ******
         ****************************/


        /// <summary>
        /// Appelée à l'initialisation du GameObject
        /// </summary>
        void Start()
        {
            MainLayout.actual = this;
            widgets = new List<Widget>();
            mouse_position = new Vector2();

            this.AddWidget(new BehaviorEditor());
            this.AddWidget(new PrimitivesCollection());
            this.AddWidget(new TeamSelection());
        }

        /// <summary>
        /// Appelée à l'update du GameObject (chaque frame)
        /// </summary>
        void Update()
        {
            OnUpdate();
        }

        /// <summary>
        /// Appelée à chaque évènement
        /// </summary>
        void OnGUI()
        {
            Event e = Event.current;
            if (mouse_position != e.mousePosition)
            {
                mouse_position = e.mousePosition;
                OnMotionEvent((int)e.mousePosition.x, (int)e.mousePosition.y);
            }
            switch (e.type)
            {
                case EventType.MouseDown:
                    OnMouseEvent(e.button, true, (int)e.mousePosition.x, (int)e.mousePosition.y);
                    break;
                case EventType.MouseUp:
                    OnMouseEvent(e.button, false, (int)e.mousePosition.x, (int)e.mousePosition.y);
                    break;
                case EventType.MouseMove:
                    OnMotionEvent((int)e.mousePosition.x, (int)e.mousePosition.y);
                    break;
                case EventType.ScrollWheel:
                    OnScrollEvent((int)e.delta.y);
                    break;
                case EventType.KeyDown:
                    OnKeyEvent(e.keyCode, true);
                    break;
                case EventType.KeyUp:
                    OnKeyEvent(e.keyCode, false);
                    break;
                case EventType.Repaint:
                    OnDraw();
                    break;
            }
        }


        /*********************************
         ****** METHODES DE GESTION ******
         *********************************/


        /// <summary>
        /// Ajoute un widget à la liste
        /// </summary>
        /// <param name="obj"></param>
        public void AddWidget(Widget obj)
        {
            widgets.Add(obj);
        }

        /// <summary>
        /// Supprime un widget de la liste
        /// </summary>
        /// <param name="obj"></param>
        public void RemoveWidget(Widget obj)
        {
            widgets.Remove(obj);
        }

        /// <summary>
        /// Supprime tous les widgets de la liste
        /// </summary>
        public void Clear()
        {
            foreach (Widget o in widgets)
            {
                o.OnDestroy();
            }
        }

        /// <summary>
        /// Crée un matériau pour l'affichage avec les fonctions d'OpenGL
        /// </summary>
        private static void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
                // Unity has a built-in shader that is useful for drawing
                // simple colored things.
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                // Turn on alpha blending
                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                // Turn backface culling off
                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                // Turn off depth writes
                lineMaterial.SetInt("_ZWrite", 0);
            }
        }


        /***********************************
         ****** METHODES D'EVENEMENTS ******
         ***********************************/


        /// <summary>
        /// Appelée lors de l'affichage
        /// </summary>
        public void OnDraw()
        {
            CreateLineMaterial();
            lineMaterial.SetPass(0);
            foreach (Widget o in widgets)
            {
                GL.PushMatrix();
                GL.LoadOrtho();
                GL.MultMatrix(Matrix4x4.TRS(new Vector3(0, 1, 0), Quaternion.identity, new Vector3((float)1 / (float)Screen.width, (float)-1 / (float)Screen.height)));
                o.OnDrawWithGL();
                GL.PopMatrix();
                o.OnDrawWithoutGL();
            }
        }

        /// <summary>
        /// Appelée lors de l'update
        /// </summary>
        public void OnUpdate()
        {
            foreach (Widget o in widgets)
            {
                o.OnUpdate();
            }
        }

        /// <summary>
        /// Appelée lors d'un évènement clavier
        /// </summary>
        /// <param name="keycode">touche associée à l'évènement</param>
        /// <param name="state">indique si la touche est appuyée ou non</param>
        public void OnKeyEvent(KeyCode keycode, bool state)
        {
            //print("[Key]: " + keycode + " = " + state);
            foreach (Widget o in widgets)
            {
                o.OnKeyEvent(keycode, state);
            }
        }

        /// <summary>
        /// Appelée lors d'un évènement souris de bouton
        /// </summary>
        /// <param name="button">bouton associé à l'évènement</param>
        /// <param name="pressed">indique si le bouton est appuyé ou non</param>
        /// <param name="x">coordonnée en x de la souris</param>
        /// <param name="y">coordonnée en y de la souris</param>
        public void OnMouseEvent(int button, bool pressed, int x, int y)
        {
            //print("[Mouse]: " + button + " = " + pressed + " -> " + x + " , " + y);
            foreach (Widget o in widgets)
            {
                o.OnMouseEvent(button, pressed, x, y);
            }
        }

        /// <summary>
        /// Appelée lors d'un évènement souris de mouvement
        /// </summary>
        /// <param name="x">coordonnée en x de la souris</param>
        /// <param name="y">coordonnée en y de la souris</param>
        public void OnMotionEvent(int x, int y)
        {
            //print("[Motion]: " + x + " , " + y);
            foreach (Widget o in widgets)
            {
                o.OnMotionEvent(x, y);
            }
        }

        /// <summary>
        /// Appelée lors d'un évènement souris de scrolling
        /// </summary>
        /// <param name="delta">valeur de scrolling</param>
        public void OnScrollEvent(int delta)
        {
            //print("[Scroll]: " + delta);
            foreach (Widget o in widgets)
            {
                o.OnScrollEvent(delta);
            }
        }
        
    }

}
