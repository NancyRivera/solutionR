using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BE_Cliente
    {
        public int IdCliente { get; set; }
        public string DocIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public int Estado { get; set; }
        public string NombreComercial { get; set; }

        public int IdTipoCliente { get; set; }
        
        public int IdTipoDocumento { get; set; }
       
        public string DireccionFactura { get; set; }
    }

}
