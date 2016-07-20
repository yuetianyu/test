Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix

Namespace ShisakuBuhinEdit.Logic
    ''' <summary>
    ''' INSTL品番関連で同期を取らせる為の簡単なinterface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface EzSyncInstlHinban
        ''' <summary>
        ''' INSTL品番が更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinban(ByVal columnIndex As Integer)
        ''' <summary>
        ''' INSTL品番区分が更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinbanKbn(ByVal columnIndex As Integer)

        ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
        ''' <summary>
        ''' INSTLデータ区分が更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlDataKbn(ByVal columnIndex As Integer)
        ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END

        '↓↓2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
        ''' <summary>
        ''' ベース情報フラグが更新された事を通知する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Sub NotifyBaseInstlFlg(ByVal columnIndex As Integer)
        ''↑↑2014/08/15 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END

        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="insertCount">列挿入数</param>
        ''' <remarks></remarks>
        Sub InsertColumnInInstl(ByVal columnIndex As Integer, ByVal insertCount As Integer)

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Sub RemoveColumnInInstl(ByVal columnIndex As Integer, ByVal removeCount As Integer)

        ''' <summary>
        ''' INSTL品番が更新された事を通知する(イベント品番コピー)
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinbanEventCopy(ByVal columnIndex As Integer)

        ''' <summary>
        ''' 新しいINSTL品番で構成を作成する(イベント品番コピー)
        ''' </summary>
        ''' <param name="shisakuEventCode">イベントコード</param>
        ''' <param name="shisakuBlockNo">ブロックNo</param>
        ''' <param name="kaiteiNo">改定No　  2014/08/04 Ⅰ.11.改訂戻し機能 i) (TES)施 追加 </param>
        ''' <remarks></remarks>
        Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventcopyflg As Boolean = False, Optional ByVal baseFlg As Boolean = False, Optional ByVal addStartIndex As Integer = 0)
        '↓↓2014/10/23 酒井 ADD BEGIN
        'Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "", Optional ByVal eventcopyflg As Boolean = False)
        '↑↑2014/10/23 酒井 ADD END
        '↓↓2014/09/25 酒井 ADD BEGIN
        '        Sub NotifyInstlHinbanGetKosei(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String, Optional ByVal KaiteiNo As String = "")
        '↑↑2014/09/25 酒井 ADD END



        ''↓↓2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bl) (TES)張 ADD BEGIN
        ''' <summary>
        ''' 補用部品検索画面で構成を作成する(イベント品番コピー) 
        ''' </summary>
        ''' <param name="HoyouKoseiMatrix">補用部品検索画面のspread全行</param>
        ''' <remarks></remarks>
        Sub NotifyHoyouGetKosei(ByVal HoyouKoseiMatrix As BuhinKoseiMatrix)
        ''↑↑2014/08/01 Ⅰ.3.設計編集 ベース車改修専用化_bl) (TES)張 ADD END

        ''' <summary>
        '''バックアップマトリクスを取得
        ''' </summary>
        ''' <remarks></remarks>
        Sub SetBackUpKosei()

        Sub BakEvent()

    End Interface
End Namespace