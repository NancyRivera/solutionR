using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Entidades
{
    public class BE_VWPartidas
    {
        public int IdPartida { get; set; }
        public string vcDescripcion { get; set; }
        public int InEstado { get; set; }
    }
}
