
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace TravelMap.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Travel
{

    public Travel()
    {

        this.Posts = new HashSet<Post>();

    }


    public Nullable<System.Guid> CountryId { get; set; }

    public Nullable<System.DateTime> StartDate { get; set; }

    public Nullable<System.DateTime> EndDate { get; set; }

    public System.Guid UserId { get; set; }

    public System.Guid TravelId { get; set; }



    public virtual Country Country { get; set; }

    public virtual ICollection<Post> Posts { get; set; }

    public virtual UserProfile UserProfile { get; set; }

}

}
