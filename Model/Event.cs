using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMaker.Model
{
    public class Event
    {
        private int _id;
        private string _name;
        private string _description;
        private string _place;
        private DateTime _dateTime;

        public int ID
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

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        public Event(string name, string description, string place, DateTime dateTime)
        {
            
            ID = setID();
            Name = name;
            Description = description;
            Place = place;
            DateTime = dateTime;
        }

        public int setID()
        {
            ObservableCollection<Event> allEvents = EventCatalogSingleton.Instance.ObservableCollection;
            if (allEvents == null || allEvents.Count == 0)
            {
                return 1;
            }
            return allEvents[allEvents.Count - 1].ID + 1;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
