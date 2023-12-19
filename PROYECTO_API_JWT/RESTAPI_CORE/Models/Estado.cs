using System;
using System.Collections.Generic;

namespace ReportApis.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdEstado { get; set; }
        public string NombreEstado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
