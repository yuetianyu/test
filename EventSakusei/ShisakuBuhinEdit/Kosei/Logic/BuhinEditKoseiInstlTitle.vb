Imports EventSakusei.ShisakuBuhinEdit.Logic.Detect
Imports EventSakusei.ShisakuBuhinEdit.Logic
Imports ShisakuCommon.Util

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 部品構成画面のINSTL品番情報を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditKoseiInstlTitle
        ''' <summary>
        ''' INSTL品番情報
        ''' </summary>
        ''' <remarks></remarks>
        Private Class InstlHinbanInfo
            Public InstlHinban As String
            Public InstlHinbanKbn As String
            ''↓↓2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(3)) (TES)張 ADD BEGIN
            Public InstlDataKbn As String
            ''↑↑2014/08/21 1)EBOM-新試作手配システム過去データの組み合わせ抽出_⑥VI(3)) (TES)張 ADD END

            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD BEGIN
            Public BaseFlg As String
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_o) (TES)張 ADD END
            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_m) (TES)張 ADD BEGIN
            Public BaseBuhinFlg As String
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_m) (TES)張 ADD END
            ''↓↓2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
            Public BaseInstlFlg As String
            ''↑↑2014/07/25 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END
        End Class

        Private _recordInstl As New IndexedList(Of InstlHinbanInfo)
        Private anEzTunnel As EzTunnelAlKosei

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="anEzTunnel">抜け穴interface</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal anEzTunnel As EzTunnelAlKosei)
            Me.anEzTunnel = anEzTunnel
        End Sub

        ''' <summary>INSTL品番</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinban(ByVal columnIndex As Integer) As String
            Get
                Return _recordInstl.Value(columnIndex).InstlHinban
            End Get
            Set(ByVal value As String)
                _recordInstl.Value(columnIndex).InstlHinban = value
            End Set
        End Property

        ''' <summary>INSTL品番区分</summary>
        ''' <param name="columnIndex">列No</param>
        Public Property InstlHinbanKbn(ByVal columnIndex As Integer) As String
            Get
                Return _recordInstl.Value(columnIndex).InstlHinbanKbn
            End Get
            Set(ByVal value As String)
                _recordInstl.Value(columnIndex).InstlHinbanKbn = value
            End Set
        End Property

        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_q) (TES)張 ADD BEGIN
        Public Property BaseBuhinFlg(ByVal columnIndex As Integer) As String
            Get
                Return _recordInstl.Value(columnIndex).BaseBuhinFlg
            End Get
            Set(ByVal value As String)
                _recordInstl.Value(columnIndex).BaseBuhinFlg = value
            End Set
        End Property
        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_q) (TES)張 ADD END

        ''↓↓2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD BEGIN
        Public Property BaseInstlFlg(ByVal columnIndex As Integer) As String
            Get
                Return _recordInstl.Value(columnIndex).BaseInstlFlg
            End Get
            Set(ByVal value As String)
                _recordInstl.Value(columnIndex).BaseInstlFlg = value
            End Set
        End Property
        ''↑↑2014/07/24 Ⅰ.3.設計編集 ベース改修専用化_t) (TES)張 ADD END

        Public Property InstlDataKbn(ByVal columnIndex As Integer) As String
            Get
                Return _recordInstl.Value(columnIndex).InstlDataKbn
            End Get
            Set(ByVal value As String)
                _recordInstl.Value(columnIndex).InstlDataKbn = value
            End Set
        End Property

        ''' <summary>
        ''' 構成の情報を返す
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>構成の情報</returns>
        ''' <remarks></remarks>
        Public Function GetStructureResult(ByVal columnIndex As Integer) As StructureResult
            Return anEzTunnel.GetStructureResult(columnIndex)
        End Function

        ''' <summary>
        ''' 入力した列タイトルの列No一覧を返す
        ''' </summary>
        ''' <returns>入力した列タイトルの列No一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputInstlHinbanColumnIndexes() As ICollection(Of Integer)
            Return _recordInstl.Keys
        End Function

        ''' <summary>
        ''' INSTL品番列に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub Insert(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            _recordInstl.Insert(columnIndex, insertCount)
        End Sub

        ''' <summary>
        ''' INSTL品番列を列削除する
        ''' </summary>
        ''' <param name="columnIndex">INSTL品番の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            _recordInstl.Remove(columnIndex, removeCount)
        End Sub
    End Class
End Namespace