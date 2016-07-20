Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db
Imports ShisakuCommon
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.Soubi

Namespace ShisakuBuhinEditBlock.Logic
    Public Class EventEdit : Inherits Observable

        Private _shisakuEventCode As String
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aHeaderSubject As EventEditHeader
        Private ReadOnly aBaseCarSubject As EventEditBaseCar
        Private ReadOnly exclusionShisakuEvent As TShisakuEventExclusion
        Private ReadOnly aShisakuDate As ShisakuDate
        Private _registError As Boolean

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="aLoginInfo">ログイン情報</param>
        ''' <param name="aHeaderSubject">ヘッダー情報（コピー処理時のみ必要）</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal aLoginInfo As LoginInfo, _
                       ByVal importFlag As Boolean, _
                       Optional ByVal aHeaderSubject As EventEditHeader = Nothing)

            Me._shisakuEventCode = shisakuEventCode
            Me.aLoginInfo = aLoginInfo
            Dim aTShisakuEventDao As TShisakuEventDaoImpl = New TShisakuEventDaoImpl
            aShisakuDate = New ShisakuDate
            _registError = False

            Me.aHeaderSubject = New EventEditHeader(shisakuEventCode, _
                                                       aLoginInfo, _
                                                       New MShisakuKaihatufugoDaoImpl, _
                                                       aTShisakuEventDao, _
                                                       New MShisakuStatusDaoImpl, _
                                                       aShisakuDate)
            aBaseCarSubject = New EventEditBaseCar(shisakuEventCode, aLoginInfo, _
                                                   Me.aHeaderSubject.IsSekkeiTenkaiIkou, _
                                                   New EventEditBaseCarDaoImpl, _
                                                   New TShisakuEventBaseSeisakuIchiranDaoImpl, _
                                                   New TShisakuEventBaseDaoImpl, _
                                                   aShisakuDate)

            exclusionShisakuEvent = New TShisakuEventExclusion

            IsViewerMode = False

            If Not IsAddMode() Then
                exclusionShisakuEvent.Save(shisakuEventCode)
            End If
            SetChanged()
        End Sub

#Region "公開プロパティGet/Set"

        ''' <summary>試作イベントコード</summary>
        ''' <returns>試作イベントコード</returns>
        Public ReadOnly Property ShisakuEventCode() As String
            Get
                Return _shisakuEventCode
            End Get
        End Property

        ''' 参照モードかを保持
        Private _isViewerMode As Boolean
        ''' <summary>参照モードか</summary>
        ''' <value>参照モードか</value>
        ''' <returns>参照モードか</returns>
        Public Property IsViewerMode() As Boolean
            Get
                Return _isViewerMode
            End Get
            Set(ByVal value As Boolean)
                If EzUtil.IsEqualIfNull(_isViewerMode, value) Then
                    Return
                End If
                _isViewerMode = value
                SetChanged()

                aHeaderSubject.IsViewerMode = value
                aBaseCarSubject.IsViewerMode = value
            End Set
        End Property

        ''' <summary>試作イベントコード</summary>
        ''' <returns>試作イベントコード</returns>
        Public ReadOnly Property RegistError() As Boolean
            Get
                Return _registError
            End Get
        End Property

#End Region

        ''' <summary>
        ''' 登録モードかを返す
        ''' </summary>
        ''' <returns>登録モードの場合、true</returns>
        ''' <remarks></remarks>
        Private Function IsAddMode() As Boolean
            Return _shisakuEventCode Is Nothing OrElse StringUtil.IsEmpty(_shisakuEventCode)
        End Function

        ''' <summary>
        ''' 登録処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RegisterMain()
            aShisakuDate.Clear()
            '通常
            aBaseCarSubject.RegisterMain(aHeaderSubject.ShisakuEventCode, True)
        End Sub

        ''' <summary>
        ''' ユーザー名の取得
        ''' </summary>
        ''' <param name="userId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New ShisakuDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

#Region "ヘッダ部"
        Public ReadOnly Property HeaderSubject() As EventEditHeader
            Get
                Return aHeaderSubject
            End Get
        End Property
#End Region

#Region "ベース車"
        Public ReadOnly Property BaseCarSubject() As EventEditBaseCar
            Get
                Return aBaseCarSubject
            End Get
        End Property

#End Region

        ' TODO TShisakuEventBaseSeisakuIchiranVoHelperへ
        ''' <summary>
        ''' 試作種別の設定
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetLabelValues_ShisakuSyubetu() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            results.Add(New LabelValueVo("", Nothing))
            results.Add(New LabelValueVo("W", "W"))
            results.Add(New LabelValueVo("D", "D"))
            Return results
        End Function

        ''' <summary>
        ''' 仕向地の設定
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetLabelValues_Shimuke() As List(Of LabelValueVo)
            Dim results As New List(Of LabelValueVo)
            results.Add(New LabelValueVo("", Nothing))
            results.Add(New LabelValueVo("国内", "国内"))
            Return results
        End Function

    End Class
End Namespace