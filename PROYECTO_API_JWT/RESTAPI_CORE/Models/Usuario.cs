using System;
using System.Collections.Generic;

namespace ReportApis.Models
{
    public partial class Usuario
    {
        public int IdUser { get; set; }
        public string NombreUser { get; set; }
        public string CorreoUser { get; set; }
        public string PassUser { get; set; }
        public int? EstadoUser { get; set; }

        public virtual Estado EstadoUserNavigation { get; set; }
    }
}
