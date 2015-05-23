using System;
using System.Runtime.Serialization;

namespace jcPIOL.UWA.Objects {
    [DataContract]
    public class NewsResponseItem {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime PostDate { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public string PostBy { get; set; }

        [DataMember]
        public int NumComments { get; set; }

        [DataMember]
        public string URLSafename { get; set; }
    }
}