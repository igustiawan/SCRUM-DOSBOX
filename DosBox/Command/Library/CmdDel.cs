// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;
using System.Text;
namespace DosBox.Command.Library
{
    public class CmdDel : DosCommand
    {
        private readonly string VER_IS_INVALID = "Invalid Parameter";
        private static readonly string NO_IS_FILE = "Could Not Find ";
        private Directory destinationDirectory;
       

        public CmdDel(string name, IDrive drive)
            : base(name, drive)
        {
        }

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            if (GetParameterCount() > 0)
            {
                this.destinationDirectory = ExtractAndCheckIfValidDirectory(GetParameterAt(0), this.Drive, outputter);
                return this.destinationDirectory != null;

            }
            else
            {
                this.destinationDirectory = null;
                return true;
            }
        }

        public static Directory ExtractAndCheckIfValidDirectory(string destinationDirectoryName, IDrive drive, IOutputter outputter)
        {
            FileSystemItem tempDestinationDirectory = drive.GetItemFromPath(destinationDirectoryName);
            if (tempDestinationDirectory == null)
            {
                outputter.PrintLine(NO_IS_FILE + drive + "\\" + destinationDirectoryName);
                return null;
            }
            //jika subdirektori tidak ingin dihapus
            //if (tempDestinationDirectory.IsDirectory())
            //{
                //outputter.PrintLine(NO_IS_FILE + drive + "\\" + destinationDirectoryName);
                //return null;
            //}
            return tempDestinationDirectory.Parent;
        }

        public override void Execute(IOutputter outputter)
        {
            if (GetParameterCount() > 0)
            {

                string fileName = GetParameterAt(0).Substring(GetParameterAt(0).LastIndexOf("\\") + 1);
                File DelFile = new File(fileName, null, null);
                this.destinationDirectory.Del(DelFile);

                outputter.Print("File " + fileName + " is Deleted");
                outputter.NewLine();
            }
            else
            {
                outputter.PrintLine(VER_IS_INVALID);
            }       
        }

    }
}