using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;
using RestSharp.Serialization.Json;
using SmartImage.Model;
using SmartImage.Utilities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SmartImage.Engines.TraceMoe
{
	public sealed class TraceMoe : QuickSearchEngine
	{
		public TraceMoe() : base("https://trace.moe/?url=") { }

		public override string Name => "trace.moe";

		public override SearchEngines Engine => SearchEngines.TraceMoe;

		private TraceMoeRootObject GetApiResults(string url)
		{
			// https://soruly.github.io/trace.moe/#/

			var rc = new RestClient("https://trace.moe/api/");

			var rq = new RestRequest("search");
			rq.AddQueryParameter("url", url);
			rq.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
			rq.RequestFormat           = DataFormat.Json;

			var re = rc.Execute<TraceMoeRootObject>(rq, Method.GET);

			// todo: null sometimes
			return re.Data;
		}

		public override SearchResult GetResult(string url)
		{
			var r = base.GetResult(url);

			var tm = GetApiResults(url);

			if (tm?.docs != null) {
				// Most similar to least similar
				var mostSimilarDoc = tm.docs[0];
				
				r.Similarity = (float?) mostSimilarDoc.similarity;
				
				r.ExtendedInfo.Add(string.Format("Name: {0}", mostSimilarDoc.title_english));
			}
			else {
				r.ExtendedInfo.Add("API returned null (possible timeout)");
			}
			


			return r;
		}
	}
}