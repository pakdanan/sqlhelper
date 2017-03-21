namespace SQLHelper.Entities
{
    public class PartyAddress:BaseEntity
    {

        public int PartyID { get; set; } = 0;
        public string Address { get; set; } = "";
        public string PostCode { get; set; } = "";
        public string Telephone { get; set; } = "";
        public int RegionID { get; set; } = 0;

    }
}
