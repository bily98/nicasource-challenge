using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicasourceChallenge.Core.Dtos
{
    public class FileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Size { get; set; }
        public string Format { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
    }
}
