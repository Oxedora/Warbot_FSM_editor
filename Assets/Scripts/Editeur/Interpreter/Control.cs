using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Editeur.Interpreter
{
	/**
	 * @Author : Celia Rouquairol
	 * 
	 * @Project : Warbot Unity version : FSM design & implementation
	 * 
	 * @Resume :
	 * Checks if all conditions are true
	 * If it is so, execute all actions
	 * Return true if all conditions were true, false otherwise.
	 * It doesn't matter if some actions return false, as long as the conditions are true the control is considered executed.
	 **/
	public abstract class Control : Instruction
	{
		private List<Condition> conditions; // conditions to check
		private List<Action> actions; // actions to execute

		// Getters
		public List<Condition> getConditions() { return conditions; }
		public List<Action> getActions() { return actions; }

        // Setters
        public void setConditions(List<Condition> c) { conditions = c; }
		public void setActions(List<Action> a) { actions = a; }
    }

}
