using System.IO;

namespace AAXClean
{
    public class NewSplitCallback
    {
        public object UserState { get; set; }
        public Chapter Chapter { get; }
        public Stream OutputFile { get; set; }
        public NewSplitCallback(Chapter chapter)
        {
            Chapter = chapter;
        }
    }
}
