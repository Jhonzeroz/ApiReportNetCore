using System;
using System.Collections.Generic;

namespace ReportApis.Models
{
    public partial class Reporte
    {
        public Reporte()
        {
            OInverseUserReporte = new HashSet<Reporte>();
        }

        public int IdReporte { get; set; }
        public int? UserReporte { get; set; }
        public int? TipoReporte { get; set; }
        public int? EstadoReporte { get; set; }
        public string Observacion { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual EstadoReporte OEstadoReporte { get; set; }
        public virtual TipoReporte OTipoReporte { get; set; }
        public virtual Reporte OUserReporte { get; set; }
        public virtual ICollection<Reporte> OInverseUserReporte { get; set; }
    }
}
