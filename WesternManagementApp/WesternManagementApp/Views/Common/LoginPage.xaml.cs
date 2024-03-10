using Microsoft.AppCenter.Analytics;
using WesternManagementApp.Services;

namespace WesternManagementApp.Views.Common;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        Preferences.Set("BaseURL", null);
        InitializeComponent();
        version.Text = "Version: " + VersionTracking.CurrentVersion;
        if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            DisplayAlert("Error", "No Internet connection available", "OK");
    }
    void Handle_Clicked(object sender, System.EventArgs e)
    {
        Analytics.TrackEvent(this.GetType().ToString() + " Handle_Clicked");
        loginBtn.IsEnabled = false;
        loader.IsRunning = true;
        password.IsEnabled = false;
        username.IsEnabled = false;
        try
        {
            if (username.Text == null || password.Text == "" || username.Text == "" || password.Text == "")
            {
                DisplayAlert("OK", "Username or Password cannot be null", "OK");
                loginBtn.IsEnabled = true;
                loader.IsRunning = false;
                password.IsEnabled = true;
                username.IsEnabled = true;
                return;
            }

            DataService.Instance.Login(username.Text, password.Text, (user) =>
            {
                if (user != null)
                {
                    Preferences.Set("username", username.Text);
                    Preferences.Set("password", password.Text);
                    Preferences.Set("ID", user.id);
                    DataService.author = user.username;
                    string role = user.role;
                    string modules = user.modules;
                    List<string> moduleList = modules.Split("|".ToCharArray()).ToList();
                    var shell = new AppShell(moduleList);

                    MainThread.BeginInvokeOnMainThread(() => {
                        Application.Current.MainPage = shell;
                    });

                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        error.IsVisible = true;
                        loader.IsRunning = false;
                        loginBtn.IsEnabled = true;
                        password.IsEnabled = true;
                        username.IsEnabled = true;
                    });
                }
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Server unreachable :" + ex.Message, "OK");
            loginBtn.IsEnabled = true;
            loader.IsRunning = false;
            password.IsEnabled = true;
            username.IsEnabled = true;

        }
    }
}
