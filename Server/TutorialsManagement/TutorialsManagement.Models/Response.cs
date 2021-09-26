using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsManagement.Models
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data) : this(data, null, true, null, true)
        {
        }
        public Response(T data, string errorCode, bool isSuccessed, string description, bool authorize)
        {
            (Data, ErrorCode, IsSuccessed, Description, Authorize) =
                    (data, errorCode, isSuccessed, description, authorize);
        }
        public T Data { get; set; }
        public string ErrorCode { get; set; }
        public bool IsSuccessed { get; set; }
        public string Description { get; set; }
        public bool Authorize { get; set; }
    }
}
