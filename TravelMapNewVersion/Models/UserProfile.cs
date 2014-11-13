//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelMap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.ChatMembers = new HashSet<ChatMember>();
            this.Followers = new HashSet<Follower>();
            this.Followers1 = new HashSet<Follower>();
            this.GroupMessages = new HashSet<GroupMessage>();
            this.Likes = new HashSet<Like>();
            this.Messages = new HashSet<Message>();
            this.Messages1 = new HashSet<Message>();
            this.Posts = new HashSet<Post>();
            this.Rooms = new HashSet<Room>();
            this.Travels = new HashSet<Travel>();
            this.security_Roles = new HashSet<security_Roles>();
        }
    
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<ChatMember> ChatMembers { get; set; }
        public virtual ICollection<Follower> Followers { get; set; }
        public virtual ICollection<Follower> Followers1 { get; set; }
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> Messages1 { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Travel> Travels { get; set; }
        public virtual ICollection<security_Roles> security_Roles { get; set; }
    }
}
