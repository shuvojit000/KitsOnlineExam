using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Repository
{
    public class UserRepository : IUserRepository
    {
        public int ValidateUser(LogInRequestDTO request)
        {

            SqlParameter prmUserName = new SqlParameter("@PartnerId", SqlDbType.BigInt);
            prmUserName.Value = request.UserName;

            SqlParameter prmPassword = new SqlParameter("@PartnerId", SqlDbType.BigInt);
            prmPassword.Value = request.Password;

            using (SqlDataReader dr = SqlServerHelper.ExecuteReaderProc("spPartnersBookingReq", prmUserName, prmPassword))
            {
                if (dr != null && dr.HasRows)
                { }
            }
            return 0;
        }

    }
}
