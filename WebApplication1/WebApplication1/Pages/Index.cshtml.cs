using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private IHostingEnvironment env;
        public string result;

        public IndexModel(IHostingEnvironment env)
        {
            this.env = env;
        }
        public async Task OnGetAsync()
        {
            var path = env.ContentRootPath;
            path = path + "\\Auth.json";
            FirebaseApp app = null;
            try
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                }, "myApp");
            }
            catch (Exception )
            {
                app = FirebaseApp.GetInstance("myApp");
            }

            var fcm = FirebaseMessaging.GetMessaging(app);
            var token = "AAAAXHZJv9Y:APA91bE683EQ_AROGZQi5T5v2d0Sx8M9-dC6Zc8jKS5NS-DCG8uGJbNSZLGG5dM8EjlWjio-_zzT3usYf07AF2Jut9Dlvq4M6XR5Yu9ClhaSxU6jXqp6xTxLdmq-Tvqxi3hXq_3Uwlkd";
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "My push notification title",
                    Body = "Content for this push notification"
                },
                Data = new Dictionary<string, string>()
                 {
                     { "AdditionalData1", "data 1" },
                     { "AdditionalData2", "data 2" },
                     { "AdditionalData3", "data 3" },
                 },
                Token = token
            };
            //this.result = await fcm.SendAsync(message);
            Console.WriteLine(fcm.SendAsync(message));
        }
    }
}
