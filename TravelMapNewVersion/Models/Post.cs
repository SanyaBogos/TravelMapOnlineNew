
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
    
public partial class Post
{

    public Post()
    {

        this.Post1 = new HashSet<Post>();

        this.PostFiles = new HashSet<PostFile>();

    }


    public System.Guid PostId { get; set; }

    public System.Guid UserId { get; set; }

    public string Text { get; set; }

    public System.Guid TypeId { get; set; }

    public System.Guid ParentId { get; set; }

    public System.DateTime Time { get; set; }

    public Nullable<System.Guid> TravelId { get; set; }



    public virtual ICollection<Post> Post1 { get; set; }

    public virtual Post Post2 { get; set; }

    public virtual Travel Travel { get; set; }

    public virtual UserProfile UserProfile { get; set; }

    public virtual PostType PostType { get; set; }

    public virtual ICollection<PostFile> PostFiles { get; set; }

}

}
