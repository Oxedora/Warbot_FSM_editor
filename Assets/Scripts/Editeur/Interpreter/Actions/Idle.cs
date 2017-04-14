using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Scripts.Editeur.Interpreter
{
    class Idle : Action
    {
        public Idle()
        {
        }

        public override bool execute(Unit u)
        {
            u.idle();

            return true;
        }
    }
}
