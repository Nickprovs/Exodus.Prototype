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
    
    public partial class SourceInstance
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int WallId { get; set; }
    
        public virtual Source Source { get; set; }
        public virtual Wall Wall { get; set; }
    }
}
