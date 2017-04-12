using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Editeur.Interpreter
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
		
	}
}
