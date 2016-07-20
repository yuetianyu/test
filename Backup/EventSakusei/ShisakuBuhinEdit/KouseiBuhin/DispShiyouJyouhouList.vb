Imports ShisakuCommon
Imports EBom.Common
Imports EBom.Common.mdlConstraint
Imports EBom.Data
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd
Imports FarPoint.Win.Spread.CellType

Namespace KouseiBuhin
    Public Class DispShiyouJyouhouList
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As HoyouBuhinFrm41KouseiBuhinSelector
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon

        Public Const TAG_SHIYOU_JYOUHOU_NO As String = "TAG_SHIYOU_JYOUHOU_NO"
        Public Const TAG_SYAGATA As String = "SYAGATA"
        Public Const TAG_GRADE As String = "GRADE"
        'Public Const TAG_SHIMUKECHI_SHIMUKE As String = "SHIMUKECHI_SHIMUKE"
        Public Const TAG_SHIMUKECHI_HANDLE As String = "SHIMUKECHI_HANDLE"
        Public Const TAG_EG_HAIKIRYOU As String = "EG_HAIKIRYOU"
        Public Const TAG_EG_KEISHIKI As String = "EG_KEISHIKI"
        Public Const TAG_EG_KAKYUKI As String = "EG_KAKYUKI"
        Public Const TAG_TM_KUDOU As String = "TM_KUDOU"
        Public Const TAG_TM_HENSOKUKI As String = "TM_HENSOKUKI"
        Public Const TAG_KATA_7_KETA_KATASHIKI As String = "KATA_7_KETA_KATASHIKI"
        Public Const TAG_KATA_SHIMUKE As String = "KATA_SHIMUKE"
        Public Const TAG_KATA_OP As String = "KATA_OP"
        Public Const TAG_OP01 As String = "OP01"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As HoyouBuhinFrm41KouseiBuhinSelector)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdBuhinShiyou)
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InitView() As RESULT

            ''Spreadの初期化
            SpreadUtil.Initialize(m_view.spdBuhinShiyou)
            SetSpdColTag()
            '
            SetSpdColPro()

        End Function
#End Region

#Region " SPREADの列のタグ値を設定する "
        ''' <summary>
        ''' SPREADの列のタグ値を設定する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SetSpdColTag()
            '列の項目を設定する
            m_view.spdBuhinShiyou_Sheet1.Columns(0).Tag = TAG_SHIYOU_JYOUHOU_NO
            m_view.spdBuhinShiyou_Sheet1.Columns(1).Tag = TAG_SYAGATA
            m_view.spdBuhinShiyou_Sheet1.Columns(2).Tag = TAG_GRADE
            'm_view.spdBuhinShiyou_Sheet1.Columns(3).Tag = TAG_SHIMUKECHI_SHIMUKE
            m_view.spdBuhinShiyou_Sheet1.Columns(3).Tag = TAG_SHIMUKECHI_HANDLE
            m_view.spdBuhinShiyou_Sheet1.Columns(4).Tag = TAG_EG_HAIKIRYOU
            m_view.spdBuhinShiyou_Sheet1.Columns(5).Tag = TAG_EG_KEISHIKI
            m_view.spdBuhinShiyou_Sheet1.Columns(6).Tag = TAG_EG_KAKYUKI
            m_view.spdBuhinShiyou_Sheet1.Columns(7).Tag = TAG_TM_KUDOU
            m_view.spdBuhinShiyou_Sheet1.Columns(8).Tag = TAG_TM_HENSOKUKI
            m_view.spdBuhinShiyou_Sheet1.Columns(9).Tag = TAG_KATA_7_KETA_KATASHIKI
            m_view.spdBuhinShiyou_Sheet1.Columns(10).Tag = TAG_KATA_SHIMUKE
            m_view.spdBuhinShiyou_Sheet1.Columns(11).Tag = TAG_KATA_OP
            'ＯＰ列
            m_view.spdBuhinShiyou_Sheet1.Columns(12).Tag = TAG_OP01

        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。"
        Public Sub SetSpdColPro()
            m_spCom.GetColFromTag(TAG_SHIYOU_JYOUHOU_NO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SYAGATA).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_GRADE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            'm_spCom.GetColFromTag(TAG_SHIMUKECHI_SHIMUKE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_HANDLE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYOU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_EG_KEISHIKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_TM_KUDOU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_KATA_7_KETA_KATASHIKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_KATA_SHIMUKE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_KATA_OP).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_OP01).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center



            '
            '   行、列のサイズ変更はこんなところで行わない！！
            '
            'For i As Integer = 0 To m_view.spdBuhin_Sheet1.ColumnCount - 1
            '    m_view.spdBuhinShiyou_Sheet1.Columns(i).Resizable = False
            'Next
            'For i As Integer = 0 To m_view.spdBuhin_Sheet1.RowCount - 1
            '    m_view.spdBuhinShiyou_Sheet1.Rows(i).Resizable = False
            'Next
        End Sub
#End Region

    End Class
End Namespace
