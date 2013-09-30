using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class SocialResponse<T>
    {
        public T User { get; set; }
        public bool Result { get; set; }
    }
}
