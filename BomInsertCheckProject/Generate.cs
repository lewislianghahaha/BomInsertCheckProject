using System;
using System.Data.SqlClient;

//生成
namespace BomInsertCheckProject
{
    public class Generate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bomno"></param>
        /// <returns></returns>
        public string InsertCheckProject(string bomno)
        {
            var result = "Finish";

            try
            {

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }







        /// <summary>
        /// 获取连接返回信息
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetCloudConn()
        {
            var sqlcon = new SqlConnection(GetConnectionString());
            return sqlcon;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            var strcon = string.Empty;

            strcon = @"Data Source='192.168.1.228';Initial Catalog='AIS20181204095717';Persist Security Info=True;User ID='sa'; Password='kingdee';
                    Pooling=true;Max Pool Size=40000;Min Pool Size=0";

            return strcon;
        }

    }
}
