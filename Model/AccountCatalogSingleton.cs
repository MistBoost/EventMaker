using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EventMaker.Annotations;
using EventMaker.Persistency;

namespace EventMaker.Model
{
    public sealed class AccountCatalogSingleton : INotifyPropertyChanged
    {
        private static AccountCatalogSingleton _instance;
        private ObservableCollection<Account> _observableCollection;
        private static readonly object instanceLocker = new Object();

        public static AccountCatalogSingleton Instance
        {
            get {
                if (_instance != null) return _instance;
                lock (instanceLocker)
                {
                    if (_instance == null)
                        _instance = new AccountCatalogSingleton();
                }
                return _instance;
            }
        }

        public ObservableCollection<Account> ObservableCollection
        {
            get { return _observableCollection; }
            set
            {
                _observableCollection = value; 
                OnPropertyChanged(nameof(ObservableCollection));
            }
        }

        private AccountCatalogSingleton()
        {
            PersistencyService.SaveGenericAsJsonAsync(new ObservableCollection<Account>(), "accounts.json");
        }

        public void Add(string username, string password)
        {
            ObservableCollection.Add(new Account(username, password));
            PersistencyService.SaveGenericAsJsonAsync(_observableCollection, "accounts.json");
        }
        public void Remove(Account account)
        {
            _observableCollection.Remove(account);
            PersistencyService.SaveGenericAsJsonAsync(_observableCollection, "accounts.json");
        }

        public async void LoadAccountsAsync()
        {
            _observableCollection = await PersistencyService.LoadGenericFromJsonAsync<Account>("accounts.json");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
