using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Scripts.Editeur.Interpreter
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 *
	 * @Structure : 
	 * action.execute (unit)
	 * {
	 *  unit.ThisAction()
	 * }
	 * @Resume :
	 * Calls the corresponding action of the unit
	 **/
	public abstract class Action : Instruction
	{
        public override XmlNode xmlStructure()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode node = doc.CreateElement(this.GetType().Name);

            return node;
        }
    }
}
