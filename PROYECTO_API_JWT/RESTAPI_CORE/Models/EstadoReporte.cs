using System;
using System.Collections.Generic;

namespace ReportApis.Models
{
    public partial class EstadoReporte
    {
        public EstadoReporte()
        {
            Reportes = new HashSet<Reporte>();
        }

        public int IdEstreporte { get; set; }
        public string NombreEstreporte { get; set; }

        public virtual ICollection<Reporte> Reportes { get; set; }
    }
}
