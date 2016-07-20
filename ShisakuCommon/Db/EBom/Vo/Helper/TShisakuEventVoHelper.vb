Imports ShisakuCommon.Util.LabelValue

Namespace Db.EBom.Vo.Helper
    ''' <summary>
    ''' 試作イベント情報ヘルパークラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TShisakuEventVoHelper
        Private ReadOnly vo As TShisakuEventVo

        ''' <summary>ユニット区分</summary>
        Public Class UnitKbn
            ''' <summary>トリム</summary>
            Public Const TRIM As String = "T"
            ''' <summary>メタル</summary>
            Public Const METAL As String = "M"
            ''' <summary>トリム＆メタル</summary>
            Public Const TRIM_AND_METAL As String = "S"
        End Class
        ''' <summary>発注有無</summary>
        Public Class HachuUmu
            ''' <summary>無</summary>
            Public Const NASHI As String = "0"
            ''' <summary>有</summary>
            Public Const ARI As String = "1"
        End Class
        ''' <summary>発注有無(名称)</summary>
        Public Class HachuUmuName
            ''' <summary>無</summary>
            Public Const NASHI As String = "無"
            ''' <summary>有</summary>
            Public Const ARI As String = "有"
        End Class
        '2012/01/07
        ''' <summary>自給品の有無</summary>
        Public Class JikyuUmu
            ''' <summary>無</summary>
            Public Const NASHI As String = "0"
            ''' <summary>有</summary>
            Public Const ARI As String = "1"
        End Class
        ''' <summary>発注有無(名称)</summary>
        Public Class JikyuUmuName
            ''' <summary>無</summary>
            Public Const NASHI As String = "無"
            ''' <summary>有</summary>
            Public Const ARI As String = "有"
        End Class
        ''' <summary>データ区分</summary>
        Public Class DataKbn
            ''' <summary>登録時の値</summary>
            Public Const REGISTER As String = "12"
            ''' <summary>保存時の値</summary>
            Public Const SAVE As String = "11"
        End Class
        ''' <summary>ステータス</summary>
        Public Class Status
            ''' <summary>設計メンテ中</summary>
            Public Const SEKKEI_MAINTAINING As String = "21"
            ''' <summary>差戻し中</summary>
            Public Const SASHIMODOSHI_ING As String = "22"
            ''' <summary>改訂受付中</summary>
            Public Const KAITEI_UKETSUKE_ING As String = "23"
            ''' <summary>完了</summary>
            Public Const KANRYO As String = "24"
            ''' <summary>中止(改訂受付後)</summary>
            Public Const CHUSHI_UKETSUKE_GO As String = "25"
            ''' <summary>中止(改訂受付前)</summary>
            Public Const CHUSHI_UKETSUKE_MAE As String = "26"
        End Class
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="vo"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal vo As TShisakuEventVo)
            Me.vo = vo
        End Sub

        ''' <summary>
        ''' 発注有無の LabelValueVo リストを返す
        ''' </summary>
        ''' <returns>LabelValueVo リスト</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeHachuUmuLabeValues() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            results.Add(New LabelValueVo(HachuUmuName.ARI, HachuUmu.ARI))
            results.Add(New LabelValueVo(HachuUmuName.NASHI, HachuUmu.NASHI))
            Return results
        End Function

        ''' <summary>
        ''' ユニット区分の LabelValueVo リストを返す
        ''' </summary>
        ''' <returns>LabelValueVo リスト</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeUnitKbnLabeValues() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            results.Add(New LabelValueVo(UnitKbn.TRIM, UnitKbn.TRIM))
            results.Add(New LabelValueVo(UnitKbn.METAL, UnitKbn.METAL))
            results.Add(New LabelValueVo(UnitKbn.TRIM_AND_METAL, UnitKbn.TRIM_AND_METAL))
            Return results
        End Function

        ''' <summary>
        ''' 発注有無の名称を返す
        ''' </summary>
        ''' <returns>発注有無の名称</returns>
        ''' <remarks></remarks>
        Public Function GetHachuUmuName() As String
            If HachuUmu.NASHI.Equals(vo.HachuUmu) Then
                Return HachuUmuName.NASHI
            ElseIf HachuUmu.ARI.Equals(vo.HachuUmu) Then
                Return HachuUmuName.ARI
            Else
                Return Nothing
            End If
        End Function

        '2012/01/07
        ''' <summary>
        ''' 自給品の LabelValueVo リストを返す
        ''' </summary>
        ''' <returns>LabelValueVo リスト</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeJikyuUmuLabeValues() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            results.Add(New LabelValueVo(JikyuUmuName.ARI, JikyuUmu.ARI))
            results.Add(New LabelValueVo(JikyuUmuName.NASHI, JikyuUmu.NASHI))
            Return results
        End Function

        '2012/01/07
        ''' <summary>
        ''' 自給品の名称を返す
        ''' </summary>
        ''' <returns>自給品の名称</returns>
        ''' <remarks></remarks>
        Public Function GetJikyuUmuName() As String
            If JikyuUmu.NASHI.Equals(vo.JikyuUmu) Then
                Return JikyuUmuName.NASHI
            ElseIf JikyuUmu.ARI.Equals(vo.JikyuUmu) Then
                Return JikyuUmuName.ARI
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' ステータスが、設計メンテ中かを返す
        ''' </summary>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsStatusSekkeiMaintaining()
            Return Status.SEKKEI_MAINTAINING.Equals(vo.Status)
        End Function
        ''' <summary>
        ''' ステータスが、差戻し中かを返す
        ''' </summary>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsStatusSashimodoshiIng()
            Return Status.SASHIMODOSHI_ING.Equals(vo.Status)
        End Function
    End Class
End Namespace