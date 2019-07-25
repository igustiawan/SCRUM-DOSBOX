// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdExit : DosCommand
    {
       
        public CmdExit(string name, IDrive drive)
            : base(name, drive)
        {
        }

        public override void Execute(IOutputter outputter)
        {
            if (GetParameterCount() == 0)
            {

            }
            else
            {
                string NameExit = GetParameterAt(0);

                if (NameExit == "GUGUS" || NameExit == "gugus")
                {
                    Environment.Exit(0);
                }
            }
            //return true;
          
        }

    }
}