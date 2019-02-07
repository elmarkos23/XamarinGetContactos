using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinGetContactos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           
        }
        async Task GetContacs()
        {
            try
            {
                var contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();
                lvContactos.ItemsSource = contacts;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }
        async void Button_Clicked(object sender, EventArgs e)
        {
          await  GetContacs();
        }
        async Task GetLocationPermissionAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Contacts))
                    {
                        await DisplayAlert("Need Camera", "Gunna need", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Contacts))
                        status = results[Permission.Contacts];
                }

                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Camera Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                return;
            }
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
           await GetLocationPermissionAsync();
        }
    }
}
