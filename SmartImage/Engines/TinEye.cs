#region

using SmartImage.Model;
using SmartImage.Searching;

#endregion

namespace SmartImage.Engines
{
	public sealed class TinEye : QuickSearchEngine
	{
		public TinEye() : base("https://www.tineye.com/search?url=") { }

		public override string Name => "TinEye";

		public override SearchEngines Engine => SearchEngines.TinEye;
	}
}