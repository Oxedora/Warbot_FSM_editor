using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    public class PrimitivesCollection : Container
    {

        private static readonly int DIM_TITLE_HEIGHT = 50;

        private static readonly Color BACKGROUND_COLOR = new Color((float)0xb5 / 255, (float)0xcf / 255, (float)0xd8 / 255); //#b5cfd8

        public Category[] Categories
        {
            get
            {
                List<Category> list = new List<Category>();
                for (int i = 1; i < this.childs.Count; i++)
                {
                    list.Add((Category)this.childs[i]);
                }
                return list.ToArray();
            }
        }

        public PrimitivesCollection() : base(new Rect(0, Screen.height * TeamSelection.DIM_HEIGHT, Screen.width * TeamSelection.DIM_WIDTH, Screen.height * (1 - TeamSelection.DIM_HEIGHT)))
        {
            this.AllowScrollbar = true;
            this.Background = BACKGROUND_COLOR;

            // A SUPPRIMER
            this.AddChild(new Label(new Rect(0, 0, this.area.width, DIM_TITLE_HEIGHT), "Primitives"));
            Category category = new Category("Catégorie 1", new Vector2(0, DIM_TITLE_HEIGHT), (int)this.area.width - Scrollbar.DIM_WIDTH);
            category.Resize += OnCategoryResize;
            category.Elements = new string[]
            {
                "SI ALORS.. SINON..",
                "Primitive 2",
                "Primitive 3",
                "Primitive 4",
                "Primitive 5",
                "Primitive 6",
                "Primitive 7",
                "Primitive 8",
                "Primitive 9",
                "Primitive 10",
                "Primitive 11",
                "Primitive 12",
                "Primitive 13",
                "Primitive 14"
            };
            this.AddChild(category);

            category = new Category("Catégorie 2", new Vector2(0, DIM_TITLE_HEIGHT + Category.DIM_ELEMENT_HEIGHT), (int)this.area.width - Scrollbar.DIM_WIDTH);
            category.Resize += OnCategoryResize;
            category.Elements = new string[]
            {
                "Primitive 1",
                "Primitive 2",
                "Primitive 3",
                "Primitive 4",
                "Primitive 5",
                "Primitive 6"
            };
            this.AddChild(category);
        }

        /// <summary>
        /// Appelée lorsque qu'une catégorie se redimensionne
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OnCategoryResize(Widget widget, int  x, int y)
        {
            for (int i = 1; i < this.childs.Count; i++)
            {
                this.childs[i].LocalPosition = new Vector2(this.childs[i].LocalPosition.x, this.childs[i - 1].LocalArea.yMax);
                this.positions[i] = this.positions[i - 1] + new Vector2(0, this.childs[i - 1].LocalArea.height);
            }
            this.scrollbar.ScrollHeight = this.childs[this.childs.Count - 1].LocalArea.yMax;
        }

        protected override void RefreshDiplaying()
        {
            foreach (Widget widget in this.childs)
            {
                widget.Active = true;
            }
        }

    }

}