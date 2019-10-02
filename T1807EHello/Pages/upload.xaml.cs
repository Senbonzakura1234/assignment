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
    public sealed partial class Upload : Page
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";

        public Upload()
        {
            this.InitializeComponent();
        }

        private void UploadTrigger(object sender, RoutedEventArgs e)
        {
            var song = new Song
            {
                name = this.Name.Text,
                description = this.Description.Text,
                singer = this.Singer.Text,
                author = this.Author.Text,
                thumbnail = this.Thumbnail.Text,
                link = this.Link.Text
            };
            Debug.WriteLine(song.name);
            Debug.WriteLine(song.description);
            Debug.WriteLine(song.singer);
            Debug.WriteLine(song.author);
            Debug.WriteLine(song.thumbnail);
            Debug.WriteLine(song.link);
          
            var songManager = new SongManagerImp();
            var songValidateData = songManager.Validation(song);
            this.VName.Text = songValidateData.name;
            this.VThumbnail.Text = songValidateData.thumbnail;
            this.VLink.Text = songValidateData.link;

            if (songValidateData.valid == false)
            {
                DialogService.ShowToast(string.Empty, "Form invalid");
                return;
            }
            else
            {
                var responseContent = songManager.Upload(song);
                Debug.WriteLine(responseContent.name);
            }
        }
    }
}
