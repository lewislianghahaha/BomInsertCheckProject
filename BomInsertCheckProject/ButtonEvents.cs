using System;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.List.PlugIn;

namespace BomInsertCheckProject
{
    public class ButtonEvents : AbstractListPlugIn
    {
        public override void BarItemClick(BarItemClickEventArgs e)
        {
            var generate = new Generate();

            //定义主键变量(用于收集所选中的行主键值)
            var flistid = string.Empty;
            //定义中间变量(对比重复使用)
            var tempid = string.Empty;

            //订单退回操作
            base.BarItemClick(e);

            //指定当点击‘更新检验列表信息’按钮时执行
            if (e.BarItemKey == "tbUpCheckProject")
            {
                //获取列表上通知复选框勾选的记录
                var selectedrows = this.ListView.SelectedRowsInfo;

                //判断若没有选择记录,即报出异常提示
                if (selectedrows.Count == 0)
                {
                    View.ShowErrMessage("没有选择记录,不能进行更新");
                }
                else
                {
                    //通过循环将选中行的主键进行收集
                    foreach (var row in selectedrows)
                    {
                        if (string.IsNullOrEmpty(tempid))
                        {
                            flistid = "'" + Convert.ToString(row.PrimaryKeyValue) + "'";
                            tempid = Convert.ToString(row.PrimaryKeyValue);
                        }
                        //todo:当检测到不相同时才进行添加
                        else if (tempid != Convert.ToString(row.PrimaryKeyValue))
                        {
                            flistid += "," + "'" + Convert.ToString(row.PrimaryKeyValue) + "'";
                            tempid = Convert.ToString(row.PrimaryKeyValue);
                        }
                    }

                    var result = generate.InsertCheckProject(flistid);

                    var message = result == "Finish" ? $@"已成功添加检验信息,请进入指定单据内进行查阅" : $@"添加检验信息出现异常,原因:'{result}'";

                    //输出
                    if (result == "Finish")
                    {
                        View.ShowMessage(message);
                    }
                    else
                    {
                        View.ShowErrMessage(message);
                    }
                }
            }
        }
    }
}
