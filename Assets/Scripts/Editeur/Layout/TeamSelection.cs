using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarBotEngine.Editeur
{

    /// <summary>
    /// Menu de sélection des équipes et unités
    /// </summary>
    public class TeamSelection : Container
    {


        /*********************************
         ****** ATTRIBUTS STATIQUES ******
         *********************************/

        /// <summary>
        /// Largeur du menu
        /// </summary
        public static readonly float DIM_WIDTH = 0.25f;
        /// <summary>
        /// Hauteur du menu
        /// </summary>
        public static readonly float DIM_HEIGHT = 0.25f;

        /// <summary>
        /// Largeur des sélecteurs
        /// </summary>
        public static readonly float DIM_SELECTOR_WIDTH = 0.75f;
        /// <summary>
        /// Hauteur des sélecteurs
        /// </summary>
        public static readonly int DIM_SELECTOR_HEIGHT = 30;

        /// <summary>
        /// Hauteur de la liste déroulante du premier sélecteur
        /// </summary>
        public static readonly float DIM_SELECTOR_DROP_HEIGHT_1 = 1f;
        /// <summary>
        /// Hauteur de la liste déroulante du second sélecteur
        /// </summary>
        public static readonly float DIM_SELECTOR_DROP_HEIGHT_2 = 1f;

        /// <summary>
        /// Couleur de fond
        /// </summary>
        private static readonly Color BACKGROUND_COLOR = new Color((float)0x73 / 255, (float)0x93 / 255, (float)0xa7 / 255); //#7393a7


        /***********************
         ****** ATTRIBUTS ******
         ***********************/


        /// <summary>
        /// Sélecteur d'équipes
        /// </summary>
        private Selector team_selector;

        /// <summary>
        /// Sélecteur d'unités
        /// </summary>
        private Selector unit_selector;


		private MainLayout mainLayout;

		/*********************
		 ***** ACCESSEURS*****
		 *********************/

		public Selector Team_selector{
			get
			{
				return this.team_selector;
			}
			set
			{ }
		}

		public Selector Unit_selector{
			get
			{
				return this.unit_selector;
			}
			set
			{ }
		}

        /********************************************
         ****** METHODES SPECIFIQUES AU WIDGET ******
         ********************************************/


        /// <summary>
        /// Constructeur de base du menu
        /// </summary>
		public TeamSelection(MainLayout ml) : base(new Rect(0, 0, Screen.width * DIM_WIDTH, Screen.height * DIM_HEIGHT))
        {
            this.Background = BACKGROUND_COLOR;
            team_selector = new Selector(new Rect(this.area.width * (1 - DIM_SELECTOR_WIDTH) / 2, (this.area.height - 2 * DIM_SELECTOR_HEIGHT) / 3, DIM_SELECTOR_WIDTH * this.area.width, DIM_SELECTOR_HEIGHT), (int)(DIM_SELECTOR_DROP_HEIGHT_1 * this.area.height), this);
            unit_selector = new Selector(new Rect(this.area.width * (1 - DIM_SELECTOR_WIDTH) / 2, (this.area.height - 2 * DIM_SELECTOR_HEIGHT) * 2 / 3 + DIM_SELECTOR_HEIGHT, DIM_SELECTOR_WIDTH * this.area.width, DIM_SELECTOR_HEIGHT), (int)(DIM_SELECTOR_DROP_HEIGHT_2 * this.area.height), this);
			mainLayout = ml;
			this.AddChild(unit_selector);
            this.AddChild(team_selector);
            team_selector.SelectItem += OnSelectItem;
            team_selector.DeployOrTuck += OnDeployOrTuck;
            unit_selector.SelectItem += OnSelectItem;

            XMLInterpreter interpreter = new XMLInterpreter();

            // A SUPPRIMER
            team_selector.Elements = interpreter.allTeamsInXmlFiles(Constants.teamsDirectory).ToArray();
            unit_selector.Elements = new string[]
            {
                "base",
                "explorer",
                "turret",
                "ingeneer",
                "fighter"
            };
        }

        /// <summary>
        /// Appelé lorsque qu'un élément est sélectionné dans un sélecteur
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="args"></param>
        void OnSelectItem(Widget widget, object args)
        {
            string selection = (string)args;
            if (widget.ID == this.team_selector.ID)
            {
                //Sélection de l'équipe
                mainLayout.Editor = new BehaviorEditor(this.team_selector.Selection, this.Unit_selector.Selection);
            }
            else
            {
                //Sélection de l'unité
				mainLayout.Primitives_collection = new PrimitivesCollection (this.Unit_selector.Selection);
                mainLayout.Editor = new BehaviorEditor(this.team_selector.Selection, this.Unit_selector.Selection);
            }
        }

        /// <summary>
        /// Appelé lorsque qu'un sélecteur est déployé
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="args"></param>
        void OnDeployOrTuck(Widget widget, object args)
        {
            bool state = (bool)args;
            if (widget.ID == this.team_selector.ID)
            {
                //Sélection de l'équipe
                this.unit_selector.Active = !state;
            }
        }

    }

}
