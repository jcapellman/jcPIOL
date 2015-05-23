using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using jcPIOL.PCL.Enums;
using jcPIOL.UWA.Objects;

namespace jcPIOL.UWA.ViewModel {
    public class MainPageModel : INotifyPropertyChanged {
        private ObservableCollection<NewsResponseItem> _newsItems;

        public ObservableCollection<NewsResponseItem> NewsItems {
            get { return _newsItems; }

            set { _newsItems = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadNews() {
            var response = await App.DataManager.AddRequest<List<NewsResponseItem>>(jcPIOLRequestType.GET, "news", null);

            if (response.JCPIOL_Status != jcPIOLDataStatus.DATA_RETRIEVED) {
                return false;
            }

            NewsItems = new ObservableCollection<NewsResponseItem>(response.ObjectData);

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
