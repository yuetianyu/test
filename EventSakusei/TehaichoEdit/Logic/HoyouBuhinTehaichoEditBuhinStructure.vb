Imports EBom.Data
Imports EBom.Common
Imports EventSakusei.TehaichoEdit.Dao
Imports System.Text.RegularExpressions
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports EventSakusei.TehaichoEdit.Logic
Imports EventSakusei.NokiIkkatuSettei
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect

#Region "部品番号・試作区分から部品構成を取得するクラス"
Public Class HoyouBuhinTehaichoEditBuhinStructure

#Region "プライベート変数"

    Private _shisakuEventCode As String
    Private _shisakuListCode As String
    Private _shisakuBlockNo As String
    Private _shisakuBukaCode As String
    Private _shisakuDate As ShisakuDate
    Private ReadOnly _make As HoyouBuhinMakeStructureResult

#End Region

#Region "コンストラクタ"

    Public Sub New(ByVal aShisakuEventCode As String, _
                            ByVal aShisakuListCode As String, _
                            ByVal aShisakuBlockNo As String, _
                            ByVal aShikuBukaCode As String, _
                            ByVal aShisakuDate As ShisakuDate)

        _shisakuEventCode = aShisakuEventCode
        _shisakuListCode = aShisakuListCode
        _shisakuBukaCode = aShikuBukaCode
        _shisakuBlockNo = aShisakuBlockNo
        _shisakuDate = aShisakuDate
        ''↓↓2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_ba) 酒井 ADD BEGIN
        '_make = New MakeStructureResultImpl(_shisakuEventCode, aShikuBukaCode, aShisakuBlockNo)
        _make = New HoyouBuhinMakeStructureResultImpl(_shisakuEventCode, aShikuBukaCode, aShisakuBlockNo)
        ''↑↑2014/09/01 Ⅰ.3.設計編集 ベース車改修専用化_ba) 酒井 ADD END
    End Sub
#End Region

#Region "メソッド"

    ''' <summary>
    ''' 「構成の情報」を元に部品表を作成する
    ''' </summary>
    ''' <param name="aStructureResult"></param>
    ''' <param name="baseLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNewKoseiMatrix(ByVal aStructureResult As HoyouBuhinStructureResult, _
                                       ByVal baseLevel As Integer?, _
                                       ByVal kaihatsuFugo As String) As HoyouBuhinBuhinKoseiMatrix

        Return _make.Compute(aStructureResult, baseLevel, kaihatsuFugo)

    End Function

#Region "構成取得メイン"

    ''' <summary>
    ''' 構成取得メイン
    ''' </summary>
    ''' <param name="aBuhinNo"></param>
    ''' <param name="aLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKouseiMatrix(ByVal aBuhinNo As String, _
                                    ByVal aBuhinNoKbn As String, _
                                    ByVal aLevel As Integer, _
                                    ByVal aKaihatsuFugo As String, _
                                    Optional ByVal fInstlHinban As Boolean = True) As HoyouBuhinBuhinKoseiMatrix

        Dim eventDao As THoyouEventDao = New THoyouEventDaoImpl
        '中身が必要
        Dim tantoDao As THoyouSekkeiTantoDao = New THoyouSekkeiTantoDaoImpl

        Dim aRhac1560Dao As Rhac1560Dao = New Rhac1560DaoImpl
        Dim aHoyouDao As HoyouDao = New HoyouDaoImpl
        Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
        Dim telDao As TShisakuTelNoDao = New TShisakuTelNoDaoImpl
        Dim aHeaderSubject As ShisakuBuhinEdit.Logic.HoyouBuhinBuhinEditHeaderSubject

        aHeaderSubject = New ShisakuBuhinEdit.Logic.HoyouBuhinBuhinEditHeaderSubject(_shisakuEventCode, _shisakuBukaCode, _shisakuBlockNo, _
                 LoginInfo.Now, _shisakuDate, eventDao, tantoDao, aRhac1560Dao, aRhac2130Dao, aHoyouDao, 0, telDao)


        Dim detector As ShisakuBuhinEdit.Logic.Detect.HoyouBuhinDetectLatestStructure = _
                         New ShisakuBuhinEdit.Logic.Detect.HoyouBuhinDetectLatestStructureImpl(aHeaderSubject.TantoVo)

        Dim inputedBuhinNo As String = aBuhinNo

        'true:Instl品番である。
        'false：Instl品番ではない。
        Dim aStructureResult As ShisakuBuhinEdit.Logic.Detect.HoyouBuhinStructureResult = _
                                    detector.Compute(inputedBuhinNo, aBuhinNoKbn, fInstlHinban, aKaihatsuFugo)

        If Not aStructureResult.IsExist Then
            Return Nothing
        End If

        Dim newKoseiMatrix As HoyouBuhinBuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, aLevel, aKaihatsuFugo)

        '結果を返す
        If newKoseiMatrix Is Nothing = True OrElse newKoseiMatrix.InputRowCount = 0 Then
            Return Nothing
        Else
            Return newKoseiMatrix

        End If

    End Function
#End Region

#End Region


End Class

#End Region
