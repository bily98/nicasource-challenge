using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NicasourceChallenge.Core.Dtos
{
    public class BlobDto
    {
        public IFormFile? File { get; set; }
        public string? Description { get; set; }
    }
}
