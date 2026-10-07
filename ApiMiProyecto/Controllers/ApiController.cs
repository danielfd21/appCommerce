using ApiMiProyecto.Model;
using ApiMiProyecto.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly CommerceService _commerceService;

        public ApiController(CommerceService commerceService)
        {
            _commerceService = commerceService;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsv(IFormFile archivo)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("LLEGO UNA PETICION A /upload");
            Console.WriteLine("Archivo: " + archivo?.FileName);
            Console.WriteLine("Tamaño: " + archivo?.Length);
            Console.WriteLine("=================================");

            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("El archivo no puede estar vacío.");
            }

            await _commerceService.ProcesarArchivo(archivo);

            return Ok("Archivo procesado correctamente.");
        }


        [HttpPost("process")]
        public async Task<IActionResult> ProcesarFecha(
        [FromBody] DateOnly fecha)
        {
            int cantidad =
                await _commerceService.ProcesarFecha(fecha);

            return Ok(new
            {
                cantidad = cantidad
            });
        }



    }
}
