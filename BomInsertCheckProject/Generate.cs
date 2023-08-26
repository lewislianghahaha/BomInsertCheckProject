using System;
using System.Data.SqlClient;

//生成
namespace BomInsertCheckProject
{
    public class Generate
    {
        Sqllist sqllist = new Sqllist();

        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="fidlist">获取在列表所获取的所有主键信息</param>
        /// <returns></returns>
        public string InsertCheckProject(string fidlist)
        {
            var result = "Finish";

            try
            {
                //todo:判断若fidlist不包含,号,不用进行下面的循环
                if (!fidlist.Contains(","))
                {
                    Generdt(sqllist.InsertRdToCheckProject(fidlist));
                }
                //todo:对获取的主键值进行以","号拆解,并循环获取FID值放到SQL语句中进行数据处理
                else
                {
                    string[] reslut = fidlist.Split(',');

                    for (var i = 0; i < result.Length; i++)
                    {
                        Generdt(sqllist.InsertRdToCheckProject(Convert.ToString(reslut[i])));
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 按照指定的SQL语句执行记录(反审核时使用)
        /// </summary>
        private void Generdt(string sqlscript)
        {
            using (var sql = GetCloudConn())
            {
                sql.Open();
                var sqlCommand = new SqlCommand(sqlscript, sql);
                sqlCommand.ExecuteNonQuery();
                sql.Close();
            }
        }

        /// <summary>
        /// 获取连接返回信息 正式:AIS20181204095717 测试:AIS20230811151520
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

            strcon = @"Data Source='192.168.1.228';Initial Catalog='AIS20230811151520';Persist Security Info=True;User ID='sa'; Password='kingdee';
                    Pooling=true;Max Pool Size=40000;Min Pool Size=0";

            return strcon;
        }

    }
}
