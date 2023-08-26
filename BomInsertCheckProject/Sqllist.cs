//相关SQL语句
namespace BomInsertCheckProject
{
    public class Sqllist
    {
        private string _result;

        /// <summary>
        /// 以BOM编码为条件,获取相关质检记录并插入至ytc_CheckProject内
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string InsertRdToCheckProject(string fid)
        {
            _result = $@"
                            DECLARE
                                @max NVARCHAR(max)='',
                                @fmaterialid INT;
                            begin
				
	                                SELECT /*@fid=a.fid,*/ @fmaterialid=a.FMATERIALID 
                                    FROM dbo.T_ENG_BOM a
	                                WHERE a.fid={fid}

			                        --填写检验标准信息
				                    delete FROM ytc_CheckProject WHERE FID = {fid}

				                    select @max = MAX(FEntryID) FROM ytc_CheckProject


					 	            INSERT into ytc_CheckProject(FID,FEntryID ,F_YTC_CHECKROWID,F_YTC_CHECKITEM,F_YTC_CHECKSTANDARD,F_YTC_toplimit,F_YTC_lowlimit)
						            select {fid},(ROW_NUMBER()over(order by t1.FINSPECTITEMID)+@max) fentryid
							            ,''
							            ,t1.FINSPECTITEMID F_YTC_CHECKITEM
							            ,(case t1.FANALYSISMETHOD when 1 then cast(t2.FTARGETVALQ as nvarchar(250)) when 3 then cast(t2.FTARGETVALT AS nvarchar(250)) when 2 then cast(t2.FTARGETVALB AS nvarchar(250)) end)  F_YTC_CHECKSTANDARD							
							            ,(case t1.FANALYSISMETHOD when 1 then cast(t2.FUPLIMITQ as nvarchar(250)) when 3 then cast(t2.FUPLIMITT AS nvarchar(250)) when 2 then cast(t2.FUPLIMITB AS nvarchar(250)) end)  F_YTC_toplimit
							            ,(case t1.FANALYSISMETHOD when 1 then cast(t2.FDOWNLIMITQ as nvarchar(250)) when 3 then cast(t2.FDOWNLIMITT AS nvarchar(250)) when 2 then cast(t2.FDOWNLIMITB AS nvarchar(250)) end)  F_YTC_lowlimit				
						            from T_QM_QCSCHEMEENTRY t1
						            inner join T_QM_QCSCHEMEENTRY_A t2 on t1.FENTRYID = t2.FENTRYID 
						            where t1.FID = (select FINCQCSCHEMEID from T_BD_MATERIALQUALITY where FMATERIALID = @fmaterialid)
				            END
                        ";

            return _result;
        }

    }
}
