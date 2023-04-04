using System;
using System.Data;

namespace Logica
{
    internal class Models
    {
        internal class Usuario
        {
            public int UsuarioID { get; internal set; }
            public string UsuarioTelefono { get; internal set; }
            public string UsuarioCedula { get; internal set; }
            public string UsuarioCorreo { get; internal set; }
            public string UsuarioDireccion { get; internal set; }
            public object MiRolTipo { get; internal set; }
            public string UsuarioNombre { get; internal set; }
            public string UsuarioContrasennia { get; internal set; }
            

            internal bool Agregar()
            {
                throw new NotImplementedException();
            }

            internal bool ConsultarPorCedula()
            {
                throw new NotImplementedException();
            }

            internal bool ConsultarPorEmail()
            {
                throw new NotImplementedException();
            }

            internal bool ConsultarPorID()
            {
                throw new NotImplementedException();
            }

            internal Usuario ConsultarPorIDRetornaUsuario()
            {
                throw new NotImplementedException();
            }

            internal bool Editar()
            {
                throw new NotImplementedException();
            }

            internal bool Eliminar()
            {
                throw new NotImplementedException();
            }

            internal DataTable ListarActivos()
            {
                throw new NotImplementedException();
            }

            internal DataTable ListarInactivos()
            {
                throw new NotImplementedException();
            }
        }

        internal class Usuario_Rol
        {
            internal DataTable Listar()
            {
                throw new NotImplementedException();
            }
        }
    }
}