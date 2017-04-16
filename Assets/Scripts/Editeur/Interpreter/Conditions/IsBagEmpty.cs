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
    public class IsBagEmpty : Condition
    {
        public IsBagEmpty()
        {
        }

        public override bool execute(Unit u)
        {
            return u.isBagEmpty();
        }

    }
}