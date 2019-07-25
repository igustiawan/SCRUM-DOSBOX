// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    
    public class CmdMkFile : DosCommand
    {
        //private readonly string EMPTY_FILE = "ERROR [Cannot Empty FileName and File Content]";
        //private readonly string EMPTY_FILECONTENT = "ERROR [Cannot Empty File Content]";
        //private readonly string ONLY_TWO_FILE = "ERROR [Only FileName and File Content]";

        public CmdMkFile(string cmdName, IDrive drive)
            : base(cmdName, drive)
        {
        }

        public override void Execute(IOutputter outputter)
        {
            string fileName = GetParameterAt(0);
            if (!string.IsNullOrEmpty(fileName))
            {

                //add variabel timestamp buat lempar ke function file
                string fileContent = GetParameterAt(1);
                string timeStamp = System.DateTime.Now.ToString();
                File newFile = new File(fileName, fileContent ?? string.Empty, timeStamp);
                this.Drive.CurrentDirectory.Add(newFile,false);
            }
            else
            {
                outputter.PrintLine("syntax of the command is incorrect");
                //return false;
            }
            //return true;      
           
        }
    }
}