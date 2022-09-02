using System.Runtime.Serialization;
using static CRI_V1.Data.CRIModel;

namespace CRI_V1.Data
{
    public class CRIModel
    {
        [DataContract]
        public class Tabs
        {
            [DataMember]
            public string Title;
            [DataMember]
            public List<string> Path;
            [DataMember]
            public List<Tabs> SubTabs;
            [DataMember]
            public List<string> Contents = new();
        }

        [DataContract]
        public class CRITabList
        {
            [DataMember]
            public List<Tabs> Tabs { get; set; } = new();
        }
    }
}
