using Microsoft.AspNetCore.Components;
using System.Runtime.Serialization;

namespace CRI_V1.Data
{
    [DataContract]
    public class Tabs
    {
        [DataMember]
        public string Title;
        [DataMember]
        public List<string> Path;
        [DataMember]
        public List<Tabs> SubFiles;
        public List<string> Contents = new();
    }

    [DataContract]
    public class DemoCRIList
    {
        [DataMember]
        public List<Tabs> FileTabs { get; set; } = new();
    }
}
