using System.IO;

namespace AAXClean
{
	public class NewSplitCallback
	{
		public Chapter Chapter { get; internal init; }
		public int? TrackNumber { get; set; }
		public int? TrackCount { get; set; }
		public string TrackTitle { get; set; }
		public Stream OutputFile { get; set; }
	}
}
