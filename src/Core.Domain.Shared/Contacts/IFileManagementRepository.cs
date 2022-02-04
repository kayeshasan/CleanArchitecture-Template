using Core.Domain.Shared.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Contacts
{
    public interface IFileManagementRepository
    {
        Task<Response<string>> UploadFile(IFormFile file, string directory);
    }
}
