﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Compra
    {
        //propiedades
        public int CompraID { get; set; }
        public DateTime CompraFecha { get; set;}

        public int CompraNumero { get; set;}
        public string CompraNotas { get; set; }
        public bool Activo { get; set; }

        //composiciones simples

        Usuario MiUsuario { get; set; } 
       public Proveedor MiProveedor { get; set; }   
        public TipoCompra MiTipoCompra { get; set; }    

        //composiciones en lista o complejas

        public List<CompraDetalle> ListaDetalles { get; set; }

        //constructor
        public Compra()
        {
            MiUsuario = new Usuario();  
            MiProveedor = new Proveedor();  
            MiTipoCompra = new TipoCompra();
            ListaDetalles = new List<CompraDetalle>();
        }

    }
}
