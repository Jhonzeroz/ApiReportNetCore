using System;
using System.Collections.Generic;

namespace ReportApis.Models
{
    public partial class TipoReporte
    {
        public TipoReporte()
        {
            Reportes = new HashSet<Reporte>();
        }

        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }

        public virtual ICollection<Reporte> Reportes { get; set; }
    }
}
