using System.Collections.Generic;
using System.Xml;

namespace Assets.Scripts.Editeur.Interpreter
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Structure : 
	 * When (conditions)
	 * {
	 *  actions
	 * }
	 * 
	 * @Resume :
	 * The execute method returns True if the conditions were all true and the actions had been executed, false otherwise.
	 * It doesn't matter if some actions return false as long as the conditions are true, the when control is considered executed.
	 **/
	class When : Control
	{
        public When()
        {
        }

		public When(List<Condition> c, List<Action> a)
		{
            setConditions(c);
			setActions(a);
		}

		/**
		 * Returns True if conditions are true, false otherwise. 
		 */
		public override bool execute(Unit u)
		{
			bool conditionIsTrue = true;

			foreach (Condition c in getConditions()) // check if all conditions are true, breaks if at least one is false
			{
				if (!c.execute(u))
				{
					conditionIsTrue = false;
					break;
				}
			}

			if (conditionIsTrue) // if all conditions are true, execute all actions
			{
				foreach (Action a in getActions())
				{
					a.execute(u);
				}
			}

			return conditionIsTrue; // tells if the when control has been executed or not
		}

        /**
         * Returns the when xml structure
         * <when>
         *  <parameter>
         *      <condition></condition>
         *  </parameter>
         *  <action>
         *      <action1></action1>
         *      <action2></action2>
         *  </action>
         * </when>
         */
        public override XmlNode xmlStructure()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode whenNode = doc.CreateElement(this.GetType().Name);

            if(getConditions().Count > 0) {
                XmlNode paramNode = doc.CreateElement("parameters");
                foreach(Condition c in getConditions())
                {
                    paramNode.AppendChild(c.xmlStructure());
                }
                whenNode.AppendChild(paramNode);
            }

            if (getActions().Count > 0) {
                XmlNode actNode = doc.CreateElement("actions");
                foreach (Action a in getActions())
                {
                    actNode.AppendChild(a.xmlStructure());
                }
                whenNode.AppendChild(actNode);
            }

            return whenNode;
        }
    }
}
