Imports FarPoint.Win.Spread.CellType
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Spd

Namespace ShisakuBuhinEditBlock.Ui

    ''' <summary>
    ''' イベント情報画面で使用するコントロールのFactoryクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventControlFactory
        Private Shared ShubetsuCellType As ComboBoxCellType
        Private Shared GoshaCellType As TextCellType

        ''' <summary>
        ''' 種別セルを返す
        ''' </summary>
        ''' <returns>種別セル</returns>
        ''' <remarks></remarks>
        Public Shared Function GetShubetsuCellType() As ComboBoxCellType
            If ShubetsuCellType Is Nothing Then
                ShubetsuCellType = SpreadUtil.CreateComboBoxCellType(Logic.EventEdit.GetLabelValues_ShisakuSyubetu, False)
                ShisakuSpreadUtil.SettingDefaultProperty(ShubetsuCellType)
                ShubetsuCellType.MaxLength = 1
            End If
            Return ShubetsuCellType
        End Function
        ''' <summary>
        ''' 号車セルを返す
        ''' </summary>
        ''' <returns>号車セル</returns>
        ''' <remarks></remarks>
        Public Shared Function GetGoshaCellType() As TextCellType
            If GoshaCellType Is Nothing Then
                GoshaCellType = ShisakuSpreadUtil.NewGeneralTextCellType()
                GoshaCellType.MaxLength = 8
            End If
            Return GoshaCellType
        End Function
    End Class
End Namespace