Imports ShisakuCommon.Db.EBom.Vo.Helper

Namespace ShisakuBuhinEditBlock.Logic
    Public Class EventEditEnabled
#Region "プロパティ"
        ' 開発符号の排他
        Private _ShisakuKaihatsuFugoEnabled As Boolean
        ' イベントフェーズ名の排他
        Private _ShisakuEventPhaseNameEnabled As Boolean
        ' イベント名の排他
        Private _ShisakuEventNameEnabled As Boolean
        ' ユニット区分の排他
        Private _UnitKbnEnabled As Boolean
        ' コピーボタンの排他
        Private _BtnCopyEnabled As Boolean
        ' Excel入力ボタンの排他
        Private _BtnExcelImportEnabled As Boolean
        ' ベース車Spreadの排他
        Private _SpdBaseCarEnabled As Boolean
        ' 完成車Spread号車列の排他
        Private _SpdCompleteCarGoshaEnabled As Boolean
        ' 装備仕様Spread号車列の排他
        Private _SpdOptionCarGoshaEnabled As Boolean
        ' 装備仕様Spreadヘッダー部の追加変更の排他
        Private _SpdOptionCarTitleModifyEnabled As Boolean
#End Region
#Region "プロパティGetter"
        ''' <summary>開発符号の排他</summary>
        ''' <value>開発符号の排他</value>
        ''' <returns>開発符号の排他</returns>
        Public ReadOnly Property ShisakuKaihatsuFugoEnabled() As Boolean
            Get
                Return _ShisakuKaihatsuFugoEnabled
            End Get
        End Property

        ''' <summary>イベントフェーズ名の排他</summary>
        ''' <value>イベントフェーズ名の排他</value>
        ''' <returns>イベントフェーズ名の排他</returns>
        Public ReadOnly Property ShisakuEventPhaseNameEnabled() As Boolean
            Get
                Return _ShisakuEventPhaseNameEnabled
            End Get
        End Property

        ''' <summary>イベント名の排他</summary>
        ''' <value>イベント名の排他</value>
        ''' <returns>イベント名の排他</returns>
        Public ReadOnly Property ShisakuEventNameEnabled() As Boolean
            Get
                Return _ShisakuEventNameEnabled
            End Get
        End Property

        ''' <summary>ユニット区分の排他</summary>
        ''' <value>ユニット区分の排他</value>
        ''' <returns>ユニット区分の排他</returns>
        Public ReadOnly Property UnitKbnEnabled() As Boolean
            Get
                Return _UnitKbnEnabled
            End Get
        End Property

        ''' <summary>コピーボタンの排他</summary>
        ''' <value>コピーボタンの排他</value>
        ''' <returns>コピーボタンの排他</returns>
        Public ReadOnly Property BtnCopyEnabled() As Boolean
            Get
                Return _BtnCopyEnabled
            End Get
        End Property

        ''' <summary>Excel入力ボタンの排他</summary>
        ''' <value>Excel入力ボタンの排他</value>
        ''' <returns>Excel入力ボタンの排他</returns>
        Public ReadOnly Property BtnExcelImportEnabled() As Boolean
            Get
                Return _BtnExcelImportEnabled
            End Get
        End Property

        ''' <summary>ベース車Spreadの排他</summary>
        ''' <value>ベース車Spreadの排他</value>
        ''' <returns>ベース車Spreadの排他</returns>
        Public ReadOnly Property SpdBaseCarEnabled() As Boolean
            Get
                Return _SpdBaseCarEnabled
            End Get
        End Property

        ''' <summary>完成車Spread号車列の排他</summary>
        ''' <value>完成車Spread号車列の排他</value>
        ''' <returns>完成車Spread号車列の排他</returns>
        Public ReadOnly Property SpdCompleteCarGoshaEnabled() As Boolean
            Get
                Return _SpdCompleteCarGoshaEnabled
            End Get
        End Property

        ''' <summary>装備仕様Spread号車列の排他</summary>
        ''' <value>装備仕様Spread号車列の排他</value>
        ''' <returns>装備仕様Spread号車列の排他</returns>
        Public ReadOnly Property SpdOptionCarGoshaEnabled() As Boolean
            Get
                Return _SpdOptionCarGoshaEnabled
            End Get
        End Property

        ''' <summary>装備仕様Spreadヘッダー部の追加変更の排他</summary>
        ''' <value>装備仕様Spreadヘッダー部の追加変更の排他</value>
        ''' <returns>装備仕様Spreadヘッダー部の追加変更の排他</returns>
        Public ReadOnly Property SpdOptionCarTitleModifyEnabled() As Boolean
            Get
                Return _SpdOptionCarTitleModifyEnabled
            End Get
        End Property
#End Region

        Private Shared ReadOnly ALL_DISABLED As New EventEditEnabled(False, False, False, False, False, False, False, False, False, False)
        Private Shared ReadOnly ALL_ENABLED As New EventEditEnabled(True, True, True, True, True, True, True, True, True, True)
        Private Shared ReadOnly ALL_ENABLED_MODIFY_MODE As New EventEditEnabled(False, False, True, False, True, True, True, True, True, True)

        ''' <summary>
        ''' コンストラクタ（非公開）
        ''' </summary>
        Private Sub New(ByVal ShisakuKaihatsuFugoEnabled As Boolean, ByVal ShisakuEventPhaseNameEnabled As Boolean, ByVal ShisakuEventNameEnabled As Boolean, ByVal UnitKbnEnabled As Boolean, ByVal BtnCopyEnabled As Boolean, ByVal BtnExcelImportEnabled As Boolean, ByVal SpdBaseCarEnabled As Boolean, ByVal SpdCompleteCarGoshaEnabled As Boolean, ByVal SpdOptionCarGoshaEnabled As Boolean, ByVal SpdOptionCarTitleModifyEnabled As Boolean)
            Me._ShisakuKaihatsuFugoEnabled = ShisakuKaihatsuFugoEnabled
            Me._ShisakuEventPhaseNameEnabled = ShisakuEventPhaseNameEnabled
            Me._ShisakuEventNameEnabled = ShisakuEventNameEnabled
            Me._UnitKbnEnabled = UnitKbnEnabled
            Me._BtnCopyEnabled = BtnCopyEnabled
            Me._BtnExcelImportEnabled = BtnExcelImportEnabled
            Me._SpdBaseCarEnabled = SpdBaseCarEnabled
            Me._SpdCompleteCarGoshaEnabled = SpdCompleteCarGoshaEnabled
            Me._SpdOptionCarGoshaEnabled = SpdOptionCarGoshaEnabled
            Me._SpdOptionCarTitleModifyEnabled = SpdOptionCarTitleModifyEnabled
        End Sub
        ''' <summary>
        ''' 使用可否情報を返す
        ''' </summary>
        ''' <param name="status">ステータス</param>
        ''' <returns>使用可否情報</returns>
        ''' <remarks></remarks>
        Public Shared Function Detect(ByVal status As String, ByVal isAddMode As Boolean) As EventEditEnabled
            Select Case status
                Case TShisakuEventVoHelper.Status.SEKKEI_MAINTAINING, _
                        TShisakuEventVoHelper.Status.KAITEI_UKETSUKE_ING, _
                        TShisakuEventVoHelper.Status.KANRYO, _
                        TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_GO, _
                        TShisakuEventVoHelper.Status.CHUSHI_UKETSUKE_MAE
                    Return ALL_DISABLED
            End Select
            If Not isAddMode Then
                Return ALL_ENABLED_MODIFY_MODE
            End If
            Return ALL_ENABLED
        End Function
    End Class
End Namespace