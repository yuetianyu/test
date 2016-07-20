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

Namespace EventEdit.Logic
    Public Class EventEdit : Inherits Observable

        '製作一覧情報
        Private _seisakuHakouNo As String
        Private _seisakuHakouNoKaiteiNo As String

        Private _shisakuEventCode As String
        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aHeaderSubject As EventEditHeader
        Private ReadOnly aBaseCarSubject As EventEditBaseCar
        '   設計展開用のベース車情報
        Private ReadOnly aBaseCarTenkaiSubject As EventEditBaseTenkaiCar
        Private ReadOnly anEzSyncBaseTenkai As EzSyncBaseTenkaiImpl
        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 af) (TES)施 ADD BEGIN
        Private ReadOnly aEbomKanshiSubject As EventEditEbomKanshi
        Private ReadOnly anEzSyncEbomKanshi As EzSyncEbomKanshiImpl
        ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 af) (TES)施 ADD END	

        Private ReadOnly aCompleteCarSubject As EventEditCompleteCar
        Private ReadOnly aBasicOptionSubject As EventEditOption
        Private ReadOnly aSpecialOptionSubject As EventEditOption
        Private ReadOnly aReferenceCarSubject As EventEditReferenceCar
        Private ReadOnly exclusionShisakuEvent As TShisakuEventExclusion
        Private ReadOnly anEzSync As EzSyncShubetsuGoshaImpl
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
                       ByVal seisakuHakouNo As String, _
                       ByVal seisakuHakouNoKaiteiNo As String, _
                       Optional ByVal aHeaderSubject As EventEditHeader = Nothing)

            Me._shisakuEventCode = shisakuEventCode
            Me.aLoginInfo = aLoginInfo
            Dim aTShisakuEventDao As TShisakuEventDaoImpl = New TShisakuEventDaoImpl
            aShisakuDate = New ShisakuDate
            anEzSync = New EzSyncShubetsuGoshaImpl
            anEzSyncBaseTenkai = New EzSyncBaseTenkaiImpl
            'sxc QA?
            anEzSyncEbomKanshi = New EzSyncEbomKanshiImpl
            'sxc QA?
            _registError = False

            Dim isEventCopy As Boolean = aHeaderSubject IsNot Nothing
            If isEventCopy Then
                Me.aHeaderSubject = aHeaderSubject
            Else
                Me.aHeaderSubject = New EventEditHeader(shisakuEventCode, _
                                                       aLoginInfo, _
                                                       New MShisakuKaihatufugoDaoImpl, _
                                                       aTShisakuEventDao, _
                                                       New MShisakuStatusDaoImpl, _
                                                       aShisakuDate, _
                                                       seisakuHakouNo, _
                                                       seisakuHakouNoKaiteiNo)
            End If

            ''anEzSyncEbomKanshi  2014/08/05 Ⅰ.5.EBOM差分出力 ag) (TES)施 ADD 
            aBaseCarSubject = New EventEditBaseCar(shisakuEventCode, aLoginInfo, anEzSync, anEzSyncBaseTenkai, _
                                                   anEzSyncEbomKanshi, _
                                                   Me.aHeaderSubject.IsSekkeiTenkaiIkou, _
                                                   New EventEditBaseCarDaoImpl, _
                                                   New TShisakuEventBaseSeisakuIchiranDaoImpl, _
                                                   New TShisakuEventBaseKaiteiDaoImpl, _
                                                   aShisakuDate, seisakuHakouNo, seisakuHakouNoKaiteiNo)
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ag) (TES)施 ADD BEGIN
            aEbomKanshiSubject = New EventEditEbomKanshi(shisakuEventCode, aLoginInfo, anEzSync, anEzSyncBaseTenkai, _
                                                    anEzSyncEbomKanshi, _
                                                   Me.aHeaderSubject.IsSekkeiTenkaiIkou, _
                                                   New EventEditBaseCarDaoImpl, _
                                                   New TShisakuEventBaseSeisakuIchiranDaoImpl, _
                                                   New TShisakuEventBaseKaiteiDaoImpl, _
                                                   aShisakuDate, seisakuHakouNo, seisakuHakouNoKaiteiNo)
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ag) (TES)施 ADDEND		

            '   ベース車情報設計展開用
            aBaseCarTenkaiSubject = New EventEditBaseTenkaiCar(shisakuEventCode, _
                                                             aLoginInfo, anEzSync, anEzSyncBaseTenkai, _
                                                             New EventEditBaseCarDaoImpl, _
                                                             New TShisakuEventBaseDaoImpl, _
                                                             aBaseCarSubject, aShisakuDate, seisakuHakouNo, seisakuHakouNoKaiteiNo, _
                                                             Me.aHeaderSubject.IsSekkeiTenkaiIkou)

            aCompleteCarSubject = New EventEditCompleteCar(shisakuEventCode, _
                                                           aLoginInfo, _
                                                           anEzSync, _
                                                           Me.aHeaderSubject.IsSekkeiTenkaiIkou, _
                                                           New TShisakuEventKanseiDaoImpl, _
                                                           New TShisakuEventKanseiKaiteiDaoImpl, _
                                                           aShisakuDate, importFlag, _
                                                           seisakuHakouNo, seisakuHakouNoKaiteiNo, _
                                                           Me.aHeaderSubject.SeisakuichiranHakouNo, _
                                                           Me.aHeaderSubject.SeisakuichiranHakouNoKai)

            aBasicOptionSubject = New EventEditOption(shisakuEventCode, aLoginInfo, _
                                                      TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.BASIC_OPTION, _
                                                      anEzSync, _
                                                      Me.aHeaderSubject.IsSekkeiTenkaiIkou, _
                                                      New MShisakuSoubiDaoImpl, _
                                                      New TShisakuEventSoubiDaoImpl, _
                                                      New TShisakuEventSoubiKaiteiDaoImpl, _
                                                      New EventSoubiDaoImpl, _
                                                      aShisakuDate, _
                                                      seisakuHakouNo, seisakuHakouNoKaiteiNo, _
                                                      aBaseCarSubject)

            aSpecialOptionSubject = New EventEditOption(shisakuEventCode, aLoginInfo, _
                                                        TShisakuEventSoubiVoHelper.ShisakuSoubiKbn.SPECIAL_OPTION, _
                                                        anEzSync, _
                                                        Me.aHeaderSubject.IsSekkeiTenkaiIkou, _
                                                        New MShisakuSoubiDaoImpl, _
                                                        New TShisakuEventSoubiDaoImpl, _
                                                        New TShisakuEventSoubiKaiteiDaoImpl, _
                                                        New EventSoubiDaoImpl, _
                                                        aShisakuDate, _
                                                        seisakuHakouNo, seisakuHakouNoKaiteiNo, _
                                                        aBaseCarSubject)

            aReferenceCarSubject = New EventEditReferenceCar(shisakuEventCode, _
                                                             aLoginInfo, _
                                                             New EventEditBaseCarDaoImpl, _
                                                             New EventEditReferenceCarDaoImpl, _
                                                             aBaseCarSubject)
            exclusionShisakuEvent = New TShisakuEventExclusion

            IsViewerMode = False
            'IsViewerMode = isEventCopy

            anEzSync.header = Me.aHeaderSubject
            anEzSync.baseCar = aBaseCarSubject
            ''設計展開用
            anEzSyncBaseTenkai.baseTenkaiCar = aBaseCarTenkaiSubject
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ag) (TES)施 ADD BEGIN
            anEzSyncEbomKanshi.EbomKanshi = aEbomKanshiSubject
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ag) (TES)施 ADDEND		

            anEzSync.completeCar = aCompleteCarSubject
            anEzSync.basicOption = aBasicOptionSubject
            anEzSync.specialOption = aSpecialOptionSubject
            anEzSync.referenceCar = aReferenceCarSubject

            If Not IsAddMode() Then
                exclusionShisakuEvent.Save(shisakuEventCode)
            End If
            If isEventCopy Then
                PostProcessAtEventCopy()
            End If
            SetChanged()
        End Sub

#Region "公開プロパティGet/Set"

        ''' <summary>製作一覧発行№</summary>
        ''' <returns>製作一覧発行№</returns>
        Public ReadOnly Property SeisakuHakouNo() As String
            Get
                Return _seisakuHakouNo
            End Get
        End Property

        ''' <summary>製作一覧発行№改訂№</summary>
        ''' <returns>製作一覧発行№改訂№</returns>
        Public ReadOnly Property SeisakuHakouNoKaiteiNo() As String
            Get
                Return _seisakuHakouNoKaiteiNo
            End Get
        End Property


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
                aCompleteCarSubject.IsViewerMode = value
                aBasicOptionSubject.IsViewerMode = value
                aSpecialOptionSubject.IsViewerMode = value
                ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ah) (TES)施 ADD BEGIN
                aEbomKanshiSubject.IsViewerMode = value
                ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ah) (TES)施 ADDEND	
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
        ''' イベント情報コピー時の後処理
        ''' </summary>
        ''' <remarks>ヘッダー部の情報は元の情報のままにする</remarks>
        Private Sub PostProcessAtEventCopy()
            _shisakuEventCode = HeaderSubject.ShisakuEventCode
            BaseCarSubject.ProcessPostCopy(HeaderSubject.ShisakuEventCode)
            CompleteCarSubject.ProcessPostCopy(HeaderSubject.ShisakuEventCode)
            BasicOptionSubject.ProcessPostCopy(HeaderSubject.ShisakuEventCode)
            SpecialOptionSubject.ProcessPostCopy(HeaderSubject.ShisakuEventCode)
            '展開用
            BaseCarSubject.ProcessPostCopy(HeaderSubject.ShisakuEventCode)
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ai) (TES)施 ADD BEGIN
            EbomKanshiSubject.ProcessPostCopy(HeaderSubject.ShisakuEventCode)
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ai) (TES)施 ADDEND		
        End Sub

        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            Register("登録", AddressOf RegisterMain)
        End Sub

        ''' <summary>
        ''' 登録処理
        ''' 登録時の履歴保持
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RegisterKaitei()
            Register("登録", AddressOf RegisterMainKaitei)
        End Sub
        ''' <summary>
        ''' 登録処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterMain()
            aShisakuDate.Clear()
            aHeaderSubject.Register()
            '通常
            aBaseCarSubject.Register(aHeaderSubject.ShisakuEventCode)
            aCompleteCarSubject.Register(aHeaderSubject.ShisakuEventCode)
            aBasicOptionSubject.Register(aHeaderSubject.ShisakuEventCode)
            aSpecialOptionSubject.Register(aHeaderSubject.ShisakuEventCode)
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 aj) (TES)施 ADD BEGIN
            aEbomKanshiSubject.Register(aHeaderSubject.ShisakuEventCode)
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 aj) (TES)施 ADDEND		

            ''登録ボタンクリック時の情報を保持する。
            'aBaseCarSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            'aCompleteCarSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            'aBasicOptionSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            'aSpecialOptionSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            'ベース車設計展開用の情報を保持する。
            '   設計展開前なら保持する。
            If StringUtil.Equals(aHeaderSubject.IsSekkeiTenkaiIkou, False) Then
                aBaseCarTenkaiSubject.Register(aHeaderSubject.ShisakuEventCode)
            End If

        End Sub
        ''' <summary>
        ''' 登録処理本体
        ''' 登録時の履歴保持
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterMainKaitei()
            aShisakuDate.Clear()
            aHeaderSubject.Register()
            '登録ボタンクリック時の情報を保持する。
            aBaseCarSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            aCompleteCarSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            aBasicOptionSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            aSpecialOptionSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 ak) (TES)施 ADD BEGIN
            ''↓↓2014/08/19 Ⅰ.5.EBOM差分出力 ak) 酒井 ADD BEGIN
            'aEbomKanshiSubject.RegisterKaitei(aHeaderSubject.ShisakuEventCode)
            ''↑↑2014/08/19 Ⅰ.5.EBOM差分出力 ak) 酒井 ADD END			
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 ak) (TES)施 ADDEND			

        End Sub
        ''' <summary>
        ''' 保存処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save()
            Register("保存", AddressOf SaveMain)
            '2012/03/07 登録エラーなら区分名は必要ない'
            If RegistError Then
                aHeaderSubject.DataKbnName = ""
                aHeaderSubject.ShisakuEventCode = ""
            End If
        End Sub
        ''' <summary>
        ''' 保存処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SaveMain()
            aShisakuDate.Clear()
            aHeaderSubject.Save()
            aBaseCarSubject.Save(aHeaderSubject.ShisakuEventCode)
            aCompleteCarSubject.Save(aHeaderSubject.ShisakuEventCode)
            aBasicOptionSubject.Save(aHeaderSubject.ShisakuEventCode)
            aSpecialOptionSubject.Save(aHeaderSubject.ShisakuEventCode)
            ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 al) (TES)施 ADD BEGIN
            aEbomKanshiSubject.Save(aHeaderSubject.ShisakuEventCode)
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 al) (TES)施 ADDEND			


            'ベース車設計展開用の情報を保持する。
            '   設計展開前なら保持する。
            If StringUtil.Equals(aHeaderSubject.IsSekkeiTenkaiIkou, False) Then
                aBaseCarTenkaiSubject.Save(aHeaderSubject.ShisakuEventCode)
            End If
        End Sub

        Private Delegate Sub RegisterInner()

        ''' <summary>
        ''' 登録
        ''' </summary>
        ''' <param name="processName">プロセス名</param>
        ''' <param name="aRegisterInner"></param>
        ''' <remarks></remarks>
        Private Sub Register(ByVal processName As String, ByVal aRegisterInner As RegisterInner)
            Using db As New EBomDbClient
                db.BeginTransaction()
                If Not IsAddMode() AndAlso exclusionShisakuEvent.WasUpdatedBySomeone() Then
                    db.Rollback()
                    Dim userName As String = ResolveUserName(exclusionShisakuEvent.GetUpdatedUserId)
                    If exclusionShisakuEvent.WasChangedStatus Then
                        ' ステータスが変わっていたら、登録中止
                        IsViewerMode = True
                        NotifyObservers()
                        Throw New TableExclusionException(String.Format("このデータは先程 {0} 様に更新されました。" & vbCrLf & "「戻る」から更新内容を確認してください。", userName))
                    End If
                    exclusionShisakuEvent.Save(_shisakuEventCode)
                    Throw New TableExclusionException(String.Format("このデータは先程 {0} 様に更新されました。" & vbCrLf & "その更新を無視して、データを {1} する場合、もう一度、{1} ボタンを押して下さい。", userName, processName))
                End If
                Try
                    aRegisterInner()
                Catch ex As Exception
                    db.Rollback()
                    MsgBox(ex.ToString)
                    'MsgBox("イベントを登録出来ませんでした。不正な文字が入力されている可能性があります。")
                    _registError = True
                    Return
                End Try

                db.Commit()
                _registError = False
            End Using

            If IsAddMode() Then
                _shisakuEventCode = aHeaderSubject.ShisakuEventCode
            End If
            ' aRegisterInner()で TShisakuEvent を更新しているから、Exclusion#Update はしていない
            exclusionShisakuEvent.Save(_shisakuEventCode)
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

        Public Sub ApplyBaseCarShubetsuGosha()
            anEzSync.NotifyAllShubetsuGosha(aBaseCarSubject)
            anEzSyncBaseTenkai.NotifyAllBaseTenkai(aBaseCarTenkaiSubject)
        End Sub
        ''↓↓2014/08/05 Ⅰ.5.EBOM差分出力 am) (TES)施 ADD BEGIN
        Public ReadOnly Property EbomKanshiSubject() As EventEditEbomKanshi
            Get
                Return aEbomKanshiSubject
            End Get
            ''↑↑2014/08/05 Ⅰ.5.EBOM差分出力 am) (TES)施 ADDEND			
        End Property
#End Region






#Region "完成車"
        Public ReadOnly Property CompleteCarSubject() As EventEditCompleteCar
            Get
                Return aCompleteCarSubject
            End Get
        End Property
#End Region

#Region "基本装備"
        Public ReadOnly Property BasicOptionSubject() As EventEditOption
            Get
                Return aBasicOptionSubject
            End Get
        End Property
#End Region

#Region "特別装備"
        Public ReadOnly Property SpecialOptionSubject() As EventEditOption
            Get
                Return aSpecialOptionSubject
            End Get
        End Property
#End Region

#Region "参考車"
        Public ReadOnly Property ReferenceCarSubject() As EventEditReferenceCar
            Get
                Return aReferenceCarSubject
            End Get
        End Property
#End Region

#Region "ベース車(設計展開用)"
        Public ReadOnly Property BaseTenkaiCarSubject() As EventEditBaseTenkaiCar
            Get
                Return aBaseCarTenkaiSubject
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