Imports EBom.Common.mdlConstraint
Imports ShisakuCommon.Ui.Spd

Namespace YosanBuhinEdit.KouseiBuhin
    Public Class DispShiyouJyouhouList
#Region " メンバー変数 "
        ''' <summary>ビュー</summary>
        Private m_view As Frm41KouseiBuhinSelector
        ''' <summary>FpSpread 共通</summary>
        Private m_spCom As SpreadCommon

        '仕様情報№
        Public Const TAG_SHIYOU_JYOUHOU_NO As String = "TAG_SHIYOU_JYOUHOU_NO"
        '車系
        Public Const TAG_SYAGATA As String = "SYAGATA"
        'ｸﾞﾚｰﾄﾞ
        Public Const TAG_GRADE As String = "GRADE"
        'ハンドル
        Public Const TAG_SHIMUKECHI_HANDLE As String = "SHIMUKECHI_HANDLE"
        '排気量
        Public Const TAG_EG_HAIKIRYOU As String = "EG_HAIKIRYOU"
        '型式
        Public Const TAG_EG_KEISHIKI As String = "EG_KEISHIKI"
        '過給器
        Public Const TAG_EG_KAKYUKI As String = "EG_KAKYUKI"
        '駆動方式
        Public Const TAG_TM_KUDOU As String = "TM_KUDOU"
        '変速機
        Public Const TAG_TM_HENSOKUKI As String = "TM_HENSOKUKI"
        '７桁型式
        Public Const TAG_KATA_7_KETA_KATASHIKI As String = "KATA_7_KETA_KATASHIKI"
        '仕向け
        Public Const TAG_KATA_SHIMUKE As String = "KATA_SHIMUKE"
        'ＯＰ
        Public Const TAG_KATA_OP As String = "KATA_OP"
        'ＯＰ項目名（開発符号）
        Public Const TAG_OP_KAIHATUFUGO As String = "OP_KAIHATUFUGO"
        'ＯＰ項目名（OPスペックコード）
        Public Const TAG_OP_CODE As String = "OP_CODE"
        'ＯＰ項目名（OP名称）
        Public Const TAG_OP_NAME As String = "OP_NAME"

#End Region

#Region " コンストラクタ "
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="f"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal f As Frm41KouseiBuhinSelector)
            m_view = f
            m_spCom = New SpreadCommon(m_view.spdBuhinShiyou)
        End Sub
#End Region

#Region " ビュー初期化 "
        ''' <summary>
        ''' ビューの初期化        
        ''' </summary>
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
            m_view.spdBuhinShiyou_Sheet1.Columns(12).Tag = TAG_OP_KAIHATUFUGO
            m_view.spdBuhinShiyou_Sheet1.Columns(13).Tag = TAG_OP_CODE
            m_view.spdBuhinShiyou_Sheet1.Columns(14).Tag = TAG_OP_NAME
       
        End Sub
#End Region

#Region "SPREADで 列のセルの水平方向の配置を設定する。"
        Public Sub SetSpdColPro()
            m_spCom.GetColFromTag(TAG_SHIYOU_JYOUHOU_NO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SYAGATA).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_GRADE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_SHIMUKECHI_HANDLE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_EG_HAIKIRYOU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_EG_KEISHIKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_EG_KAKYUKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_TM_KUDOU).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_TM_HENSOKUKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_KATA_7_KETA_KATASHIKI).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_KATA_SHIMUKE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_KATA_OP).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_OP_KAIHATUFUGO).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_OP_CODE).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            m_spCom.GetColFromTag(TAG_OP_NAME).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        End Sub
#End Region

    End Class
End Namespace
