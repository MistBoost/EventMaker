using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace EventMaker.Converter
{
    class EventBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int dateTimeDiff = DateTime.Compare((DateTime)value, DateTime.Now);
            //todays event
            if (((DateTime)value).Year == DateTime.Today.Year && ((DateTime)value).DayOfYear == DateTime.Today.DayOfYear)
            {
                return new SolidColorBrush(Color.FromArgb(0xDD, 57, 230, 72));
            } 
            else //finished or future event
            {
                if (dateTimeDiff < 0)
                {
                    return new SolidColorBrush(Color.FromArgb(0xDD, 250, 102, 57));
                }
                else if (dateTimeDiff > 0)
                {
                    return new SolidColorBrush(Color.FromArgb(0xDD, 53, 167, 242));
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
