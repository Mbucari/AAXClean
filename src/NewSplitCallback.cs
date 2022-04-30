using System.IO;

namespace AAXClean
{
    public class NewSplitCallback
    {
        public Chapter Chapter { get; internal init; }
        public object UserState { get; set; }
        public Stream OutputFile { get; set; }
    }
}
