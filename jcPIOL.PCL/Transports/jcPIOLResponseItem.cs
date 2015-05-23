using System;
using System.Runtime.Serialization;

using jcPIOL.PCL.Enums;

namespace jcPIOL.PCL.Transports {
    [DataContract]
    public class jcPIOLResponseItem<T> {
        [DataMember]
        public Guid? JCPIOL_TransactionGUID { get; set; }

        [DataMember]
        public jcPIOLDataStatus JCPIOL_Status { get; set; }

        [DataMember]
        public T ObjectData { get; set; }
    }
}