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
            FROM Party p JOIN PartyAddress pa ON p.ID=pa.PartyID WHERE pa.PartyID=@partyID  ";
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