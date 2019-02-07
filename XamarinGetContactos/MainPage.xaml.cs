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
            funCargar();
        }
        async void funCargar()
        {

            try
            {

                var contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();
                
                lvContactos.ItemsSource = contacts;
            }
            catch (Exception ex)
            {
               await DisplayAlert("Error",ex.Message.ToString(),"OK");
            }
        }
    }
}
