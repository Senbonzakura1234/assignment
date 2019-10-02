using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";

        public Register()
        {
            this.InitializeComponent();
        }

        private void RegisterTrigger(object sender, RoutedEventArgs e)
        {
            if (this.Gender.SelectedItem != null)
            {
                var member = new Member
                {
                    firstName = this.Firstname.Text,
                    lastName = this.Lastname.Text,
                    email = this.Email.Text,
                    address = this.Address.Text,
                    avatar = this.Avatar.Text,
                    birthday = this.Birthday.Date.ToString("yyyy-mm-dd"),
                    introduction = this.Introduction.Text,
                    phone = this.Phone.Text,
                    password = this.Password.Password,
                    gender = this.Gender.SelectedItem.ToString() == "Male" ? "1" : "0"
                };


                var rePass = this.RePassword.Password;
                if (string.IsNullOrWhiteSpace(member.password) ||
                    string.IsNullOrWhiteSpace(member.email) ||
                    string.IsNullOrWhiteSpace(member.firstName) ||
                    string.IsNullOrWhiteSpace(member.lastName) ||
                    string.IsNullOrWhiteSpace(member.phone) ||
                    rePass != member.password)
                {
                    DialogService.ShowToast(string.Empty, "Form invalid");
                    return;
                }
                else
                {
                    var service = new MemberServiceImp();
                    var responseMember = service.Register(member);
                    Debug.WriteLine(responseMember.email);
                }
            }
            else
            {
                DialogService.ShowToast(string.Empty, "Form invalid");
                return;
            }
        }
    }
}
