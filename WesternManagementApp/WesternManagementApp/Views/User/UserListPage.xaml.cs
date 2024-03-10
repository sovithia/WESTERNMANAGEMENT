using WesternManagementApp.Services;

namespace WesternManagementApp;

public partial class UserListPage : ContentPage
{
	public UserListPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DataService.Instance.getUserList((data) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                listView.ItemsSource = data;
                listView.HeaderTemplate = new DataTemplate(() => { return new Label() { Text = "Items:" + data.Count, HorizontalTextAlignment = TextAlignment.Center }; });
                if (data.Count > 0)
                    listView.ItemsLayout.ScrollToRowIndex(0);
            });
        });
    }


    void listView_ItemTapped(System.Object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        USER selectedItem = (USER)e.DataItem;
        Navigation.PushAsync(new UserDetailsPage(selectedItem));
    }
}
