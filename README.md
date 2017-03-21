# sqlhelper
Another helper (including mapper and usage sample) library for database interaction. It is based on http://aapl.codeplex.com , but differ completely in GenericMapper.cs, cause the original version only cope with simple DTO (Data Transfer Object), meanwhile there is a need to map between field from SQL Join to property of Composite Object, eg.:

    string SQLquery = @"SELECT    p.*
	                             ,pa.[ID]                 as Address_ID
                               ,pa.[PartyID]            as Address_PartyID
                               ,pa.[Address]            as Address_FullAddress
                               ,pa.[PostCode]           as Address_PostCode
                               ,pa.[Telephone]          as Address_Telephone
            FROM Party p JOIN PartyAddress pa ON p.ID=pa.PartyID 
            WHERE p.ID=@partyID ";
            
Map to :

    public class PartyAddress :Party
    {
        public Address Address { get; set; }
    }

Where :

    public class Address
    {
        public int ID { get; set; } = 0;
        public int PartyID { get; set; } = 0;
        public string FullAddress { get; set; } = "";
        public string PostCode { get; set; } = "";
        public string Telephone { get; set; } = "";
    }

