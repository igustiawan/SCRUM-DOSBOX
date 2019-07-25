// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdTime : DosCommand
    {
        private readonly string TIME_IS_INVALID = "Time is Invalid";


        public CmdTime(string name, IDrive drive)
            : base(name, drive)
        {
        }

        public override void Execute(IOutputter outputter)
        {
            if (GetParameterCount() > 0)
            {
                outputter.PrintLine(TIME_IS_INVALID);
            }
            else
            {
                outputter.PrintLine("TIME " + System.DateTime.Now.ToString("HH:mm:ss"));
            }
           // return true;
        }

    }
}