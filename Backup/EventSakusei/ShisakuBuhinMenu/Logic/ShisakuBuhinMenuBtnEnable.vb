Imports ShisakuCommon.Db.EBom.Vo.Helper

Namespace ShisakuBuhinMenu.Logic
    ''' <summary>
    ''' 試作部品メニューの使用可否情報を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShisakuBuhinMenuBtnEnable
#Region "プロパティ"
        ' 「イベント登録・編集」機能の使用可否
        Private _eventRegister As Boolean
        ' 〆切日を表示するか？
        Private _showShimerkiribi As Boolean
        ' 「〆切日」機能の使用可否
        Private _shimekiribi As Boolean
        ' 「設計展開」機能の使用可否
        Private _sekkeiTenkai As Boolean
        ' 「差戻し」機能の使用可否
        Private _sashimodoshi As Boolean
        ' 「〆切」機能の使用可否
        Private _shimekiri As Boolean
        ' 「完了」機能の使用可否
        Private _kanryo As Boolean
        ' 「中止」機能の使用可否
        Private _chushi As Boolean
        ' 「手配帳作成」機能の使用可否
        Private _tehaichoSakusei As Boolean
        ' 「Excel出力」機能の使用可否
        Private _excelExport As Boolean
#End Region

#Region "プロパティGetter"
        ''' <summary>「イベント登録・編集」機能の使用可否</summary>
        ''' <value>「イベント登録・編集」機能の使用可否</value>
        ''' <returns>「イベント登録・編集」機能の使用可否</returns>
        Public ReadOnly Property EventRegister() As Boolean
            Get
                Return _eventRegister
            End Get
        End Property

        ''' <summary>〆切日を表示するか？</summary>
        ''' <value>〆切日を表示するか？</value>
        ''' <returns>〆切日を表示するか？</returns>
        Public ReadOnly Property ShowShimerkiribi() As Boolean
            Get
                Return _showShimerkiribi
            End Get
        End Property

        ''' <summary>「〆切日」機能の使用可否</summary>
        ''' <value>「〆切日」機能の使用可否</value>
        ''' <returns>「〆切日」機能の使用可否</returns>
        Public ReadOnly Property Shimekiribi() As Boolean
            Get
                Return _shimekiribi
            End Get
        End Property

        ''' <summary>「設計展開」機能の使用可否</summary>
        ''' <value>「設計展開」機能の使用可否</value>
        ''' <returns>「設計展開」機能の使用可否</returns>
        Public ReadOnly Property SekkeiTenkai() As Boolean
            Get
                Return _sekkeiTenkai
            End Get
        End Property

        ''' <summary>「差戻し」機能の使用可否</summary>
        ''' <value>「差戻し」機能の使用可否</value>
        ''' <returns>「差戻し」機能の使用可否</returns>
        Public ReadOnly Property Sashimodoshi() As Boolean
            Get
                Return _sashimodoshi
            End Get
        End Property

        ''' <summary>「〆切」機能の使用可否</summary>
        ''' <value>「〆切」機能の使用可否</value>
        ''' <returns>「〆切」機能の使用可否</returns>
        Public ReadOnly Property Shimekiri() As Boolean
            Get
                Return _shimekiri
            End Get
        End Property

        ''' <summary>「完了」機能の使用可否</summary>
        ''' <value>「完了」機能の使用可否</value>
        ''' <returns>「完了」機能の使用可否</returns>
        Public ReadOnly Property Kanryo() As Boolean
            Get
                Return _kanryo
            End Get
        End Property

        ''' <summary>「中止」機能の使用可否</summary>
        ''' <value>「中止」機能の使用可否</value>
        ''' <returns>「中止」機能の使用可否</returns>
        Public ReadOnly Property Chushi() As Boolean
            Get
                Return _chushi
            End Get
        End Property

        ''' <summary>「手配帳作成」機能の使用可否</summary>
        ''' <value>「手配帳作成」機能の使用可否</value>
        ''' <returns>「手配帳作成」機能の使用可否</returns>
        Public ReadOnly Property TehaichoSakusei() As Boolean
            Get
                Return _tehaichoSakusei
            End Get
        End Property

        ''' <summary>「Excel出力」機能の使用可否</summary>
        ''' <value>「Excel出力」機能の使用可否</value>
        ''' <returns>「Excel出力」機能の使用可否</returns>
        Public ReadOnly Property ExcelExport() As Boolean
            Get
                Return _excelExport
            End Get
        End Property
#End Region
        ''' <summary>Empty</summary>
        Public Shared ReadOnly EMPTY As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(False, False, False, False, False, False, False, False, False, False)

        Private Shared ReadOnly BEFORE_REGISTER As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, False, False, False, False, False, False, False, False, False)
        Private Shared ReadOnly SAVE As ShisakuBuhinMenuBtnEnable = BEFORE_REGISTER
        Private Shared ReadOnly SEKKEI_MAINTAINING As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, False, False, True, True, False, True, False, True)
        Private Shared ReadOnly SASHIMODOSHI_ING As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, True, True, False, False, False, True, False, True)
        Private Shared ReadOnly KAITEI_UKETSUKE_ING As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, False, False, False, False, True, True, True, True)
        Private Shared ReadOnly KANRYO_ As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, False, False, False, True, False, True, True, True)
        Private Shared ReadOnly CHUSHI_UKETSUKE_GO As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, False, False, False, True, True, False, False, True)
        Private Shared ReadOnly CHUSHI_UKETSUKE_MAE As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, False, False, True, True, True, False, False, True)
        Private Shared ReadOnly SHIMEKIRIBI_INPUT As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, True, True, False, False, False, True, False, False)
        Private Shared ReadOnly SHIMEKIRIBI_UNINPUT As ShisakuBuhinMenuBtnEnable = New ShisakuBuhinMenuBtnEnable(True, True, True, False, False, False, False, True, False, False)

        Private Sub New(ByVal eventRegister As Boolean, ByVal showShimerkiribi As Boolean, ByVal shimekiribi As Boolean, ByVal sekkeiTenkai As Boolean, ByVal sashimodoshi As Boolean, ByVal shimekiri As Boolean, ByVal kanryo As Boolean, ByVal chushi As Boolean, ByVal tehaichoSakusei As Boolean, ByVal excelExport As Boolean)
            Me._eventRegister = eventRegister
            Me._showShimerkiribi = showShimerkiribi
            Me._shimekiribi = shimekiribi
            Me._sekkeiTenkai = sekkeiTenkai
            Me._sashimodoshi = sashimodoshi
            Me._shimekiri = shimekiri
            Me._kanryo = kanryo
            Me._chushi = chushi
            Me._tehaichoSakusei = tehaichoSakusei
            Me._excelExport = excelExport
        End Sub

        ''' <summary>
        ''' 使用可否情報を返す
        ''' </summary>
        ''' <param name="dataKbn">データ区分</param>
        ''' <param name="status">ステータス</param>
        ''' <param name="shimekiribi">〆切日</param>
        ''' <returns>使用可否情報</returns>
        ''' <remarks></remarks>
        Public Shared Function Detect(ByVal dataKbn As String, ByVal status As String, ByVal shimekiribi As Nullable(Of Date)) As ShisakuBuhinMenuBtnEnable
            If dataKbn Is Nothing Then
                Return BEFORE_REGISTER
            ElseIf TShisakuEventVoHelper.DataKbn.SAVE.Equals(dataKbn) Then
                Return SAVE
            ElseIf TShisakuEventVoHelper.DataKbn.REGISTER.Equals(dataKbn) Then
                If TShisakuEventVoHelper.Status.SEKKEI_MAINTAINING.Equals(status) Then
                    Return SEKKEI_MAINTAINING
                ElseIf TShisakuEventVoHelper.Status.SASHIMODOSHI_ING.Equals(status) Then
                    Return SASHIMODOSHI_ING
                ElseIf TShisakuEventVoHelper.Status.KAITEI_UKETSUKE_ING.Equals(status) Then
                    Return KAITEI_UKETSUKE_ING
                ElseIf TShisakuEventVoHelper.Status.KANRYO.Equals(status) Then
                    Return KANRYO_
                ElseIf TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_GO.Equals(status) Then
                    Return CHUSHI_UKETSUKE_GO
                ElseIf TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_MAE.Equals(status) Then
                    Return CHUSHI_UKETSUKE_MAE
                ElseIf shimekiribi IsNot Nothing Then
                    Return SHIMEKIRIBI_INPUT
                Else
                    Return SHIMEKIRIBI_UNINPUT
                End If
            End If
            Throw New NotSupportedException(String.Format("この組み合わせは未対応です. (dataKbn, status, shimekiribi)=({0}, {1}, {2})", dataKbn, status, shimekiribi))
        End Function
    End Class
End Namespace