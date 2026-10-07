namespace ApiMiProyecto.Model
{
    public class Personas
    {

        public String Cedula { get; set; }
        public String Nombre { get; set; }

        public String Apellido { get; set; }

        public int Edad { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public String correo { get; set; }


        public Personas(String cedula, String nombre, String apellido, int edad, DateOnly fechaNacimiento, String correo)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Edad = edad;
            this.FechaNacimiento = fechaNacimiento;
            this.correo = correo;
        }

        public Personas()
        {

        }

    }
}
