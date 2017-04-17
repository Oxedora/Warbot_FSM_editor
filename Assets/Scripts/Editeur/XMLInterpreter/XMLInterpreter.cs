using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

namespace WarBotEngine.Editeur
{
    public class XMLInterpreter : XmlDocument
    {
        public XMLInterpreter()
        {
        }

        /**
         * Generate a xml file containing only the given team name and saved to the given path
         */
        public void generateEmptyFile(string teamName, string path)
        {
            // Creating new xml document
            XmlDocument doc = new XmlDocument();

            // Creating root node
            XmlNode root = doc.CreateElement(Constants.nodeContainer);
            doc.AppendChild(root);

            // Appending team name
            XmlNode node = doc.CreateElement(Constants.nodeTeam);
            node.InnerText = teamName;
            root.AppendChild(node);

            doc.Save(path + "/" + teamName + Constants.xmlExtension);
        }

        /**
         * Returns the instruction corresponding to the given xmlnode
         */
        public Instruction whichInstruction(XmlNode ins)
        {
            switch (ins.Name)
            {
			case "When":
					List<Condition> conditions = new List<Condition> ();
					XmlNode cond = ins.FirstChild;
					if (cond != null) {
						foreach (XmlNode c in cond) {
							conditions.Add ((Condition)whichInstruction (c));
						}
					}
					List<Action> actions = new List<Action> ();
					XmlNode act = ins.LastChild;
					if (act != null) { 
						foreach (XmlNode a in act) {
							actions.Add ((Action)whichInstruction (a));
						}
					}

                    return new When(conditions, actions);
                case "Move":
                    return new Move();
                case "Shoot":
                    return new Shoot();
                case "IsBagEmpty":
                    return new IsBagEmpty();
                default:
                    return new Idle();
            }
        }


        /**
         * Look into each file in the given path and return the file name containing the given teamName
         * If no file corresponds, return an empty string
         */
        public string whichFileName(string teamName, string path)
        {
            string fileName = "";
            foreach (string file in Directory.GetFiles(path))
            {
                if (file.Contains(Constants.xmlExtension))
                {
                    Debug.Log(file);
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        if (stream.CanRead)
                        {
                            Load(stream);
                            XmlNode team = SelectSingleNode("//" + Constants.nodeTeam);
                            if (team.InnerText != null && team.InnerText.Equals(teamName))
                            {
                                fileName = file;
                            }

                        }
                    }

                    if (!fileName.Equals("")) // if we found the right file
                    {
                        break; // stop looking for other files
                    }
                }
            }

            return fileName;
        }

        public Dictionary<string, List<Instruction>> xmlToBehavior(string teamName, string path)
        {
            // Try to find an already existing file with this team name
            string fileName = whichFileName(teamName, path);

            // If no file has been found, create a new one with the given team name
            if (fileName.Equals("")) { fileName = teamName; }

            Dictionary<string, List<Instruction>> behavior = new Dictionary<string, List<Instruction>>();

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                Load(stream);
                XmlNodeList units = GetElementsByTagName(Constants.nodeUnit);
                for(int i = 0; i < units.Count; i++)
                {
                    string unitName = units.Item(i).Attributes.Item(0).Name;
                    behavior.Add(unitName, xmlToUnitBehavior(teamName, path, unitName));
                }
            }
                
            return behavior;
        }

        public List<Instruction> xmlToUnitBehavior(string teamName, string path, string unitName)
        {
            // Try to find an already existing file with this team name
            string fileName = whichFileName(teamName, path);

            // If no file has been found, create a new one with the given team name
            if (fileName.Equals("")) { fileName = teamName; }

            List<Instruction> behavior = new List<Instruction>();

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                Load(stream);
                // select the node containing the unit name
                XmlNode unitBehavior = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
                if (unitBehavior != null && unitBehavior.HasChildNodes)
                {
                    foreach (XmlNode ins in unitBehavior.ChildNodes)
                    {
                        behavior.Add(whichInstruction(ins));
                    }
                }
            }
            return behavior;
        }

        public void behaviorToXml(string teamName, string path, string unitName, List<Instruction> behavior)
        {
            // Try to find an already existing file with this team name
            string fileName = whichFileName(teamName, path);

            // If no file has been found, create a new one with the given team name
            if (fileName.Equals(""))
            {
                fileName = teamName + Constants.xmlExtension;
                generateEmptyFile(teamName, path);
            }

            // Load the file
            Load(path + "/" + fileName);

            // Get all nodes named "unit"
            XmlNode node = SelectSingleNode("//" + Constants.nodeUnit + "[@" + Constants.attributeName + "='" + unitName + "']");
            if(node == null)
            {
                node = CreateElement(Constants.nodeUnit);
            }
            Debug.Log(node.OuterXml);
            node.RemoveAll();

            XmlAttribute name = CreateAttribute(Constants.attributeName);
            name.Value = unitName;
            node.Attributes.Append(name);

            foreach (Instruction i in behavior)
            {
                node.AppendChild(ImportNode(i.xmlStructure(), true));
            }
            Debug.Log(node.OuterXml);
            XmlNode embbed = FirstChild;
            embbed.AppendChild(node);

            Save(path + "/" + fileName);
        }

        public List<string> allTeamsInXmlFiles(string path)
        {
            List<string> teams = new List<string>();

            foreach (string file in Directory.GetFiles(path))
            {
                if (file.Contains(Constants.xmlExtension))
                {
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        if (stream.CanRead)
                        {
                            Load(stream);
                            XmlNode team = SelectSingleNode("//" + Constants.nodeTeam);
                            if (team.InnerText != null)
                            {
                                teams.Add(team.InnerText);
                            }
                        }
                    }
                }
            }

            return teams;
        }

    }

}
        