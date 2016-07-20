Imports ShisakuCommon.Util
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon
Imports EventSakusei.ShisakuBuhinEdit.Selector.Dao
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Selector.Logic

    ''' <summary>
    ''' イベント品番コピーサブジェクト
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventBuhinCopySubject : Inherits Observable
        Private shisakuEventCode As String
        Private shisakublockNo As String
        Private selimpl As DispEventBuhinCopySelectorDao

        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuBlockNo As String)
            selimpl = New DispEventBuhinCopySelectorDaoImpl
            Me.shisakublockNo = shisakuBlockNo
            Me.shisakuEventCode = shisakuEventCode

            Dim eventVo As New TShisakuEventVo
            eventVo = selimpl.FindBykaiHatsuFugo(shisakuBlockNo, shisakuEventCode)

            If Not eventVo Is Nothing Then
                _KaihatsuFugo = eventVo.ShisakuKaihatsuFugo
            End If

            'グループNo'
            KaihatsuFugoLabelValues = GetLabelValues_KaihatsuFugo()
            SetChanged()

        End Sub

#Region "コンボボックスの処理"

        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <remarks></remarks>
        Private Class KaihatsuFugoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim lVo As New TShisakuEventVo
                aLocator.IsA(lVo).Label(lVo.ShisakuKaihatsuFugo).Value(lVo.ShisakuKaihatsuFugo)
            End Sub
        End Class
        ''' <summary>
        ''' 開発符号
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetLabelValues_KaihatsuFugo() As List(Of LabelValueVo)
            If KaihatsuFugo Is String.Empty Then
                Return New List(Of LabelValueVo)
            End If
            Dim vo As New TShisakuEventVo
            vo.ShisakuKaihatsuFugo = KaihatsuFugo
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of TShisakuEventVo).Extract(selimpl.FindEventkaiHatsuFugoList(shisakublockNo, shisakuEventCode), New KaihatsuFugoExtraction)
            results.Sort(New LabelValueComparer)
            Return results

        End Function
#End Region

#Region "公開プロパティ"
        '' 開発符号
        Private _KaihatsuFugo As String

#End Region


#Region "公開プロパティの実装"

        ''' <summary>開発符号</summary>
        ''' <returns>開発符号</returns>
        Public Property KaihatsuFugo() As String
            Get
                Return _KaihatsuFugo
            End Get
            Set(ByVal value As String)

                If EzUtil.IsEqualIfNull(_KaihatsuFugo, value) Then
                    Return
                End If
                _KaihatsuFugo = value
                SetChanged()

                KaihatsuFugoLabelValues() = GetLabelValues_KaihatsuFugo()
            End Set
        End Property

#End Region

#Region "公開プロパティの選択値"

        '' 開発符号の選択値
        Private _KaihatsuFugoLabelValues As List(Of LabelValueVo)

        ''' <summary>開発符号の選択値</summary>
        ''' <value>開発符号の選択値</value>
        ''' <returns>開発符号の選択値</returns>
        Public Property KaihatsuFugoLabelValues() As List(Of LabelValueVo)
            Get
                Return _KaihatsuFugoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _KaihatsuFugoLabelValues = value
            End Set
        End Property

#End Region




    End Class

End Namespace