using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CL1.Models
{
    public class Clientes
    {
        [Required][Display(Name = "Código")] public string idCliente { get; set; } = string.Empty;
        [Required][Display(Name = "Cliente")] public string nomCliente { get; set; } = string.Empty;
        [Required][Display(Name = "Dirección")] public string dirCliente { get; set; } = string.Empty;
        [Required][Display(Name = "País")] public string idpais { get; set; } = string.Empty;
        [Required][Display(Name = "Teléfono")] public string fonoCliente { get; set; } = string.Empty;

    }
}
