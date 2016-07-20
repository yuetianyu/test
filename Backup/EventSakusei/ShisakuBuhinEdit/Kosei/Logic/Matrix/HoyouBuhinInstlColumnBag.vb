Namespace ShisakuBuhinEdit.Kosei.Logic.Matrix
    ''' <summary>INSTL列情報を表すクラス</summary>
    Public Class HoyouBuhinInstlColumnBag
        ''' <summary>員数1セルと行情報を表すクラス</summary>
        Private Class InsuCellBag
            ''' <summary>員数以外の一行情報</summary>
            Public KoseiRecordVo As HoyouBuhinBuhinKoseiRecordVo
            ''' <summary>員数の情報</summary>
            Public InsuVo As BuhinKoseiInsuCellVo
            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="KoseiRecordVo">員数以外の一行情報</param>
            ''' <param name="InsuVo">員数の情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal KoseiRecordVo As HoyouBuhinBuhinKoseiRecordVo, ByVal InsuVo As BuhinKoseiInsuCellVo)
                Me.KoseiRecordVo = KoseiRecordVo
                Me.InsuVo = InsuVo
            End Sub
        End Class

        Private _keys As New List(Of HoyouBuhinBuhinKoseiRecordVo)
        Private _cells As New List(Of InsuCellBag)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' 入力セル数を返す
        ''' </summary>
        Public ReadOnly Property Count() As Integer
            Get
                Return _keys.Count
            End Get
        End Property

        ''' <summary>
        ''' 行情報を返す（員数以外）
        ''' </summary>
        ''' <param name="index">行index</param>
        ''' <returns>行情報</returns>
        ''' <remarks></remarks>
        Default Public ReadOnly Property Record(ByVal index As Integer) As HoyouBuhinBuhinKoseiRecordVo
            Get
                Return _cells(index).KoseiRecordVo
            End Get
        End Property

        ''' <summary>
        ''' 員数情報を返す
        ''' </summary>
        ''' <param name="index">行index</param>
        ''' <returns>員数情報</returns>
        ''' <remarks></remarks>
        Public Property InsuVo(ByVal index As Integer) As BuhinKoseiInsuCellVo
            Get
                Return _cells(index).InsuVo
            End Get
            Set(ByVal value As BuhinKoseiInsuCellVo)
                _cells(index).InsuVo = value
            End Set
        End Property

        ''' <summary>
        ''' 行情報の位置indexを返す
        ''' </summary>
        ''' <param name="recordVo">行情報</param>
        ''' <returns>位置index</returns>
        ''' <remarks>見つからなければ、-1</remarks>
        Public Function IndexOf(ByVal recordVo As HoyouBuhinBuhinKoseiRecordVo) As Integer
            Return _keys.IndexOf(recordVo)
        End Function
        ''' <summary>
        ''' 行情報が含まれているかを返す
        ''' </summary>
        ''' <param name="recordVo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Contains(ByVal recordVo As HoyouBuhinBuhinKoseiRecordVo) As Boolean
            Return 0 <= IndexOf(recordVo)
        End Function
        ''' <summary>
        ''' 行情報を取り除く
        ''' </summary>
        ''' <param name="recordVo">行情報</param>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal recordVo As HoyouBuhinBuhinKoseiRecordVo)
            If Not Contains(recordVo) Then
                Return
            End If
            Dim index As Integer = IndexOf(recordVo)
            _keys.RemoveAt(index)
            _cells.RemoveAt(index)
        End Sub

        ''' <summary>
        ''' セル情報を追加する
        ''' </summary>
        ''' <param name="recordVo">行情報</param>
        ''' <param name="insuVo">セルの員数情報</param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal recordVo As HoyouBuhinBuhinKoseiRecordVo, ByVal insuVo As BuhinKoseiInsuCellVo)
            _keys.Add(recordVo)
            _cells.Add(New InsuCellBag(recordVo, insuVo))
        End Sub
        ''' <summary>
        ''' セル情報を挿入する
        ''' </summary>
        ''' <param name="index">挿入index</param>
        ''' <param name="recordVo">行情報</param>
        ''' <param name="insuVo">セルの員数情報</param>
        ''' <remarks></remarks>
        Public Sub Insert(ByVal index As Integer, ByVal recordVo As HoyouBuhinBuhinKoseiRecordVo, ByVal insuVo As BuhinKoseiInsuCellVo)
            If Count <= index Then
                Add(recordVo, insuVo)
                Return
            End If
            _keys.Insert(index, recordVo)
            _cells.Insert(index, New InsuCellBag(recordVo, insuVo))
        End Sub

        ''' <summary>
        ''' セル内から行情報を取り除く
        ''' </summary>
        ''' <param name="index">番号</param>
        ''' <remarks></remarks>
        Public Sub RemoveCell(ByVal index As Integer)
            '行番号を表すのか列番号を表すのか不明'
            '_keys(index) = New HoyouBuhinBuhinKoseiRecordVo()
            _cells(index) = New InsuCellBag(Nothing, Nothing)
        End Sub



        ''' <summary>
        ''' 親情報のセルindexを返す
        ''' </summary>
        ''' <param name="baseIndex">親を探索する基点となるセルのindex</param>
        ''' <returns>親情報セルindex</returns>
        ''' <remarks>無ければ、-1</remarks>
        Public Function GetParentIndex(ByVal baseIndex As Integer) As Integer
            For i As Integer = 1 To baseIndex
                If Record(baseIndex - i).Level + 1 = Record(baseIndex).Level AndAlso InsuVo(baseIndex - i).InsuSuryo IsNot Nothing Then
                    Return baseIndex - i
                End If
            Next
            Return -1
        End Function
    End Class
End Namespace