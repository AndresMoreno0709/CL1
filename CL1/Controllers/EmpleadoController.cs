using CL1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace CL1.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmpleadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IEnumerable<Cargo> listarCargo()
        {
            List<Cargo> lista = new List<Cargo>();
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_listarCargo", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    lista.Add(new Cargo
                    {
                        idcargo = reader.GetInt32(0),
                        desCargo = reader.GetString(1),
                    });
                }
                cn.Close();
            }
            return lista;
        }

        IEnumerable<Distrito> listarDistrito()
        {
            List<Distrito> lista = new List<Distrito>();
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_listar_distrito", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    lista.Add(new Distrito
                    {
                        idDistrito = reader.GetInt32(0),
                        nomDistrito = reader.GetString(1),
                    });
                }
                cn.Close();
            }
            return lista;
        }

        IEnumerable<Empleado> listarEmpleado()
        {
            List<Empleado> lista = new List<Empleado>();
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_listarEmpelado", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    lista.Add(new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        ApeEmpleado = reader.GetString(1),
                        NomEmpleado = reader.GetString(2),
                        FecNac = reader.GetDateTime(3),
                        DirEmpleado = reader.GetString(4),
                        idDistrito = reader.GetInt32(5),
                        fonoEmpleado = reader.GetString(6),
                        idCargo = reader.GetInt32(7),
                        FecContrata = reader.GetDateTime(8),
                    });
                }
                cn.Close();
            }
            return lista;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.cargo = new SelectList(await Task.Run(() => listarCargo()), "idcargo", "desCargo");
            ViewBag.distrito = new SelectList(await Task.Run(() => listarDistrito()), "idDistrito", "nomDistrito");
            return View(new Empleado());

        }

        [HttpPost]
        public async Task<IActionResult> create(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return View(empleado);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_empleado", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idempleado", empleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("@nomempleado", empleado.NomEmpleado);
                    cmd.Parameters.AddWithValue("@apeEmpleado", empleado.ApeEmpleado);
                    cmd.Parameters.AddWithValue("@fecNac", empleado.FecNac);
                    cmd.Parameters.AddWithValue("@dirEmpleado", empleado.DirEmpleado);
                    cmd.Parameters.AddWithValue("@iddistrito", empleado.idDistrito);
                    cmd.Parameters.AddWithValue("@fonoEmpleado", empleado.fonoEmpleado);
                    cmd.Parameters.AddWithValue("@idcargo", empleado.idCargo);
                    cmd.Parameters.AddWithValue("@fecContrata", empleado.FecContrata);
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha creado {cantidad} empleado";

                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            ViewBag.mensaje = mensaje;
            ViewBag.cargo = new SelectList(await Task.Run(() => listarCargo()), "idcargo", "desCargo",empleado.idCargo);
            ViewBag.distrito = new SelectList(await Task.Run(() => listarDistrito()), "idDistrito", "nomDistrito",empleado.idDistrito);

            return View(empleado);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Empleado? reg = listarEmpleado().Where(c=>c.IdEmpleado==id).FirstOrDefault();

            if (reg == null) {
                return RedirectToAction("Index");
            }

            ViewBag.cargo = new SelectList(await Task.Run(() => listarCargo()), "idcargo", "desCargo", reg.idCargo);
            ViewBag.distrito = new SelectList(await Task.Run(() => listarDistrito()), "idDistrito", "nomDistrito", reg.idDistrito);
            return View(reg);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return View(empleado);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_empleado", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idempleado", empleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("@nomempleado", empleado.NomEmpleado);
                    cmd.Parameters.AddWithValue("@apeEmpleado", empleado.ApeEmpleado);
                    cmd.Parameters.AddWithValue("@fecNac", empleado.FecNac);
                    cmd.Parameters.AddWithValue("@dirEmpleado", empleado.DirEmpleado);
                    Distrito? dis = listarDistrito().Where(p => p.idDistrito ==empleado.idDistrito).FirstOrDefault();
                    cmd.Parameters.AddWithValue("@iddistrito", dis.idDistrito);
                    cmd.Parameters.AddWithValue("@fonoEmpleado", empleado.fonoEmpleado);
                    Cargo? cargo = listarCargo().Where(c => c.idcargo ==empleado.idCargo).FirstOrDefault();
                    cmd.Parameters.AddWithValue("@idcargo",cargo.idcargo);
                    cmd.Parameters.AddWithValue("@fecContrata", empleado.FecContrata);
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {cantidad} un empleado";

                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            ViewBag.mensaje = mensaje;
            ViewBag.cargo = new SelectList(await Task.Run(() => listarCargo()), "idcargo", "desCargo", empleado.idCargo);
            ViewBag.distrito = new SelectList(await Task.Run(() => listarDistrito()), "idDistrito", "nomDistrito", empleado.idDistrito);

            return View(empleado);
        }

        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => listarEmpleado()));
        }

    }
}
