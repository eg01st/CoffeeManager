//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeeManager.Api
{
    using System;
    using System.Collections.Generic;
    
    public partial class UtilizedCup
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public int CupTypeId { get; set; }
        public System.DateTime DateTime { get; set; }
        public int CoffeeRoomNo { get; set; }
    
        public virtual CupType CupType { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
