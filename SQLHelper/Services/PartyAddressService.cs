using SQLHelper.Persistence;
using System.Data.SqlClient;
using SQLHelper.Entities;
using SQLHelper.Helper;

namespace SQLHelper.Services
{
    public class PartyAddressService : ServiceBase
    {
        #region Constants
        #endregion

        #region Properties
        #endregion

        #region Fields

        private PartyAddressPersister partyAddressPersister;

        #endregion


        #region Constructors
        public PartyAddressService()
        {
            partyAddressPersister = new PartyAddressPersister();
        }

        public PartyAddressService(PartyAddressPersister partyAddressPersister)
        {
            this.partyAddressPersister = partyAddressPersister;
        }

        #endregion

        #region Enums


        #endregion

        #region Methods
        public PartyAddress GetPartyAddressByPartyId(int partyID)
        {
            string query = @"SELECT    p.*
	                                  ,pa.[ID]                 as PartyAddress_ID
                                      ,pa.[PartyID]            as PartyAddress_PartyID
                                      ,pa.[Code]               as PartyAddress_Code
                                      ,pa.[Name]               as PartyAddress_Name
                                      ,pa.[Description]        as PartyAddress_Description
                                      ,pa.[Address]            as PartyAddress_Address
                                      ,pa.[PostCode]           as PartyAddress_PostCode
                                      ,pa.[Telephone]          as PartyAddress_Telephone
                                      ,pa.[RegionID]           as PartyAddress_RegionID
                                      ,pa.[CreatedById]        as PartyAddress_CreatedById
                                      ,pa.[CreatedByName]      as PartyAddress_CreatedByName
                                      ,pa.[CreatedOn]          as PartyAddress_CreatedOn
                                      ,pa.[LastModifiedById]   as PartyAddress_LastModifiedById
                                      ,pa.[LastModifiedByName] as PartyAddress_LastModifiedByName
                                      ,pa.[LastModifiedOn]     as PartyAddress_LastModifiedOn
                                      ,pa.[Status]             as PartyAddress_Status
	                                  ,r.[ID]                 as Region_ID
                                      ,r.[Code]               as Region_Code
                                      ,r.[Name]               as Region_Name
                                      ,r.[Description]        as Region_Description
                                      ,r.[CreatedById]        as Region_CreatedById
                                      ,r.[CreatedByName]      as Region_CreatedByName
                                      ,r.[CreatedOn]          as Region_CreatedOn
                                      ,r.[LastModifiedById]   as Region_LastModifiedById
                                      ,r.[LastModifiedByName] as Region_LastModifiedByName
                                      ,r.[LastModifiedOn]     as Region_LastModifiedOn
                                      ,r.[Status]             as Region_Status
            FROM Party p JOIN PartyAddress pa ON p.ID=pa.PartyID 
                         JOIN Region r ON pa.RegionID=r.ID 
            WHERE p.ID=@partyID ";
            SqlDao dao = new SqlDao();
            SqlCommand command = dao.GetSqlCommand(query);
            command.Parameters.Add(dao.CreateParameter("@partyID", partyID));
            return dao.GetSingle<PartyAddress>(command);
        }
 
        public int Save(PartyAddress partyAddress, SqlDao dao)
        {
            return partyAddressPersister.Save(partyAddress,dao);

        }



        #endregion
    }
}