using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Domain.Common
{
    public class EntityBase 
    {

        public Guid Id { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        public bool  IsDeleted { get; set; } = false;



    }
}
