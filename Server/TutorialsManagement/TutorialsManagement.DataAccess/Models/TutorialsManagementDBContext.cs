using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsManagement.DataAccess.Models
{
    public partial class TutorialsManagementDBContext : DbContext
    {
        public TutorialsManagementDBContext()
        {

        }

        public TutorialsManagementDBContext(DbContextOptions<TutorialsManagementDBContext> options)
            : base(options)
        {

        }
    }
}
