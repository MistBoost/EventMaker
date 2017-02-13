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
        private static EventCatalogSingleton _instance;
        private ObservableCollection<Event> _observableCollection;

        public ObservableCollection<Event> ObservableCollection
        {
            get { return _observableCollection; }
            set
            {
                _observableCollection = value;
                OnPropertyChanged(nameof(ObservableCollection));
            }
        }
        public static EventCatalogSingleton Instance => _instance ?? (_instance = new EventCatalogSingleton());

        private EventCatalogSingleton()
        {
            LoadEventsAsync();
        }

        public void Add(int id, string name, string description, string place, DateTime dateTime)
        {
            ObservableCollection.Add(new Event(id, name, description, place, dateTime));
            PersistencyService.SaveGenericAsJsonAsync(_observableCollection, "events.json");
        }

        public void Remove(Event _event)
        {
            _observableCollection.Remove(_event);
            PersistencyService.SaveGenericAsJsonAsync(_observableCollection, "events.json");
        }

        public async void LoadEventsAsync()
        {
            _observableCollection = await PersistencyService.LoadGenericFromJsonAsync<Event>("events.json");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
