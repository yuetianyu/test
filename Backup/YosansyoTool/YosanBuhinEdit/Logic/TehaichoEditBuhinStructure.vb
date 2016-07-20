Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports YosansyoTool.YosanBuhinEdit.Logic.Detect
Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Matrix

Namespace YosanBuhinEdit.Logic

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
        Private Function GetNewKoseiMatrix(ByVal aStructureResult As StructureResult, _
                                           ByVal baseLevel As Integer?, _
                                           ByVal kaihatsuFugo As String) As BuhinKoseiMatrix

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
                                        Optional ByVal fInstlHinban As Boolean = True) As BuhinKoseiMatrix

            'Dim yosanEventDao As TYosanEventDao = New TYosanEventDaoImpl
            '中身が必要
            'Dim tantoDao As THoyouSekkeiTantoDao = New THoyouSekkeiTantoDaoImpl

            'Dim aRhac1560Dao As Rhac1560Dao = New Rhac1560DaoImpl
            'Dim aYosanDao As YosanDao = New YosanDaoImpl
            'Dim aRhac2130Dao As Rhac2130Dao = New Rhac2130DaoImpl
            'Dim telDao As TShisakuTelNoDao = New TShisakuTelNoDaoImpl
            'Dim aHeaderSubject As BuhinEditHeaderSubject

            'aHeaderSubject = New BuhinEditHeaderSubject(_shisakuEventCode, LoginInfo.Now, _shisakuDate, _
            '                                            yosanEventDao, aRhac1560Dao, aRhac2130Dao, aYosanDao, 0, telDao)

            Dim detector As DetectLatestStructure = New DetectLatestStructureImpl(_shisakuEventCode)

            Dim inputedBuhinNo As String = aBuhinNo

            'true:Instl品番である。
            'false：Instl品番ではない。
            Dim aStructureResult As StructureResult = detector.Compute(inputedBuhinNo, aBuhinNoKbn, fInstlHinban, aKaihatsuFugo)

            If Not aStructureResult.IsExist Then
                Return Nothing
            End If

            Dim newKoseiMatrix As BuhinKoseiMatrix = GetNewKoseiMatrix(aStructureResult, aLevel, aKaihatsuFugo)

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

End Namespace
