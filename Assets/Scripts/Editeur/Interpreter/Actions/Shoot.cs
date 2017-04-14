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
	 * Shoot ()
	 * {
	 *  unit.shoot()
	 * }
	 * 
	 * @Resume :
	 * Calls the shoot method of the unit.
	 * Returns true to tells above the method was executed
	 **/
	public class Shoot : Action
	{
        public Shoot()
        {
        }
		public override bool execute(Unit u)
		{
			u.shoot();

			return true;
		}
    }
}
