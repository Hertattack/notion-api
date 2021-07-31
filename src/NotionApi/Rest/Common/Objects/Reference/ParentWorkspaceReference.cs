using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Reference
{
    public class ParentWorkspaceReference : ParentReference
    {
        [JsonProperty("workspace")] public bool Workspace { get; set; }
    }
}