using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Scripts.Editeur.Interpreter.Actions
{
    class Idle : Action
    {
        public Idle()
        {
            this.setStartAttribute("<idle>");
            this.setEndAttribute("</idle>");
        }

        public override bool execute(Unit u)
        {
            u.idle();

            return true;
        }
    }
}
