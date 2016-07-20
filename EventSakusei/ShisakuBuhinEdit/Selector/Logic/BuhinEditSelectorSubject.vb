Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Util
Imports EventSakusei.Soubi

Namespace ShisakuBuhinEdit.Selector.Logic

    Public Class BuhinEditSelectorSubject : Inherits Observable
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly anEventSoubiDao As EventSoubiDao

        ' 機能仕様表示選択（ベース車情報）
        Private _baseCarSubject As BuhinEditSelectorBaseCompleteSubject
        ' 機能仕様表示選択（設計展開ベース車情報）
        Private _baseTenkaiCarSubject As BuhinEditSelectorBaseCompleteSubject
        ' 機能仕様表示選択（完成車情報）
        Private _completeCarSubject As BuhinEditSelectorBaseCompleteSubject
        ' 機能仕様表示選択（基本装備仕様情報）
        Private _basicOptionSubject As BuhinEditSelectorOptionSubject
        ' 機能仕様表示選択（特別装備仕様情報）
        Private _specialOptionSubject As BuhinEditSelectorOptionSubject
        ' 機能仕様表示選択（ＷＢ車特別装備仕様情報）
        Private _specialOptionWbSubject As BuhinEditSelectorOptionSubject

        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal anEventSoubiDao As EventSoubiDao, _
                       ByVal voBag As BuhinEditAlShowColumnBag)
            Me.shisakuEventCode = shisakuEventCode
            Me.anEventSoubiDao = anEventSoubiDao

            _baseCarSubject = New BuhinEditSelectorBaseCompleteSubject(voBag.SoubiVos)
            _baseTenkaiCarSubject = New BuhinEditSelectorBaseCompleteSubject(voBag.SoubiVos)
            _completeCarSubject = New BuhinEditSelectorBaseCompleteSubject(voBag.SoubiVos)
            _basicOptionSubject = New BuhinEditSelectorOptionSubject(shisakuEventCode, TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.BASIC_OPTION, anEventSoubiDao, voBag.SoubiShiyouVos, "B")
            _specialOptionSubject = New BuhinEditSelectorOptionSubject(shisakuEventCode, TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.SPECIAL_OPTION, anEventSoubiDao, voBag.SoubiShiyouVos, "C")
            _specialOptionWbSubject = New BuhinEditSelectorOptionSubject(shisakuEventCode, TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.SPECIAL_OPTION, anEventSoubiDao, voBag.SoubiShiyouVos, "W")

            ' 表示の内容 eventSoubiDao
            'basicVos = anEventSoubiDao.FindWithTitleNameBy(shisakuEventCode, TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.BASIC_OPTION)
            'specialVos = anEventSoubiDao.FindWithTitleNameBy(shisakuEventCode, TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.SPECIAL_OPTION)

            ' チャック済みの内容 soubiDao/soubiShiyouDao

            SetChanged()
        End Sub

        ''' <summary>機能仕様表示選択（ベース車情報）</summary>
        ''' <returns>機能仕様表示選択（ベース車情報）</returns>
        Public ReadOnly Property BaseCarSubject() As BuhinEditSelectorBaseCompleteSubject
            Get
                Return _baseCarSubject
            End Get
        End Property

        ''' <summary>機能仕様表示選択（設計展開ベース車情報）</summary>
        ''' <returns>機能仕様表示選択（設計展開ベース車情報）</returns>
        Public ReadOnly Property BaseTenkaiCarSubject() As BuhinEditSelectorBaseCompleteSubject
            Get
                Return _baseTenkaiCarSubject
            End Get
        End Property

        ''' <summary>機能仕様表示選択（完成車情報）</summary>
        ''' <returns>機能仕様表示選択（完成車情報）</returns>
        Public ReadOnly Property CompleteCarSubject() As BuhinEditSelectorBaseCompleteSubject
            Get
                Return _completeCarSubject
            End Get
        End Property

        ''' <summary>機能仕様表示選択（基本装備仕様情報）</summary>
        ''' <returns>機能仕様表示選択（基本装備仕様情報）</returns>
        Public ReadOnly Property BasicOptionSubject() As BuhinEditSelectorOptionSubject
            Get
                Return _basicOptionSubject
            End Get
        End Property

        ''' <summary>機能仕様表示選択（特別装備仕様情報）</summary>
        ''' <returns>機能仕様表示選択（特別装備仕様情報）</returns>
        Public ReadOnly Property SpecialOptionSubject() As BuhinEditSelectorOptionSubject
            Get
                Return _specialOptionSubject
            End Get
        End Property

        ''' <summary>機能仕様表示選択（ＷＢ車特別装備仕様情報）</summary>
        ''' <returns>機能仕様表示選択（ＷＢ車特別装備仕様情報）</returns>
        Public ReadOnly Property SpecialOptionWbSubject() As BuhinEditSelectorOptionSubject
            Get
                Return _specialOptionWbSubject
            End Get
        End Property

    End Class
End Namespace