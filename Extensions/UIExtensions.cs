using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BITS.SmartAdmin.WebUI.Extensions.UIExtensions
{
    public partial class CommitteeResponse
    {
        [JsonProperty("results")]
        public List<CommitteeResponseResult> Results { get; set; }
    }

    public partial class CommitteeResponseResult
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("children")]
        public List<CommitteeResponseResultChild> Children { get; set; }
    }

    public partial class CommitteeResponseResultChild
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class JsTreeModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public bool children { get; set; } // if node has sub-nodes set true or not set false
        public string type { get; set; }
        public JsTreeJsonData data { get; set; }
    }

    public class JsTreeJsonData
    {
        public bool ShowCheckIn { get; set; }
        public bool ShowCheckOut { get; set; }
        public bool ShowUndoCheckout { get; set; }
        public bool EnableCheckOut { get; set; }
        public string CheckedOutBy { get; set; }
    }

    public partial class ClaimResponse
    {
        [JsonProperty("results")]
        public List<ClaimResponseResult> Results { get; set; }
    }

    public partial class ClaimResponseResult
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("children")]
        public List<ClaimResponseResultChild> Children { get; set; }
    }

    public partial class ClaimResponseResultChild
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public string ParentText { get; set; }
    }
    public  class DropDown
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
    public class ActionResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
