using System;
using Newtonsoft.Json;
using WesternManagementApp;

namespace WesternManagementApp.Services
{
    public delegate void typeresponseDelegate<T>(T response);
    public delegate void typeresponseDelegate<T, U>(T response, U response2);
    public delegate void typeresponseDelegate<T, U, V>(T response, U response2, V response3);
    public delegate void typeresponseDelegate<T, U, V, W>(T response, U response2, V response3, W response4);
    public delegate void typeresponseDelegate<T, U, V, W, X>(T response, U response2, V response3, W response4, X response5);

    public class DataService
	{
        public delegate void stringDelegate(string str);
        public delegate void responseDelegate<T>(T obj);
        public delegate void readyDelegate();
        public delegate void successDelegate(bool response);

        private Dictionary<string, string> headers;
        public USER CurrentUser { get; set; }
        public string BaseURL = "";
        static public string author;
        private static DataService instance;
        public static DataService Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataService();
                return instance;
            }
        }


        public DataService()
        {
            headers = new Dictionary<string, string>();
        }

        public void Login(string username, string password, responseDelegate<USER> del)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["username"] = username;
            data["password"] = password;

            if (Preferences.ContainsKey("Fcmtocken"))
                data["fcmtoken"] = Preferences.Get("Fcmtocken", "");
            RESTEngine.HttpPost(result => {
                if (result != null)
                {
                    Dictionary<string, object> json = JsonConvert.DeserializeObject<Dictionary<string, object>>(result,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    USER user = JsonConvert.DeserializeObject<USER>(json["user"].ToString(),
                    new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    this.CurrentUser = user;
                    Dictionary<string, string> token = JsonConvert.DeserializeObject<Dictionary<string, string>>(json["token"].ToString());
                    headers["Authorization"] = "bearer " + token["token"];

                    Preferences.Set("ID", user.id);
                    del(user);
                }
            }, this.BaseURL + "/login", data, this.headers, true);
        }

        public void getUserList(responseDelegate<List<USER>> del, string role = "")
        {
            
            RESTEngine.HttpGet((str) =>
            {
                List<USER> users = JsonConvert.DeserializeObject<List<USER>>(str);
                del(users);
            }, BaseURL + "/user" , this.headers);
        }

        public void createUser(successDelegate del, USER user)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["username"] = user.username;
            data["email"] = user.email;
            data["password"] = user.password;
            data["firstname"] = user.firstname;
            data["lastname"] = user.lastname;
            data["role"] = user.role;
            data["phone"] = user.phone;
            RESTEngine.HttpPost((str) =>
            {
                del(true);
            }, BaseURL + "/user", data, this.headers);
        }

        public void updateUser(successDelegate del, USER user)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["username"] = user.username;
            data["email"] = user.email;
            data["password"] = user.password;
            data["firstname"] = user.firstname;
            data["lastname"] = user.lastname;
            data["role"] = user.role;
            data["phone"] = user.phone;
            RESTEngine.HttpPut((str) =>
            {
                del(true);
            }, BaseURL + "/user/" + user.id, data, this.headers);
        }
    }
}

