# How to work with swiping with RTL in Xamarin.Forms ListView (SfListview)

You can change the [SwipeView](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SwipeView.html?) based on the [FlowDirection](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.visualelement.flowdirection?view=xamarin-forms#Xamarin_Forms_VisualElement_FlowDirection) using converter in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview?).

You can also refer the following article.

https://www.syncfusion.com/kb/11479/how-to-work-with-swiping-with-rtl-in-xamarin-forms-listview-sflistview

**XAML**

Define the DataTemplates for [LeftSwipeTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~LeftSwipeTemplate.html?) and [RightSwipeTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~RightSwipeTemplate.html?) in [Application.Resources](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.application.resources).

``` xml
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ListViewXamarin.App">
    <Application.Resources>
        <ResourceDictionary>
            <DataTemplate x:Name="RightSwipeTemplate" x:Key="RightSwipeTemplate">
                <Grid BackgroundColor="#009EDA" HorizontalOptions="Fill" VerticalOptions="Fill">
                    <Label Text="Right Swipe template" Padding="10" TextColor="White" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Grid>
            </DataTemplate>
 
            <DataTemplate x:Name="LeftSwipeTemplate" x:Key="LeftSwipeTemplate">
                <Grid BackgroundColor="#DC595F" HorizontalOptions="Fill" VerticalOptions="Fill">
                    <Label Text="Left Swipe template" Padding="10" TextColor="White" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Grid>
            </DataTemplate>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```
**C#**

Change the **FlowDirection** property in the Button [Command](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.button.command#Xamarin_Forms_Button_Command) execute method.
``` c#
namespace ListViewXamarin
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        private FlowDirection listViewFlowDirection;
        public FlowDirection ListViewFlowDirection
        {
            get { return listViewFlowDirection; }
            set
            {
                listViewFlowDirection = value;
                this.RaisedOnPropertyChanged("ListViewFlowDirection");
            }
        }
 
        public Command ButtonCommand { get; set; }
        
        public ContactsViewModel()
        {
            ButtonCommand = new Command(OnButtonClicked);
        }
        
        private void OnButtonClicked(object obj)
        {
            if (this.ListViewFlowDirection == FlowDirection.RightToLeft)
                this.ListViewFlowDirection = FlowDirection.LeftToRight;
            else
                this.ListViewFlowDirection = FlowDirection.RightToLeft;
        }
    }
}
```
        
**C#**

Converter to return the DataTemplate for **LeftSwipeTemplate** and **RightSwipeTemplate** based on the **FlowDirection**.
``` c#
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
```
**XAML**

Binding the converter to change the SwipeTemplates based on the FlowDirection of the ListView.
``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage" Padding="{OnPlatform iOS='0,40,0,0'}"
             FlowDirection="{x:Static Device.FlowDirection}">
    <ContentPage.BindingContext>
        <local:ContactsViewModel x:Name="viewModel"/>
    </ContentPage.BindingContext>
 
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:SwipeTemplateConverter x:Key="SwipeTemplateConverter"/>
        </ResourceDictionary>
    </ContentPage.Resources>
 
    <ContentPage.Content>
        <StackLayout>
            <Button x:Name="button" Text="Change FlowDirection" Command="{Binding ButtonCommand}"/>
            <syncfusion:SfListView x:Name="listView" ItemSpacing="1" ItemSize="60" AllowSwiping="True" ItemsSource="{Binding ContactsInfo}" FlowDirection="{Binding ListViewFlowDirection}"
                                RightSwipeTemplate="{Binding ListViewFlowDirection, Converter={StaticResource SwipeTemplateConverter}, ConverterParameter=RightSwipeTemplate}"
                                LeftSwipeTemplate="{Binding ListViewFlowDirection, Converter={StaticResource SwipeTemplateConverter}, ConverterParameter=LeftSwipeTemplate}">
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        <Grid x:Name="grid">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="70" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
                            <Grid Grid.Column="1" RowSpacing="1" Padding="10,0,0,0" VerticalOptions="Center" HorizontalOptions="StartAndExpand">
                                <Label LineBreakMode="NoWrap" TextColor="#474747" Text="{Binding ContactName}"/>
                                <Label Grid.Row="1" Grid.Column="0" TextColor="#474747" LineBreakMode="NoWrap" Text="{Binding ContactNumber}"/>
                            </Grid>
                        </Grid>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

**Output**

![SwipingRTL](https://github.com/SyncfusionExamples/swiping-with-rtl-listview-xamarin/blob/master/ScreenShots/SwipingRTL.gif)
