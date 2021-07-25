using NAudio.Lame;
using System.IO;

namespace AAXClean
{
    public class NewSplitCallback
    {
        public Chapter Chapter { get; }
        public Stream OutputFile { get; set; }
        public LameConfig LameConfig { get; set; }
        internal NewSplitCallback(Chapter chapter, LameConfig currentConfig)
        {
            Chapter = chapter;
            LameConfig = currentConfig;
        }
    }
}
