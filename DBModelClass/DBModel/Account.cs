//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBModelClass.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MasterID { get; set; }
        public int DepartID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Memo { get; set; }
    }
}