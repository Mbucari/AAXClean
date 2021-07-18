using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean
{
    public class NewSplitCallback
    {
        public Chapter Chapter{get;}
        public Stream OutputFile { get; set; }
        public LameConfig LameConfig { get; set; }
        internal NewSplitCallback(Chapter chapter, LameConfig currentConfig)
        {
            Chapter = chapter;
            LameConfig = currentConfig;
        }
    }
}
