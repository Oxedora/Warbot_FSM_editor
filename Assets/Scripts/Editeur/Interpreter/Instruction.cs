using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace WarBotEngine.Editeur
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Resume :
	 * An instruction to execute
	 **/
	public abstract class Instruction
	{
        /***********/
        /* METHODS */
        /***********/

        // Calls the unit corresponding method
        public abstract bool execute(Unit u);

        // Returns the instruction xml structure
        public abstract XmlNode xmlStructure();

		public string Type()
		{
			return this.GetType ().Name;
		}
    }
}
