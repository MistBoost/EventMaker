using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMaker.Converter;
using EventMaker.Model;
using EventMaker.ViewModel;
using Windows.UI.Xaml.Controls;

namespace EventMaker.Handler
{
    public class EventHandler
    {
        public EventViewModel EventViewModel { get; set; }

        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel = eventViewModel;
        }

        /// <summary>
        /// Adds event to the collection (data is binded in EventViewModel)
        /// </summary>
        public void CreateEvent()
        {
            EventCatalogSingleton.Instance.Add(EventViewModel.Name, EventViewModel.Description, EventViewModel.Place, DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventViewModel.Date, EventViewModel.Time));
        }


        /// <summary>
        /// Deletes event according to selected index binded in the ViewModel
        /// </summary>
        public async void DeleteEvent()
        {
            if (EventCatalogSingleton.Instance.ObservableCollection.Count != 0 && EventViewModel.SelectedEventIndex != -1)
            {
                //Delete event confirmation
                ContentDialog deleteEventDialog = new ContentDialog()
                {
                    Title = "Delete event permanently?",
                    Content = "If you delete this event, you won't be able to recover it. Do you want to delete it?",
                    PrimaryButtonText = "Delete",
                    SecondaryButtonText = "Cancel"
                };

                ContentDialogResult result = await deleteEventDialog.ShowAsync();

                if(result == ContentDialogResult.Primary)
                {
                    EventCatalogSingleton.Instance.Remove(EventCatalogSingleton.Instance.ObservableCollection[EventViewModel.SelectedEventIndex]);
                } else
                {
                    deleteEventDialog.Hide();
                }       
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
