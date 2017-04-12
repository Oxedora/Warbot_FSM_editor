using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Scripts.Editeur.Interpreter.Actions
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Structure : 
	 * Move ()
	 * {
	 *  unit.move()
	 * }
	 * 
	 * @Resume :
	 * Calls the move method of the unit.
	 * Returns true to tells above the method was executed
	 **/
	public class Move : Action
	{
        public Move()
        {
        }

        public override bool execute(Unit u)
		{
			u.move();

			return true;
		}
    }
}
