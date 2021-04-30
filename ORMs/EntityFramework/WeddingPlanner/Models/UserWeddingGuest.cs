namespace WeddingPlanner.Models
{
    // the join table for Many User : Many Wedding relationship
    // Many Users can attend many Weddings
    // Many Weddings can have many Users as guests
    public class UserWeddingGuest // Many User : Many Wedding
    {
        public int UserWeddingGuestId {get;set;}
        // Foriegn Keys
        public int UserId {get;set;}
        public int WeddingId {get;set;}
        // navigation properties
        public User User {get;set;} // User who is guest of the Wedding
        public Wedding Wedding {get;set;} // The Wedding that the User is a guest of
    }
}