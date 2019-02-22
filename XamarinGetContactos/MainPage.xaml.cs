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
            valPermiso();


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

                    status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);


                //if (status != PermissionStatus.Granted)
                //{
                //    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Contacts))
                //    {
                //        await DisplayAlert("Need location", "Gunna need that location", "OK");
                //    }

                //    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Contacts });
                //    status = results[Permission.Contacts];
                //}

                if (status == PermissionStatus.Granted)
                {
                    await DisplayAlert("Mensaje","Permiso OK","OK");
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }

                //solicitar permiso
                status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts))[Permission.Contacts];
                
               // await DisplayAlert("Results", status.ToString(), "OK");

            }
            catch (Exception ex)
            {

                await DisplayAlert("Mensaje", ex.Message.ToString(), "OK");
            }
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
           await GetLocationPermissionAsync();
        }

        private async void valPermiso()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);

            if (status == PermissionStatus.Granted)
            {
                await GetContacs();
            }
            else
            {
                //solicitar permiso
                status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts))[Permission.Contacts];
                if (status == PermissionStatus.Granted)
                {
                    await GetContacs();
                }
                else
                {
                    await DisplayAlert("Mensaje", "No ha Permitido el acceso a sus contactos", "OK");
                }

            }
        }



        
    }
}
