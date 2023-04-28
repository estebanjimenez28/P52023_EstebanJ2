using Logica.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Proveedor
    {
        //propiedades simples
        public int ProveedorID { get; set; }
        public string ProveedorNombre { get; set; }
        public string ProveedorCedula { get; set; }
        public string ProveedorEmail { get; set; }
        public string ProveedorDireccion { get; set; }

        public string ProveedorNotas { get; set; }

        public bool Activo { get; set; }

        //propiedaes compuestas 
        public TipoProveedor MiRolTipo { get; set; }

        public Proveedor()
        {
 
            MiRolTipo = new TipoProveedor();
        }



        //Funciones y metodos 
        public bool Agregar()
        {

            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.ProveedorEmail));


            MiCnn.ListaDeParametros.Add(new SqlParameter("@Nombre", this.ProveedorNombre));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.ProveedorCedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.ProveedorDireccion));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Notas", this.ProveedorNotas));
 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@ProveedorTipo", this.MiRolTipo.ProveedorTipo));

            //pasos 1.6.3 y 1.6.4
            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPProveedorAgregar");

            //paso 1.6.5
            if (resultado > 0)
            {
                R = true;
            }

            return R;
        }
        public bool Editar()
        {
            bool R = false;

            return R;
        }
        public bool Eliminar()
        {
            bool R = false;

            return R;
        }
        public bool ConsultarPorCedula()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            //agregamos el parametro de cedula 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.ProveedorCedula));

            DataTable consulta = new DataTable();
            //paso 1.3.3 y 1.3.4
            consulta = MiCnn.EjecutarSELECT("SPProveedorConsultarPorCedula");

            //paso 1.3.5
            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }
        public bool ConsultarPorEmail()
        {
            bool R = false;

            //paso 1.4.1 y 1.4.2
            Conexion MiCnn = new Conexion();

            //agregamos el parametro de correo 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.ProveedorEmail));

            DataTable consulta = new DataTable();
            //paso 1.4.3 y 1.4.4
            consulta = MiCnn.EjecutarSELECT("SPProveedorConsultarPorEmail");

            //paso 1.4.5
            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }
        public bool ConsultarPorID()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.ProveedorID));

            //necesito un datatable para capturar la info del usuario 
            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPProveedorConsultarPorID");

            if (dt != null && dt.Rows.Count > 0)
            {
                R = true;
            }
            return R;
        }
        public Proveedor ConsultarPorIDRetornaProveedor()
        {
            Proveedor R = new Proveedor();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.ProveedorID));

            //necesito un datatable para capturar la info del usuario 
            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPProveedorConsultarPorID");

            if (dt != null && dt.Rows.Count > 0)
            {
                //esta consulta debería tener solo un registro, 
                //se crea un objeto de tipo datarow para capturar la info 
                //contenida en index 0 del dt (datatable)
                DataRow dr = dt.Rows[0];

                R.ProveedorID = Convert.ToInt32(dr["ProveedorID"]);
                R.ProveedorNombre = Convert.ToString(dr["ProveedorNombre"]);

                R.ProveedorCedula = Convert.ToString(dr["ProveedorCedula"]);
                R.ProveedorEmail = Convert.ToString(dr["ProveedorEmail"]);
                R.ProveedorNotas = Convert.ToString(dr["ProveedorNotas"]);
                R.ProveedorDireccion = Convert.ToString(dr["ProveedorDireccion"]);

          

                //composiciones
                R.MiRolTipo.ProveedorTipo = Convert.ToInt32(dr["ProveedorTipoID"]);
                R.MiRolTipo.ProveedorTipoDescripcion = Convert.ToString(dr["ProveedorTipoDescripcion"]);

            }


            return R;
        }

        public DataTable Listar(bool verActivos = true, string FiltroBusqueda = "")
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", verActivos));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FiltroBusqueda", FiltroBusqueda));

            R = MiCnn.EjecutarSELECT("SPListaProveedor");

            return R;
        }
        public DataTable ListarActivos(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            //en este caso como el SP tiene un parámetro, debemos por lo tanto definir ese parámetro 
            //en la lista de parámetros del objeto de conexion 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", true));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));

            R = MiCnn.EjecutarSELECT("SPListaProveedor");

            return R;
        }

        public DataTable ListarInactivos(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();
            Conexion MiCnn = new Conexion();

            //en este caso como el SP tiene un parámetro, debemos por lo tanto definir ese parámetro 
            //en la lista de parámetros del objeto de conexion 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", false));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));


            R = MiCnn.EjecutarSELECT("SSPListaProveedor");

            return R;
        }

        public Proveedor ValidarProveedor()
        {
            Proveedor R = new Proveedor();

            Conexion MiCnn = new Conexion();

            
            //necesito un datatable para capturar la info del proveedor
            DataTable dt = new DataTable();

      

            if (dt != null && dt.Rows.Count > 0)
            {
         
                DataRow dr = dt.Rows[0];

                R.ProveedorID = Convert.ToInt32(dr["ProveedorID"]);
                R.ProveedorNombre = Convert.ToString(dr["ProveedorNombre"]);

                R.ProveedorCedula = Convert.ToString(dr["ProveedorCedula"]);
                R.ProveedorEmail = Convert.ToString(dr["ProveedorEmail"]);
                R.ProveedorNotas = Convert.ToString(dr["ProveedorNotas"]);
                R.ProveedorDireccion = Convert.ToString(dr["ProveedorDireccion"]);

                

                //composiciones
                R.MiRolTipo.ProveedorTipo = Convert.ToInt32(dr["ProveedorTipoID"]);
                R.MiRolTipo.ProveedorTipoDescripcion = Convert.ToString(dr["ProveedorTipoDescripcion"]);

            }


            return R;
        }








    }
}