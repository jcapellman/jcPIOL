using System;
using System.Runtime.Serialization;

using jcPIOL.PCL.Enums;

namespace jcPIOL.PCL.Transports {
    [DataContract]
    public class jcPIOLResponseItem {
        [DataMember]
        public Guid? JCPIOL_TransactionGUID { get; set; }

        [DataMember]
        public jcPIOLDataStatus JCPIOL_Status { get; set; }
    }
}