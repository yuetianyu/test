Imports YosansyoTool.YosanBuhinEdit.Logic.Detect
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.Logic
    ''' <summary>
    ''' 予算書部品表編集画面の表示全般を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditSubject : Inherits Observable
        Private ReadOnly aShisakuDate As New ShisakuDate
        Private ReadOnly exclusionYosanEvent As New TYosanEventExclusion

        Private ReadOnly aLoginInfo As LoginInfo
        Private ReadOnly aHeaderSubject As BuhinEditHeaderSubject
        Private ReadOnly aKoseiSubject As BuhinEditKoseiSubject

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="yosanEventCode"></param>
        ''' <param name="yosanUnitKbn"></param>
        ''' <param name="aLoginInfo"></param>
        ''' <param name="KSMode"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal yosanEventCode As String, ByVal yosanUnitKbn As String, ByVal aLoginInfo As LoginInfo, ByVal KSMode As Integer)
            Me.aLoginInfo = aLoginInfo

            Dim yosanEventDao As TYosanEventDao = New TYosanEventDaoImpl
            Dim aRhac1560Dao As Rhac1560Dao = New Rhac1560DaoImpl
            Dim aYosanDao As YosanDao = New YosanDaoImpl
            Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
            Dim telDao As TShisakuTelNoDao = New TShisakuTelNoDaoImpl

            aHeaderSubject = New BuhinEditHeaderSubject(yosanEventCode, aLoginInfo, aShisakuDate, yosanEventDao, aRhac1560Dao, aRhac2130Dao, aYosanDao, KSMode, telDao)

            Dim detectImpl As DetectLatestStructureImpl = New DetectLatestStructureImpl(yosanEventCode)

            'イベントコードがいる'
            aKoseiSubject = New BuhinEditKoseiSubject(yosanEventCode, yosanUnitKbn, aLoginInfo, aShisakuDate, _
                                                      detectImpl, aHeaderSubject.yosanEventVo, _
                                                      New MakeStructureResultImpl, _
                                                      New MakerNameResolverImpl, _
                                                      New TYosanBuhinEditDaoImpl, _
                                                      New TYosanBuhinEditInsuDaoImpl, _
                                                      New TYosanBuhinEditPatternDaoImpl, _
                                                      New TYosanBuhinEditRirekiDaoImpl, _
                                                      New TYosanBuhinEditInsuRirekiDaoImpl, _
                                                      New TYosanBuhinEditPatternRirekiDaoImpl)

            'ここから部品構成の取得開始'
            exclusionYosanEvent.Save(yosanEventCode)
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
        Public ReadOnly Property HeaderSubject() As BuhinEditHeaderSubject
            Get
                Return aHeaderSubject
            End Get
        End Property

        ''' <summary>部品構成画面用のSubject</summary>
        Public ReadOnly Property KoseiSubject() As BuhinEditKoseiSubject
            Get
                Return aKoseiSubject
            End Get
        End Property

    End Class
End Namespace