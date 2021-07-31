using Newtonsoft.Json;

namespace NotionApi.Rest.Common
{
    public class ParentWorkspaceReference : ParentReference
    {
        [JsonProperty("workspace")] public bool Workspace { get; set; }
    }
}