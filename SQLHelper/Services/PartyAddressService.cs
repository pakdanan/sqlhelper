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

        #endregion


        #region Constructors

        #endregion

        #region Enums


        #endregion

        #region Methods
        public PartyAddress GetPartyAddressByPartyId(int partyID)
        {
            string query = @"SELECT    p.*
	                                  ,pa.[ID]                 as Address_ID
                                      ,pa.[PartyID]            as Address_PartyID
                                      ,pa.[Code]               as Address_Code
                                      ,pa.[Name]               as Address_Name
                                      ,pa.[Description]        as Address_Description
                                      ,pa.[Address]            as Address_FullAddress
                                      ,pa.[PostCode]           as Address_PostCode
                                      ,pa.[Telephone]          as Address_Telephone
                                      ,pa.[RegionID]           as Address_RegionID
                                      ,pa.[CreatedById]        as Address_CreatedById
                                      ,pa.[CreatedByName]      as Address_CreatedByName
                                      ,pa.[CreatedOn]          as Address_CreatedOn
                                      ,pa.[LastModifiedById]   as Address_LastModifiedById
                                      ,pa.[LastModifiedByName] as Address_LastModifiedByName
                                      ,pa.[LastModifiedOn]     as Address_LastModifiedOn
                                      ,pa.[Status]             as Address_Status
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
 


        #endregion
    }
}