using SQLHelper.Entities;
using SQLHelper.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHelper.Persistence
{
    public class PartyAddressPersister
    {

        public int Save(PartyAddress partyAddress, SqlDao dao)
        {
            int result = 0;
            if (dao == null)
                dao = new SqlDao();
            SqlCommand command = dao.GetSprocCommand("spPartyAddressSave");
            command.Parameters.Add(dao.CreateParameter("@ID", partyAddress.ID));
            command.Parameters.Add(dao.CreateParameter("@PartyID", partyAddress.PartyID));
            command.Parameters.Add(dao.CreateParameter("@RegionID", partyAddress.RegionID));
            command.Parameters.Add(dao.CreateParameter("@Code", partyAddress.Code, 100));
            command.Parameters.Add(dao.CreateParameter("@Name", partyAddress.Name, 100));
            command.Parameters.Add(dao.CreateParameter("@Address", partyAddress.Address, 300));
            command.Parameters.Add(dao.CreateParameter("@PostCode", partyAddress.PostCode, 100));
            command.Parameters.Add(dao.CreateParameter("@Telephone", partyAddress.Telephone, 100));
            command.Parameters.Add(dao.CreateParameter("@Description", partyAddress.Description, 100));
            command.Parameters.Add(dao.CreateParameter("@CreatedById", partyAddress.CreatedById));
            command.Parameters.Add(dao.CreateParameter("@CreatedByName", partyAddress.CreatedByName, 100));
            command.Parameters.Add(dao.CreateParameter("@LastModifiedById", partyAddress.LastModifiedById));
            command.Parameters.Add(dao.CreateParameter("@LastModifiedByName", partyAddress.LastModifiedByName, 100));
            command.Parameters.Add(dao.CreateParameter("@Status", partyAddress.Status));
            object obj = dao.ExecuteScalar(command);
            if (obj != null)
                result = Convert.ToInt32(obj);

            return result;

        }



    }

}
