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
    
    public partial class Produccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produccion()
        {
            this.MovimientoProduccion = new HashSet<MovimientoProduccion>();
        }
    
        public int idProduccion { get; set; }
        public short idLote { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public string EstadoProduccion { get; set; }
    
        public virtual Lote Lote { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovimientoProduccion> MovimientoProduccion { get; set; }
    }
}
