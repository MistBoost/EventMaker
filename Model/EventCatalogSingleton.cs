using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EventMaker.Annotations;
using EventMaker.Persistency;

namespace EventMaker.Model
{
    public sealed class EventCatalogSingleton : INotifyPropertyChanged
    {
        private static volatile EventCatalogSingleton _instance;
        private static readonly object instanceLocker = new Object();
        private ObservableCollection<Event> _observableCollection;

        public ObservableCollection<Event> ObservableCollection
        {
            get {
                return _observableCollection;
            }
            set
            {
                _observableCollection = value;
                OnPropertyChanged(nameof(ObservableCollection));
            }
        }
        public static EventCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (instanceLocker)
                    {
                        if (_instance == null)
                            _instance = new EventCatalogSingleton();
                    }
                }
                return _instance;
            }
        }

        private EventCatalogSingleton()
        {
            LoadEventsAsync();
        }

        public void Add(int id, string name, string description, string place, DateTime dateTime)
        {   
            if(ObservableCollection == null)
            {
                ObservableCollection = new ObservableCollection<Event>();
            }
            ObservableCollection.Add(new Event(ObservableCollection.Count+1, name, description, place, dateTime));
            PersistencyService.SaveEventsAsJsonAsync(_observableCollection);
        }

        public void Remove(Event eventToBeRemoved)
        {
            ObservableCollection.Remove(eventToBeRemoved);
            PersistencyService.SaveEventsAsJsonAsync(_observableCollection);
        }

        public async void LoadEventsAsync()
        {
            _observableCollection = await PersistencyService.LoadEventsFromJsonAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
