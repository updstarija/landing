//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LandingPage.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Seccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seccion()
        {
            this.SubSeccion = new HashSet<SubSeccion>();
        }
    
        public int id { get; set; }
        public string titulo { get; set; }
        public string subTitulo { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public int numero { get; set; }
        public bool estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubSeccion> SubSeccion { get; set; }
    }
}
