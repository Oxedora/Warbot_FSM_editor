﻿using System;
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
	 * message (messageToSend)
	 * {
	 *  unit.message(messageToSend)
	 * }
	 * @Resume :
	 * Calls the sendMessage method of the unit depending of parameters.
	 * Returns true to tells above the method was executed
	 **/
	public abstract class Message : Action
	{
	}
}
