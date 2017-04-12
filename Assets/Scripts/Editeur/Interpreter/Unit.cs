﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Editeur.Interpreter
{
	public class Unit
	{
        private List<Instruction> behavior;
		private string a;
		Unit(string b) { a = b; }

		public void pickFood()
		{
			Console.WriteLine("j'ai pris de la bouffe ptdr");
		}

		public void move()
		{
			Console.WriteLine("g bouG");
		}

		public void shoot()
		{
			Console.WriteLine("et vlan ! dans les dents");
		}

        public void idle()
        {
            Console.WriteLine("je glande");
        }

        public void run()
        {
            while (true)
            {
                foreach(Instruction i in behavior)
                {
                    if (i.execute(this)) { break; }
                }
            }
        }

    }
}