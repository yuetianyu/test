Namespace Db.Kosei.Vo.Helper
    ''' <summary>権限(ﾕｰｻﾞｰ別)クラスの拡張</summary>
    Public Class MAuthorityUserVoHelper
        Private vo As MAuthorityUserVo
        ''' <summary>Construct</summary>
        Public Sub New(ByVal vo As MAuthorityUserVo)
            Me.vo = vo
        End Sub
        ''' <summary>システムId</summary>
        Public Class AppNo
            ''' <summary>新試作手配システム</summary>
            Public Const TRIAL_MANUFACTURE As String = "1000"
        End Class

        Public Class KinoId1
            ''' <summary>メニュー</summary>
            Public Const MENU As String = "MENU"
            ''' <summary>試作部品表</summary>
            Public Const SHISAKU_BUHIN As String = "EVENT038"
            ''' <summary>車系／開発符号マスター</summary>
            Public Const KAIHATU_FUGOU As String = "MASTER006"
            '2012/01/24 承認アラート機能追加の為
            ''' <summary>承認通知</summary>
            Public Const SHONIN As String = "SHONIN"

            '2014/02/05 日程管理ツール権限追加の為
            ''' <summary>承認通知</summary>
            Public Const NITTEI As String = "NITTEI"
            '2014/10/08 現調管理機能追加の為
            ''' <summary>オーダーシート</summary>
            Public Const ORDER_SHEET As String = "ORDERSHEET"
        End Class
        '2014/02/05 日程管理ツール権限追加の為
        Public Class NitteiKinoId
            ''' <summary>参照権限</summary>
            Public Const NITTEI_KINO2ID_COMPARE As Integer = 0
            ''' <summary>更新権限</summary>
            Public Const NITTEI_KINO2ID_UPDATE As Integer = 1
            ''' <summary>管理者権限</summary>
            Public Const NITTEI_KINO2ID_ADMIN As Integer = 2
        End Class
        Public Class NitteiKinoIdName
            ''' <summary>参照権限</summary>
            Public Const NITTEI_KINO2ID_COMPARE_NAME As String = "参照権限"
            ''' <summary>更新権限</summary>
            Public Const NITTEI_KINO2ID_UPDATE_NAME As String = "更新権限"
            ''' <summary>管理者権限</summary>
            Public Const NITTEI_KINO2ID_ADMIN_NAME As String = "管理者権限"
        End Class

        '2015/02/05 オーダーシート権限追加の為
        Public Class OrderKinoId
            ''' <summary>使用権限</summary>
            Public Const ORDER_KINO2ID_USE As String = "OPEN"
            ''' <summary>編集権限</summary>
            Public Const ORDER_KINO2ID_EDIT As String = "EDIT"
        End Class
        Public Class OrderKinoIdName
            ''' <summary>使用権限</summary>
            Public Const ORDER_KINO2ID_USE_NAME As String = "使用権限"
            ''' <summary>編集権限</summary>
            Public Const ORDER_KINO2ID_EDIT_NAME As String = "編集権限"
        End Class

        Public Class KinoId2
            ''' <summary>承認押下</summary>
            Public Const SHOUNIN As String = "APPROVAL"
            ''' <summary>使用権限</summary>
            Public Const SHIYOU As String = "OPEN"
            '2014/10/08 現調管理機能追加の為
            ''' <summary>編集権限</summary>
            Public Const HENSHUU As String = "EDIT"
        End Class

        Public Class AuthorityKbn
            ''' <summary>権限なし</summary>
            Public Const KENGEN_NASHI As Integer = 0
            ''' <summary>権限あり</summary>
            Public Const KENGEN_ARI As Integer = 1
        End Class

        Public Class AuthorityKbnMoji
            ''' <summary>権限なし</summary>
            Public Const KENGEN_NASHI As String = "なし"
            ''' <summary>権限あり</summary>
            Public Const KENGEN_ARI As String = "あり"
        End Class

        Public Class MenuAuthorityKbn
            ''' <summary>権限なし</summary>
            Public Const KENGEN_NASHI As Integer = 0
            ''' <summary>設計課メニュー権限あり</summary>
            Public Const SEKKEI As Integer = 1
            ''' <summary>試作課メニュー権限あり</summary>
            Public Const SHISAKU As Integer = 2
            ''' <summary>試作１課メニュー権限あり</summary>
            Public Const SHISAKU1KA As Integer = 3
        End Class

    End Class
End Namespace