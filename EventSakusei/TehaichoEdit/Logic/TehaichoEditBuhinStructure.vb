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
Public Class TehaichoEditBuhinStructure

#Region "プライベート変数"

    Private _shisakuEventCode As String
    Private _shisakuListCode As String
    Private _shisakuBlockNo As String
    Private _shisakuBukaCode As String
    Private _shisakuDate As ShisakuDate
    Private ReadOnly _make As MakeStructureResult

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
        _make = New MakeStructureResultImpl(_shisakuEventCode, aShikuBukaCode, aShisakuBlockNo)

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
    Private Function GetNewKoseiMatrix(ByVal aStructureResult As StructureResult, ByVal baseLevel As Integer?) As BuhinKoseiMatrix

        Return _make.Compute(aStructureResult, baseLevel)

    End Function

#Region "構成取得メイン"

    ''' <summary>
    ''' 構成取得メイン
    ''' </summary>
    ''' <param name="aBuhinNo"></param>
    ''' <param name="aLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKouseiMatrix(ByVal aBuhinNo As String, ByVal aBuhinNoKbn As String, ByVal aLevel As Integer) As BuhinKoseiMatrix

        Dim eventDao As TShisakuEventDao = New TShisakuEventDaoImpl
        '中身が必要
        Dim blockDao As TShisakuSekkeiBlockDao = New TShisakuSekkeiBlockDaoImpl

        Dim aRhac1560Dao As Rhac1560Dao = New Rhac1560DaoImpl
        Dim aShisakuDao As ShisakuDao = New ShisakuDaoImpl
        Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
        Dim telDao As TShisakuTelNoDao = New TShisakuTelNoDaoImpl
        Dim aHeaderSubject As ShisakuBuhinEdit.Logic.BuhinEditHeaderSubject


        aHeaderSubject = New ShisakuBuhinEdit.Logic.BuhinEditHeaderSubject(_shisakuEventCode, _shisakuBukaCode, _shisakuBlockNo, _
                 LoginInfo.Now, _shisakuDate, eventDao, blockDao, aRhac1560Dao, aRhac2130Dao, aShisakuDao, 0, telDao)


        Dim detector As ShisakuBuhinEdit.Logic.Detect.DetectLatestStructure = _
                         New ShisakuBuhinEdit.Logic.Detect.DetectLatestStructureImpl(aHeaderSubject.BlockVo)

        Dim inputedBuhinNo As String = aBuhinNo

        Dim aStructureResult As ShisakuBuhinEdit.Logic.Detect.StructureResult = detector.Compute(inputedBuhinNo, aBuhinNoKbn, False)

        If Not aStructureResult.IsExist Then
            Return Nothing
        End If

        Dim newKoseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, aLevel)

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
