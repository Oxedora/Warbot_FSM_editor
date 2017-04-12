﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Entrée ou sortie d'une primitive
    /// </summary>
    public class PrimitiveContainer : Primitive
    {


        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/


        /// <summary>
        /// Hauteur minimale entre la dernière primitive et la fin du conteneur
        /// </summary>
        protected static readonly int DIM_MINIMUM_SPACE = 20;


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        /// <summary>
        /// Indique si les primitives sont en entrée ou en sortie d'une autre
        /// </summary>
        protected bool inner_container;

        /// <summary>
        /// Conteneur interne
        /// </summary>
        protected Container container;


        /************************
         ****** ACCESSEURS ******
         ************************/


        /// <summary>
        /// Premier élément de la colonne
        /// </summary>
        public Primitive First { get { return (Primitive)this.container.Childs[0]; } }

        /// <summary>
        /// Hauteur interne
        /// </summary>
        public int InnerHeight { get { return (this.container.Active) ? this.First.TotalHeight + DIM_MINIMUM_SPACE : 0; } }


        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base du conteneur
        /// </summary>
        /// <param name="pos">position du conteneur</param>
        /// <param name="name">nom du conteneur</param>
        /// <param name="parent">primitive parent</param>
        /// <param name="inner">entrée ?</param>
        public PrimitiveContainer(Vector2 pos, string name, Primitive parent, bool inner)
        {
            this.inner_container = inner;
            this.area = new Rect(pos.x, pos.y, Primitive.DIM_WIDTH, Primitive.DIM_TITLE_HEIGHT);
            this.container = new Container(new Rect(0, this.area.height, this.area.width, DIM_TITLE_HEIGHT + DIM_MINIMUM_SPACE));

            this.AddChild(this.container);
            Primitive begin = new Primitive(new Vector2(0, 0), Primitive.NAME_PRIMITIVE_BEGIN);
            this.container.AddChild(begin);
            begin.ExtendHeight += OnInnerExtend;

            this.title = new Label(new Rect(0, 0, this.area.width, this.area.height), name);
            this.title.Color = Color.red;
            this.AddChild(this.title);
        }

        /// <summary>
        /// Tente de placer la primitive entrée en paramètre
        /// </summary>
        /// <param name="primitive">primitive à placer</param>
        /// <param name="cursor">position du curseur</param>
        public override void PushPrimitive(Primitive primitive, Vector2 cursor)
        {
            this.First.PushPrimitive(primitive, cursor);
        }

        /// <summary>
        /// Appelée lors de l'extension des primitives internes
        /// </summary>
        /// <param name="w"></param>
        /// <param name="args"></param>
        protected void OnInnerExtend(Widget w, object args)
        {
            this.container.LocalArea = new Rect(0, this.area.height, this.area.width, this.First.TotalHeight + DIM_MINIMUM_SPACE);
            if (this.container.Active)
                this.ExtendPrimitive((int)this.container.LocalArea.height);
            else
                this.ExtendPrimitive(0);
        }

    }

}