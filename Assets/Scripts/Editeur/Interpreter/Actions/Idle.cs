using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarBotEngine.Editeur
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
