using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mpeg4Lib.Boxes
{
	public class AppleListBox : Box
	{
		public AppleListBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
		{
			long endPos = Header.FilePosition + Header.TotalBoxSize;

			while (file.Position < endPos)
			{
				AppleTagBox appleTag = new AppleTagBox(file, new BoxHeader(file), this);

				if (appleTag.Header.TotalBoxSize == 0)
					break;
				Children.Add(appleTag);
			}
		}

		public IEnumerable<AppleTagBox> Tags => GetChildren<AppleTagBox>();

		protected override void Render(Stream file) { }

		public void AddTag(string name, string data)
		{
			AppleTagBox.Create(this, name, Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
		}

		public void AddTag(string name, byte[] data, AppleDataType type)
		{
			AppleTagBox.Create(this, name, data, type);
		}		

		public void EditOrAddTag(string name, string data)
		{
			EditOrAddTag(name, Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
		}

		public void EditOrAddTag(string name, byte[] data)
		{
			EditOrAddTag(name, data, AppleDataType.ContainsData);
		}

		public void EditOrAddTag(string name, byte[] data, AppleDataType type)
		{
			AppleTagBox tag = Tags.Where(t => t.Header.Type == name).FirstOrDefault();

			if (tag is null)
			{
				AddTag(name, data, type);
			}
			else if (tag?.Data.DataType == type)
			{
				tag.Data.Data = data;
			}
		}

		public string GetTagString(string name)
		{
			byte[] tag = GetTagBytes(name);
			if (tag is null) return null;

			return Encoding.UTF8.GetString(tag);
		}

		public byte[] GetTagBytes(string name)
			=> Tags
			.Where(t => t.Header.Type == name)
			.FirstOrDefault()
			?.GetChild<AppleDataBox>()
			?.Data;		
	}
}
