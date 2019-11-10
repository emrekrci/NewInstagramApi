using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaFollowers.Desktop
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static string accesTokenUri = "https://api.instagram.com/oauth/access_token";
        public static string accessToken = "";
        public static string userID = "";
        public MainPage()
        {
            InitializeComponent();
            getAccessToken();
        }

        public async void getAccessToken()
        {
            var valueCollection = new Dictionary<string, string>
            {
                {"app_id", WebPage.appID },
                { "app_secret", WebPage.appSecret},
                { "grant_type", "authorization_code"},
                {"redirect_uri", "https://github.com/emrekrci/" },
                { "code", WebPage.code},
            };

            HttpClient client = new HttpClient();
            var values = new FormUrlEncodedContent(valueCollection);
            var response = await client.PostAsync("https://api.instagram.com/oauth/access_token", values);
            var content = await response.Content.ReadAsStringAsync();
            var result2 = JsonConvert.DeserializeObject<JObject>(content);
            accessToken = result2.GetValue("access_token").ToString();
            userID = result2.GetValue("user_id").ToString();
            txtAccessToken.Text = accessToken;
            txtUserID.Text = userID;
        }

        private async void btnGetFollowerCount_Click(object sender, RoutedEventArgs e)
        {
            string count = "";
            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync($"https://graph.instagram.com/{MainPage.userID}/?fields=username,id,media,media_count&access_token={MainPage.accessToken}");
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JObject>(content);
                var media = result.GetValue("media");
                var data = media["data"];
                foreach (var item in data)
                {
                    var id = item["id"];
                    Console.WriteLine(id + "\n");
                }
            }
            txtFollowerCount.Text = count;
        }
    }
}
