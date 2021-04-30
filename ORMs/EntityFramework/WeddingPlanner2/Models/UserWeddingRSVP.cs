using System;

namespace WeddingPlanner2.Models
{
    public class UserWeddingRSVP
    {
        public int UserWeddingRSVPId {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // foriegn Keys
        public int UserId {get;set;}
        public int WeddingId {get;set;}
        // navigation properties
        public User User {get;set;} // user attending wedding
        public Wedding Wedding {get;set;} // which wedding user is attending
    }
}