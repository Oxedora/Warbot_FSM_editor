using System.Xml;

namespace WarBotEngine.Editeur
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 *
	 * @Structure : 
	 * condition ()
	 * {
	 *  unit.ThisAction()
	 * }
	 * @Resume :
	 * Calls the corresponding action of the unit
	 * What is "Condition" in the design pattern is classical method for the unit
	 **/
	public abstract class Condition : Instruction
	{
        public override XmlNode xmlStructure()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode node = doc.CreateElement(this.GetType().Name);

            return node;
        }

    }
}
