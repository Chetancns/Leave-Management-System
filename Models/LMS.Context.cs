//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMSTRYONE.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LMSEntities : DbContext
    {
        public LMSEntities()
            : base("name=LMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Register> Registers { get; set; }

        public System.Data.Entity.DbSet<LMSTRYONE.ViewModel.ApplyLeaveViewModel> ApplyLeaveViewModels { get; set; }
    }
}
