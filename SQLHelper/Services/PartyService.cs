using SQLHelper.Persistence;
using System.Collections.Generic;
using System.Data.SqlClient;
using SQLHelper.Entities;
using SQLHelper.Helper;

namespace SQLHelper.Services
{
    public class PartyService : ServiceBase
    {
        #region Constants
        #endregion

        #region Properties
        #endregion

        #region Fields

        private PartyPersister partyPersister;

        #endregion


        #region Constructors

        public PartyService()
        {
            partyPersister = new PartyPersister();
        }

        public PartyService(PartyPersister partyPersister)
        {
            this.partyPersister = partyPersister;
        }

        #endregion

        #region Enums


        #endregion

        #region Methods

        public Party GetPartyById(int id)
        {
            string query = @"SELECT * FROM [dbo].[Party] where ID=@ID";
            SqlDao dao = new SqlDao();
            SqlCommand command = dao.GetSqlCommand(query);
            command.Parameters.Add(dao.CreateParameter("@ID", id));
            return dao.GetSingle<Party>(command);
        }
        public List<Party> GetAllParty()
        {
            string query = @"SELECT * FROM [dbo].[Party] ";
            SqlDao dao = new SqlDao();
            SqlCommand command = dao.GetSqlCommand(query);
            return dao.GetList<Party>(command);
        }

        public int Save(Party party, SqlDao dao)
        {
            return partyPersister.Save(party,dao);
        }

 




        #endregion
    }
}