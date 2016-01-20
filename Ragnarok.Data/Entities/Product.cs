using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Data.Entities
{
    public class Product
    {
        public virtual long Id { get; set; }
        public virtual string Description { get; set; }
    }
}
