using SQLHelper.Entities;
using SQLHelper.Helper;
using System;
using System.Data.SqlClient;

namespace SQLHelper.Persistence
{
    public class PartyPersister
    {

        public int Save(Party party, SqlDao dao)
        {
            int result = 0;
            if (dao == null)
                dao = new SqlDao();
            SqlCommand command = dao.GetSprocCommand("spPartySave");
            command.Parameters.Add(dao.CreateParameter("@ID", party.ID));
            command.Parameters.Add(dao.CreateParameter("@Code", party.Code, 20));
            command.Parameters.Add(dao.CreateParameter("@Name", party.Name, 100));
            command.Parameters.Add(dao.CreateParameter("@Description", party.Description, 200));
            command.Parameters.Add(dao.CreateParameter("@Email", party.Email, 100));
            command.Parameters.Add(dao.CreateParameter("@Hobby", party.MobilePhone, 200));
            command.Parameters.Add(dao.CreateParameter("@CreatedById", party.CreatedById));
            command.Parameters.Add(dao.CreateParameter("@CreatedByName", party.CreatedByName, 100));
            command.Parameters.Add(dao.CreateParameter("@LastModifiedById", party.LastModifiedById));
            command.Parameters.Add(dao.CreateParameter("@LastModifiedByName", party.LastModifiedByName, 100));
            command.Parameters.Add(dao.CreateParameter("@Status", party.Status));
            object obj = dao.ExecuteScalar(command);
            if (obj != null)
                result = Convert.ToInt32(obj);

            return result;


        }



    }
}