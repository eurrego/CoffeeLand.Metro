//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class AbonoCompra
    {
        public int idAbonoCompra { get; set; }
        public int idCompra { get; set; }
        public decimal Valor { get; set; }
        public System.DateTime Fecha { get; set; }
    
        public virtual Compra Compra { get; set; }
    }
}
