using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using jcPIOL.PCL.Enums;
using jcPIOL.PCL.Transports;

namespace jcPIOL.PCL {
    public class jcPIOLManager {
        private readonly List<object> _storage;

        private readonly jcPIOLBaseWebAPIHandler _webAPIHandler;

        public jcPIOLManager(string webAPIURL) {
            _storage = new List<object>();

            _webAPIHandler = new jcPIOLBaseWebAPIHandler(webAPIURL);
        }

        public async Task<jcPIOLResponseItem<T>> AddRequest<T>(jcPIOLRequestType requestType, string urlRequest, Object obj) {
            var responseItem = new jcPIOLResponseItem<T> {
                JCPIOL_Status = jcPIOLDataStatus.REQUEST_FOR_DATA,
                JCPIOL_TransactionGUID = Guid.NewGuid()
            };

            if (!NetworkInterface.GetIsNetworkAvailable()) {
                // Record Request
            } else {
                switch (requestType) {
                    case jcPIOLRequestType.GET:
                        var response = await _webAPIHandler.Get<T>(urlRequest);

                        responseItem.ObjectData = response;
                        responseItem.JCPIOL_Status = jcPIOLDataStatus.DATA_RETRIEVED;
                        
                        break;
                    case jcPIOLRequestType.DELETE:
                        var deleteResponse = await _webAPIHandler.Delete(urlRequest);

                        if (deleteResponse) {
                            responseItem.JCPIOL_Status = jcPIOLDataStatus.FAILED_REQUEST;
                        }

                        break;
                }

                _storage.Add(responseItem);
            }

            return responseItem;
        }
    }
}