namespace CL1.Models
{
    public class Empleado
    {
public int IdEmpleado { get; set; }
        public string ApeEmpleado { get; set; }=string.Empty;
        public string NomEmpleado { get; set; } = string.Empty;
        public DateTime FecNac { get; set; }
        public  string DirEmpleado { get; set; } = string.Empty;
        public int idDistrito { get; set; }
        public string fonoEmpleado { get; set; } = string.Empty;
        public int idCargo { get; set; }
        public DateTime FecContrata { get; set; }
    }
}
