using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsManagement.Models
{
    public partial class Tutorial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
    }

    public partial class TutorialResponseModel
    {
        public int Id { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
