using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;

namespace BomInsertCheckProject
{
    public class ButtonEvents : AbstractBillPlugIn
    {
        Generate generate = new Generate();

        public override void BarItemClick(BarItemClickEventArgs e)
        {
            var message = string.Empty;

            //订单退回操作
            base.BarItemClick(e);

            //定义获取表头信息对像
            var docScddIds1 = View.Model.DataObject;
            //获取表头中单据编号信息(注:这里的Number为单据编号中"绑定实体属性"项中获得)
            //todo:获取BOM编码
            var dhstr = docScddIds1["Number"].ToString();

            //当点击提交 时会执行
            if (e.BarItemKey == "tbSplitSubmit" || e.BarItemKey== "tbSubmit")
            {
                //todo:执行相关插入操作



            }

        }
    }
}
