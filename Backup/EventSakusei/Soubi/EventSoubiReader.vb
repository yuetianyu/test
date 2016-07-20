Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util

Namespace Soubi
    ''' <summary>
    ''' 試作イベント装備情報の読込を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EventSoubiReader

        Private Const TITLE_ROW_NO As Integer = -1

        Private ReadOnly shisakuEventCode As String
        Private ReadOnly shisakuSoubiKbn As String
        Private ReadOnly tSoubiDao As TShisakuEventSoubiDao
        Private ReadOnly optionDao As EventSoubiDao
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="shisakuSoubiKbn">試作装備区分</param>
        ''' <param name="tSoubiDao">試作イベント装備情報Dao</param>
        ''' <param name="optionDao">イベント装備Dao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuSoubiKbn As String, ByVal tSoubiDao As TShisakuEventSoubiDao, ByVal optionDao As EventSoubiDao)
            Me.shisakuEventCode = shisakuEventCode
            Me.optionDao = optionDao
            Me.shisakuSoubiKbn = shisakuSoubiKbn
            Me.tSoubiDao = tSoubiDao
            Me.optionDao = optionDao
            ReadRecords()
        End Sub

        Private _recordDimension As New IndexedList(Of IndexedList(Of EventEditOptionVo))

        ''' <summary>装備情報</summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnNo">列No</param>
        ''' <returns>装備情報</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Records(ByVal rowNo As Integer, ByVal columnNo As Integer) As EventEditOptionVo
            Get
                Return _recordDimension.Value(rowNo).Value(columnNo)
            End Get
        End Property

        ''' <summary>
        ''' 入力行の行Noの一覧を返す（タイトル行を除く）
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowNos() As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each rowNo As Integer In _recordDimension.Keys
                If rowNo = TITLE_ROW_NO Then
                    Continue For
                End If
                results.Add(rowNo)
            Next
            Return results
        End Function

        ''' <summary>
        ''' 入力した列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力した列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputTitleNameColumnNos() As ICollection(Of Integer)
            Return GetInputColumnNos(TITLE_ROW_NO)
        End Function

        ''' <summary>列タイトルの装備情報</summary>
        Private ReadOnly Property TitleNameRecords(ByVal columnNo As Integer) As EventEditOptionVo
            Get
                Return Records(TITLE_ROW_NO, columnNo)
            End Get
        End Property

        ''' <summary>
        ''' 入力した列の列No一覧を返す（種別列・号車列を除く）
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <returns>入力した列の列No一覧（種別列・号車列を除く）</returns>
        ''' <remarks></remarks>
        Public Function GetInputColumnNos(ByVal rowNo As Integer) As ICollection(Of Integer)
            Dim results As New List(Of Integer)
            For Each columnNo As Integer In _recordDimension.Value(rowNo).Keys
                results.Add(columnNo)
            Next
            Return results
        End Function

        ''' <summary>タイトル名</summary>
        ''' <param name="columnNo">列No</param>
        Public Property TitleName(ByVal columnNo As Integer) As String
            Get
                Return Records(TITLE_ROW_NO, columnNo).ShisakuTekiyou
            End Get
            Set(ByVal value As String)
                Records(TITLE_ROW_NO, columnNo).ShisakuTekiyou = value
            End Set
        End Property

        ''' <summary>タイトル名（大区分）</summary>
        ''' <param name="columnNo">列No</param>
        Public Property TitleNameDai(ByVal columnNo As Integer) As String
            Get
                Return Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouDai
            End Get
            Set(ByVal value As String)
                Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouDai = value
            End Set
        End Property

        ''' <summary>タイトル名（中区分）</summary>
        ''' <param name="columnNo">列No</param>
        Public Property TitleNameChu(ByVal columnNo As Integer) As String
            Get
                Return Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouChu
            End Get
            Set(ByVal value As String)
                Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouChu = value
            End Set
        End Property

        ''' <summary>タイトルの項目コード</summary>
        ''' <param name="columnNo">列No</param>
        Public Property TitleRetuKoumokuCode(ByVal columnNo As Integer) As String
            Get
                Return Records(TITLE_ROW_NO, columnNo).ShisakuRetuKoumokuCode
            End Get
            Set(ByVal value As String)
                Records(TITLE_ROW_NO, columnNo).ShisakuRetuKoumokuCode = value
            End Set
        End Property


        ''' <summary>試作適用</summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnNo">列No</param>
        Public Property ShisakuTekiyou(ByVal rowNo As Integer, ByVal columnNo As Integer) As String
            Get
                Return Records(rowNo, columnNo).ShisakuTekiyou
            End Get
            Set(ByVal value As String)
                Records(rowNo, columnNo).ShisakuTekiyou = value
            End Set
        End Property

        ''' <summary>
        ''' 情報を読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecords()

            '' タイトル
            Dim titleVos As List(Of TShisakuEventSoubiNameVo) = optionDao.FindWithTitleNameBy(shisakuEventCode, shisakuSoubiKbn)
            For Each vo As TShisakuEventSoubiNameVo In titleVos
                If vo.HyojijunNo <> TITLE_ROW_NO Then
                    Continue For
                End If
                vo.ShisakuTekiyou = vo.ShisakuRetuKoumokuName
                vo.ShisakuTekiyouDai = vo.ShisakuRetuKoumokuNameDai
                vo.ShisakuTekiyouChu = vo.ShisakuRetuKoumokuNameChu

                VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
            Next

            ' データ部
            Dim param As New TShisakuEventSoubiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            '全体をとってきてるからダメ'
            Dim vos As List(Of TShisakuEventSoubiVo) = tSoubiDao.FindBy(param)
            For Each vo As TShisakuEventSoubiVo In vos
                If vo.HyojijunNo = TITLE_ROW_NO Then
                    Continue For
                End If
                VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
            Next

        End Sub
    End Class
End Namespace