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
                               -- @max NVARCHAR(max)='',
                                @fmaterialid INT,
                                @count INT,
                                @I INT=1;
                            begin
				
	                                SELECT @fmaterialid=a.FMATERIALID 
                                    FROM dbo.T_ENG_BOM a
	                                WHERE a.fid={fid}

			                        --填写检验标准信息
				                    delete FROM ytc_CheckProject WHERE FID = {fid}

				                    --select @max = MAX(FEntryID) FROM ytc_CheckProject

                                    -------------------------------------------获取主键及插入-----------------------------------------------------------------------

                                    --查询将要插入的值
                                    select ROW_NUMBER()OVER(ORDER BY T1.FID) ID,
	                                    t1.FINSPECTITEMID F_YTC_CHECKITEM
	                                    ,(case t1.FANALYSISMETHOD when 1 then cast(t2.FTARGETVALQ as nvarchar(250)) when 3 then cast(t2.FTARGETVALT AS nvarchar(250)) when 2 then cast(t2.FTARGETVALB AS nvarchar(250)) end)  F_YTC_CHECKSTANDARD							
	                                    ,(case t1.FANALYSISMETHOD when 1 then cast(t2.FUPLIMITQ as nvarchar(250)) when 3 then cast(t2.FUPLIMITT AS nvarchar(250)) when 2 then cast(t2.FUPLIMITB AS nvarchar(250)) end)  F_YTC_toplimit
	                                    ,(case t1.FANALYSISMETHOD when 1 then cast(t2.FDOWNLIMITQ as nvarchar(250)) when 3 then cast(t2.FDOWNLIMITT AS nvarchar(250)) when 2 then cast(t2.FDOWNLIMITB AS nvarchar(250)) end)  F_YTC_lowlimit
                                    INTO #TEMP0				
                                    from T_QM_QCSCHEMEENTRY t1
                                    inner join T_QM_QCSCHEMEENTRY_A t2 on t1.FENTRYID = t2.FENTRYID 
                                    where t1.FID = (select FINCQCSCHEMEID from T_BD_MATERIALQUALITY where FMATERIALID = @fmaterialid)

                                    --根据#TEMP0进行循环,并获取Z_ytc_CheckProject主键
                                    SELECT @count=COUNT(*) FROM #TEMP0

                                    WHILE @I<=@count
                                    BEGIN
	                                    --获取Z_ytc_CheckProject主键->作为ytc_CheckProject.FEntryid值
	                                    DECLARE
		                                    @id INT;
	                                    BEGIN
		                                    INSERT INTO dbo.Z_ytc_CheckProject( Column1 )
		                                    VALUES  (1)

		                                    SELECT @id=Id FROM dbo.Z_ytc_CheckProject

		                                    DELETE FROM dbo.Z_ytc_CheckProject
	                                    END

	                                    --插入
	                                    INSERT into ytc_CheckProject(FID,FEntryID ,F_YTC_CHECKROWID,F_YTC_CHECKITEM,F_YTC_CHECKSTANDARD,F_YTC_toplimit,F_YTC_lowlimit)
	                                    SELECT @FID,@ID,
	                                                ''
				                                    ,A.F_YTC_CHECKITEM,A.F_YTC_CHECKSTANDARD,A.F_YTC_toplimit,A.F_YTC_lowlimit
	                                    FROM #TEMP0 A WHERE ID=@I

	                                    SET @I=@I+1
                                    END
				            END
                        ";

            return _result;
        }

    }
}
