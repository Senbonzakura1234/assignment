using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Windows.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace T1807EHello.Entity
{
    internal class MemberServiceImp : IMemberService
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        private const string LoginUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        private const string InformationUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        public string Login(string email, string password)
        {
            var memberLogin = new MemberLogin
            {
                email = email,
                password = password
            };
            // validate phía client.
            var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin),
                Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var responseContent = client.PostAsync(LoginUrl, dataContent).Result.Content.ReadAsStringAsync().Result;
            var jsonJObject = JObject.Parse(responseContent);
            Debug.WriteLine(jsonJObject["token"]);

            return jsonJObject["token"].ToString();
        }
        public Member Register(Member member)
        {
            var httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                "application/json");
            var responseContent = httpClient.PostAsync(ApiUrl, content).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Member>(responseContent); 
        }
        public Member GetInformation(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var responseContent = client.GetAsync(InformationUrl).Result.Content.ReadAsStringAsync().Result;
            return responseContent == null ? null : JsonConvert.DeserializeObject<Member>(responseContent);
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
