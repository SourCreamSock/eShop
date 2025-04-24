using Microsoft.Extensions.Configuration;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;

namespace Web.Application.Services
{
    public class PictureService : IPictureService
    {        
        private readonly string baseUri;
        public PictureService(IConfiguration configuration)
        {
            baseUri = configuration["BaseUri"] ?? throw new InvalidOperationException();
        }
        public string FullPathToPicture(string value)
        {
            return baseUri + "Pictures/" + value + ".jpg";
        }
    }
}
