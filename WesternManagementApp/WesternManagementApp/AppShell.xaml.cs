using Microsoft.AppCenter.Analytics;

namespace WesternManagementApp;


public class OneModule
{
    public OneModule(string c, string t, int elementCount = 1, string c2 = null, string title2 = null, string c3 = null, string title3 = null)
    {
        ClassName = c;
        if (t.Substring(0, 1) != "[")
            Title = "[" + t + "]";
        else
            Title = t;
        ClassName2 = c2;
        ClassName3 = c3;

    }
    public int ElementCount { get; set; }
    public string ClassName { get; set; }
    public string ClassName2 { get; set; }
    public string ClassName3 { get; set; }
    public string Title { get; set; }
    public string Title2 { get; set; }
    public string Title3 { get; set; }
}

public class ModuleAttributes
{
    public List<OneModule> ModulesList { get; set; }
    public string Title { get; set; }
    public string PictureName { get; set; }

    public ModuleAttributes()
    {
        ModulesList = new List<OneModule>();
    }
}

public partial class AppShell : Shell
{
    Dictionary<string, ModuleAttributes> moduleList;
    public AppShell()
	{
		InitializeComponent();
	}
    private void initData()
    {
        this.moduleList = new Dictionary<string, ModuleAttributes>();

        // Common
        moduleList["Common"] = new ModuleAttributes { ModulesList = { new OneModule("Settings", "Settings") }, Title = "Other", PictureName = "other" };


    }


    public AppShell(List<string> modules)
    {

        MainThread.BeginInvokeOnMainThread(action: () => {
            Analytics.TrackEvent("Constructor " + this.GetType().ToString());
        });
        initData();
        InitializeComponent();
        Style style = CommonShell;

        foreach (string module in modules)
        {
            try
            {
                ModuleAttributes attr = moduleList[module];
                var tab = new Tab
                {
                    Title = attr.Title,
                    Icon = attr.PictureName
                    //Route = attr.Title,
                    //Style = style
                };


                foreach (OneModule oneModule in attr.ModulesList)
                {
                    Type type = null;
                    try
                    {
                        type = Type.GetType(GetType().Namespace + "." + oneModule.ClassName + "Page");
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Error", oneModule.ClassName + " cannot be created", "OK");
                        continue;
                    }
                    if (type == null)
                    {
                        DisplayAlert("Error", oneModule.ClassName + " cannot be created", "OK");
                        continue;
                    }


                    tab.Items.Add(new ShellContent()
                    {
                        Title = oneModule.Title,
                        ContentTemplate = new DataTemplate(type),
                    });

                    if (oneModule.ElementCount > 1)
                    {
                        Type type2 = Type.GetType(GetType().Namespace + oneModule.ClassName2 + "Page");
                        tab.Items.Add(new ShellContent()
                        {
                            Title = oneModule.Title2,
                            ContentTemplate = new DataTemplate(type2),
                        });
                        if (oneModule.ElementCount > 2)
                        {
                            Type type3 = Type.GetType(GetType().Namespace + oneModule.ClassName3 + "Page");
                            tab.Items.Add(new ShellContent()
                            {
                                Title = oneModule.Title3,
                                ContentTemplate = new DataTemplate(type3),
                            });
                        }
                    }
                }
                if (tab.Items.Count > 0)
                    itemContainer.Items.Add(tab);

                itemContainer.Items.Add(tab);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");

            }
        }
    }


}

