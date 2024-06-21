using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Imgur.API;
using Logic.dtos;

namespace Logic
{
    public class PhotoPublisherService
    {
        private string ClientId = "99e309afc41ca46";
        private string ClientSecret = "c717d66bd33daab56bdbe1d167857f938073bd30";
        public string UploadImage(byte[] imageData)
        {
            var client = new ApiClient(ClientId, ClientSecret);
            var imageEndpoint = new ImageEndpoint(client, new HttpClient());
            using (var imageStream = new MemoryStream(imageData))
            {
                try
                {
                    IImage imageUpload = imageEndpoint.UploadImageAsync(imageStream).GetAwaiter().GetResult();
                    return imageUpload.Link;
                }
                catch (ImgurException ex)
                {
                    
                    Console.WriteLine("An error occurred while uploading the image: " + ex.Message);
                    return ex.Message;
                }
            }
        }
    }

}
