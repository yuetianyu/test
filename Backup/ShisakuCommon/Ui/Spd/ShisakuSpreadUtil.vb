Imports ShisakuCommon.Ui.Spd.Event
Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win.Spread

Namespace Ui.Spd


    ''' <summary>
    ''' 試作システム固有のユーティリティを集めたクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuSpreadUtil

        ''' <summary>
        ''' イベント情報画面で使用するComboBoxCellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As ComboBoxCellType)
            aCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 入力時、前方一致
            aCellType.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            aCellType.AutoCompleteSource = AutoCompleteSource.ListItems
            '' 小文字を大文字にする
            'aCellType.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            aCellType.CharacterSet = CharacterSet.Ascii
            '' 編集可能にする
            aCellType.Editable = True
        End Sub

        ''' <summary>
        ''' イベント情報画面で使用するTextCellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As TextCellType)
            aCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            '' 小文字を大文字にする
            aCellType.CharacterCasing = CharacterCasing.Upper
            '' 半角英数記号(Asciiだと全角も入力出来る)
            aCellType.CharacterSet = CharacterSet.Ascii
        End Sub

        ''' <summary>
        ''' イベント情報画面で使用するDateTimeCellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As DateTimeCellType)
            aCellType.DropDownButton = True
        End Sub

        ''' <summary>
        ''' イベント情報画面で使用するCheckBoxCellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As CheckBoxCellType)
            '' nop 今は無し
        End Sub

        ''' <summary>
        ''' 通貨CellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As CurrencyCellType)
            aCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            aCellType.Separator = ","
            aCellType.ShowSeparator = True
            aCellType.ShowCurrencySymbol = False
        End Sub

        ''' <summary>
        ''' 通常使われるテキストCellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralTextCellType() As TextCellType
            Dim result As New TextCellType
            SettingDefaultProperty(result)
            Return result
        End Function

        ''' <summary>
        ''' 通常使われる日時CellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralDateTimeCellType() As DateTimeCellType
            Dim result As New DateTimeCellType
            SettingDefaultProperty(result)
            Return result
        End Function

        ''' <summary>
        ''' 通常使われるコンボボックスCellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralComboBoxCellType() As ComboBoxCellType
            Dim result As New ComboBoxCellType
            SettingDefaultProperty(result)
            Return result
        End Function

        ''' <summary>
        ''' 通常使われるチェックボックスCellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralCheckBoxCellType() As CheckBoxCellType
            Dim result As New CheckBoxCellType
            SettingDefaultProperty(result)
            Return result
        End Function

        ''' <summary>
        ''' 通常使われる通貨CellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralCurrencyCellType() As CurrencyCellType
            Dim result As New CurrencyCellType
            SettingDefaultProperty(result)
            Return result
        End Function

        '↓↓↓2014/12/24 メタル項目を追加 TES)張 ADD BEGIN
        ''' <summary>
        ''' 通常使われる数値CellTypeを作成して返す
        ''' </summary>
        ''' <returns>新しいインスタンス</returns>
        ''' <remarks></remarks>
        Public Shared Function NewGeneralNumberCellType() As NumberCellType
            Dim result As New NumberCellType
            SettingDefaultProperty(result)
            Return result
        End Function
        ''' <summary>
        ''' 数値CellTypeの初期設定を行う
        ''' </summary>
        ''' <param name="aCellType">設定先のCellType</param>
        ''' <remarks></remarks>
        Public Shared Sub SettingDefaultProperty(ByVal aCellType As NumberCellType)
            aCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.Arrows
            aCellType.Separator = ","
            aCellType.ShowSeparator = True
        End Sub
        '↑↑↑2014/12/24 メタル項目を追加 TES)張 ADD END

        ''' <summary>
        ''' セル右クリック時のイベントを追加する
        ''' </summary>
        ''' <param name="spread">追加対象のSpread</param>
        ''' <param name="inputSupport">入力サポートを使用していたら指定</param>
        ''' <remarks></remarks>
        Public Shared Sub AddEventCellRightClick(ByVal spread As FpSpread, ByVal inputSupport As ShisakuInputSupport)
            Dim clickEvent As New SpreadRightClickEvent(spread, inputSupport)
            AddHandler spread.CellClick, AddressOf clickEvent.Spread_CellClick
        End Sub

    End Class
End Namespace