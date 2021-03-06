#region

using System;
using System.Collections.Generic;
using System.Text;
using SimpleCore;
using SimpleCore.Utilities;
using SmartImage.Searching;

#endregion

namespace SmartImage.Model
{
	public sealed class SearchResult
	{
		public SearchResult(string url, string name, float? similarity = null)
		{
			Url          = url;
			Name         = name;
			Similarity   = similarity;
			ExtendedInfo = new List<string>();
		}

		public string Url { get; }

		public string Name { get; }

		public float? Similarity { get; internal set; }

		public bool Success => Url != null;

		public List<string> ExtendedInfo { get; }

		public override string ToString()
		{
			return String.Format("{0}: {1}", Name, Url);
		}

		public string Format(string tag)
		{
			var sb = new StringBuilder();
			sb.AppendFormat("[{0}] {1}: {2}\n", tag, Name, Success ? CliOutput.RAD_SIGN : CliOutput.MUL_SIGN);

			if (Success) {
				const string ELLIPSES = "...";
				int          lim      = Console.BufferWidth - (30 + ELLIPSES.Length);
				string       url      = Url.Truncate(lim);


				sb.AppendFormat("\tResult url: {0}\n", url);
			}

			if (Similarity.HasValue) {
				sb.AppendFormat("\tSimilarity: {0:P}\n", Similarity);
			}

			foreach (string s in ExtendedInfo) {
				sb.AppendFormat("\t{0}\n", s);
			}

			return sb.ToString();
		}
	}
}