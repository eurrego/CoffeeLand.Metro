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
    
    public partial class DeudaPersona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeudaPersona()
        {
            this.AbonoDeuda = new HashSet<AbonoDeuda>();
            this.RegistroPago_DeudaPersona = new HashSet<RegistroPago_DeudaPersona>();
        }
    
        public int idDeudaPersona { get; set; }
        public string DocumentoPersona { get; set; }
        public decimal Valor { get; set; }
        public System.DateTime Fecha { get; set; }
        public string EstadoCuenta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbonoDeuda> AbonoDeuda { get; set; }
        public virtual Persona Persona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroPago_DeudaPersona> RegistroPago_DeudaPersona { get; set; }
    }
}
