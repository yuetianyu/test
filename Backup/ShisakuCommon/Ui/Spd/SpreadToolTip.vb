Imports FarPoint.Win.Spread

Namespace Ui.Spd


    ''' <summary>
    ''' Spreadの特定セルでのToolTip表示を設定するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpreadToolTip
        Public Interface AnEvent
            ''' <summary>
            ''' ToolTipのイベント<br/>
            ''' セル行列判断や、文言や、Viewの設定
            ''' </summary>
            ''' <param name="sender">TextTipFetchイベントに準ずる</param>
            ''' <param name="e">TextTipFetchイベントに準ずる</param>
            ''' <remarks></remarks>
            Sub TextTipFetch(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TextTipFetchEventArgs)
        End Interface
        Private ReadOnly spread As FpSpread
        Private events As New List(Of AnEvent)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="spread">Spread</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal spread As FpSpread)
            Me.spread = spread

            'ツールチップの表示開始待ち時間(ミリ秒)
            spread.TextTipDelay = 500

            'ツールチップの表示位置(設定しないと TextTipFetch イベントが発生しない)
            spread.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.Floating
        End Sub
        ''' <summary>
        ''' ToolTipイベントを登録する
        ''' </summary>
        ''' <param name="anEvent">ToolTipイベント</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal anEvent As AnEvent)
            AddHandler spread.TextTipFetch, AddressOf anEvent.TextTipFetch
            events.Add(anEvent)
        End Sub
        ''' <summary>
        ''' 登録済みToolTipイベントをクリアする
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            For Each anEvent As AnEvent In events
                RemoveHandler spread.TextTipFetch, AddressOf anEvent.TextTipFetch
            Next
            events.Clear()
        End Sub
    End Class
End Namespace