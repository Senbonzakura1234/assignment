using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();

            var service = new MemberServiceImp();
            var token = service.ReadTokenFile("token.txt");
            Debug.WriteLine(token);

            var responseMember = service.GetInformation(token);
            if (responseMember != null)
            {
                Debug.WriteLine(responseMember.email);
            }
        }
        private void LoginTrigger(object sender, RoutedEventArgs e)
        {
            var service = new MemberServiceImp();
            var token = service.Login(this.Email.Text, this.Password.Password);
            var tokenFile = service.CreateTokenFile(token);
            Debug.WriteLine(tokenFile.Path);

            //Email = "dungpath1805040@fpt.edu.vn";
            //Password = "12345";
        }
    }
}
