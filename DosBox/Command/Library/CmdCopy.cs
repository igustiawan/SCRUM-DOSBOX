// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdCopy : DosCommand
    {
        private static readonly string NO_IS_FILE = "Could Not Find ";
        private static readonly string DESTINATION_IS_FILE = "Destination must be a directory..";
        private readonly string VER_IS_INVALID = "Invalid Parameter";

        private Directory destinationDirectory;

        public CmdCopy(string name, IDrive drive)
            : base(name, drive)
        {

        }
       
        protected override bool CheckParameterValues(IOutputter outputter)
        {
            if (GetParameterCount() > 0)
            {
                this.destinationDirectory = ExtractAndCheckIfValidDirectory(GetParameterAt(1), this.Drive, outputter);
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
                outputter.PrintLine(NO_IS_FILE +  destinationDirectoryName);
                return null;
            }
            if (!tempDestinationDirectory.IsDirectory())
            {
                outputter.PrintLine(DESTINATION_IS_FILE);
                return null;
            }
            return (Directory)tempDestinationDirectory;
        }

        public override void Execute(IOutputter outputter)
        {
            if (GetParameterCount() > 0)
            {
                string fileName = GetParameterAt(0);
                string fileContent = GetContent(fileName, Drive.CurrentDirectory);
                string timeStamp = GetTimeStamp(fileName, Drive.CurrentDirectory);

                if (GetParameterCount() == 3 && GetParameterAt(2).Equals("/y", StringComparison.InvariantCultureIgnoreCase))
                {
                    File DelFile = new File(fileName, null, null);
                    this.destinationDirectory.Del(DelFile);

                    File CopyFile = new File(fileName, fileContent, timeStamp);
                    this.destinationDirectory.Add(CopyFile,true);

                    outputter.Print("Copy File " + fileName + " is Succeed Replace");
                    outputter.NewLine();

                }
                else
                {
                    File CopyFile = new File(fileName, fileContent, timeStamp);
                    this.destinationDirectory.Add(CopyFile,false);

                    outputter.Print("Copy File " + fileName + " is Succeed");
                    outputter.NewLine();
                }
                   
            }
            else
            {
                outputter.PrintLine(VER_IS_INVALID);
            }
               

        }

        private string GetContent(string fileName, Directory directoryToLookup)
        {
            string Content = "";
            IEnumerable<FileSystemItem> content = directoryToLookup.Content;

            foreach (FileSystemItem i in content)
            {
                if (i.Name == fileName)
                {
                    Content = i.FileContent();
                }
            }
            return Content;
        }

        public string GetTimeStamp(string fileName, Directory directoryToLookup)
        {
            string timeStamp = "";
            IEnumerable<FileSystemItem> content = directoryToLookup.Content;
            foreach (FileSystemItem i in content)
            {
                if (i.Name == fileName)
                {
                    timeStamp = i.CreatedDate;
                }
            }
            return timeStamp;
        }

    }
}