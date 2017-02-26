using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EventMaker.Annotations;
using EventMaker.Common;
using EventMaker.Handler;
using EventMaker.Model;
using System.Threading;
using Windows.UI.Xaml;

namespace EventMaker.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private string _place;
        private DateTimeOffset _date;
        private TimeSpan _time;
        private string _searchQuery;
        private int _selectedIndex;
        private string _eventsChangedNotification;
        private bool _isEditOpen; 
        private ICommand _createEventCommand;
        private ICommand _deleteEventCommand;
        private ICommand _closeDialogCommand;

        public EventCatalogSingleton EventCatalogSingleton { get; set; }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Place
        {
            get { return _place; }
            set { _place = value; }
        }

        public DateTimeOffset Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string SearchQuery
        {
            get { return _searchQuery; }
            set { _searchQuery = value;
                OnPropertyChanged("SearchQuery");
            }
        }

        public string EventsChangedNotification
        {
            get { return _eventsChangedNotification; }
            set
            {
                _eventsChangedNotification = value;
                OnPropertyChanged("EventsChangedNotification");
            }
        }

        public bool IsEditOpen
        {
            get { return _isEditOpen; }
            set { _isEditOpen = value; }
        }

        public Handler.EventHandler EventHandler { get; set; }

        public ICommand CreateEventCommand
        {
            get { return _createEventCommand; }
            set { _createEventCommand = value; }
        }

        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand; }
            set { _deleteEventCommand = value; }
        }

        public ICommand CloseDialogCommand
        {
            get { return _closeDialogCommand; }
            set { _closeDialogCommand = value; }
        }

        public RelayCommand OpenEditCommand { get; set; }
        public RelayCommand CloseEditCommand { get; set; }

        public int SelectedEventIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedEventIndex");
            }
        }


        public EventViewModel()
        {
            EventCatalogSingleton = EventCatalogSingleton.Instance;
            EventCatalogSingleton.ObservableCollection.CollectionChanged += EventsCollection_CollectionChanged;
            var dt = DateTime.Now;
            Date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, new TimeSpan());
            Time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            SelectedEventIndex = -1;

            //creates an instance of EventHandler with an instance of EventViewModel passed as an argument
            EventHandler = new Handler.EventHandler(this);
            _createEventCommand = new RelayCommand(EventHandler.CreateEvent);
            _deleteEventCommand = new RelayCommand(EventHandler.DeleteEvent);
        }


        private void EventsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            EventsChangedNotification = "Event";
            if (e.NewItems != null)
            {
                foreach (Event item in e.NewItems)
                {
                    EventsChangedNotification += $" {item.Name} ";
                }
                EventsChangedNotification += "successfully added.";
            }
            else if (e.OldItems != null)
            {
                foreach (Event item in e.OldItems)
                {
                    EventsChangedNotification += $" {item.Name} ";
                }
                EventsChangedNotification += "successfully removed.";
            }

            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += (a, args) =>
            {
                EventsChangedNotification = "";
            };
            _timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}