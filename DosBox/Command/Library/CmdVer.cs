// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdVer : DosCommand
    {
        private readonly string VER_IS_INVALID = "Invalid";


        public CmdVer(string name, IDrive drive)
            : base(name, drive)
        {
        }


        public override void Execute(IOutputter outputter)
        {
            if (GetParameterCount() > 0)
            {
                if (GetParameterCount() == 1 && GetParameterAt(0).Equals("/w", StringComparison.InvariantCultureIgnoreCase))
                {
                    outputter.PrintLine("Microsoft Windows XP [Versi 5.1.2600]");
                    outputter.PrintLine("Igustiawan");
                    outputter.PrintLine("Igustiawan@prima-solusindo.com");
                    //return true;
                }
                else
                {
                    outputter.PrintLine(VER_IS_INVALID);
                    //return false;
                }
            }
            else
            {
                outputter.PrintLine("Microsoft Windows XP [Versi 5.1.2600]");
                //return true;
            }

            //return false;

           
        }

    }
}