using System;
using System.Collections.Generic;

using jcPIOL.PCL.Transports;

namespace jcPIOL.PCL {
    public class jcPIOLManager {
        private Dictionary<Guid, Object> _storage;

        public jcPIOLManager() {
            _storage = new Dictionary<Guid, object>();
        }

        public jcPIOLResponseItem AddRequest(Object obj) {
            _storage.Add(Guid.NewGuid(), obj);

            return new jcPIOLResponseItem();
        }
    }
}