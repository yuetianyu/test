Namespace Db.EBom.Vo.Helper
    Public Class TShisakuSekkeiBlockMemoVoHelper

        ''' <summary>試作ブロック№改訂№</summary>
        Public Class ShisakuBlockNoKaiteiNo
            ''' <summary>初期値</summary>
            Public Const DEFAULT_VALUE As String = "000"
        End Class
        ''' <summary>試作メモ表示順</summary>
        Public Class ShisakuMemoHyoujiJun
            ''' <summary>開始値</summary>
            Public Const START_VALUE As Integer = 0
        End Class
        ''' <summary>試作号車</summary>
        Public Class ShisakuGousya
            ''' <summary>開始値</summary>
            Public Const TITLE_VALUE As String = "<TITLE;>"
        End Class

        Private vo As TShisakuSekkeiBlockMemoVo
        Public Sub New(ByVal vo As TShisakuSekkeiBlockMemoVo)
            Me.vo = vo
        End Sub

        ''' <summary>
        ''' メモタイトル欄のデータか？を返す
        ''' </summary>
        ''' <param name="vo">試作設計ブロックメモ情報</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsMemoTitleData(ByVal vo As TShisakuSekkeiBlockMemoVo) As Boolean
            Return ShisakuGousya.TITLE_VALUE.Equals(vo.ShisakuGousya)
        End Function
        ''' <summary>
        ''' メモタイトル欄のデータか？を返す
        ''' </summary>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Function IsMemoTitleData() As Boolean
            Return IsMemoTitleData(vo)
        End Function
    End Class
End Namespace