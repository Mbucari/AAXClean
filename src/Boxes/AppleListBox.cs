using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AAXClean.Boxes
{
	internal class AppleListBox : Box
	{
		internal AppleListBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
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

		protected override void Render(Stream file)
		{
			return;
		}

		public void AddTag(string name, string data)
		{
			AddTag(name, Encoding.ASCII.GetBytes(data), AppleDataType.ContainsText);
		}

		public void AddTag(string name, byte[] data)
		{
			AddTag(name, data, AppleDataType.ContainsData);
		}

		private void AddTag(string name, byte[] data, AppleDataType type)
		{
			AppleTagBox.Create(this, name, data, type);
		}

		public void EditOrAddTag(string name, string data)
		{
			EditOrAddTag(name, Encoding.UTF8.GetBytes(data), AppleDataType.ContainsText);
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
			else if (tag.Data.DataType == type)
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
		{
			AppleTagBox tag = Tags.Where(t => t.Header.Type == name).FirstOrDefault();

			if (tag is null) return null;

			AppleDataBox tagData = tag.GetChild<AppleDataBox>();

			if (tagData is null) return null;

			return tagData.Data;
		}
	}
}
