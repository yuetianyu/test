Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.Soubi
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports System.Text
Imports ShisakuCommon.ShisakuComFunc

Namespace EventEdit.Logic
    Public Class EventEditOption : Inherits Observable

        Private Const SHUBETSU_GOSHA_COLUMN_NO As Integer = -1
        Private Const TITLE_ROW_NO As Integer = -1

        Private shisakuEventCode As String

        Private seisakuHakouNo As String
        Private seisakuHakouNoKaiteiNo As String

        Private kanseiList As New List(Of TSeisakuIchiranKanseiVo)
        Private wbList As New List(Of TSeisakuIchiranWbVo)

        Private ReadOnly login As LoginInfo
        Private ReadOnly shisakuSoubiKbn As String
        Private ReadOnly mSoubiDao As MShisakuSoubiDao
        Private ReadOnly tSoubiDao As TShisakuEventSoubiDao
        Private ReadOnly tSoubiKaiteiDao As TShisakuEventSoubikaiteiDao
        Private ReadOnly specialDao As EventSoubiDao
        Private ReadOnly aDate As ShisakuDate
        Private ReadOnly aEzSync As EzSyncShubetsuGosha
        'ベース情報
        Private ReadOnly aBaseCar As EventEditBaseCar

        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal shisakuSoubiKbn As String, _
                       ByVal aEzSync As EzSyncShubetsuGosha, _
                       ByVal isSekkeiTenkaiIkou As Boolean, _
                       ByVal mSoubiDao As MShisakuSoubiDao, _
                       ByVal tSoubiDao As TShisakuEventSoubiDao, _
                       ByVal tSoubiKaiteiDao As TShisakuEventSoubiKaiteiDao, _
                       ByVal specialDao As EventSoubiDao, _
                       ByVal aDate As ShisakuDate, _
                       ByVal seisakuHakouNo As String, _
                       ByVal seisakuHakouNoKaiteiNo As String, _
                       ByVal aBaseCar As EventEditBaseCar)
            Me.shisakuEventCode = shisakuEventCode
            Me.login = login
            Me.shisakuSoubiKbn = shisakuSoubiKbn
            Me.mSoubiDao = mSoubiDao
            Me.tSoubiDao = tSoubiDao
            Me.tSoubiKaiteiDao = tSoubiKaiteiDao
            Me.specialDao = specialDao
            Me.aDate = aDate
            Me.aEzSync = aEzSync
            Me._isSekkeiTenkaiIkou = isSekkeiTenkaiIkou
            'ベース情報
            Me.aBaseCar = aBaseCar

            Me.seisakuHakouNo = seisakuHakouNo
            Me.seisakuHakouNoKaiteiNo = seisakuHakouNoKaiteiNo

            '製作一覧キー情報があれば製作一覧情報からセットする。
            If StringUtil.IsNotEmpty(seisakuHakouNo) Then
                If Not IsAddMode() Then
                    '編集モードかつ設計展開以降なら
                    If StringUtil.Equals(shisakuSoubiKbn, "1") Then
                        '基本装備仕様
                        If StringUtil.Equals(isSekkeiTenkaiIkou, True) Then
                            ReadRecordsUpdateBasicAto()
                        Else
                            'ReadRecordsUpdateBasicAto()
                            'ReadRecordsUpdateBasicMae()
                        End If
                    ElseIf StringUtil.Equals(shisakuSoubiKbn, "2") Then
                        '特別装備仕様
                        If StringUtil.Equals(isSekkeiTenkaiIkou, True) Then
                            ReadRecordsUpdateSpecialAto()
                        Else
                            'ReadRecordsUpdateSpecialAto()
                            'ReadRecordsUpdateSpecialMae()
                        End If
                    End If
                End If
            Else
                If Not IsAddMode() Then
                    ReadRecords()
                End If
            End If


            SetChanged()
        End Sub
#Region "装備情報のDelegateプロパティ"
        ''' <summary>試作種別</summary>
        ''' <param name="rowNo">行No</param>
        Public Property ShisakuSyubetu(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo, SHUBETSU_GOSHA_COLUMN_NO).ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo, SHUBETSU_GOSHA_COLUMN_NO).ShisakuSyubetu, value) Then
                    Return
                End If
                Records(rowNo, SHUBETSU_GOSHA_COLUMN_NO).ShisakuSyubetu = value
                SetChanged()
                aEzSync.NotifyShubetsu(Me, rowNo)
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <param name="rowNo">行No</param>
        Public Property ShisakuGousya(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo, SHUBETSU_GOSHA_COLUMN_NO).ShisakuGousya
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(rowNo, SHUBETSU_GOSHA_COLUMN_NO).ShisakuGousya, value) Then
                    Return
                End If
                Records(rowNo, SHUBETSU_GOSHA_COLUMN_NO).ShisakuGousya = value
                SetChanged()
                aEzSync.NotifyGosha(Me, rowNo)
            End Set
        End Property

        ''' <summary>タイトル名</summary>
        ''' <param name="columnNo">列No</param>
        Public Property TitleName(ByVal columnNo As Integer) As String
            Get
                Return Records(TITLE_ROW_NO, columnNo).ShisakuTekiyou
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(TITLE_ROW_NO, columnNo).ShisakuTekiyou, value) Then
                    Return
                End If
                Records(TITLE_ROW_NO, columnNo).ShisakuTekiyou = value
                SetChanged()
            End Set
        End Property

        ''' <summary>タイトル名（大区分）</summary>
        ''' <param name="columnNo">列No</param>
        Public Property TitleNameDai(ByVal columnNo As Integer) As String
            Get
                Return Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouDai
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouDai, value) Then
                    Return
                End If
                Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouDai = value
                SetChanged()
            End Set
        End Property

        ''' <summary>タイトル名（中区分）</summary>
        ''' <param name="columnNo">列No</param>
        Public Property TitleNameChu(ByVal columnNo As Integer) As String
            Get
                Return Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouChu
            End Get
            Set(ByVal value As String)
                If EzUtil.IsEqualIfNull(Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouChu, value) Then
                    Return
                End If
                Records(TITLE_ROW_NO, columnNo).ShisakuTekiyouChu = value
                SetChanged()
            End Set
        End Property

        ''' <summary>タイトルの項目コード</summary>
        ''' <param name="columnNo">列No</param>
        Private Property TitleRetuKoumokuCode(ByVal columnNo As Integer) As String
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
                If EzUtil.IsEqualIfNull(Records(rowNo, columnNo).ShisakuTekiyou, value) Then
                    Return
                End If
                Records(rowNo, columnNo).ShisakuTekiyou = value
                SetChanged()
            End Set
        End Property
#End Region

        Public Class EventEditOptionReader
            Private _recordDimension As New IndexedList(Of IndexedList(Of EventEditOptionVo))

            ''' <summary>装備情報</summary>
            ''' <param name="rowNo">行No</param>
            ''' <param name="columnNo">列No</param>
            ''' <returns>装備情報</returns>
            ''' <remarks></remarks>
            Private ReadOnly Property Records(ByVal rowNo As Integer, ByVal columnNo As Integer) As EventEditOptionVo
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

            Private shisakuEventCode As String
            Private ReadOnly shisakuSoubiKbn As String
            Private ReadOnly mSoubiDao As MShisakuSoubiDao
            Private ReadOnly tSoubiDao As TShisakuEventSoubiDao
            Private ReadOnly specialDao As EventSoubiDao
            Private Sub ReadRecords()

                '' タイトル
                Dim titleVos As List(Of TShisakuEventSoubiNameVo) = specialDao.FindWithTitleNameBy(shisakuEventCode, shisakuSoubiKbn)
                For Each vo As TShisakuEventSoubiNameVo In titleVos
                    If vo.HyojijunNo <> TITLE_ROW_NO Then
                        Continue For
                    End If
                    vo.ShisakuTekiyou = vo.ShisakuRetuKoumokuName
                    vo.ShisakuTekiyouDai = vo.ShisakuRetuKoumokuNameDai
                    vo.ShisakuTekiyouChu = vo.ShisakuRetuKoumokuNameChu

                    VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
                Next

                '' データ部
                Dim param As New TShisakuEventSoubiVo
                param.ShisakuEventCode = shisakuEventCode
                param.ShisakuSoubiKbn = shisakuSoubiKbn
                Dim vos As List(Of TShisakuEventSoubiVo) = tSoubiDao.FindBy(param)
                For Each vo As TShisakuEventSoubiVo In vos
                    If vo.HyojijunNo = TITLE_ROW_NO Then
                        Continue For
                    End If
                    VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
                Next
            End Sub

        End Class
#Region "行列情報取得・操作"
        Private _recordDimension As New IndexedList(Of IndexedList(Of EventEditOptionVo))

        ''' <summary>装備情報</summary>
        ''' <param name="rowNo">行No</param>
        ''' <param name="columnNo">列No</param>
        ''' <returns>装備情報</returns>
        ''' <remarks></remarks>
        Private ReadOnly Property Records(ByVal rowNo As Integer, ByVal columnNo As Integer) As EventEditOptionVo
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
                If columnNo = SHUBETSU_GOSHA_COLUMN_NO Then
                    Continue For
                End If
                results.Add(columnNo)
            Next
            Return results
        End Function

        ''' <summary>
        ''' 行を挿入する
        ''' </summary>
        ''' <param name="rowNo">挿入先の行No</param>
        ''' <remarks></remarks>
        Public Sub InsertRow(ByVal rowNo As Integer)
            _recordDimension.Insert(rowNo)
        End Sub

        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="rowNo">削除する行No</param>
        ''' <remarks></remarks>
        Public Sub DeleteRow(ByVal rowNo As Integer)
            _recordDimension.Remove(rowNo)
        End Sub

        ''' <summary>
        ''' 列を挿入する
        ''' </summary>
        ''' <param name="columnNo">挿入先の列No</param>
        ''' <remarks></remarks>
        Public Sub InsertColumn(ByVal columnNo As Integer)
            For Each rowNo As Integer In _recordDimension.Keys
                _recordDimension.Value(rowNo).Insert(columnNo)
            Next
        End Sub

        ''' <summary>
        ''' 列を削除する
        ''' </summary>
        ''' <param name="columnNo">削除する列No</param>
        Public Sub DeleteColumn(ByVal columnNo As Integer)
            For Each rowNo As Integer In _recordDimension.Keys
                _recordDimension.Value(rowNo).Remove(columnNo)
            Next
        End Sub

        Private Sub ReadRecords()

            '' タイトル
            Dim titleVos As List(Of TShisakuEventSoubiNameVo) = specialDao.FindWithTitleNameBy(shisakuEventCode, shisakuSoubiKbn)
            For Each vo As TShisakuEventSoubiNameVo In titleVos
                If vo.HyojijunNo <> TITLE_ROW_NO Then
                    Continue For
                End If
                vo.ShisakuTekiyou = vo.ShisakuRetuKoumokuName
                vo.ShisakuTekiyouDai = vo.ShisakuRetuKoumokuNameDai
                vo.ShisakuTekiyouChu = vo.ShisakuRetuKoumokuNameChu

                VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
            Next

            '' データ部
            Dim param As New TShisakuEventSoubiVo
            param.ShisakuEventCode = shisakuEventCode
            param.ShisakuSoubiKbn = shisakuSoubiKbn
            Dim vos As List(Of TShisakuEventSoubiVo) = tSoubiDao.FindBy(param)
            For Each vo As TShisakuEventSoubiVo In vos
                If vo.HyojijunNo = TITLE_ROW_NO Then
                    Continue For
                End If
                VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
            Next
        End Sub

        Private Sub ReadRecordsUpdateBasicMae()

        End Sub

        Private Sub ReadRecordsUpdateBasicAto()

            '初期設定
            Dim OptionSeisakuIchiran = New List(Of TSeisakuIchiranOpkoumokuVo)
            Dim Ichiran = New SeisakuIchiranDaoImpl
            Dim OPList As New List(Of TSeisakuIchiranOpkoumokuVo)
            Dim columntCnt As Integer = 0
            '   製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            ''完成車情報
            'kanseiList = Ichiran.GetTSeisakuIchiranKansei(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            ''ＷＢ車情報
            'wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            '-------------------------------------------------------------------------------------
            'タイトル部
            '   試作イベントの仕様情報列を設定する。
            '-------------------------------------------------------------------------------------
            '' タイトル
            Dim titleVos As List(Of TShisakuEventSoubiNameVo) = specialDao.FindWithTitleNameBy(shisakuEventCode, shisakuSoubiKbn)
            For Each vo As TShisakuEventSoubiNameVo In titleVos
                If vo.HyojijunNo <> TITLE_ROW_NO Then
                    Continue For
                End If
                vo.ShisakuTekiyou = Trim(vo.ShisakuRetuKoumokuName)
                vo.ShisakuTekiyouDai = Trim(vo.ShisakuRetuKoumokuNameDai)
                vo.ShisakuTekiyouChu = Trim(vo.ShisakuRetuKoumokuNameChu)

                VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
                '列順をカウント。
                If columntCnt <= vo.ShisakuSoubiHyoujiNo + 1 Then columntCnt = vo.ShisakuSoubiHyoujiNo + 1
            Next
            '-------------------------------------------------------------------------------------
            'タイトル部
            '   製作一覧のOP情報(完成車／ＷＢ車でグループ化)を設定する。
            '-------------------------------------------------------------------------------------
            '' タイトル
            '   列順をカウント。
            Dim holdColumntCnt As Integer = columntCnt
            'If columntCnt > 0 Then columntCnt = columntCnt + 1
            '   列情報をセットする。
            OPList = Ichiran.GetTSeisakuIchiranOpkoumoku(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            For Each vo As TSeisakuIchiranOpkoumokuVo In OPList
                Dim i As Integer = 0
                '該当箇所の適用に値をセット
                For intColumn As Integer = 0 To holdColumntCnt

                    'Nothingとブランクは同義
                    Dim motoKaihatsuFugo As String = ""
                    Dim motoOpSpecCode As String = ""
                    Dim motoOpName As String = ""
                    Dim sakiKaihatsuFugo As String = ""
                    Dim sakiOpSpecCode As String = ""
                    Dim sakiOpName As String = ""
                    If StringUtil.IsNotEmpty(vo.KaihatsuFugo) Then
                        motoKaihatsuFugo = StrConv(Trim(vo.KaihatsuFugo), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(vo.OpSpecCode) Then
                        motoOpSpecCode = StrConv(Trim(vo.OpSpecCode), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(vo.OpName) Then
                        motoOpName = StrConv(Trim(vo.OpName), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                        sakiKaihatsuFugo = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                        sakiOpSpecCode = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                        sakiOpName = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                    End If

                    '該当仕様情報列の適用をセットする。
                    If StringUtil.Equals(motoKaihatsuFugo, sakiKaihatsuFugo) And _
                        StringUtil.Equals(motoOpSpecCode, sakiOpSpecCode) And _
                        StringUtil.Equals(motoOpName, sakiOpName) Then
                        i = 1
                        Exit For
                    End If
                Next
                '追加項目なら。
                If i = 0 Then
                    Dim record As New TShisakuEventSoubiNameVo
                    record.ShisakuEventCode = shisakuEventCode
                    record.HyojijunNo = TITLE_ROW_NO
                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                    record.ShisakuRetuKoumokuNameDai = vo.KaihatsuFugo
                    record.ShisakuRetuKoumokuNameChu = vo.OpSpecCode
                    record.ShisakuRetuKoumokuName = vo.OpName
                    record.ShisakuTekiyouDai = vo.KaihatsuFugo
                    record.ShisakuTekiyouChu = vo.OpSpecCode
                    record.ShisakuTekiyou = vo.OpName
                    record.ShisakuSoubiHyoujiNo = columntCnt
                    'タイトルの表示順（-1）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                    VoUtil.CopyProperties(record, Records(TITLE_ROW_NO, columntCnt))
                    '列順をカウント。
                    columntCnt = columntCnt + 1
                End If
            Next
            '-------------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------------
            '' データ部
            '   号車情報の取得
            Dim strSyubetu As String = ""
            Dim strDai, strVoDai As String
            Dim strChu, strVoChu As String
            Dim strSho, strVoSho As String
            '↓↓2014/10/29 酒井 ADD BEGIN
            'Ver6_2 1.95以降の修正内容の展開
            'For intRow As Integer = 0 To aBaseCar._record.Values.Count
            For Each intRow As Integer In aBaseCar._record.Keys
                '↑↑2014/10/29 酒井 ADD END
                'ブランク行は読み飛ばす。
                If StringUtil.IsEmpty(aBaseCar._record(intRow).ShisakuGousya) Then
                    Continue For
                End If

                Dim strGousya As String = ""
                strGousya = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                              aBaseCar._record(intRow).ShisakuGousya, "")


                ' ''OP項目情報の取得
                ''strGousya = aBaseTenkaiCar._record(intRow).ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                ''製作一覧から設定
                'Dim strGousya As String = ""
                'For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList
                '    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                '    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                '    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                '                                                       aBaseCar._record(intRow).ShisakuGousya)
                '    Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                '    '製作一覧の号車が試作イベントの号車を含むなら
                '    If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                '        strGousya = voSeisakuKansei.Gousya
                '        Exit For
                '    End If
                'Next

                If StringUtil.Equals(aBaseCar._record(intRow).ShisakuSyubetu, "W") Then
                    strSyubetu = "W"
                ElseIf StringUtil.Equals(aBaseCar._record(intRow).ShisakuSyubetu, "D") Then
                    strSyubetu = "D"
                Else
                    strSyubetu = "C"
                End If
                '削除データの場合には試作イベント情報からセット
                If StringUtil.Equals(strSyubetu, "D") Then
                    '' データ部
                    Dim param As New TShisakuEventSoubiVo
                    param.ShisakuEventCode = shisakuEventCode
                    param.ShisakuSoubiKbn = shisakuSoubiKbn
                    param.HyojijunNo = aBaseCar._record(intRow).HyojijunNo
                    Dim vos As List(Of TShisakuEventSoubiVo) = tSoubiDao.FindBy(param)
                    For Each vo As TShisakuEventSoubiVo In vos
                        ''タイトル情報列はスキップ
                        'If vo.HyojijunNo = TITLE_ROW_NO Then
                        '    Continue For
                        'End If
                        '
                        If vo.HyojijunNo = aBaseCar._record(intRow).HyojijunNo Then
                            '装備仕様項目名を取得する。
                            Dim voMShisakuSoubi As MShisakuSoubiVo = mSoubiDao.FindByPk(shisakuSoubiKbn, vo.ShisakuRetuKoumokuCode)

                            '該当箇所の適用に値をセット
                            For intColumn As Integer = 0 To columntCnt
                                'Nothingとブランクは同義
                                strDai = ""
                                strChu = ""
                                strSho = ""
                                strVoDai = ""
                                strVoChu = ""
                                strVoSho = ""
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                                    strDai = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                                    strChu = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                                    strSho = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                                End If
                                '
                                If StringUtil.IsNotEmpty(voMShisakuSoubi.ShisakuRetuKoumokuNameDai) Then
                                    strVoDai = StrConv(Trim(voMShisakuSoubi.ShisakuRetuKoumokuNameDai), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(voMShisakuSoubi.ShisakuRetuKoumokuNameChu) Then
                                    strVoChu = StrConv(Trim(voMShisakuSoubi.ShisakuRetuKoumokuNameChu), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(voMShisakuSoubi.ShisakuRetuKoumokuName) Then
                                    strVoSho = StrConv(Trim(voMShisakuSoubi.ShisakuRetuKoumokuName), VbStrConv.Narrow)
                                End If
                                '該当仕様情報列の適用をセットする。
                                If StringUtil.Equals(strDai, strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    'データセット
                                    Dim record As New TShisakuEventSoubiNameVo
                                    record.ShisakuEventCode = shisakuEventCode
                                    record.HyojijunNo = aBaseCar._record(intRow).HyojijunNo
                                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                                    record.ShisakuRetuKoumokuNameDai = strDai
                                    record.ShisakuRetuKoumokuNameChu = strChu
                                    record.ShisakuRetuKoumokuName = strSho
                                    record.ShisakuTekiyouDai = ""
                                    record.ShisakuTekiyouChu = ""
                                    record.ShisakuTekiyou = vo.ShisakuTekiyou
                                    record.ShisakuSoubiHyoujiNo = Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo
                                    '号車の表示順（Row）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                                    VoUtil.CopyProperties(record, Records(aBaseCar._record(intRow).HyojijunNo, _
                                                                          Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo))
                                End If
                            Next
                        End If
                    Next
                Else
                    OptionSeisakuIchiran = Ichiran.GetTSeisakuIchiranOpkoumokuGousya( _
                                                    seisakuHakouNo, seisakuHakouNoKaiteiNo, strSyubetu, strGousya)
                    If StringUtil.IsNotEmpty(OptionSeisakuIchiran) Then
                        For Each vo As TSeisakuIchiranOpkoumokuVo In OptionSeisakuIchiran
                            '該当箇所の適用に値をセット
                            For intColumn As Integer = 0 To columntCnt
                                'Nothingとブランクは同義
                                strDai = ""
                                strChu = ""
                                strSho = ""
                                strVoDai = ""
                                strVoChu = ""
                                strVoSho = ""
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                                    strDai = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                                    strChu = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                                    strSho = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(vo.KaihatsuFugo) Then
                                    strVoDai = StrConv(Trim(vo.KaihatsuFugo), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(vo.OpSpecCode) Then
                                    strVoChu = StrConv(Trim(vo.OpSpecCode), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(vo.OpName) Then
                                    strVoSho = StrConv(Trim(vo.OpName), VbStrConv.Narrow)
                                End If
                                '該当仕様情報列の適用をセットする。
                                If StringUtil.Equals(strDai, strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    'データセット
                                    Dim record As New TShisakuEventSoubiNameVo
                                    record.ShisakuEventCode = shisakuEventCode
                                    record.HyojijunNo = aBaseCar._record(intRow).HyojijunNo
                                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                                    record.ShisakuRetuKoumokuNameDai = strDai
                                    record.ShisakuRetuKoumokuNameChu = strChu
                                    record.ShisakuRetuKoumokuName = strSho
                                    record.ShisakuTekiyouDai = ""
                                    record.ShisakuTekiyouChu = ""
                                    record.ShisakuTekiyou = vo.Tekiyou
                                    record.ShisakuSoubiHyoujiNo = Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo
                                    '号車の表示順（Row）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                                    VoUtil.CopyProperties(record, Records(aBaseCar._record(intRow).HyojijunNo, _
                                                                          Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo))
                                End If
                            Next
                        Next
                    End If
                End If
            Next
            '-------------------------------------------------------------------------------------

        End Sub

        Private Sub ReadRecordsUpdateSpecialMae()

        End Sub

        Private Sub ReadRecordsUpdateSpecialAto()
            '初期設定
            Dim SpecialOptionSeisakuIchiran = New List(Of TSeisakuTokubetuOrikomiVo)
            Dim SpecialOptionSeisakuIchiranWB = New List(Of TSeisakuWbSoubiShiyouVo)
            'Dim OptionSeisakuIchiran = New List(Of TSeisakuIchiranOpkoumokuVo)
            Dim TOKUBETUList As New List(Of TSeisakuTokubetuOrikomiVo)
            Dim TOKUBETUListWB As New List(Of TSeisakuWbSoubiShiyouVo)
            'Dim OPList As New List(Of TSeisakuIchiranOpkoumokuVo)
            Dim Ichiran = New SeisakuIchiranDaoImpl
            Dim columntCnt As Integer = 0
            '   製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            '-------------------------------------------------------------------------------------
            'タイトル部
            '   試作イベントの仕様情報列を設定する。
            '-------------------------------------------------------------------------------------
            '' タイトル
            Dim titleVos As List(Of TShisakuEventSoubiNameVo) = specialDao.FindWithTitleNameBy(shisakuEventCode, shisakuSoubiKbn)
            For Each vo As TShisakuEventSoubiNameVo In titleVos
                If vo.HyojijunNo <> TITLE_ROW_NO Then
                    Continue For
                End If
                vo.ShisakuTekiyou = vo.ShisakuRetuKoumokuName
                vo.ShisakuTekiyouDai = vo.ShisakuRetuKoumokuNameDai
                vo.ShisakuTekiyouChu = vo.ShisakuRetuKoumokuNameChu

                VoUtil.CopyProperties(vo, Records(vo.HyojijunNo, vo.ShisakuSoubiHyoujiNo))
                '列順をカウント。（＋１）
                If columntCnt <= vo.ShisakuSoubiHyoujiNo + 1 Then columntCnt = vo.ShisakuSoubiHyoujiNo + 1
            Next
            '-------------------------------------------------------------------------------------
            'タイトル部・その２
            '   製作一覧の特別織込み(完成車でグループ化)を設定する。
            '-------------------------------------------------------------------------------------
            '' タイトル
            '   列順をカウント。
            Dim holdColumntCnt As Integer = columntCnt
            'If columntCnt > 0 Then columntCnt = columntCnt + 1
            '   列情報をセットする。
            TOKUBETUList = Ichiran.GetTSeisakuIchiranTokubetu(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            For Each vo As TSeisakuTokubetuOrikomiVo In TOKUBETUList
                Dim i As Integer = 0
                '該当箇所の適用に値をセット
                Dim motoDaiKbnName As String = ""
                Dim motoChuKbnName As String = ""
                Dim motoShoKbnName As String = ""
                If StringUtil.IsNotEmpty(vo.DaiKbnName) Then
                    motoDaiKbnName = StrConv(Trim(vo.DaiKbnName), VbStrConv.Narrow)
                End If
                If StringUtil.IsNotEmpty(vo.ChuKbnName) Then
                    motoChuKbnName = StrConv(Trim(vo.ChuKbnName), VbStrConv.Narrow)
                End If
                If StringUtil.IsNotEmpty(vo.ShoKbnName) Then
                    motoShoKbnName = StrConv(Trim(vo.ShoKbnName), VbStrConv.Narrow)
                End If
                For intColumn As Integer = 0 To holdColumntCnt

                    'Nothing、ブランクは同義
                    Dim sakiDaiKbnName As String = ""
                    Dim sakiChuKbnName As String = ""
                    Dim sakiShoKbnName As String = ""
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                        sakiDaiKbnName = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                        sakiChuKbnName = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                        sakiShoKbnName = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                    End If

                    '該当仕様情報列の適用をセットする。
                    If StringUtil.Equals(motoDaiKbnName, sakiDaiKbnName) And _
                        StringUtil.Equals(motoChuKbnName, sakiChuKbnName) And _
                        StringUtil.Equals(motoShoKbnName, sakiShoKbnName) Then
                        i = 1
                        Exit For
                    End If
                Next
                '追加項目なら。
                If i = 0 Then
                    Dim record As New TShisakuEventSoubiNameVo
                    record.ShisakuEventCode = shisakuEventCode
                    record.HyojijunNo = TITLE_ROW_NO
                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                    record.ShisakuRetuKoumokuNameDai = vo.DaiKbnName
                    record.ShisakuRetuKoumokuNameChu = vo.ChuKbnName
                    record.ShisakuRetuKoumokuName = vo.ShoKbnName
                    record.ShisakuTekiyouDai = vo.DaiKbnName
                    record.ShisakuTekiyouChu = vo.ChuKbnName
                    record.ShisakuTekiyou = vo.ShoKbnName
                    record.ShisakuSoubiHyoujiNo = columntCnt
                    'タイトルの表示順（-1）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                    VoUtil.CopyProperties(record, Records(TITLE_ROW_NO, columntCnt))
                    '列順をカウント。
                    columntCnt = columntCnt + 1
                End If
            Next
            '-------------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------------
            'タイトル部・その３
            '   製作一覧の特別織込情報(ＷＢ車でグループ化)を設定する。
            '-------------------------------------------------------------------------------------
            '' タイトル
            '   列順をカウント。
            Dim holdColumntCntWb As Integer = columntCnt
            'If columntCnt > 0 Then columntCnt = columntCnt + 1
            '   列情報をセットする。
            TOKUBETUListWB = Ichiran.GetTSeisakuIchiranTokubetuWB(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            For Each vo As TSeisakuWbSoubiShiyouVo In TOKUBETUListWB
                Dim i As Integer = 0
                '該当箇所の適用に値をセット
                Dim motoDaiKbnName As String = ""
                Dim motoChuKbnName As String = ""
                Dim motoShoKbnName As String = ""
                motoDaiKbnName = StrConv(Trim("W " & vo.DaiKbnName), VbStrConv.Narrow)
                If StringUtil.IsNotEmpty(vo.ChuKbnName) Then
                    motoChuKbnName = StrConv(Trim(vo.ChuKbnName), VbStrConv.Narrow)
                End If
                If StringUtil.IsNotEmpty(vo.ShoKbnName) Then
                    motoShoKbnName = StrConv(Trim(vo.ShoKbnName), VbStrConv.Narrow)
                End If

                For intColumn As Integer = 0 To holdColumntCntWb
                    'Nothingとブランクは同義
                    Dim sakiDaiKbnName As String = ""
                    Dim sakiChuKbnName As String = ""
                    Dim sakiShoKbnName As String = ""
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                        sakiDaiKbnName = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                        sakiChuKbnName = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                    End If
                    If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                        sakiShoKbnName = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                    End If

                    '該当仕様情報列の適用をセットする。（VOの大区分に"W " & を付けて比較する）
                    If StringUtil.Equals(motoDaiKbnName, sakiDaiKbnName) And _
                        StringUtil.Equals(motoChuKbnName, sakiChuKbnName) And _
                        StringUtil.Equals(motoShoKbnName, sakiShoKbnName) Then
                        i = 1
                        Exit For
                    End If
                Next
                '追加項目なら。
                If i = 0 Then
                    Dim record As New TShisakuEventSoubiNameVo
                    record.ShisakuEventCode = shisakuEventCode
                    record.HyojijunNo = TITLE_ROW_NO
                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                    record.ShisakuRetuKoumokuNameDai = "W " & vo.DaiKbnName
                    record.ShisakuRetuKoumokuNameChu = vo.ChuKbnName
                    record.ShisakuRetuKoumokuName = vo.ShoKbnName
                    record.ShisakuTekiyouDai = "W " & vo.DaiKbnName
                    record.ShisakuTekiyouChu = vo.ChuKbnName
                    record.ShisakuTekiyou = vo.ShoKbnName
                    record.ShisakuSoubiHyoujiNo = columntCnt
                    'タイトルの表示順（-1）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                    VoUtil.CopyProperties(record, Records(TITLE_ROW_NO, columntCnt))
                    '列順をカウント。
                    columntCnt = columntCnt + 1
                End If
            Next
            '-------------------------------------------------------------------------------------

            '-------------------------------------------------------------------------------------
            '' データ部
            '   号車情報の取得
            Dim strSyubetu As String = ""
            Dim strDai, strVoDai As String
            Dim strChu, strVoChu As String
            Dim strSho, strVoSho As String
            '↓↓2014/10/29 酒井 ADD BEGIN
            'Ver6_2 1.95以降の修正内容の展開
            'For intRow As Integer = 0 To aBaseCar._record.Values.Count
            For Each intRow As Integer In aBaseCar._record.Keys
                '↑↑2014/10/29 酒井 ADD END
                'ブランク行は読み飛ばす。
                If StringUtil.IsEmpty(aBaseCar._record(intRow).ShisakuGousya) Then
                    Continue For
                End If

                Dim strGousya As String = ""
                strGousya = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                              aBaseCar._record(intRow).ShisakuGousya, "")

                ' ''OP項目情報の取得
                ''strGousya = aBaseTenkaiCar._record(intRow).ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                ''製作一覧から設定
                'Dim strGousya As String = ""
                'For Each voSeisakuKansei As TSeisakuIchiranKanseiVo In kanseiList
                '    '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                '    '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                '    Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                '                                                       aBaseCar._record(intRow).ShisakuGousya)
                '    Dim strSeisakuGousya As String = voSeisakuKansei.Gousya.PadLeft(4, "0")
                '    '製作一覧の号車が試作イベントの号車を含むなら
                '    If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                '        strGousya = voSeisakuKansei.Gousya
                '        Exit For
                '    End If
                'Next

                If StringUtil.Equals(aBaseCar._record(intRow).ShisakuSyubetu, "W") Then
                    strSyubetu = "W"
                ElseIf StringUtil.Equals(aBaseCar._record(intRow).ShisakuSyubetu, "D") Then
                    strSyubetu = "D"
                Else
                    strSyubetu = "C"
                End If
                '削除データの場合には試作イベント情報からセット
                If StringUtil.Equals(strSyubetu, "D") Then
                    '' データ部
                    Dim param As New TShisakuEventSoubiVo
                    param.ShisakuEventCode = shisakuEventCode
                    param.ShisakuSoubiKbn = shisakuSoubiKbn
                    param.HyojijunNo = aBaseCar._record(intRow).HyojijunNo
                    Dim vos As List(Of TShisakuEventSoubiVo) = tSoubiDao.FindBy(param)
                    For Each vo As TShisakuEventSoubiVo In vos
                        '
                        If vo.HyojijunNo = aBaseCar._record(intRow).HyojijunNo Then
                            '装備仕様項目名を取得する。
                            Dim voMShisakuSoubi As MShisakuSoubiVo = mSoubiDao.FindByPk(shisakuSoubiKbn, vo.ShisakuRetuKoumokuCode)

                            strVoDai = ""
                            strVoChu = ""
                            strVoSho = ""
                            If StringUtil.IsNotEmpty(voMShisakuSoubi.ShisakuRetuKoumokuNameDai) Then
                                strVoDai = StrConv(Trim(voMShisakuSoubi.ShisakuRetuKoumokuNameDai), VbStrConv.Narrow)
                            End If
                            If StringUtil.IsNotEmpty(voMShisakuSoubi.ShisakuRetuKoumokuNameChu) Then
                                strVoChu = StrConv(Trim(voMShisakuSoubi.ShisakuRetuKoumokuNameChu), VbStrConv.Narrow)
                            End If
                            If StringUtil.IsNotEmpty(voMShisakuSoubi.ShisakuRetuKoumokuName) Then
                                strVoSho = StrConv(Trim(voMShisakuSoubi.ShisakuRetuKoumokuName), VbStrConv.Narrow)
                            End If

                            '該当箇所の適用に値をセット
                            For intColumn As Integer = 0 To columntCnt
                                'Nothingとブランクは同義
                                strDai = ""
                                strChu = ""
                                strSho = ""
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                                    strDai = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                                    strChu = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                                    strSho = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                                End If
                                '該当仕様情報列の適用をセットする。
                                If StringUtil.Equals(strDai, strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    'データセット
                                    Dim record As New TShisakuEventSoubiNameVo
                                    record.ShisakuEventCode = shisakuEventCode
                                    record.HyojijunNo = aBaseCar._record(intRow).HyojijunNo
                                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                                    record.ShisakuRetuKoumokuNameDai = strDai
                                    record.ShisakuRetuKoumokuNameChu = strChu
                                    record.ShisakuRetuKoumokuName = strSho
                                    record.ShisakuTekiyouDai = ""
                                    record.ShisakuTekiyouChu = ""
                                    record.ShisakuTekiyou = vo.ShisakuTekiyou
                                    record.ShisakuSoubiHyoujiNo = Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo
                                    '号車の表示順（Row）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                                    VoUtil.CopyProperties(record, Records(aBaseCar._record(intRow).HyojijunNo, _
                                                                          Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo))
                                End If
                            Next
                        End If
                    Next
                ElseIf StringUtil.Equals(strSyubetu, "C") Then
                    '--------------------------------------------------------------------------------------------------------------
                    '完成車情報の特別織込み
                    SpecialOptionSeisakuIchiran = Ichiran.GetTSeisakuIchiranTokubetuGousya( _
                                                    seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)
                    If StringUtil.IsNotEmpty(SpecialOptionSeisakuIchiran) Then
                        For Each vo As TSeisakuTokubetuOrikomiVo In SpecialOptionSeisakuIchiran
                            '該当箇所の適用に値をセット
                            strVoDai = ""
                            strVoChu = ""
                            strVoSho = ""
                            If StringUtil.IsNotEmpty(vo.DaiKbnName) Then
                                strVoDai = StrConv(Trim(vo.DaiKbnName), VbStrConv.Narrow)
                            End If
                            If StringUtil.IsNotEmpty(vo.ChuKbnName) Then
                                strVoChu = StrConv(Trim(vo.ChuKbnName), VbStrConv.Narrow)
                            End If
                            If StringUtil.IsNotEmpty(vo.ShoKbnName) Then
                                strVoSho = StrConv(Trim(vo.ShoKbnName), VbStrConv.Narrow)
                            End If
                            For intColumn As Integer = 0 To columntCnt
                                'Nothingとブランクは同義
                                strDai = ""
                                strChu = ""
                                strSho = ""
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                                    strDai = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                                    strChu = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                                    strSho = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                                End If
                                '該当仕様情報列の適用をセットする。
                                If StringUtil.Equals(strDai, strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    'データセット
                                    Dim record As New TShisakuEventSoubiNameVo
                                    record.ShisakuEventCode = shisakuEventCode
                                    record.HyojijunNo = aBaseCar._record(intRow).HyojijunNo
                                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                                    record.ShisakuRetuKoumokuNameDai = strDai
                                    record.ShisakuRetuKoumokuNameChu = strChu
                                    record.ShisakuRetuKoumokuName = strSho
                                    record.ShisakuTekiyouDai = ""
                                    record.ShisakuTekiyouChu = ""
                                    record.ShisakuTekiyou = vo.Tekiyou
                                    record.ShisakuSoubiHyoujiNo = Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo
                                    '号車の表示順（Row）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                                    VoUtil.CopyProperties(record, Records(aBaseCar._record(intRow).HyojijunNo, _
                                                                          Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo))
                                End If
                            Next
                        Next
                    End If
                ElseIf StringUtil.Equals(strSyubetu, "W") Then
                    '--------------------------------------------------------------------------------------------------------------
                    '--------------------------------------------------------------------------------------------------------------
                    'ＷＢ車情報の特別装備仕様
                    SpecialOptionSeisakuIchiranWB = Ichiran.GetTSeisakuIchiranTokubetuGousyaWB( _
                                                    seisakuHakouNo, seisakuHakouNoKaiteiNo, strGousya)
                    If StringUtil.IsNotEmpty(SpecialOptionSeisakuIchiranWB) Then
                        For Each vo As TSeisakuWbSoubiShiyouVo In SpecialOptionSeisakuIchiranWB
                            '該当箇所の適用に値をセット
                            strVoDai = ""
                            strVoChu = ""
                            strVoSho = ""
                            strVoDai = StrConv(Trim("W " & vo.DaiKbnName), VbStrConv.Narrow)     '"W"を付けて。
                            If StringUtil.IsNotEmpty(vo.ChuKbnName) Then
                                strVoChu = StrConv(Trim(vo.ChuKbnName), VbStrConv.Narrow)
                            End If
                            If StringUtil.IsNotEmpty(vo.ShoKbnName) Then
                                strVoSho = StrConv(Trim(vo.ShoKbnName), VbStrConv.Narrow)
                            End If
                            For intColumn As Integer = 0 To columntCnt
                                'Nothingとブランクは同義
                                strDai = ""
                                strChu = ""
                                strSho = ""
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai) Then
                                    strDai = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouDai), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu) Then
                                    strChu = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyouChu), VbStrConv.Narrow)
                                End If
                                If StringUtil.IsNotEmpty(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou) Then
                                    strSho = StrConv(Trim(Records(TITLE_ROW_NO, intColumn).ShisakuTekiyou), VbStrConv.Narrow)
                                End If
                                '該当仕様情報列の適用をセットする。
                                If StringUtil.Equals(strDai, strVoDai) And _
                                    StringUtil.Equals(strChu, strVoChu) And _
                                    StringUtil.Equals(strSho, strVoSho) Then
                                    'データセット
                                    Dim record As New TShisakuEventSoubiNameVo
                                    record.ShisakuEventCode = shisakuEventCode
                                    record.HyojijunNo = aBaseCar._record(intRow).HyojijunNo
                                    record.ShisakuSoubiKbn = shisakuSoubiKbn
                                    record.ShisakuRetuKoumokuNameDai = strDai
                                    record.ShisakuRetuKoumokuNameChu = strChu
                                    record.ShisakuRetuKoumokuName = strSho
                                    record.ShisakuTekiyouDai = ""
                                    record.ShisakuTekiyouChu = ""
                                    record.ShisakuTekiyou = vo.Tekiyou
                                    record.ShisakuSoubiHyoujiNo = Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo
                                    '号車の表示順（Row）と仕様情報の表示順（Column）で基本仕様情報を登録する。
                                    VoUtil.CopyProperties(record, Records(aBaseCar._record(intRow).HyojijunNo, _
                                                                          Records(TITLE_ROW_NO, intColumn).ShisakuSoubiHyoujiNo))
                                End If
                            Next
                        Next
                    End If
                    '--------------------------------------------------------------------------------------------------------------
                End If
            Next
            '-------------------------------------------------------------------------------------
        End Sub

#End Region

#Region "公開プロパティ"
        ''' 参照モードかを保持
        Private _isViewerMode As Boolean
        ''' <summary>参照モードか</summary>
        ''' <value>参照モードか</value>
        ''' <returns>参照モードか</returns>
        Public Property IsViewerMode() As Boolean
            Get
                Return _isViewerMode
            End Get
            Set(ByVal value As Boolean)
                If EzUtil.IsEqualIfNull(_isViewerMode, value) Then
                    Return
                End If
                _isViewerMode = value
                SetChanged()
            End Set
        End Property
        ' 設計展開以降か？
        Private _isSekkeiTenkaiIkou As Boolean
        ''' <summary>設計展開以降か？</summary>
        ''' <value>設計展開以降か？</value>
        ''' <returns>設計展開以降か？</returns>
        Public ReadOnly Property IsSekkeiTenkaiIkou() As Boolean
            Get
                Return _isSekkeiTenkaiIkou
            End Get
        End Property
#End Region

        Private Function IsAddMode() As Boolean
            Return StringUtil.IsEmpty(shisakuEventCode)
        End Function

        ''' <summary>
        ''' イベント情報コピー処理時の初期化など
        ''' </summary>
        ''' <param name="shisakuEventCode">元試作イベントコード</param>
        ''' <remarks></remarks>
        Friend Sub ProcessPostCopy(ByVal shisakuEventCode As String)
            Me.shisakuEventCode = shisakuEventCode
            ' 自身が登録ユーザーになるようにクリア
            For Each columnNo As Integer In GetInputTitleNameColumnNos()
                TitleNameRecords(columnNo).CreatedUserId = Nothing
                TitleNameRecords(columnNo).CreatedDate = Nothing
                TitleNameRecords(columnNo).CreatedTime = Nothing
            Next
            For Each rowNo As Integer In GetInputRowNos()
                For Each columnNo As Integer In GetInputColumnNos(rowNo)
                    Records(rowNo, columnNo).CreatedUserId = Nothing
                    Records(rowNo, columnNo).CreatedDate = Nothing
                    Records(rowNo, columnNo).CreatedTime = Nothing
                Next
            Next
        End Sub


        ''' <summary>
        ''' 編集モードかを返す
        ''' </summary>
        ''' <param name="rowNo">行No</param>
        ''' <returns>編集モードなら、true</returns>
        ''' <remarks></remarks>
        Public Function IsEditModes(ByVal rowNo As Integer) As Boolean
            Return Not IsViewerMode _
                AndAlso (Not StringUtil.IsEmpty(ShisakuSyubetu(rowNo)) OrElse Not StringUtil.IsEmpty(ShisakuGousya(rowNo)))
        End Function

        Public Sub Register(ByVal newShisakuEventCode As String)
            RegisterMain(newShisakuEventCode, True)
        End Sub
        Public Sub RegisterKaitei(ByVal newShisakuEventCode As String)
            RegisterMainKaitei(newShisakuEventCode, True)
        End Sub
        Public Sub Save(ByVal newShisakuEventCode As String)
            RegisterMain(newShisakuEventCode, False)
        End Sub

        'テキストチェック用
        Private Function CheckText(ByVal str As String) As Integer
            Static Encode_JIS As Encoding = Encoding.GetEncoding("Shift_JIS")
            Dim Str_Count As Integer = str.Length
            Dim ByteCount = Encode_JIS.GetByteCount(str)

            If Str_Count * 2 = ByteCount Then
                Return vbWide '4   
            ElseIf Str_Count = ByteCount Then
                Return vbNarrow '8   
            Else
                Return -1
            End If

        End Function


        Private Sub RegisterMain(ByVal newShisakuEventCode As String, ByVal IsRegister As Boolean)

            '-------------------------------------------------------------------------------------------------
            ' ２次改修分　変更箇所を登録する。
            '試作イベント完成車情報を取得
            Dim OptionList As List(Of TShisakuEventSoubiVo)
            Dim eventOptionDao As EventEditOptionRirekiDao = New EventEditOptionRirekiDaoImpl()
            OptionList = eventOptionDao.GetShisakuEventOptionList(newShisakuEventCode)

            Dim SoubiVo As TShisakuEventSoubiKaiteiVo

            '設計展開以降か？そうなら変更点を作成する。
            '   true:設計展開以降、false:設計展開前
            '   IsRegisterがTrue：登録ボタン押下、IsRegisterがFalse：保存ボタン押下
            If IsSekkeiTenkaiIkou() And StringUtil.Equals(IsRegister, True) Then

                ''試作イベントコードと表示順（rowNo）からBASEを取得し、無ければインサートしない
                Dim shisakuEventBaseDao2 As New TShisakuEventBaseSeisakuIchiranDaoImpl
                For Each rowNo As Integer In GetInputRowNos()
                    Dim eventBaseVo As TShisakuEventBaseSeisakuIchiranVo = shisakuEventBaseDao2.FindByPk(newShisakuEventCode, rowNo)
                    If eventBaseVo Is Nothing Then
                        Continue For
                    End If

                    'SPREADに入力されている列が対象となる
                    For Each columnNo As Integer In GetInputTitleNameColumnNos()
                        '下の判断だとダメ？
                        'For Each columnNo As Integer In GetInputColumnNos(rowNo)
                        ''タイトル列に入力されている列を対象とする。
                        'If Not StringUtil.IsEmpty(TitleNameRecords(columnNo).ShisakuRetuKoumokuCode) Then
                        '初期設定
                        Dim ShisakuRetuKoumokuCodeBefore As String = ""
                        Dim ShisakuRetuKoumokuCodeAfter As String = ""
                        Dim ShisakuTekiyouBefore As String = ""
                        Dim ShisakuTekiyouAfter As String = ""
                        Dim ShisakuTekiyou As String = ""
                        Dim i As Integer = 0
                        Dim strTekiyou As String = ""

                        'DB情報を取得する。
                        SoubiVo = eventOptionDao.FindShisakuEventOptionKaitei(newShisakuEventCode, rowNo, columnNo, shisakuSoubiKbn)

                        If StringUtil.IsEmpty(Records(rowNo, columnNo).ShisakuTekiyou) Then
                            strTekiyou = ""
                        Else
                            strTekiyou = StrConv(Records(rowNo, columnNo).ShisakuTekiyou, VbStrConv.Narrow) '半角へ
                        End If

                        '適用文字数チェック
                        '   適用のバイト数をチェックする。
                        If StringUtil.IsNotEmpty(strTekiyou) Then
                            Dim ret As Integer = CheckText(strTekiyou)
                            If ret = 4 Or ret = -1 Then
                                strTekiyou = Left(strTekiyou, 1)
                            End If
                        End If

                        '値があればDB情報と画面情報を比較する。
                        If StringUtil.IsNotEmpty(SoubiVo) Then
                            ''列項目コードでチェック
                            'If SoubiVo.ShisakuRetuKoumokuCode <> TitleNameRecords(columnNo).ShisakuRetuKoumokuCode Then
                            '    ShisakuRetuKoumokuCodeBefore = SoubiVo.ShisakuRetuKoumokuCode
                            '    ShisakuRetuKoumokuCodeAfter = TitleNameRecords(columnNo).ShisakuRetuKoumokuCode
                            '    i += 1
                            'End If
                            '適用
                            If SoubiVo.ShisakuTekiyou IsNot Nothing Then
                                ShisakuTekiyou = SoubiVo.ShisakuTekiyou.Trim
                            Else
                                ShisakuTekiyou = ""
                            End If
                            If ShisakuTekiyou.TrimEnd <> strTekiyou.TrimEnd Then
                                ShisakuTekiyouBefore = SoubiVo.ShisakuTekiyou.Trim
                                ShisakuTekiyouAfter = strTekiyou
                                i += 1
                            End If
                        Else
                            '画面にあってDBに無い場合は追加とみなして変更履歴情報を作成する。
                            ' Nothingの場合スルー
                            If strTekiyou IsNot Nothing Then
                                '   変更前にはブランクをセットする。
                                ShisakuTekiyouBefore = ""   'ブランクをセット
                                ShisakuTekiyouAfter = strTekiyou
                                i += 1
                            End If
                        End If

                        '変更情報を作成
                        If i > 0 Then
                            '前後のどちらかに値が有れば処理続行
                            If StringUtil.IsNotEmpty(ShisakuTekiyouBefore.TrimEnd) Or _
                                StringUtil.IsNotEmpty(ShisakuTekiyouAfter.TrimEnd) Then
                                eventOptionDao.InsertShisakuEventOption(newShisakuEventCode, rowNo, columnNo, shisakuSoubiKbn, _
                                                                                  ShisakuRetuKoumokuCodeBefore, _
                                                                                  ShisakuRetuKoumokuCodeAfter, _
                                                                                  ShisakuTekiyouBefore, _
                                                                                  ShisakuTekiyouAfter)
                            End If
                        End If

                        'End If

                    Next

                Next

            End If
            '-------------------------------------------------------------------------------------------------

            '' 既存データを削除

            If Not IsAddMode() Then
                Dim param As New TShisakuEventSoubiVo
                param.ShisakuEventCode = newShisakuEventCode
                param.ShisakuSoubiKbn = shisakuSoubiKbn
                tSoubiDao.DeleteBy(param)
            End If

            Dim numbering As New NumberingShisakuRetuKoumokuCode(shisakuSoubiKbn)
            For Each columnNo As Integer In GetInputTitleNameColumnNos()
                Dim param As New MShisakuSoubiVo
                param.ShisakuSoubiKbn = shisakuSoubiKbn
                If StringUtil.IsEmpty(TitleName(columnNo)) Then
                    param.ShisakuRetuKoumokuName = ""
                Else
                    param.ShisakuRetuKoumokuName = TitleName(columnNo)
                End If
                If StringUtil.IsEmpty(TitleNameDai(columnNo)) Then
                    param.ShisakuRetuKoumokuNameDai = ""
                Else
                    param.ShisakuRetuKoumokuNameDai = TitleNameDai(columnNo)
                End If
                If StringUtil.IsEmpty(TitleNameChu(columnNo)) Then
                    param.ShisakuRetuKoumokuNameChu = ""
                Else
                    param.ShisakuRetuKoumokuNameChu = TitleNameChu(columnNo)
                End If
                Dim mShisakuSoubiVos As List(Of MShisakuSoubiVo) = mSoubiDao.FindBy(param)

                '装備情報が正しく登録されない現象がでた。
                '以下の判断条件に問題があると思われる。
                'If IsNewTitleInTSoubi(columnNo) Then
                If Not StringUtil.IsEmpty(mShisakuSoubiVos) Then

                    If 0 < mShisakuSoubiVos.Count Then
                        '大項目、または中項目がNothingの場合複数ヒットするのでここで
                        '入力した値と同じかチェックする。
                        Dim strNameDai As String
                        Dim strNameChu As String
                        Dim strName As String
                        Dim strNameDai2 As String
                        Dim strNameChu2 As String
                        Dim strName2 As String
                        Dim lngCnt As Long = 0
                        '
                        If StringUtil.IsEmpty(TitleNameDai(columnNo)) Then
                            strNameDai2 = ""
                        Else
                            strNameDai2 = StrConv(TitleNameDai(columnNo), VbStrConv.Narrow)
                        End If
                        If StringUtil.IsEmpty(TitleNameChu(columnNo)) Then
                            strNameChu2 = ""
                        Else
                            strNameChu2 = StrConv(TitleNameChu(columnNo), VbStrConv.Narrow)
                        End If
                        If StringUtil.IsEmpty(TitleName(columnNo)) Then
                            strName2 = ""
                        Else
                            strName2 = StrConv(TitleName(columnNo), VbStrConv.Narrow)
                        End If
                        '同一項目値かチェック
                        For Each vo As MShisakuSoubiVo In mShisakuSoubiVos
                            If StringUtil.IsEmpty(vo.ShisakuRetuKoumokuNameDai) Then
                                strNameDai = ""
                            Else
                                strNameDai = StrConv(vo.ShisakuRetuKoumokuNameDai, VbStrConv.Narrow)
                            End If
                            If StringUtil.IsEmpty(vo.ShisakuRetuKoumokuNameChu) Then
                                strNameChu = ""
                            Else
                                strNameChu = StrConv(vo.ShisakuRetuKoumokuNameChu, VbStrConv.Narrow)
                            End If
                            If StringUtil.IsEmpty(vo.ShisakuRetuKoumokuName) Then
                                strName = ""
                            Else
                                strName = StrConv(vo.ShisakuRetuKoumokuName, VbStrConv.Narrow)
                            End If
                            If strNameDai = strNameDai2 And _
                                strNameChu = strNameChu2 And _
                                strName = strName Then
                                Exit For
                            End If
                            lngCnt = lngCnt + 1
                        Next
                        '列項目コードを取得
                        TitleNameRecords(columnNo).ShisakuRetuKoumokuCode = _
                                mShisakuSoubiVos(lngCnt).ShisakuRetuKoumokuCode

                        'デバッグコード 20140613
                        Console.WriteLine("A columnNo:" & columnNo & "/" & TitleNameRecords(columnNo).ShisakuRetuKoumokuCode)

                    Else
                        TitleNameRecords(columnNo).ShisakuRetuKoumokuCode = numbering.NextShisakuRetuKoumokuCode
                        InsertMShisakuSoubi(columnNo, aDate)

                        'デバッグコード 20140613
                        Console.WriteLine("B columnNo:" & columnNo & "/" & TitleNameRecords(columnNo).ShisakuRetuKoumokuCode)
                    End If
                Else
                    Dim mSoubiVo As MShisakuSoubiVo = mSoubiDao.FindByPk(shisakuSoubiKbn, TitleRetuKoumokuCode(columnNo))
                    If mSoubiVo IsNot Nothing Then
                        If Not TitleName(columnNo).Equals(mSoubiVo.ShisakuRetuKoumokuName) Then
                            mSoubiVo.ShisakuRetuKoumokuName = TitleName(columnNo)
                            mSoubiVo.ShisakuRetuKoumokuNameDai = TitleNameDai(columnNo)
                            mSoubiVo.ShisakuRetuKoumokuNameChu = TitleNameChu(columnNo)
                            mSoubiVo.UpdatedUserId = login.UserId
                            mSoubiVo.UpdatedDate = aDate.CurrentDateDbFormat
                            mSoubiVo.UpdatedTime = aDate.CurrentTimeDbFormat
                            mSoubiDao.UpdateByPk(mSoubiVo)
                        End If
                    Else
                        TitleNameRecords(columnNo).ShisakuRetuKoumokuCode = numbering.NextShisakuRetuKoumokuCode
                        InsertMShisakuSoubi(columnNo, aDate)
                    End If
                End If
            Next

            '2012/03/13 空白の列を登録しないようにする
            For Each columnNo As Integer In GetInputTitleNameColumnNos()
                If StringUtil.IsEmpty(TitleName(columnNo)) Then
                    DeleteColumn(columnNo)
                End If
            Next
            For Each columnNo As Integer In GetInputTitleNameColumnNos()
                InsertTSoubiTitle(newShisakuEventCode, aDate, columnNo)
            Next

            ''2012/02/21 号車がNothingの列はインサートしない
            ''試作イベントコードと表示順（rowNo）からBASEを取得し、無ければインサートしない
            Dim shisakuEventBaseDao As New TShisakuEventBaseSeisakuIchiranDaoImpl
            For Each rowNo As Integer In GetInputRowNos()
                Dim eventBaseVo As TShisakuEventBaseSeisakuIchiranVo = shisakuEventBaseDao.FindByPk(newShisakuEventCode, rowNo)
                If eventBaseVo Is Nothing Then
                    Continue For
                End If

                '-------------------------------------------------------------------
                '２次改修
                For Each columnNo As Integer In GetInputTitleNameColumnNos()
                    'タイトル行のカラムで見るべき？以下のFOR文はコメントにしておく
                    'For Each columnNo As Integer In GetInputColumnNos(rowNo)

                    If Not StringUtil.IsEmpty(TitleNameRecords(columnNo).ShisakuRetuKoumokuCode) Then
                        InsertTSoubiData(newShisakuEventCode, aDate, rowNo, columnNo)
                    End If
                Next
            Next

            shisakuEventCode = newShisakuEventCode
        End Sub
        Private Sub RegisterMainKaitei(ByVal newShisakuEventCode As String, ByVal IsRegister As Boolean)

            '-------------------------------------------------------------------------------------------------
            '' 既存データを削除
            If Not IsAddMode() Then
                Dim param As New TShisakuEventSoubiKaiteiVo
                param.ShisakuEventCode = newShisakuEventCode
                param.ShisakuSoubiKbn = shisakuSoubiKbn
                tSoubiKaiteiDao.DeleteBy(param)
            End If

            '2012/03/13 空白の列を登録しないようにする
            For Each columnNo As Integer In GetInputTitleNameColumnNos()
                If StringUtil.IsEmpty(TitleName(columnNo)) Then
                    DeleteColumn(columnNo)
                End If
            Next
            For Each columnNo As Integer In GetInputTitleNameColumnNos()
                InsertTSoubiTitleKaitei(newShisakuEventCode, aDate, columnNo)
            Next

            ''2012/02/21 号車がNothingの列はインサートしない
            ''試作イベントコードと表示順（rowNo）からBASEを取得し、無ければインサートしない
            Dim shisakuEventBasekaiteiDao As New TShisakuEventBaseKaiteiDaoImpl
            For Each rowNo As Integer In GetInputRowNos()
                Dim eventBaseKaiteiVo As TShisakuEventBaseKaiteiVo = shisakuEventBasekaiteiDao.FindByPk(newShisakuEventCode, rowNo)
                If eventBaseKaiteiVo Is Nothing Then
                    Continue For
                End If

                '-------------------------------------------------------------------
                '２次改修
                For Each columnNo As Integer In GetInputTitleNameColumnNos()
                    'タイトル行のカラムで見るべき？以下のFOR文はコメントにしておく
                    'For Each columnNo As Integer In GetInputColumnNos(rowNo)

                    If Not StringUtil.IsEmpty(TitleNameRecords(columnNo).ShisakuRetuKoumokuCode) Then
                        InsertTSoubiDataKaitei(newShisakuEventCode, aDate, rowNo, columnNo)
                    End If
                Next
            Next

            shisakuEventCode = newShisakuEventCode
        End Sub
        ''' <summary>
        ''' 列タイトルを試作装備マスタに登録する
        ''' </summary>
        ''' <param name="columnNo">列タイトルの列No</param>
        ''' <param name="aDate">更新日時クラス</param>
        ''' <remarks></remarks>
        Private Sub InsertMShisakuSoubi(ByVal columnNo As Integer, ByVal aDate As ShisakuDate)

            Dim soubi As New MShisakuSoubiVo
            soubi.ShisakuSoubiKbn = shisakuSoubiKbn
            soubi.ShisakuRetuKoumokuCode = TitleNameRecords(columnNo).ShisakuRetuKoumokuCode
            If StringUtil.IsEmpty(TitleName(columnNo)) Then
                soubi.ShisakuRetuKoumokuName = ""
            Else
                soubi.ShisakuRetuKoumokuName = TitleName(columnNo)
            End If
            If StringUtil.IsEmpty(TitleNameDai(columnNo)) Then
                soubi.ShisakuRetuKoumokuNameDai = ""
            Else
                soubi.ShisakuRetuKoumokuNameDai = TitleNameDai(columnNo)
            End If
            If StringUtil.IsEmpty(TitleNameChu(columnNo)) Then
                soubi.ShisakuRetuKoumokuNameChu = ""
            Else
                soubi.ShisakuRetuKoumokuNameChu = TitleNameChu(columnNo)
            End If
            soubi.CreatedUserId = login.UserId
            soubi.CreatedDate = aDate.CurrentDateDbFormat
            soubi.CreatedTime = aDate.CurrentTimeDbFormat
            soubi.UpdatedUserId = login.UserId
            soubi.UpdatedDate = aDate.CurrentDateDbFormat
            soubi.UpdatedTime = aDate.CurrentTimeDbFormat
            mSoubiDao.InsertBy(soubi)
        End Sub

        ''' <summary>
        ''' 試作イベント装備に列タイトル情報を登録する
        ''' </summary>
        ''' <param name="newShisakuEventCode">試作イベントコード</param>
        ''' <param name="aDate">更新日時クラス</param>
        ''' <param name="columnNo">登録する列タイトルの列No</param>
        ''' <remarks></remarks>
        Private Sub InsertTSoubiTitle(ByVal newShisakuEventCode As String, ByVal aDate As ShisakuDate, ByVal columnNo As Integer)
            InsertTSoubi(newShisakuEventCode, aDate, TITLE_ROW_NO, columnNo, True)
        End Sub

        ''' <summary>
        ''' 試作イベント装備に列タイトル情報を登録する
        ''' </summary>
        ''' <param name="newShisakuEventCode">試作イベントコード</param>
        ''' <param name="aDate">更新日時クラス</param>
        ''' <param name="columnNo">登録する列タイトルの列No</param>
        ''' <remarks></remarks>
        Private Sub InsertTSoubiTitleKaitei(ByVal newShisakuEventCode As String, ByVal aDate As ShisakuDate, ByVal columnNo As Integer)
            InsertTSoubiKaitei(newShisakuEventCode, aDate, TITLE_ROW_NO, columnNo, True)
        End Sub
        ''' <summary>
        ''' 試作イベント装備に登録する
        ''' </summary>
        ''' <param name="newShisakuEventCode">試作イベントコード</param>
        ''' <param name="aDate">更新日時クラス</param>
        ''' <param name="rowNo">登録するデータの行No</param>
        ''' <param name="columnNo">登録するデータの列No</param>
        ''' <remarks></remarks>
        Private Sub InsertTSoubiData(ByVal newShisakuEventCode As String, ByVal aDate As ShisakuDate, ByVal rowNo As Integer, ByVal columnNo As Integer)
            InsertTSoubi(newShisakuEventCode, aDate, rowNo, columnNo, False)
        End Sub
        ''' <summary>
        ''' 試作イベント装備に登録する
        ''' </summary>
        ''' <param name="newShisakuEventCode">試作イベントコード</param>
        ''' <param name="aDate">更新日時クラス</param>
        ''' <param name="rowNo">登録するデータの行No</param>
        ''' <param name="columnNo">登録するデータの列No</param>
        ''' <remarks></remarks>
        Private Sub InsertTSoubiDataKaitei(ByVal newShisakuEventCode As String, ByVal aDate As ShisakuDate, ByVal rowNo As Integer, ByVal columnNo As Integer)
            InsertTSoubiKaitei(newShisakuEventCode, aDate, rowNo, columnNo, False)
        End Sub
        ''' <summary>
        ''' 試作イベント装備に登録する
        ''' </summary>
        ''' <param name="newShisakuEventCode">試作イベントコード</param>
        ''' <param name="aDate">更新日時クラス</param>
        ''' <param name="rowNo">登録するデータの行No</param>
        ''' <param name="columnNo">登録するデータの列No</param>
        ''' <param name="IsTekiyoNothing">適用を登録しない場合、true</param>
        ''' <remarks></remarks>
        Private Sub InsertTSoubi(ByVal newShisakuEventCode As String, ByVal aDate As ShisakuDate, ByVal rowNo As Integer, ByVal columnNo As Integer, ByVal IsTekiyoNothing As Boolean)

            Dim soubi As New TShisakuEventSoubiVo
            soubi.ShisakuEventCode = newShisakuEventCode
            soubi.HyojijunNo = rowNo
            soubi.ShisakuSoubiHyoujiNo = columnNo
            soubi.ShisakuSoubiKbn = shisakuSoubiKbn
            soubi.ShisakuRetuKoumokuCode = TitleNameRecords(columnNo).ShisakuRetuKoumokuCode
            soubi.ShisakuTekiyou = IIf(IsTekiyoNothing, Nothing, Records(rowNo, columnNo).ShisakuTekiyou)   '

            soubi.ShisakuTekiyou = StrConv(soubi.ShisakuTekiyou, VbStrConv.Narrow)
            '適用文字数チェック
            '   適用のバイト数をチェックする。
            If StringUtil.IsNotEmpty(soubi.ShisakuTekiyou) Then
                Dim ret As Integer = CheckText(soubi.ShisakuTekiyou)
                If ret = 4 Or ret = -1 Then
                    soubi.ShisakuTekiyou = Left(soubi.ShisakuTekiyou, 1)
                End If
            End If



            soubi.ShisakuTekiyouDai = IIf(IsTekiyoNothing, Nothing, Records(rowNo, columnNo).ShisakuTekiyouDai) '大区分
            soubi.ShisakuTekiyouChu = IIf(IsTekiyoNothing, Nothing, Records(rowNo, columnNo).ShisakuTekiyouChu) '中区分
            If StringUtil.IsEmpty(Records(rowNo, columnNo).CreatedUserId) Then
                soubi.CreatedUserId = login.UserId
                soubi.CreatedDate = aDate.CurrentDateDbFormat
                soubi.CreatedTime = aDate.CurrentTimeDbFormat
            Else
                soubi.CreatedUserId = Records(rowNo, columnNo).CreatedUserId
                soubi.CreatedDate = Records(rowNo, columnNo).CreatedDate
                soubi.CreatedTime = Records(rowNo, columnNo).CreatedTime
            End If
            soubi.UpdatedUserId = login.UserId
            soubi.UpdatedDate = aDate.CurrentDateDbFormat
            soubi.UpdatedTime = aDate.CurrentTimeDbFormat
            tSoubiDao.InsertBy(soubi)
        End Sub
        ''' <summary>
        ''' 試作イベント装備に登録する
        ''' </summary>
        ''' <param name="newShisakuEventCode">試作イベントコード</param>
        ''' <param name="aDate">更新日時クラス</param>
        ''' <param name="rowNo">登録するデータの行No</param>
        ''' <param name="columnNo">登録するデータの列No</param>
        ''' <param name="IsTekiyoNothing">適用を登録しない場合、true</param>
        ''' <remarks></remarks>
        Private Sub InsertTSoubiKaitei(ByVal newShisakuEventCode As String, ByVal aDate As ShisakuDate, ByVal rowNo As Integer, ByVal columnNo As Integer, ByVal IsTekiyoNothing As Boolean)

            Dim soubi As New TShisakuEventSoubiKaiteiVo
            soubi.ShisakuEventCode = newShisakuEventCode
            soubi.HyojijunNo = rowNo
            soubi.ShisakuSoubiHyoujiNo = columnNo
            soubi.ShisakuSoubiKbn = shisakuSoubiKbn
            soubi.ShisakuRetuKoumokuCode = TitleNameRecords(columnNo).ShisakuRetuKoumokuCode
            soubi.ShisakuTekiyou = IIf(IsTekiyoNothing, Nothing, Records(rowNo, columnNo).ShisakuTekiyou)   '


            soubi.ShisakuTekiyou = StrConv(soubi.ShisakuTekiyou, VbStrConv.Narrow)
            '適用文字数チェック
            '   適用のバイト数をチェックする。
            If StringUtil.IsNotEmpty(soubi.ShisakuTekiyou) Then
                Dim ret As Integer = CheckText(soubi.ShisakuTekiyou)
                If ret = 4 Or ret = -1 Then
                    soubi.ShisakuTekiyou = Left(soubi.ShisakuTekiyou, 1)
                End If
            End If



            soubi.ShisakuTekiyouDai = IIf(IsTekiyoNothing, Nothing, Records(rowNo, columnNo).ShisakuTekiyouDai) '大区分
            soubi.ShisakuTekiyouChu = IIf(IsTekiyoNothing, Nothing, Records(rowNo, columnNo).ShisakuTekiyouChu) '中区分
            If StringUtil.IsEmpty(Records(rowNo, columnNo).CreatedUserId) Then
                soubi.CreatedUserId = login.UserId
                soubi.CreatedDate = aDate.CurrentDateDbFormat
                soubi.CreatedTime = aDate.CurrentTimeDbFormat
            Else
                soubi.CreatedUserId = Records(rowNo, columnNo).CreatedUserId
                soubi.CreatedDate = Records(rowNo, columnNo).CreatedDate
                soubi.CreatedTime = Records(rowNo, columnNo).CreatedTime
            End If
            soubi.UpdatedUserId = login.UserId
            soubi.UpdatedDate = aDate.CurrentDateDbFormat
            soubi.UpdatedTime = aDate.CurrentTimeDbFormat
            tSoubiKaiteiDao.InsertBy(soubi)
        End Sub

        ''' <summary>
        ''' 試作イベント装備仕様テーブルに初めて登録するタイトルかを返す
        ''' </summary>
        ''' <param name="columnNo">列No</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Private Function IsNewTitleInTSoubi(ByVal columnNo As Integer) As Boolean

            Return StringUtil.IsEmpty(TitleNameRecords(columnNo).CreatedUserId)
        End Function






        Private _columnCount As Integer
        ''' <summary>メモ列の列数</summary>
        Public Property ColumnCount() As Integer
            Get
                Return _columnCount
            End Get
            Set(ByVal value As Integer)
                _columnCount = value
            End Set
        End Property

        ''' <summary>
        ''' 入力行の行Noの一覧を返す（タイトル行含む） ※順不同
        ''' </summary>
        ''' <returns>入力行の行Noの一覧</returns>
        ''' <remarks></remarks>
        Private Function GetInputRowIndexesWithTitle() As ICollection(Of Integer)
            Return _recordDimension.Keys
        End Function

        ''' <summary>
        ''' 列挿入する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumn(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            For Each rowIndex As Integer In GetInputRowIndexesWithTitle()
                Me._recordDimension(rowIndex).Insert(columnIndex, insertCount)
            Next
            _columnCount += insertCount
        End Sub

        ''' <summary>
        ''' 列削除する
        ''' </summary>
        ''' <param name="columnIndex">列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumn(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            For Each rowIndex As Integer In GetInputRowIndexesWithTitle()
                Me._recordDimension(rowIndex).Remove(columnIndex, removeCount)
            Next
            _columnCount -= removeCount
        End Sub

        ''' <summary>
        ''' メモ欄に列挿入する
        ''' </summary>
        ''' <param name="columnIndex">メモ欄の中の列index</param>
        ''' <param name="insertCount">挿入列数</param>
        ''' <remarks></remarks>
        Public Sub InsertColumnInMemo(ByVal columnIndex As Integer, ByVal insertCount As Integer)
            InsertColumn(columnIndex, insertCount)
            SetChanged()
        End Sub

        ''' <summary>
        ''' メモ欄を列削除する
        ''' </summary>
        ''' <param name="columnIndex">メモ欄の中の列index</param>
        ''' <param name="removeCount">削除列数</param>
        ''' <remarks></remarks>
        Public Sub RemoveColumnInMemo(ByVal columnIndex As Integer, ByVal removeCount As Integer)
            RemoveColumn(columnIndex, removeCount)
            SetChanged()
        End Sub



    End Class
End Namespace