using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportApis.Models;
using System.Data.SqlClient;
using System.Data;
using RESTAPI_CORE.Modelos;


namespace ReportApis.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]

    public class ReportesController : ControllerBase
    {
        public readonly REPORTESAPPSContext _dbcontext;

        public ReportesController(REPORTESAPPSContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Reporte> lista = new List<Reporte>();
            try
            {
                lista = _dbcontext.Reportes.Include(c => c.OEstadoReporte).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Reporte objeto)
        {

            try
            {
                _dbcontext.Reportes.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Gracias por reportar tu incidencia", result = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Valida por favor la informacion ingresada", result = false });

            
            }
        }
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Reporte objeto)
        {
            Reporte reporteExistente = _dbcontext.Reportes.Find(objeto.IdReporte);

            if (reporteExistente == null)
            {
                return NotFound(new { mensaje = "Reporte no encontrado" });
            }

            try
            {
                reporteExistente.TipoReporte = objeto.TipoReporte ?? reporteExistente.TipoReporte;
                reporteExistente.EstadoReporte = objeto.EstadoReporte ?? reporteExistente.EstadoReporte;
                reporteExistente.Observacion = objeto.Observacion ?? reporteExistente.Observacion;

                _dbcontext.Reportes.Update(reporteExistente);
                _dbcontext.SaveChanges();

                return Ok(new { mensaje = "Reporte actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("Buscar/{numeroReporte:int}")]
        public IActionResult Buscar(int numeroReporte)
        {
            try
            {
                List<Reporte> reportesEncontrados = new List<Reporte>(); 

                string cadenaSQL = "tu_cadena_de_conexion_a_la_base_de_datos";

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_buscar_reporte_por_numero", conexion); 
                    cmd.Parameters.AddWithValue("numeroReporte", numeroReporte);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var reporte = new Reporte
                            {
                                IdReporte = reader.GetInt32("IdReporte"),
                                Observacion = reader.GetString("Observacion")
                            };
                            reportesEncontrados.Add(reporte);
                        }
                    }
                }

                return Ok(reportesEncontrados); // Devuelve la lista de reportes encontrados
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }


    }











}


