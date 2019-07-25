// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdType : DosCommand
    {
        private readonly string SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED = "The system cannot find the file specified";
        private readonly string ACCESS_DENIED = "Access is denied";

        private Directory directoryToPrint;

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            if (GetParameterCount() == 0)
            {
                directoryToPrint = Drive.CurrentDirectory;
            }
            else
            {
                this.directoryToPrint = CheckAndPreparePathParameter(GetParameterAt(0), outputter);
            }
            return this.directoryToPrint != null;
        }

        private Directory CheckAndPreparePathParameter(string pathName, IOutputter outputter)
        {
            FileSystemItem fsi = Drive.GetItemFromPath(pathName);
            if (fsi == null)
            {
                outputter.PrintLine(SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED);
                return null;
            }
            if (!fsi.IsDirectory())
            {
                return fsi.Parent;
            }
            if (fsi.IsDirectory())
            {
                outputter.PrintLine(ACCESS_DENIED);
                return null;
            }
            return (Directory)fsi;
        }

        public CmdType(string name, IDrive drive)
            : base(name, drive)
        {
        }

        private static void PrintContent(IEnumerable<FileSystemItem> directoryContent, string name, IOutputter outputter)
        { 
            foreach (FileSystemItem item in directoryContent)
            {
                if (item.Name == name)
                {
                    outputter.Print(item.FileContent());
                }
            }
        }

        public override void Execute(IOutputter outputter)
        {         
            PrintContent(this.directoryToPrint.Content, GetParameterAt(0), outputter);          
            outputter.NewLine();
        }

    }
}