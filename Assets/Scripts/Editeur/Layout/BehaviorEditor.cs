using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class BehaviorEditor : Container
    {

        private static readonly Color BACKGROUND_COLOR = new Color((float)0xe8 / 255, (float)0xec / 255, (float)0xf1 / 255); //#e8ecf1

        private static BehaviorEditor actual = null;

        public static BehaviorEditor Actual { get { return actual; } }

        public Primitive First { get { return (Primitive)this.childs[0]; } }

        public BehaviorEditor() : base(new Rect(Screen.width * TeamSelection.DIM_WIDTH, 0, Screen.width * (1 - TeamSelection.DIM_WIDTH), Screen.height))
        {
            BehaviorEditor.actual = this;

            this.AllowScrollbar = true;
            this.AllowMotionScroll = true;
            this.Background = BACKGROUND_COLOR;

            this.AddChild(new Primitive(new Vector2(100, 0), Primitive.NAME_PRIMITIVE_BEGIN));
        }

    }

}
