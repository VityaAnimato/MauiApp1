using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    static HttpClient httpClient = new HttpClient();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAuthorizeButtonClicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(loginEntry.Text))
        {
            await DisplayAlert("Уведомление", "Вы не ввели логин! Повторите ввод.", "ОK");
            return;
        }
        if (String.IsNullOrEmpty(passwordEntry.Text))
        {
            await DisplayAlert("Уведомление", "Вы не ввели пароль! Повторите ввод.", "ОK");
            return;
        }

        var user = new User(loginEntry.Text, passwordEntry.Text);
        var jsonDataToAPI = JsonConvert.SerializeObject(user);

        var uri = new Uri("http://testwork.cloud39.ru/BonusWebApi/Account/Login/");
        var content = new StringContent(jsonDataToAPI, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

        string jsonDataFromAPI = await response.Content.ReadAsStringAsync();

        var parsedJsonDataFromAPI = JToken.Parse(jsonDataFromAPI);
        var token = parsedJsonDataFromAPI["Token"].ToObject<string>();

        if (token == null)
            await DisplayAlert("Уведомление", "Логин и/или пароль неверен! Повторите ввод.", "ОK");
        else
        {
            var fullName = parsedJsonDataFromAPI["Client"]["FullName"].ToObject<string>();
            await DisplayAlert("Уведомление", $"Уважаемый(ая) {fullName}! Вы авторизованы.", "ОK");
        }
    }
}


