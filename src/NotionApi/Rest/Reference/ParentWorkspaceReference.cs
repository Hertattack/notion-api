using Newtonsoft.Json;

namespace NotionApi.Rest.Reference
{
    public class ParentWorkspaceReference : ParentReference
    {
        [JsonProperty("workspace")] public bool Workspace { get; set; }
    }
}