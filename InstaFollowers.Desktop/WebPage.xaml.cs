using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstaFollowers.Desktop
{
    /// <summary>
    /// Interaction logic for WebPage.xaml
    /// </summary>
    public partial class WebPage : Page
    {

        public static string appID = "532747647568845";
        public static string appSecret = "ec9f0ff3fc124fb48c8a96dbeb300706";
        public static string redirectUri = HttpUtility.UrlEncode("https://github.com/emrekrci/");
        public static string code = "";
        public WebPage()
        {
            InitializeComponent();
            OpenMainWeb();
        }

        private void OpenMainWeb()
        {
            webSiteUrl.Text = $"https://api.instagram.com/oauth/authorize?app_id={appID}&redirect_uri={redirectUri}&scope=user_profile,user_media&response_type=code";
            webPanel.Navigate($"https://api.instagram.com/oauth/authorize?app_id={appID}&redirect_uri={redirectUri}&scope=user_profile,user_media&response_type=code");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = webPanel.Source;
            webSiteUrl.Text = uri.ToString();
            var list = HttpUtility.ParseQueryString(uri.Query);
            code = list.Get("code");
            NavigationService.Navigate(new MainPage());
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            string url = webSiteUrl.Text;
            webPanel.Navigate("https://" + url);
        }
    }
}
