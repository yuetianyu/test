Namespace Db.EBom.Vo.Helper
    Public Class TSeisakuIchiranHdVoHelper
        ''' <summary>ステータス</summary>
        Public Class Status
            ''' <summary>未承認</summary>
            Public Const MISYOUNIN As String = "10"
            ''' <summary>承認済み</summary>
            Public Const SYOUNIN As String = "20"
        End Class
        ''' <summary>ステータス文字</summary>
        Public Class StatusMoji
            ''' <summary>未承認</summary>
            Public Const MISYOUNIN_MOJI As String = STATUS_A_NAME
            ''' <summary>承認済み</summary>
            Public Const SYOUNIN_MOJI As String = STATUS_B_NAME
        End Class
        ''' <summary>状態</summary>
        Public Class Jyoutai
            ''' <summary>中止</summary>
            Public Const CHUSHI As String = JYOTAI_A
            ''' <summary>使用中</summary>
            Public Const OK As String = JYOTAI_B
        End Class
        ''' <summary>状態文字</summary>
        Public Class JyoutaiMoji
            ''' <summary>中止</summary>
            Public Const CHUSHI_MOJI As String = JYOTAI_A_NAME
            ''' <summary>使用中</summary>
            Public Const OK_MOJI As String = JYOTAI_B_NAME
        End Class
        ''' <summary>台数</summary>
        Public Class Daisu
            ''' <summary>ぷらす</summary>
            Public Const PURASU As String = "+"
        End Class

#Region "local member 拡張"
        '' 登録日
        Private _TourokuDate As String
        '' 最終出力日
        Private _SyutsuryokuDate As String

#End Region

#Region "local member 拡張 getとset"

        ''' <summary>登録日</summary>
        ''' <value>登録日</value>
        ''' <returns>登録日</returns>
        Public ReadOnly Property TourokuBi() As String
            Get
                If Not vo.TourokuDate Is Nothing Then
                    _TourokuDate = ShisakuComFunc.moji8Convert2Date(vo.TourokuDate)
                Else
                    _TourokuDate = ""
                End If
                Return _TourokuDate
            End Get
        End Property

        ''' <summary>最終出力日</summary>
        ''' <value>最終出力日</value>
        ''' <returns>最終出力日</returns>
        Public ReadOnly Property SyutsuryokuBi() As String
            Get
                If Not vo.SyutsuryokuDate Is Nothing Then
                    _SyutsuryokuDate = ShisakuComFunc.moji8Convert2Date(vo.SyutsuryokuDate)
                Else
                    _SyutsuryokuDate = ""
                End If
                Return _SyutsuryokuDate
            End Get
        End Property

#End Region

        ''' <summary>試作設計ブロックVo</summary>
        Private vo As TSeisakuHakouHdVo
        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <param name="vo">製作一覧発行情報Vo</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal vo As TSeisakuHakouHdVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace