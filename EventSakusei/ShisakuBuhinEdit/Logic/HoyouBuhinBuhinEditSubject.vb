Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.ShisakuBuhinEdit.Al.Logic
Imports EventSakusei.ShisakuBuhinEdit.Al.Dao

Namespace ShisakuBuhinEdit.Logic
    ''' <summary>
    ''' 補用部品表編集画面の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinBuhinEditSubject : Inherits Observable
        Private ReadOnly aShisakuDate As New ShisakuDate
        Private ReadOnly exclusionHoyouEvent As New ThoyouEventExclusion

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aHeaderSubject As HoyouBuhinBuhinEditHeaderSubject
        Private ReadOnly aKoseiSubject As HoyouBuhinBuhinEditKoseiSubject
        Private ReadOnly anAlSubject As BuhinEditAlSubject
        Public Sub New(ByVal hoyouEventCode As String, ByVal hoyouBukaCode As String, ByVal hoyouTantoNo As String, ByVal aLoginInfo As LoginInfo, ByVal KSMode As Integer, _
                       ByVal hoyouVos As List(Of THoyouBuhinEditVo), _
                       ByVal hyouInstlVos As List(Of TShisakuBuhinEditInstlVo), ByVal anAlSubject As BuhinEditAlSubject)
            Me.aLoginInfo = aLoginInfo

            Dim eventDao As THoyouEventDao = New THoyouEventDaoImpl
            Dim tantoDao As THoyouSekkeiTantoDao = New THoyouSekkeiTantoDaoImpl
            Dim aRhac1560Dao As Rhac1560Dao = New Rhac1560DaoImpl
            Dim aHoyouDao As HoyouDao = New HoyouDaoImpl
            Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
            Dim telDao As TShisakuTelNoDao = New TShisakuTelNoDaoImpl

            aHeaderSubject = New HoyouBuhinBuhinEditHeaderSubject(hoyouEventCode, hoyouBukaCode, hoyouTantoNo, aLoginInfo, aShisakuDate, eventDao, tantoDao, aRhac1560Dao, aRhac2130Dao, aHoyouDao, KSMode, telDao)

            Dim detectImpl As HoyouBuhinDetectLatestStructureImpl = New HoyouBuhinDetectLatestStructureImpl(aHeaderSubject.TantoVo)

            aKoseiSubject = New HoyouBuhinBuhinEditKoseiSubject(aHeaderSubject.TantoVo, aLoginInfo, aShisakuDate, _
                                                       detectImpl, _
                                                       New HoyouBuhinMakeStructureResultImpl(hoyouEventCode, hoyouBukaCode, hoyouTantoNo), _
                                                       New MakerNameResolverImpl, _
                                                       New THoyouBuhinEditDaoImpl, _
                                                       New TShisakuBuhinEditInstlDaoImpl, _
                                                       hoyouVos, hyouInstlVos)

            Me.anAlSubject = anAlSubject
            exclusionHoyouEvent.Save(hoyouEventCode)
            SetChanged()
        End Sub

        Private Class MakerNameResolverImpl : Implements MakerNameResolver
            Private dao As New Rhac0610DaoImpl
            Private cache As New Dictionary(Of String, Rhac0610Vo)
            Public Function Resolve(ByVal makerCode As String) As String Implements MakerNameResolver.Resolve
                If StringUtil.IsEmpty(makerCode) Then
                    Return Nothing
                End If
                If Not cache.ContainsKey(makerCode) Then
                    cache.Add(makerCode, dao.FindByPk(makerCode))
                End If

                Dim result As Rhac0610Vo = cache(makerCode)
                If result Is Nothing Then
                    Return Nothing
                End If
                Return result.MakerName
            End Function
        End Class

        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Register()
            aHeaderSubject.Memo = Nothing
            Register("登録", AddressOf RegisterMain)
        End Sub

        ''' <summary>
        ''' 登録処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub RegisterMain()
            aShisakuDate.Clear()
            aHeaderSubject.Register()
            ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD BEGIN
            anAlSubject.Register()
            ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD END
            aKoseiSubject.Register()
        End Sub

        ''' <summary>
        ''' 保存処理
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Save(ByVal memo As String)
            aHeaderSubject.Memo = memo
            Register("保存", AddressOf SaveMain)
        End Sub

        ''' <summary>
        ''' 保存処理本体
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SaveMain()
            aShisakuDate.Clear()
            aHeaderSubject.Save()
            ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD BEGIN
            anAlSubject.Register()
            ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD END
            aKoseiSubject.Register()
        End Sub

        Private Delegate Sub RegisterInner()
        ''' <summary>
        ''' 登録処理
        ''' </summary>
        ''' <param name="processName">処理名</param>
        ''' <param name="aRegisterInner">処理本体のDelegate</param>
        ''' <remarks></remarks>
        Private Sub Register(ByVal processName As String, ByVal aRegisterInner As RegisterInner)
            Using db As New EBomDbClient
                db.BeginTransaction()

                aRegisterInner()

                db.Commit()
            End Using
        End Sub

        ''' <summary>
        ''' ユーザー名を解決する
        ''' </summary>
        ''' <param name="userId">ユーザーID</param>
        ''' <returns>ユーザー名</returns>
        ''' <remarks></remarks>
        Private Function ResolveUserName(ByVal userId As String) As String

            Dim dao As New HoyouDaoImpl
            Dim userVo As Rhac0650Vo = dao.FindUserById(userId)

            If userVo Is Nothing Then
                Return userId
            End If
            Return userVo.ShainName
        End Function

        ''' <summary>ヘッダー部用のSubject</summary>
        Public ReadOnly Property HeaderSubject() As HoyouBuhinBuhinEditHeaderSubject
            Get
                Return aHeaderSubject
            End Get
        End Property

        ''' <summary>部品構成画面用のSubject</summary>
        Public ReadOnly Property KoseiSubject() As HoyouBuhinBuhinEditKoseiSubject
            Get
                Return aKoseiSubject
            End Get
        End Property

        ''↓↓2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD BEGIN
        ''' <summary>部品構成画面用のSubject</summary>
        Public ReadOnly Property AlSubject() As BuhinEditAlSubject
            Get
                Return anAlSubject
            End Get
        End Property
        ''↑↑2014/08/27 Ⅰ.3.設計編集 ベース車改修専用化_bz) (TES)張 ADD END

        ''' <summary>
        ''' 担当者変更を、反映する
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ApplyTanto()
            aHeaderSubject.ApplyTanto()
            aKoseiSubject.SupersedeTantoVo(aHeaderSubject.TantoVo)

            Dim ezTunnel As New HoyouBuhinEzTunnelAlKoseiImpl
            ezTunnel.Initialize(aKoseiSubject)

            SetChanged()
        End Sub
    End Class
End Namespace