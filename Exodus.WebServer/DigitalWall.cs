//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exodus.WebServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class DigitalWall
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DigitalWall()
        {
            this.SpaceSessions = new HashSet<SpaceSession>();
        }
    
        public int Id { get; set; }
    
        public virtual Wall Wall { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpaceSession> SpaceSessions { get; set; }
    }
}
