using WesternManagementApp.Services;

namespace WesternManagementApp;

public partial class UserCreatePage : ContentPage
{
    public USER currentItem { get; set; }
    protected bool isCreate;
    public UserCreatePage()
    {
        InitializeComponent();
    }
    void rolePicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
        /*
        Officer
        School_Principal
        Campus Accounting
        Authorized_Expert
        Finance_Manager
        VP_Finance
        BoardMember
        Manager
        Vice_President_Operation
        */
    }
    void OnSaveClicked(System.Object sender, System.EventArgs e)
    {
        if (isCreate == true)
        {
            DataService.Instance.createUser((success) => {
                MainThread.BeginInvokeOnMainThread(() => {
                    Application.Current.MainPage.DisplayAlert("Ok", "User created", "OK");
                });
            }, currentItem);
        }
        else
        {
            DataService.Instance.updateUser((success) =>
            {
                MainThread.BeginInvokeOnMainThread(() => {
                    Application.Current.MainPage.DisplayAlert("Ok", "User updated", "OK");
                });
            }, currentItem);
        }

    }
}

public class UserDetailsPage : UserCreatePage
{
    public UserDetailsPage(USER selectedUser) : base()
    {
        currentItem = selectedUser;
        OnPropertyChanged("currentItem");
    }
}