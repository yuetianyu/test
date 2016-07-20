Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon

Namespace ShisakuBuhinEdit.Kosei.Logic.Merge

    ''' <summary>
    ''' 部品表のINSTL列情報をマージするメソッドクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MergeInstlColumnBag

        Private ReadOnly aMergeMatrix As MergeMatrix
        Private ReadOnly _matrix As BuhinKoseiMatrix
        Private ReadOnly isIkanshaKaishu As Boolean

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(New BuhinKoseiMatrix)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="matrix">部品表Spreadを表すクラス</param>
        ''' <param name="CanMergeIfDefferenceOyaHinban">親品番違いでもマージする場合、true</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal matrix As BuhinKoseiMatrix, Optional ByVal CanMergeIfDefferenceOyaHinban As Boolean = True, Optional ByVal isIkanshaKaishu As Boolean = False)
            Me._matrix = matrix
            Me.aMergeMatrix = New MergeMatrix(matrix, isIkanshaKaishu)
            Me.CanMergeIfDefferenceOyaHinban = CanMergeIfDefferenceOyaHinban
            Me.isIkanshaKaishu = isIkanshaKaishu
        End Sub

#Region "Record関連"
        ''' <summary>マージした部品表</summary>
        Public ReadOnly Property ResultMatrix() As BuhinKoseiMatrix
            Get
                Return _matrix
            End Get
        End Property

        ''' <summary>
        ''' 部品表の行情報
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        Public ReadOnly Property Record(ByVal rowIndex As Integer) As BuhinKoseiRecordVo
            Get
                Return _matrix.Record(rowIndex)
            End Get
        End Property

        ''' <summary>部品表の行数</summary>
        Public ReadOnly Property RecordCount() As Integer
            Get
                Return _matrix.InputRowCount
            End Get
        End Property

        ''' <summary>
        ''' 部品表の員数情報
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        Public ReadOnly Property InsuVo(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As BuhinKoseiInsuCellVo
            Get
                Return _matrix.InsuVo(rowIndex, columnIndex)
            End Get
        End Property
#End Region

        ''' <summary>
        ''' 親品番違いでマージするか？
        ''' </summary>
        Public Property CanMergeIfDefferenceOyaHinban() As Boolean
            Get
                Return aMergeMatrix.AProperty.CanMergeIfDefferenceOyaHinban
            End Get
            Set(ByVal value As Boolean)
                aMergeMatrix.AProperty.CanMergeIfDefferenceOyaHinban = value
            End Set
        End Property

        ''' <summary>
        ''' 展開済みリストをマージする
        ''' </summary>
        ''' <param name="columnBag">INSTL列情報</param>
        ''' <param name="columnIndex">マージ列index</param>
        ''' <remarks></remarks>
        Public Sub Compute(ByVal columnBag As InstlColumnBag, ByVal columnIndex As Integer, Optional ByVal KaiteiNo As String = "")
            ComputeMain(columnBag, columnIndex, KaiteiNo)
        End Sub
        ''' <summary>
        ''' 展開済みリストをマージする
        ''' </summary>
        ''' <param name="columnBags">INSTL列情報</param>
        ''' <remarks></remarks>
        Public Sub Compute(ByVal ParamArray columnBags As InstlColumnBag())
            Dim columnIndex As Integer = _matrix.GetNewInsuColumnIndex
            For Each columnBag As InstlColumnBag In columnBags
                ComputeMain(columnBag, columnIndex)
                columnIndex += 1
            Next
        End Sub

        ''' <summary>
        ''' 1列ごとにマージする、マージのMain処理
        ''' </summary>
        ''' <param name="columnBag">INSTL列情報</param>
        ''' <param name="columnIndex">マージ先の列index</param>
        ''' <remarks></remarks>
        Private Sub ComputeMain(ByVal columnBag As InstlColumnBag, ByVal columnIndex As Integer, Optional ByVal KaiteiNo As String = "")
            If columnBag Is Nothing OrElse columnBag.Count = 0 Then
                Return
            End If
            If columnBag(0).BuhinNo Is Nothing Then
                columnBag(0).BuhinNo = "       "
            End If

            '同列内の圧縮を行う'
            columnBag = MergeColumn(columnBag)

            aMergeMatrix.InitializeMergeTop(columnBag.Record(0).Level)

            Dim dataImpl As DataImpl = New DataImpl(columnBag)

            For i As Integer = 0 To columnBag.Count - 1
                dataImpl.Index = i
                aMergeMatrix.Compute(columnIndex, dataImpl, KaiteiNo)
            Next

            '2012/01/27 ソート処理追加'
            '列毎にソートする'
            If KaiteiNo = "" Then
                aMergeMatrix.Sort(columnIndex)
            End If
        End Sub

        ''' <summary>
        ''' 同列内での圧縮処理
        ''' </summary>
        ''' <param name="columnBag"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MergeColumn(ByVal columnBag As InstlColumnBag) As InstlColumnBag

            Dim col As New InstlColumnBag()

            'レベル、集計コード、取引先コード、供給セクション、部品番号が同一の員数をマージする'
            For index As Integer = 0 To columnBag.Count - 1
                If columnBag(index) Is Nothing Then
                    Exit For
                End If


                If columnBag.Record(index).MergeFlag = 0 OrElse columnBag.Record(index).MergeFlag = Nothing Then
                    col.Add(columnBag.Record(index), columnBag.InsuVo(index))
                End If


                For index2 As Integer = 0 To columnBag.Count - 1
                    If columnBag(index2) Is Nothing Then
                        Exit For
                    End If


                    If index < index2 Then

                        '同レベル'
                        If columnBag.Record(index).Level = columnBag(index2).Level _
                                AndAlso columnBag(index2).Level <> 0 _
                                AndAlso EzUtil.IsEqualIfNull(columnBag.Record(index).ShukeiCode, columnBag.Record(index2).ShukeiCode) _
                                AndAlso EzUtil.IsEqualIfNull(columnBag.Record(index).SiaShukeiCode, columnBag.Record(index2).SiaShukeiCode) _
                                AndAlso EzUtil.IsEqualIfNull(columnBag.Record(index).MakerCode, columnBag.Record(index2).MakerCode) _
                                AndAlso EzUtil.IsEqualIfNull(columnBag.Record(index).KyoukuSection, columnBag.Record(index2).KyoukuSection) _
                                AndAlso EzUtil.IsEqualIfNull(columnBag.Record(index).BuhinNo, columnBag.Record(index2).BuhinNo) _
                                AndAlso EzUtil.IsEqualIfNull(columnBag.Record(index).BuhinNoKbn, columnBag.Record(index2).BuhinNoKbn) _
                                AndAlso EzUtil.IsEqualIfNull(columnBag.Record(index).InstlDataKbn, columnBag.Record(index2).InstlDataKbn) _
                                AndAlso columnBag.Record(index2).MergeFlag = 0 Then

                            If Me.isIkanshaKaishu Then
                                If EzUtil.IsEqualIfNull(columnBag.Record(index).BaseBuhinSeq, columnBag.Record(index2).BaseBuhinSeq) Then

                                    If col.Count <= index Then
                                        col.InsuVo(col.Count - 1).InsuSuryo = col.InsuVo(col.Count - 1).InsuSuryo + columnBag.InsuVo(index2).InsuSuryo
                                    Else
                                        col.InsuVo(index).InsuSuryo = col.InsuVo(index).InsuSuryo + columnBag.InsuVo(index2).InsuSuryo
                                    End If
                                    'col.InsuVo(index).InsuSuryo = col.InsuVo(index).InsuSuryo + columnBag.InsuVo(index2).InsuSuryo
                                    columnBag.Record(index2).MergeFlag = 1
                                    'columnBag.RemoveCell(index2)
                                End If
                            Else
                                If col.Count <= index Then
                                    col.InsuVo(col.Count - 1).InsuSuryo = col.InsuVo(col.Count - 1).InsuSuryo + columnBag.InsuVo(index2).InsuSuryo
                                Else
                                    col.InsuVo(index).InsuSuryo = col.InsuVo(index).InsuSuryo + columnBag.InsuVo(index2).InsuSuryo
                                End If
                                'col.InsuVo(index).InsuSuryo = col.InsuVo(index).InsuSuryo + columnBag.InsuVo(index2).InsuSuryo
                                columnBag.Record(index2).MergeFlag = 1
                                'columnBag.RemoveCell(index2)
                            End If

                        End If
                    End If
                Next
            Next


            Return col
        End Function

        ''' <summary>
        ''' INSTL列情報に対する IData実装クラス
        ''' </summary>
        ''' <remarks></remarks>
        Private Class DataImpl : Implements MergeMatrix.IData

            Private ReadOnly columnBag As InstlColumnBag
            ''' <summary>INSTL列情報の中のindex</summary>
            Public Index As Integer = 0

            ''' <summary>
            ''' コンストラクタ
            ''' </summary>
            ''' <param name="columnBag">INSTL列情報</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal columnBag As InstlColumnBag)
                Me.columnBag = columnBag
            End Sub

            Public Function GetInsuVo() As BuhinKoseiInsuCellVo Implements MergeMatrix.IData.GetInsuVo
                Return columnBag.InsuVo(Index)
            End Function

            Public Function GetBuhinNo() As String Implements MergeMatrix.IData.GetBuhinNo
                Return columnBag.Record(Index).BuhinNo
            End Function

            Public Function GetBuhinNoKbn() As String Implements MergeMatrix.IData.GetBuhinNoKbn
                Return columnBag.Record(Index).BuhinNoKbn
            End Function

            Public Function GetGencyoCkdKbn() As String Implements MergeMatrix.IData.GetGencyoCkdKbn
                Return columnBag.Record(Index).GencyoCkdKbn
            End Function

            Public Function GetLevel() As Integer? Implements MergeMatrix.IData.GetLevel
                Return columnBag.Record(Index).Level
            End Function

            Public Function GetMakerCode() As String Implements MergeMatrix.IData.GetMakerCode
                Return columnBag.Record(Index).MakerCode
            End Function

            Public Function GetRecordNewRow() As BuhinKoseiRecordVo Implements MergeMatrix.IData.GetRecordNewRow
                Return columnBag.Record(Index)
            End Function

            Public Function GetParent() As MergeMatrix.IData Implements MergeMatrix.IData.GetParent
                Dim parent As New DataImpl(columnBag)
                parent.Index = columnBag.GetParentIndex(Index)
                Return parent
            End Function

            Public Function GetShukeiCode() As String Implements MergeMatrix.IData.GetShukeiCode
                Return columnBag.Record(Index).ShukeiCode
            End Function

            Public Function GetSiaShukeiCode() As String Implements MergeMatrix.IData.GetSiaShukeiCode
                Return columnBag.Record(Index).SiaShukeiCode
            End Function

            Public Function GetSaishiyouFuka() As String Implements MergeMatrix.IData.GetSaishiyouFuka
                Return columnBag.Record(Index).Saishiyoufuka
            End Function

            '2012/01/23 供給セクション追加
            Public Function GetKyoukuSection() As String Implements MergeMatrix.IData.GetKyoukuSection
                Return columnBag.Record(Index).KyoukuSection
            End Function
            '2014/09/23 酒井 ADD
            Public Function GetBaseBuhinFlg() As String Implements MergeMatrix.IData.GetBaseBuhinFlg
                Return columnBag.Record(Index).BaseBuhinFlg
            End Function

            '2015/09/08 ADD
            Public Function GetBaseBuhinSeq() As Nullable(Of Int32) Implements MergeMatrix.IData.GetBaseBuhinSeq
                Return columnBag.Record(Index).BaseBuhinSeq
            End Function




            Public Function GetInstlDataKbn() As String Implements MergeMatrix.IData.GetInstlDataKbn
                Return columnBag.Record(Index).InstlDataKbn
            End Function

        End Class
    End Class
End Namespace