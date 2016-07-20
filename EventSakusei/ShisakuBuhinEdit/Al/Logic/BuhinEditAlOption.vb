Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.Soubi

Namespace ShisakuBuhinEdit.Al.Logic

    ''' <summary>
    ''' A/Lの装備品列の情報を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlOption
        Private ReadOnly optionReader As EventSoubiReader
        Private ReadOnly shisakuEventCode As String
        Private ReadOnly shisakuSoubiKbn As String
        Private ReadOnly tSoubiDao As TShisakuEventSoubiDao
        Private ReadOnly optionDao As EventSoubiDao
        Private _showColumnInfos As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="tSoubiDao">試作イベント装備情報Dao</param>
        ''' <param name="optionDao">イベント装備情報Dao</param>
        ''' <param name="showColumnInfos">表示/非表示をもつ試作設計ブロック装備仕様情報の一覧</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal shisakuSoubiKbn As String, _
                       ByVal tSoubiDao As TShisakuEventSoubiDao, _
                       ByVal optionDao As EventSoubiDao, _
                       ByVal showColumnInfos As List(Of TShisakuSekkeiBlockSoubiShiyouVo))
            Me.optionDao = optionDao
            Me.shisakuSoubiKbn = shisakuSoubiKbn
            Me.tSoubiDao = tSoubiDao
            Me.optionDao = optionDao
            Me._showColumnInfos = ExtractSoubiKbn(showColumnInfos, shisakuSoubiKbn)
            optionReader = New EventSoubiReader(shisakuEventCode, shisakuSoubiKbn, tSoubiDao, optionDao)
        End Sub

        ''' <summary>
        ''' 試作装備区分で抽出する
        ''' </summary>
        ''' <param name="soubiShiyouVos">試作設計ブロック装備仕様情報の一覧</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <returns>抽出結果</returns>
        ''' <remarks></remarks>
        Private Function ExtractSoubiKbn(ByVal soubiShiyouVos As List(Of TShisakuSekkeiBlockSoubiShiyouVo), ByVal shisakuSoubiKbn As String) As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Dim results As New List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            For Each vo As TShisakuSekkeiBlockSoubiShiyouVo In soubiShiyouVos
                If Not shisakuSoubiKbn.Equals(vo.ShisakuSoubiKbn) Then
                    Continue For
                End If
                results.Add(vo)
            Next
            Return results
        End Function

        ''' <summary>
        ''' 列表示情報
        ''' </summary>
        Public Property ShowColumnInfos() As List(Of TShisakuSekkeiBlockSoubiShiyouVo)
            Get
                Return _showColumnInfos
            End Get
            Set(ByVal value As List(Of TShisakuSekkeiBlockSoubiShiyouVo))
                _showColumnInfos = ExtractSoubiKbn(value, shisakuSoubiKbn)
            End Set
        End Property

        Private _RetuKoumokuCodeIndexes As Dictionary(Of String, Integer)
        ''' <summary>
        ''' タイトルの項目コードで列indexを返す
        ''' </summary>
        ''' <param name="retuKoumokuCode">タイトルの項目コード</param>
        ''' <returns>列index</returns>
        ''' <remarks></remarks>
        Private Function GetColumnNoByRetuKoumokuCode(ByVal retuKoumokuCode As String) As Integer

            If (String.Equals(retuKoumokuCode, "218")) Then
                Dim c As String = ""
            End If

            If _RetuKoumokuCodeIndexes Is Nothing Then
                _RetuKoumokuCodeIndexes = New Dictionary(Of String, Integer)
                For Each columnNo As Integer In optionReader.GetInputTitleNameColumnNos()
                    Try
                        _RetuKoumokuCodeIndexes.Add(optionReader.TitleRetuKoumokuCode(columnNo), columnNo)
                    Catch ex As Exception
                    End Try
                Next
            End If
            Dim result As Integer = 0
            Try
                result = _RetuKoumokuCodeIndexes(retuKoumokuCode)
            Catch ex As Exception
                result = Integer.Parse(retuKoumokuCode)
            End Try
            Return result
            
        End Function

        ''' <summary>列数</summary>
        Public ReadOnly Property ColumnCount() As Integer
            Get
                Return _showColumnInfos.Count
            End Get
        End Property

        ''' <summary>
        ''' 装備名
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        Public ReadOnly Property Title(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _showColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return optionReader.TitleName(GetColumnNoByRetuKoumokuCode(_showColumnInfos(columnIndex).ShisakuRetuKoumokuCode))
            End Get
        End Property

        ''' <summary>
        ''' 装備名（大区分）
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        Public ReadOnly Property TitleDai(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _showColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return optionReader.TitleNameDai(GetColumnNoByRetuKoumokuCode(_showColumnInfos(columnIndex).ShisakuRetuKoumokuCode))
            End Get
        End Property

        ''' <summary>
        ''' 装備名（中区分）
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        Public ReadOnly Property TitleChu(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _showColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return optionReader.TitleNameChu(GetColumnNoByRetuKoumokuCode(_showColumnInfos(columnIndex).ShisakuRetuKoumokuCode))
            End Get
        End Property

        ''' <summary>
        ''' 試作適用
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        Public ReadOnly Property Info(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _showColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return optionReader.ShisakuTekiyou(rowIndex, GetColumnNoByRetuKoumokuCode(_showColumnInfos(columnIndex).ShisakuRetuKoumokuCode))
            End Get
        End Property

        '-------------------------------------------------------------------------------------------------------
        '２次改修
        ''' <summary>
        ''' 試作装備のカラム位置を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        Public ReadOnly Property SoubiColumnInfo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _showColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return GetColumnNoByRetuKoumokuCode(_showColumnInfos(columnIndex).ShisakuRetuKoumokuCode)
            End Get
        End Property

    End Class
End Namespace