using System;
using System.Collections.Generic;
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

        public Event(int id, string name, string description, string place, DateTime dateTime)
        {
            ID = id;
            Name = name;
            Description = description;
            Place = place;
            DateTime = dateTime;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
