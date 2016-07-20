Imports Microsoft.Office.Interop
Imports System.Text

Namespace ShisakuBuhinMSTMaintenance.Common

    Public Class ExcelRead

        Private mFileName As String
        Private mRCnt As Integer
        Private mCCnt As Integer
        Private mArrData(,) As Object
        Private mResultBuf() As String

#Region "プロパティ"
        ''' <summary>
        ''' 読み取ったエクセルデータのインスタンス参照.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property bufRead() As Object(,)
            Get
                Return mArrData
            End Get
        End Property

        ''' <summary>
        ''' 有効行数を返す.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property RowCnt() As Integer
            Get
                Return mRCnt
            End Get
        End Property

        ''' <summary>
        ''' 有効列数を返す.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ColumnCnt() As Integer
            Get
                Return mCCnt
            End Get
        End Property
#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="fileName">読み込むエクセルファイルのパスを指定</param>
        ''' <param name="aColCnt">有効読み込み列数</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal fileName As String, ByVal aColCnt As Integer)
            mFileName = fileName

            mCCnt = aColCnt

            Call mainProcess()
        End Sub

#End Region

        ''' <summary>
        ''' ファイル読み込みメイン.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub mainProcess()
            'Try

            Call read()

            'Catch exArg As ArgumentException
            '    MessageBox.Show(exArg.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'Catch ex As Exception

            'End Try

        End Sub


        ''' <summary>
        ''' エクセルファイル読み込み実装.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub read()

            'Excelを起動する
            Dim excelApp As Excel.Application
            Dim xlsBooks As Excel.Workbooks
            Dim xlsBook As Excel.Workbook
            Dim xlsSheets As Excel.Sheets
            Dim xlsSheet As Excel.Worksheet

            excelApp = CreateObject("Excel.Application")
            excelApp.DisplayAlerts = False

            xlsBooks = excelApp.Workbooks

            ''ファイル存在チェック.
            'If True = IO.Directory.Exists(mFileName) Then
            '    Throw New IO.IOException("指定されたファイルは存在しません.")
            'End If
            If mFileName Is Nothing Then Exit Sub

            xlsBook = xlsBooks.Open(mFileName)

            xlsSheets = xlsBook.Worksheets
            xlsSheet = CType(xlsSheets.Item(1), Excel.Worksheet)

            With xlsSheet
                '有効行数格納
                mRCnt = .UsedRange.Rows.Count
                '有効列数格納
                If mCCnt <> .UsedRange.Columns.Count Then Throw New ArgumentException("取り込むエクセルファイルの有効列数とデータベースの列数が一致しません。")

                'エクセルシート情報から配列の生成
                mArrData = excelApp.Range(.Cells(1, 1), .Cells(mRCnt, mCCnt)).Value

            End With

            xlsBooks.Close()

            excelApp = Nothing

        End Sub



    End Class

End Namespace