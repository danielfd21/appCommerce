using ApiMiProyecto.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMiProyecto.Configuracion
{
    public class BD : DbContext
    { 

        public BD(DbContextOptions<BD> options) : base(options)
        {

        }

       

    }
}
