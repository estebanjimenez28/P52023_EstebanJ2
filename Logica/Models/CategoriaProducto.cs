﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class CategoriaProducto
    {

        public int CategoriaID { get; set; }
        public string CategoriaDescripcion { get; set; }

        public DataTable Listar()
        {
            DataTable R = new DataTable();
            //aca va el codigo funcional que carga la lista de categorias

            return R;

        }
    }
}
