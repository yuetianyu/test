Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports EventSakusei.ShisakuBuhinEditBlock.Logic.Tenkai


''' <summary>
''' INSTL品番関連で同期を取らせる為の簡単な実装クラス
''' </summary>
''' <remarks>A/L作成機能で入力したINSTL品番を、部品構成編集へ反映する</remarks>
Public Class BuhinEditInstlHinbanBlock
    Implements EzSyncInstlHinban
    Private alSubject As BuhinEditInstlSupplier
    Private koseiSubject As BuhinEditBaseSupplier

    Private Initializing As Boolean
    ''' <summary>
    ''' 初期設定する
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize(ByVal alSubject As BuhinEditInstlSupplier, ByVal koseiSubject As BuhinEditBaseSupplier)
        Me.alSubject = alSubject
        Me.koseiSubject = koseiSubject
        Initializing = True
        koseiSubject.IsSuspend_OnChangedInstlHinbanOrKbn = True
        Try
            For Each columnIndex As Integer In alSubject.GetInputInstlHinbanColumnIndexes
                NotifyInstlHinban(columnIndex)
                NotifyInstlHinbanKbn(columnIndex)
                ''↓↓2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
                NotifyBaseInstlFlg(columnIndex)
                ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END
            Next
        Finally
            koseiSubject.IsSuspend_OnChangedInstlHinbanOrKbn = False
            Initializing = False
        End Try
    End Sub

    ''' <summary>
    ''' INSTL品番が更新された事を通知する
    ''' </summary>
    ''' <param name="columnIndex">列index</param>
    ''' <remarks></remarks>
    Public Sub NotifyInstlHinban(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlHinban
        koseiSubject.InstlHinban(columnIndex) = alSubject.InstlHinban(columnIndex)
        If Not Initializing Then
            koseiSubject.NotifyTitleObservers(columnIndex)
        End If
    End Sub

    ''' <summary>
    ''' INSTL品番区分が更新された事を通知する
    ''' </summary>
    ''' <param name="columnIndex">列index</param>
    ''' <remarks></remarks>
    Public Sub NotifyInstlHinbanKbn(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlHinbanKbn
        koseiSubject.InstlHinbanKbn(columnIndex) = alSubject.InstlHinbanKbn(columnIndex)
        If Not Initializing Then
            koseiSubject.NotifyTitleObservers(columnIndex)
        End If
    End Sub

    ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
    ''' <summary>
    ''' INSTL品番区分が更新された事を通知する
    ''' </summary>
    ''' <param name="columnIndex">列index</param>
    ''' <remarks></remarks>
    Public Sub NotifyInstlDataKbn(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlDataKbn
        koseiSubject.InstlDataKbn(columnIndex) = alSubject.InstlDataKbn(columnIndex)
        If Not Initializing Then
            koseiSubject.NotifyTitleObservers(columnIndex)
        End If
    End Sub
    ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
    '↓↓2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
    ''' <summary>
    ''' ベース情報フラグが更新された事を通知する
    ''' </summary>
    ''' <param name="columnIndex">列index</param>
    ''' <remarks></remarks>
    Public Sub NotifyBaseInstlFlg(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyBaseInstlFlg
        koseiSubject.BaseInstlFlg(columnIndex) = alSubject.BaseInstlFlg(columnIndex)
        If Not Initializing Then
            koseiSubject.NotifyTitleObservers(columnIndex)
        End If
    End Sub
    ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END

    ''' <summary>
    ''' INSTL品番列に列挿入する
    ''' </summary>
    ''' <param name="columnIndex">列index</param>
    ''' <param name="insertCount">列挿入数</param>
    ''' <remarks></remarks>
    Public Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer) Implements EzSyncInstlHinban.InsertColumnInInstl
        'koseiSubject.InsertColumnInInstl(columnIndex, insertCount)
    End Sub

    ''' <summary>
    ''' INSTL品番列を列削除する
    ''' </summary>
    ''' <param name="columnIndex">列index</param>
    ''' <param name="removeCount">削除列数</param>
    ''' <remarks></remarks>
    Public Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer) Implements EzSyncInstlHinban.RemoveColumnInInstl
        'koseiSubject.RemoveColumnInInstl(columnIndex, removeCount)
        'koseiSubject.NotifyObservers()
    End Sub

    ''' <summary>
    ''' INSTL品番が更新された事を通知する(イベント品番コピー)
    ''' </summary>
    ''' <param name="columnIndex">列index</param>
    ''' <remarks></remarks>
    Public Sub NotifyInstlHinbanEventCopy(ByVal columnIndex As Integer) Implements EzSyncInstlHinban.NotifyInstlHinbanEventCopy
        'おいただけ'
    End Sub

    ''' <summary>
    ''' 新しいINSTL品番で構成を作成する(イベント品番コピー)
    ''' </summary>
    ''' <param name="shisakuEventCode">イベントコード</param>
    ''' <param name="shisakuBlockNo">ブロックNo</param>
    ''' <remarks></remarks>
    Public Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventCopyFlg As Boolean = False, Optional ByVal baseFlg As Boolean = False, Optional ByVal addStartIndex As Integer = 0) Implements EzSyncInstlHinban.NotifyInstlHinbanGetKosei
        '↓↓2014/10/23 酒井 ADD BEGIN
        'Public Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventCopyFlg As Boolean = False) Implements EzSyncInstlHinban.NotifyInstlHinbanGetKosei
        '↑↑2014/10/23 酒井 ADD END
        '↓↓2014/09/25 酒井 ADD BEGIN
        '    Public Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "") Implements EzSyncInstlHinban.NotifyInstlHinbanGetKosei
        '↑↑2014/09/25 酒井 ADD END
        '何もしない'
    End Sub

    ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bm) (TES)張 ADD BEGIN
    ''' <summary>
    ''' 補用部品検索画面で構成を作成する(イベント品番コピー)
    ''' </summary>
    ''' <param name="HoyouKoseiMatrix">補用部品検索画面のspread全行</param>
    ''' <remarks></remarks>
    Public Sub NotifyHoyouGetKosei(ByVal HoyouKoseiMatrix As ShisakuBuhinEdit.Kosei.Logic.Matrix.BuhinKoseiMatrix) Implements EzSyncInstlHinban.NotifyHoyouGetKosei

    End Sub
    ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bm) (TES)張 ADD END

    Public Sub bakEvent() Implements EzSyncInstlHinban.BakEvent

    End Sub

    ''' <summary>
    '''バックアップマトリクスを取得
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetBackUpKosei() Implements EzSyncInstlHinban.SetBackUpKosei

    End Sub


End Class