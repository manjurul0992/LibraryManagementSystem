using LMS.WPFClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;



namespace LMS.WPFClient
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        HttpClient client = new HttpClient();
        Uri baseAddress = new Uri("https://localhost:44382/"); 
        public LoginScreen()
        {
            client=new HttpClient(); 
            client.BaseAddress = baseAddress;
            InitializeComponent();
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
           // var loginApiUrl = "Members/login";

            LoginVM vm = new LoginVM()
            {
                Username = txtUserName.Text,
                Password = txtPass.Password
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            var response = await client.PostAsJsonAsync(client.BaseAddress + "Members/login", vm);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResVM>();

                if (loginResponse != null && loginResponse.LoginStatus)
                {
                    SessionManager.SetValue("UserID", loginResponse.UserId);

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            else
            {

            }
        }
    }
}
