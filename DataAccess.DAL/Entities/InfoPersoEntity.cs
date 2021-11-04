using DataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL.Entities
{
    public class InfoPersoEntity : IEntities<int>
    {
        public int ID{get;set;}
        public bool IsMarried { get; set; }
        public string CompteEnBanque { get; set; }
        public int NbEnfants { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public int IdUser { get; set; }
    }
}
