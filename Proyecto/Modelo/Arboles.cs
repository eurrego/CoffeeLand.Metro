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
    
    public partial class Arboles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Arboles()
        {
            this.MovimientosArboles = new HashSet<MovimientosArboles>();
        }
    
        public int idArboles { get; set; }
        public short idLote { get; set; }
        public byte idTIpoArbol { get; set; }
        public int Cantidad { get; set; }
    
        public virtual Lote Lote { get; set; }
        public virtual TipoArbol TipoArbol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovimientosArboles> MovimientosArboles { get; set; }
    }
}