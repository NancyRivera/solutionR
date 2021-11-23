using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BE_VWSubpartida
    {
        public int IdSubpartida { get; set; }
        public string vcSubpartida { get; set; }
        public string vcDescripcion { get; set; }
        public int InPartida { get; set; }
        public int InEstado { get; set; }
    }
}
