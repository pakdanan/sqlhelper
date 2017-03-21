namespace SQLHelper.Entities
{
    public class Party :BaseEntity
    {
        public string MobilePhone { get; set; } = "";
        public string Email { get; set; } = "";
        public PartyAddress Address { get; set; }
    }

}
