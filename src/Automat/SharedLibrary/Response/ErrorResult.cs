using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Response
{
    public class ErrorResult
    {
        public Dictionary<string, List<string>> Validation { get; set; }
        public string Message { get; set; }

        public ErrorResult(string error)
        {
            this.Message = error;
        }

        public ErrorResult(Dictionary<string, List<string>> errors, string error="Validation Fail!")
        {
            this.Validation = errors;
            this.Message = error;
        }
    }
}
