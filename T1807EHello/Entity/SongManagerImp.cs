using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Windows.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace T1807EHello.Entity
{
    internal class SongManagerImp : ISongManager
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        private const string LoginUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        private const string InformationUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        private const string UploadUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/post-free";

        public Song Upload(Song member)
        {
            var httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                "application/json");
            var responseContent = httpClient.PostAsync(UploadUrl, content).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Song>(responseContent); 
        }

        public ValidateData Validation(Song song)
        {
            var songValidateData = new ValidateData
            {
                
                name = !string.IsNullOrWhiteSpace(song.name) 
                    ? song.name.Length <= 50 ? "Name is Valid" : "Name must less then 50 characters"
                    : "Name is required",
                thumbnail = !string.IsNullOrWhiteSpace(song.thumbnail) ? "Thumbnail is valid" : "Thumbnail is required",
                link = !string.IsNullOrWhiteSpace(song.link)
                    ? song.link.EndsWith(".mp3") ? "Link is valid" : "Link must end with .mp3"
                    : "Link is required",
                valid = song.name != null &&
                        song.thumbnail != null &&
                        song.link != null && song.name.Length <= 50 && song.link.EndsWith(".mp3")
            };


            return songValidateData;
        }

        public string GetDataFromServer(string songListUrl)
        {
            var client = new HttpClient();
            var responseContent = client.GetAsync(songListUrl).Result.Content.ReadAsStringAsync().Result;
            return responseContent;
        }
    }
}
