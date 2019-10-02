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
        public string Login(string email, string password)
        {
            //var memberLogin = new MemberLogin
            //{
            //    email = email,
            //    password = password
            //};
            //// validate phía client.
            //var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin),
            //    Encoding.UTF8, "application/json");
            //var client = new HttpClient();
            //var responseContent = client.PostAsync(LoginUrl, dataContent).Result.Content.ReadAsStringAsync().Result;
            //var jsonJObject = JObject.Parse(responseContent);
            //Debug.WriteLine(jsonJObject["token"]);

            //return jsonJObject["token"].ToString();
            return null;
        }
        public Song Upload(Song member)
        {
            var httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                "application/json");
            var responseContent = httpClient.PostAsync(UploadUrl, content).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Song>(responseContent); 
        }
        public Song GetInformation(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var responseContent = client.GetAsync(InformationUrl).Result.Content.ReadAsStringAsync().Result;
            return responseContent == null ? null : JsonConvert.DeserializeObject<Song>(responseContent);
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

        public StorageFile CreateTokenFile(string token)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var tokenFile = storageFolder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
            FileIO.WriteTextAsync(tokenFile, token).GetAwaiter().GetResult();
            return tokenFile;
        }

        public string ReadTokenFile(string fileName)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = storageFolder.GetFileAsync(fileName).GetAwaiter().GetResult();
            var token = FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            return token;
        }
    }
}
