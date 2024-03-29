// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Z�hlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using System.Linq;
using System;

namespace DosBox.Filesystem
{
    /// <summary>
    /// This class implements the behavior of concrete directories.
    /// Composite-Pattern: Composite
    ///
    /// Responsibilities:
    /// - defines behavior for components (directories) having children. 
    /// - stores child components (files and subdirectories). 
    /// - implements child-related operations in the Component interface. These are:
    ///   - getContent()
    ///   - add(Directory), add(File)
    ///   - getNumberOfFiles(), getNumberOfDirectories()
    /// </summary>
    public class Directory : FileSystemItem
    {
        private readonly List<FileSystemItem> content;

        public Directory(string name)
            : base(name,null, System.DateTime.Now.ToString()) //add lemparan mkdir timestamp
        {
            content = new List<FileSystemItem>();
        }

        public override IEnumerable<FileSystemItem> Content
        {
            get { return content; }
        }

        public override bool IsDirectory()
        {
            return true;
        }


        public bool isExistName(string name)
        {
            foreach (FileSystemItem i in content)
            {
                if (i.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new System.Exception("A subdirectory or file " + name + " already exists.");
                }
            }
            return true;
        }

        /// <summary>
        /// Adds a new subdirectory to this directory.
        /// If the directory to add is already part of another directory structure,
        /// it is removed from there.
        /// </summary>
        public void Add(Directory directoryToAdd)
        {
            if (isExistName(directoryToAdd.Name))
            {
                content.Add(directoryToAdd);
                if (HasAnotherParent(directoryToAdd))
                {
                    RemoveParent(directoryToAdd);
                }

                directoryToAdd.Parent = this;
            }
        }

        public void Add(File fileToAdd,bool replace)
        {
            if (replace == true)
            {
                content.Add(fileToAdd);
                if (fileToAdd.Parent != null)
                {
                    fileToAdd.Parent.content.Remove(fileToAdd);
                }

                fileToAdd.Parent = this;
            }
            else
            {
                if (isExistName(fileToAdd.Name))
                {
                    content.Add(fileToAdd);
                    if (fileToAdd.Parent != null)
                    {
                        fileToAdd.Parent.content.Remove(fileToAdd);
                    }

                    fileToAdd.Parent = this;
                }
            }

        }


        public void Del(File fileToDel)
        {
            fileToDel.Parent = this;
            int Index = IndexDel(fileToDel.Name);
            content.RemoveAt(Index);      
        }
       

        public int IndexDel(string name)
        {
            int index1 =0;
            foreach (FileSystemItem i in content)
            {
                if (i.Name == name)
                {
                    index1 = content.IndexOf(i);
                }
            }
            return index1;
        }

        //public string GetContent(string name)
        //{
        //    string Content = "";
        //    foreach (FileSystemItem i in content)
        //    {
        //        if (i.Name == name)
        //        {
        //            Content = i.FileContent();
        //        }
        //    }
        //    return Content;
        //}

        //public string GetTimeStamp(string name)
        //{
        //    string timeStamp = "";
        //    foreach (FileSystemItem i in content)
        //    {
        //        if (i.Name == name)
        //        {
        //            timeStamp = i.CreatedDate;
        //        }
        //    }
        //    return timeStamp;
        //}

        private static bool RemoveParent(FileSystemItem item)
        {
            return item.Parent.content.Remove(item);
        }

        private static bool HasAnotherParent(FileSystemItem item)
        {
            return item.Parent != null;
        }


        /// <summary>
        /// Removes a directory or a file from current directory.
        /// Sets the parent of the removed item to null, if was contained in this directory.
        /// </summary>
        /// 
        public void Remove(FileSystemItem item)
        {
            if (content.Contains(item))
            {
                item.Parent = null;
                content.Remove(item);
            }
        }

        public override int GetNumberOfContainedFiles()
        {
            return content.Count(item => !item.IsDirectory());
        }

        public override int GetNumberOfContainedDirectories()
        {
            return content.Count(item => item.IsDirectory());
        }

        public override int GetSize()
        {
            return 0;
        }

        public override string FileContent()
        {
            return "";
        }
    }
}