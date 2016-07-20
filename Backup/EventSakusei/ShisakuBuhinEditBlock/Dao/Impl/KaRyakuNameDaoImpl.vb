Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.EventEdit.Dao
Imports EventSakusei.ShisakuBuhinEditBlock
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl

Namespace ShisakuBuhinEditBlock.Dao

    Public Class KaRyakuNameDaoImpl : Inherits DaoEachFeatureImpl
        Implements KaRyakuNameDao

        Public Const site_kbn = 1
        '担当承認課と課長承認課に部課コードではなく、課略名を表示させるための処理を追加'
        Public Function GetKa_Ryaku_Name(ByVal syoninka As String) As Rhac1560Vo Implements KaRyakuNameDao.GetKa_Ryaku_Name
            Dim Bu_Code As String
            Dim Ka_Code As String
            Bu_Code = Left(syoninka, 2)
            Ka_Code = Right(syoninka, 2)
            '/*** 20140911 CHANGE START ***/
            'Dim sql As String = _
            '    "SELECT " _
            '    & "   KA_RYAKU_NAME " _
            '    & "   FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 WITH (NOLOCK, NOWAIT) " _
            '    & "   WHERE" _
            '    & "   SITE_KBN =" & site_kbn _
            '    & "   AND BU_CODE =@BuCode" _
            '    & "   AND KA_CODE =@KaCode"
                
            Dim sql As String = _
                "SELECT " _
                & "   KA_RYAKU_NAME " _
                & "   FROM " & RHACLIBF_DB_NAME & ".dbo.RHAC1560 WITH (NOLOCK, NOWAIT) " _
                & "   WHERE" _
                & "   SITE_KBN ='" & site_kbn & "'" _
                & "   AND BU_CODE =@BuCode" _
                & "   AND KA_CODE =@KaCode"
            '/*** 20140911 CHANGE END ***/

            Dim db As New EBomDbClient
            Dim param As New Rhac1560Vo
            param.BuCode = Bu_Code
            param.KaCode = Ka_Code

            '2012/03/23 課略名が取得できなかった場合は課コードをそのまま返す。
            'これは課マスターに存在せず、mBOMのSHISAKU_BUKA_CODEに入力された課名が
            'そのまま格納されている場合の対応
            '例）DGD1,DGD2,DGS5,HEV,TESTなど
            Dim vo As Rhac1560Vo
            vo = db.QueryForObject(Of Rhac1560Vo)(sql, param)
            If Not vo Is Nothing Then
                Return vo
            Else
                vo = New Rhac1560Vo
                vo.KaRyakuName = syoninka
                Return vo
            End If
        End Function

    End Class
End Namespace