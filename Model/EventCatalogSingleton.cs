using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EventMaker.Annotations;
using EventMaker.Persistency;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace EventMaker.Model
{
    public sealed class EventCatalogSingleton : INotifyPropertyChanged
    {
        private static volatile EventCatalogSingleton _instance;
        private static readonly object instanceLocker = new Object();
        public static Event selectedEvent { get; set; }
        private ObservableCollection<Event> _observableCollection;

        public ObservableCollection<Event> ObservableCollection
        {
            get {
                return (_observableCollection == null) ? new ObservableCollection<Event>() : _observableCollection;
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

        public int Add(string name, string description, string place, DateTime dateTime)
        {
            Event newEvent = new Event(name, description, place, dateTime);
            ObservableCollection.Add(newEvent);
            PersistencyService.SaveGenericAsJsonAsync(_observableCollection, "events.json");
            return ObservableCollection[ObservableCollection.Count-1].ID;
        }

        public void Remove(Event eventToBeRemoved)
        {
            ObservableCollection.Remove(eventToBeRemoved);
            PersistencyService.SaveGenericAsJsonAsync(_observableCollection, "events.json");
        }

        public void Edit(string name, string description, string place, DateTime dateTime)
        {
            //PersistencyService.SaveGenericAsJsonAsync(_observableCollection, "events.json");
        }

        public async void LoadEventsAsync()
        {
            _observableCollection = await PersistencyService.LoadGenericFromJsonAsync<Event>("events.json");
            if (_observableCollection == null)
            {
                _observableCollection = new ObservableCollection<Event>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
