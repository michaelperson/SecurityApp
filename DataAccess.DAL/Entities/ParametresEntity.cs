using DataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Entities
{
    public class ParametresEntity : IEntities<int>
    {
        public int ID { get; set; }
    }
}
