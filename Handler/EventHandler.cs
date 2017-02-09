using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMaker.Converter;
using EventMaker.Model;
using EventMaker.ViewModel;

namespace EventMaker.Handler
{
    public class EventHandler
    {
        public EventViewModel EventViewModel { get; set; }

        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel = eventViewModel;
        }

        public void CreateEvent()
        {
            EventCatalogSingleton.Instance.Add(EventViewModel.Id, EventViewModel.Name, EventViewModel.Description, EventViewModel.Place, DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventViewModel.Date, EventViewModel.Time));
        }

        public void DeleteEvent()
        {
            if (EventCatalogSingleton.Instance.ObservableCollection.Count != 0 && EventViewModel.SelectedEventIndex != -1)
            {
                EventCatalogSingleton.Instance.Remove(EventCatalogSingleton.Instance.ObservableCollection[EventViewModel.SelectedEventIndex]);
            }
        }

        public void ChangeSelection()
        {
            if (EventCatalogSingleton.Instance.ObservableCollection.Count != 0 && EventViewModel.SelectedEventIndex != -1)
            {
                EventCatalogSingleton.Instance.ObservableCollection.RemoveAt(EventViewModel.SelectedEventIndex);
            }
        }
    }
}
