namespace SQLHelper.Entities
{
    public class Address:BaseEntity
    {

        public int PartyID { get; set; } = 0;
        public string FullAddress { get; set; } = "";
        public string PostCode { get; set; } = "";
        public string Telephone { get; set; } = "";
        public int RegionID { get; set; } = 0;
        public Region Region { get; set; }

    }
}
