using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BE_VWPartida_Centro_Costo
    {
        public int IdPartidaCentro { get; set; }
        public int IdPartida { get; set; }
        public int IdGastoCentroCosto { get; set; }
        public string Codigo { get; set; }
        public int IdEstado { get; set; }
        public DateTime deFechReg { get; set; }
    }
}
