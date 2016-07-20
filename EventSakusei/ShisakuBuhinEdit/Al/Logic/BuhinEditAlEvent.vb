Imports EventSakusei.ShisakuBuhinEdit.Al.Dao
Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util
Imports ShisakuCommon

Namespace ShisakuBuhinEdit.Al.Logic

    ''' <summary>
    ''' ベース車情報・完成車情報の表示値を担うクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditAlEvent

        Private ReadOnly shisakuEventCode As String
        Private ReadOnly alDao As BuhinEditAlDao
        Private _ShowColumnInfos As List(Of TShisakuSekkeiBlockSoubiVo)

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="showColumnInfos">試作設計ブロック装備情報の一覧</param>
        ''' <param name="alDao">A/L画面用のDao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, ByVal showColumnInfos As List(Of TShisakuSekkeiBlockSoubiVo), ByVal alDao As BuhinEditAlDao)
            Me.shisakuEventCode = shisakuEventCode
            Me._ShowColumnInfos = showColumnInfos
            Me.alDao = alDao

            ReadRecord()
        End Sub

'/*** 20140911 CHANGE START（コンストラクタ追加） ***/
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="shisakuEventCode">試作イベントコード</param>
        ''' <param name="showColumnInfos">試作設計ブロック装備情報の一覧</param>
        ''' <param name="alDao">A/L画面用のDao</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal showColumnInfos As List(Of TShisakuSekkeiBlockSoubiVo), ByVal alDao As BuhinEditAlDao, _
                       ByVal vos As List(Of BuhinEditAlEventVo))
            Me.shisakuEventCode = shisakuEventCode
            Me._ShowColumnInfos = showColumnInfos
            Me.alDao = alDao

            ReadRecord(vos)
        End Sub


        ''' <summary>
        ''' 情報を読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecord(ByVal vos As List(Of BuhinEditAlEventVo))
            For Each vo As BuhinEditAlEventVo In vos
                _record.Add(vo.HyojijunNo, vo)
            Next
        End Sub
'/*** 20140911 CHANGE END ***/
        ''' <summary>
        ''' 情報を読み込む
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecord()
            Dim vos As List(Of BuhinEditAlEventVo) = alDao.FindEventInfoById(shisakuEventCode)
            For Each vo As BuhinEditAlEventVo In vos
                _record.Add(vo.HyojijunNo, vo)
            Next
        End Sub

        Private _record As New IndexedList(Of BuhinEditAlEventVo)

        ''' <summary>ベース車・完成車情報</summary>
        ''' <returns>ベース車・完成車情報</returns>
        Private ReadOnly Property Records(ByVal rowNo As Integer) As BuhinEditAlEventVo
            Get
                Return _record.Value(rowNo)
            End Get
        End Property

        ''' <summary>
        ''' 入力行の行Noの一覧を返す
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Public Function GetInputRowNos() As ICollection(Of Integer)
            Return _record.Keys
        End Function

        ''' <summary>列表示情報</summary>
        Public Property ShowColumnInfos() As List(Of TShisakuSekkeiBlockSoubiVo)
            Get
                Return _ShowColumnInfos
            End Get
            Set(ByVal value As List(Of TShisakuSekkeiBlockSoubiVo))
                _ShowColumnInfos = value
            End Set
        End Property

        ''' <summary>号車</summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>号車</returns>
        Public ReadOnly Property ShisakuGosha(ByVal rowIndex As Integer) As String
            Get
                Return Records(rowIndex).ShisakuGousya
            End Get
        End Property
        ''' 2012-01-11
        ''' <summary>試作種別</summary>
        ''' <param name="rowIndex">行index</param>
        ''' <returns>試作種別</returns>
        Public ReadOnly Property ShisakuSyubetu(ByVal rowIndex As Integer) As String
            Get
                Return Records(rowIndex).ShisakuSyubetu
            End Get
        End Property

        ''' <summary>(全体の)列数</summary>
        Public ReadOnly Property ColumnCount() As Integer
            Get
                Return _ShowColumnInfos.Count
            End Get
        End Property

        ''' <summary>
        ''' ベース車情報の列数を返す
        ''' </summary>
        ''' <returns>ベース車情報の列数</returns>
        ''' <remarks></remarks>
        Public Function GetColumnCountBase() As Integer
            Dim result As Integer = 0
            For Each vo As TShisakuSekkeiBlockSoubiVo In _ShowColumnInfos
                If TShisakuSekkeiBlockSoubiVoHelper.IsBaseCarHyoujiJun(vo.ShisakuSoubiHyoujiJun) Then
                    result += 1
                End If
            Next
            Return result
        End Function


        ''' <summary>
        ''' 設計展開用のベース車情報の列数を返す
        ''' </summary>
        ''' <returns>設計展開ベース車情報の列数</returns>
        ''' <remarks></remarks>
        Public Function GetColumnCountBaseTenkai() As Integer
            Dim result As Integer = 0
            For Each vo As TShisakuSekkeiBlockSoubiVo In _ShowColumnInfos
                If TShisakuSekkeiBlockSoubiVoHelper.IsBaseTenkaiCarHyoujiJun(vo.ShisakuSoubiHyoujiJun) Then
                    result += 1
                End If
            Next
            Return result
        End Function

        ''' <summary>タイトル名</summary>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>タイトル名</returns>
        Public ReadOnly Property Title(ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _ShowColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return TShisakuSekkeiBlockSoubiVoHelper.GetNameByCode(_ShowColumnInfos(columnIndex).ShisakuSoubiHyoujiJun)
            End Get
        End Property


        '-------------------------------------------------------------------------------------------------------
        '２次改修
        '   カラムIDを返す
        ''' <summary>(該当レコード・該当列の)内容値</summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容値</returns>
        Public ReadOnly Property InfoColumnId(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _ShowColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return _ShowColumnInfos(columnIndex).ShisakuSoubiHyoujiJun
            End Get
        End Property
        '-------------------------------------------------------------------------------------------------------

        ''' <summary>(該当レコード・該当列の)内容値</summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="columnIndex">列index</param>
        ''' <returns>内容値</returns>
        Public ReadOnly Property Info(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As String
            Get
                If columnIndex < 0 OrElse _ShowColumnInfos.Count <= columnIndex Then
                    Throw New ArgumentOutOfRangeException("columnIndex", columnIndex, "範囲外")
                End If
                Return ResolveValue(rowIndex, _ShowColumnInfos(columnIndex).ShisakuSoubiHyoujiJun)
            End Get
        End Property

        ''' <summary>
        ''' 該当レコード・該当表示順の値を返す
        ''' </summary>
        ''' <param name="rowIndex">行index</param>
        ''' <param name="shisakuHyojijunNo">試作装備表示順</param>
        ''' <returns>値</returns>
        ''' <remarks></remarks>
        Private Function ResolveValue(ByVal rowIndex As Integer, ByVal shisakuHyojijunNo As String) As String
            Select Case shisakuHyojijunNo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_KAIHATUFUGOU
                    Return Records(rowIndex).BaseKaihatsuFugo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_SHIYOUJYOUHOU_NO
                    Return Records(rowIndex).BaseShiyoujyouhouNo

                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_SYASYU
                    Return Records(rowIndex).BaseSeisakuSyasyu
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_GRADE
                    Return Records(rowIndex).BaseSeisakuGrade
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_SHIMUKE
                    Return Records(rowIndex).BaseSeisakuShimuke
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_HANDORU
                    Return Records(rowIndex).BaseSeisakuHandoru
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_EG_HAIKIRYOU
                    Return Records(rowIndex).BaseSeisakuEgHaikiryou
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_EG_KATASHIKI
                    Return Records(rowIndex).BaseSeisakuEgKatashiki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_EG_KAKYUUKI
                    Return Records(rowIndex).BaseSeisakuEgKakyuuki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_TM_KUDOU
                    Return Records(rowIndex).BaseSeisakuTmKudou
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_TM_HENSOKUKI
                    Return Records(rowIndex).BaseSeisakuTmHensokuki

                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_APPLIED_NO
                    Return Records(rowIndex).BaseAppliedNo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_KATASHIKI
                    Return Records(rowIndex).BaseKatashiki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_SHIMUKE
                    Return Records(rowIndex).BaseShimuke
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_OP
                    Return Records(rowIndex).BaseOp
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_GAISOUSYOKU
                    Return Records(rowIndex).BaseGaisousyoku
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_NAISOUSYOKU
                    Return Records(rowIndex).BaseNaisousyoku
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_EVENT_CODE
                    Return Records(rowIndex).ShisakuBaseEventCode
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.BASE_GOUSYA
                    Return Records(rowIndex).ShisakuBaseGousya
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.SEISAKU_SYATAI_NO
                    Return Records(rowIndex).ShisakuSyadaiNo


                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYAGATA
                    Return Records(rowIndex).ShisakuSyagata
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GRADE
                    Return Records(rowIndex).ShisakuGrade
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_SHIMUKECHI_SHIMUKE
                    Return Records(rowIndex).ShisakuShimukechiShimuke
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_HANDORU
                    Return Records(rowIndex).ShisakuHandoru
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KATASHIKI
                    Return Records(rowIndex).ShisakuEgKatashiki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_HAIKIRYOU
                    Return Records(rowIndex).ShisakuEgHaikiryou
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_SYSTEM
                    Return Records(rowIndex).ShisakuEgSystem
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_KAKYUUKI
                    Return Records(rowIndex).ShisakuEgKakyuuki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO1
                    Return Records(rowIndex).ShisakuEgMemo1
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_EG_MEMO2
                    Return Records(rowIndex).ShisakuEgMemo2
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_KUDOU
                    Return Records(rowIndex).ShisakuTmKudou
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_HENSOKUKI
                    Return Records(rowIndex).ShisakuTmHensokuki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_FUKU_HENSOKUKI
                    Return Records(rowIndex).ShisakuTmFukuHensokuki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO1
                    Return Records(rowIndex).ShisakuTmMemo1
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_TM_MEMO2
                    Return Records(rowIndex).ShisakuTmMemo2
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KATASHIKI
                    Return Records(rowIndex).ShisakuKatashiki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIMUKE
                    Return Records(rowIndex).ShisakuShimuke
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_OP
                    Return Records(rowIndex).ShisakuOp
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU
                    Return Records(rowIndex).ShisakuGaisousyoku
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GAISOUSYOKU_NAME
                    Return Records(rowIndex).ShisakuGaisousyokuName
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU
                    Return Records(rowIndex).ShisakuNaisousyoku
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_NAISOUSYOKU_NAME
                    Return Records(rowIndex).ShisakuNaisousyokuName
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SYADAI_NO
                    Return Records(rowIndex).ShisakuSyadaiNo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIYOU_MOKUTEKI
                    Return Records(rowIndex).ShisakuShiyouMokuteki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHIKEN_MOKUTEKI
                    Return Records(rowIndex).ShisakuShikenMokuteki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SIYOU_BUSYO
                    Return Records(rowIndex).ShisakuSiyouBusyo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_GROUP
                    Return Records(rowIndex).ShisakuGroup
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_JUNJYO
                    Return Records(rowIndex).ShisakuSeisakuJunjyo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KANSEIBI
                    'NULL許容型だから
                    If Records(rowIndex).ShisakuKanseibi.HasValue Then
                        Return Records(rowIndex).ShisakuKanseibi
                    Else
                        Return ""
                    End If
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_KOUSHI_NO
                    Return Records(rowIndex).ShisakuKoushiNo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU_KBN
                    Return Records(rowIndex).ShisakuSeisakuHouhouKbn
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SEISAKU_HOUHOU
                    Return Records(rowIndex).ShisakuSeisakuHouhou
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.KANSEI_SHISAKU_MEMO
                    Return Records(rowIndex).ShisakuMemo



                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_KAIHATUFUGOU
                    Return Records(rowIndex).TenkaiBaseKaihatsuFugo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_SHIYOUJYOUHOU_NO
                    Return Records(rowIndex).TenkaiBaseShiyoujyouhouNo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_APPLIED_NO
                    Return Records(rowIndex).TenkaiBaseAppliedNo
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_KATASHIKI
                    Return Records(rowIndex).TenkaiBaseKatashiki
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_SHIMUKE
                    Return Records(rowIndex).TenkaiBaseShimuke
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_OP
                    Return Records(rowIndex).TenkaiBaseOp
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_GAISOUSYOKU
                    Return Records(rowIndex).TenkaiBaseGaisousyoku
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_NAISOUSYOKU
                    Return Records(rowIndex).TenkaiBaseNaisousyoku
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_EVENT_CODE
                    Return Records(rowIndex).TenkaiShisakuBaseEventCode
                Case TShisakuSekkeiBlockSoubiVoHelper.ShisakuSoubiHyoujiJun.TENKAI_BASE_GOUSYA
                    Return Records(rowIndex).TenkaiShisakuBaseGousya

            End Select
            Throw New ArgumentException("値 " & shisakuHyojijunNo & " は不明な値です.", "shisakuHyojijunNo")
        End Function


        Private _RowNoByGoshaIndexes As Dictionary(Of String, Integer)
        ''' <summary>
        ''' 号車からRowNoを返す Dictionary を返す
        ''' </summary>
        ''' <returns>Dictionary</returns>
        ''' <remarks></remarks>
        Public Function GetRowNoByGoshaIndexes() As Dictionary(Of String, Integer)
            If _RowNoByGoshaIndexes Is Nothing Then
                _RowNoByGoshaIndexes = New Dictionary(Of String, Integer)
                For Each rowNo As Integer In GetInputRowNos()
                    '号車と号車の間にブランクが２行以上あると号車が重複エラーとなるため除いてみる。
                    ''''_RowNoByGoshaIndexesに同じ号車を格納しようとしてエラーになっている。
                    ''''そのようなデータが存在しているのか自体が疑問だがキーの有無をチェックしてみる　太田
                    If Not StringUtil.IsEmpty(Records(rowNo).ShisakuGousya) Then
                        If Not _RowNoByGoshaIndexes.ContainsKey(Records(rowNo).ShisakuGousya) Then
                            _RowNoByGoshaIndexes.Add(Records(rowNo).ShisakuGousya, rowNo)
                        End If
                    End If

                Next
            End If
            Return _RowNoByGoshaIndexes
        End Function

    End Class
End Namespace