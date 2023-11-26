using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IImageStorage
    {
        Task<string> SaveImage(Stream imageStream, string type);

        string UriFor(string imageId);
    }
}
