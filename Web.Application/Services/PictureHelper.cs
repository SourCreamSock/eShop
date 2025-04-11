using Microsoft.Extensions.Configuration;

namespace Catalog.API.Services
{
    public class PictureHelper : IPictureHelper
    {        
        private readonly string baseUri;
        public PictureHelper(IConfiguration configuration)
        {
            baseUri = configuration.GetValue<string>("BaseUri");
        }
        public string FullPathToPicture(string value)
        {
            return baseUri + "Pictures/" + value + ".jpg";
        }
    }
}
