using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class SwipeTemplateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)parameter == "RightSwipeTemplate")
            {
                if ((FlowDirection)value == FlowDirection.RightToLeft)
                    return App.Current.Resources["LeftSwipeTemplate"] as DataTemplate; 
                else
                    return App.Current.Resources["RightSwipeTemplate"] as DataTemplate;
            }
            else
            {
                if ((FlowDirection)value == FlowDirection.RightToLeft)
                    return App.Current.Resources["RightSwipeTemplate"] as DataTemplate;
                else
                    return App.Current.Resources["LeftSwipeTemplate"] as DataTemplate;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
