using System.Data.SqlClient;

namespace QuanLyHocSinh_ADO
{
    public static class Db
    {
        // Doi "Server=" cho dung voi may cua ban neu can
        public const string ChuoiKetNoi = "Server=.\\SQLEXPRESS; Database=DBHocSinh; Integrated Security=SSPI;";

        // Chay lenh INSERT / UPDATE / DELETE (mo hinh Connected)
        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(ChuoiKetNoi))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddRange(ps);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
