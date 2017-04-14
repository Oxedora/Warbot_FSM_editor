using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Assets.Scripts.Editeur.Interpreter;
using System.Reflection;
using UnityEngine;
using WarBotEngine.Editeur;

[assembly: AssemblyVersionAttribute("1.0")]
namespace Assets.Scripts.Editeur.Interpreter
{
    class TestInterpreter : MonoBehaviour
    {
        void Start()
        {
            
            string teamName = "LoShenCompany";
            string path = "./";
            string unitName = "base";
            XMLInterpreter interpreter = new XMLInterpreter();
            
            List<Instruction> behavior = new List<Instruction>();
            /** Ecriture d'un fichier xml
            Condition bag = new IsBagEmpty();
            Action idle = new Idle();
            Action shoot = new Shoot();
            //Action move = new Move();
            When when = new When();

            when.addCondition(bag);
           // when.addAction(move);
            when.addAction(shoot);

            behavior.Add(when);
            behavior.Add(idle);

            interpreter.behaviorToXml(teamName, path, unitName, behavior);
            Debug.Log("fini");
            */
            behavior = interpreter.xmlToUnitBehavior(teamName, path, unitName);
            Debug.Log(behavior.Count);
            foreach(Instruction i in behavior)
            {
                Debug.Log(i.xmlStructure().OuterXml);
            }
        }

        void Update()
        {

        }
    }
}
