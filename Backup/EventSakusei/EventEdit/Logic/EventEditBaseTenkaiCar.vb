Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.ShisakuComFunc

Namespace EventEdit.Logic
    Public Class EventEditBaseTenkaiCar : Inherits Observable

        Private ReadOnly shisakuEventBaseTenkaiDao As TShisakuEventBaseDao    '設計展開用
        Private ReadOnly aDate As ShisakuDate

        Private shisakuEventCode As String
        Private ReadOnly login As LoginInfo
        Private ReadOnly baseCarDao As EventEditBaseCarDao
        Private ReadOnly aBaseCar As EventEditBaseCar
        Private ReadOnly aEzSync As EzSyncShubetsuGosha
        Private ReadOnly aEzSyncBaseTenkai As EzSyncBaseTenkai

        'ベース車情報
        Private seisakuHakouNo As String
        Private seisakuHakouNoKaiteiNo As String
        Private baseList As New List(Of TSeisakuIchiranBaseVo)
        Private wbList As New List(Of TSeisakuIchiranWbVo)

        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal aEzSync As EzSyncShubetsuGosha, _
                       ByVal aEzSyncBaseTenkai As EzSyncBaseTenkai, _
                       ByVal baseCarDao As EventEditBaseCarDao, _
                       ByVal shisakuEventBaseTenkaiDao As TShisakuEventBaseDao, _
                       ByVal aBaseCar As EventEditBaseCar, _
                       ByVal aDate As ShisakuDate, _
                       ByVal seisakuHakouNo As String, _
                       ByVal seisakuHakouNoKaiteiNo As String, _
                       ByVal isSekkeiTenkaiIkou As Boolean)
            Me.shisakuEventCode = shisakuEventCode
            Me.login = login
            Me.baseCarDao = baseCarDao
            Me.aBaseCar = aBaseCar
            Me.shisakuEventBaseTenkaiDao = shisakuEventBaseTenkaiDao
            Me.aEzSync = aEzSync
            Me.aEzSyncBaseTenkai = aEzSyncBaseTenkai
            Me.aDate = aDate
            Me._isSekkeiTenkaiIkou = isSekkeiTenkaiIkou

            Me.seisakuHakouNo = seisakuHakouNo
            Me.seisakuHakouNoKaiteiNo = seisakuHakouNoKaiteiNo

            '製作一覧キー情報があれば製作一覧情報からセットする。
            If StringUtil.IsNotEmpty(seisakuHakouNo) Then
                '編集モードかつ設計展開以降なら
                If Not IsAddMode() And isSekkeiTenkaiIkou Then
                    ReadRecords()
                    'ReadRecordsUpdate()
                Else
                    ReadRecordsInitial()
                End If
            Else
                If Not IsAddMode() Then
                    ReadRecords()
                End If
            End If

            SetChanged()
        End Sub

#Region "ベース車情報のDelegateプロパティ"
        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuSyubetu = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGousya
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuGousya = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車開発符号</summary>
        ''' <value>ベース車開発符号</value>
        ''' <returns>ベース車開発符号</returns>
        Public Property BaseKaihatsuFugo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseKaihatsuFugo
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseKaihatsuFugo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車仕様情報№</summary>
        ''' <value>ベース車仕様情報№</value>
        ''' <returns>ベース車仕様情報№</returns>
        Public Property BaseShiyoujyouhouNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseShiyoujyouhouNo
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseShiyoujyouhouNo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車アプライド№</summary>
        ''' <value>ベース車アプライド№</value>
        ''' <returns>ベース車アプライド№</returns>
        Public Property BaseAppliedNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseAppliedNo
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseAppliedNo = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車型式</summary>
        ''' <value>ベース車型式</value>
        ''' <returns>ベース車型式</returns>
        Public Property BaseKatashiki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseKatashiki
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseKatashiki = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車仕向</summary>
        ''' <value>ベース車仕向</value>
        ''' <returns>ベース車仕向</returns>
        Public Property BaseShimuke(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseShimuke
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseShimuke = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車OP</summary>
        ''' <value>ベース車OP</value>
        ''' <returns>ベース車OP</returns>
        Public Property BaseOp(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseOp
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseOp = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車外装色</summary>
        ''' <value>ベース車外装色</value>
        ''' <returns>ベース車外装色</returns>
        Public Property BaseGaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseGaisousyoku
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseGaisousyoku = value
                SetChanged()
            End Set
        End Property

        ''' <summary>ベース車内装色</summary>
        ''' <value>ベース車内装色</value>
        ''' <returns>ベース車内装色</returns>
        Public Property BaseNaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).BaseNaisousyoku
            End Get
            Set(ByVal value As String)
                Records(rowNo).BaseNaisousyoku = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作ベースイベントコード</summary>
        ''' <value>試作ベースイベントコード</value>
        ''' <returns>試作ベースイベントコード</returns>
        Public Property ShisakuBaseEventCode(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuBaseEventCode
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuBaseEventCode = value
                SetChanged()
            End Set
        End Property

        ''' <summary>試作ベース号車</summary>
        ''' <value>試作ベース号車</value>
        ''' <returns>試作ベース号車</returns>
        Public Property ShisakuBaseGousya(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuBaseGousya
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuBaseGousya = value
                SetChanged()
            End Set
        End Property

#End Region

#Region "行情報取得・操作"
        Private _record As New IndexedList(Of TShisakuEventBaseVo)

        ''' <summary>ベース車情報</summary>
        ''' <returns>ベース車情報</returns>
        Private ReadOnly Property Records(ByVal rowNo As Integer) As TShisakuEventBaseVo
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

        ''' <summary>
        ''' 行を挿入する
        ''' </summary>
        ''' <param name="rowNo">挿入先の行No</param>
        ''' <remarks></remarks>
        Public Sub InsertRow(ByVal rowNo As Integer)
            _record.Insert(rowNo)
        End Sub

        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="rowNo">削除する行No</param>
        ''' <remarks></remarks>
        Public Sub DeleteRow(ByVal rowNo As Integer)
            _record.Remove(rowNo)
        End Sub

        Private Sub ReadRecords()
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            Dim vos As List(Of TShisakuEventBaseVo) = shisakuEventBaseTenkaiDao.FindBy(param)
            For Each vo As TShisakuEventBaseVo In vos
                Dim rowNo As Integer = Convert.ToInt32(vo.HyojijunNo) '' 表示順は NotNull項目
                Dim record As New TShisakuEventBaseVo
                VoUtil.CopyProperties(vo, record)
                If record.SekkeiTenkaiKbn IsNot Nothing Then
                    record.SekkeiTenkaiKbn = Convert.ToString(TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_GO.Equals(record.SekkeiTenkaiKbn))
                End If
                _record.Add(rowNo, record)
            Next
        End Sub

        ''' <summary>
        ''' 行の読み込み
        '''　イニシャル
        ''' 　　（号車のみセットする）
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecordsInitial()

            '初期設定
            Dim HoldRowNo As Integer = 0
            Dim Ichiran = New SeisakuIchiranDaoImpl
            'ベース車情報
            baseList = Ichiran.GetTSeisakuIchiranBase(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            '製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            For Each vo As TSeisakuIchiranBaseVo In baseList
                Dim rowNo As Integer = Convert.ToInt32(vo.HyojijunNo) - 1 '' 表示順は NotNull項目　製作一覧は1番からなのでマイナス１
                Dim record As New TShisakuEventBaseVo
                '号車のみ
                '   開発符号＋号車
                record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & vo.Gousya
                '   号車が７桁未満なら開発符号と製作一覧号車の間に「#」を付ける。
                If record.ShisakuGousya.Length <= 7 Then
                    record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & "#" & vo.Gousya
                End If

                record.SekkeiTenkaiKbn = Nothing

                _record.Add(rowNo, record)

                HoldRowNo = HoldRowNo + 1
            Next

            'WB車情報
            If HoldRowNo <> 0 Then
                HoldRowNo = HoldRowNo + 1
            End If
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)

            For Each vo As TSeisakuIchiranWbVo In wbList
                Dim rowNo As Integer = HoldRowNo + Convert.ToInt32(vo.HyojijunNo) - 1 '' 表示順は NotNull項目　製作一覧は1番からなのでマイナス１
                Dim record As New TShisakuEventBaseVo
                '号車のみ
                '   開発符号＋号車
                record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & vo.Gousya
                '   号車が７桁未満なら開発符号と製作一覧号車の間に「W」を付ける。

                '   2014/02/14 製作一覧の号車の１桁目にWがついている場合がある。
                '       その時はWを付けない。
                If record.ShisakuGousya.Length <= 7 Then
                    If vo.Gousya.IndexOf("W") = 0 Then
                        record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & vo.Gousya
                    Else
                        record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & "W" & vo.Gousya
                    End If
                End If
                'If record.ShisakuGousya.Length <= 7 Then
                '    record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & "W" & vo.Gousya
                'End If

                record.ShisakuSyubetu = "W" 'ホワイトボディ
                record.SekkeiTenkaiKbn = Nothing

                _record.Add(rowNo, record)

            Next
        End Sub

        ''' <summary>
        ''' 行の読み込み
        '''　製作一覧更新
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ReadRecordsUpdate()

            '初期設定
            Dim Ichiran = New SeisakuIchiranDaoImpl
            Dim HoldRowNo As Integer = 0
            'Dim strGousya As String
            '製作一覧HD情報
            Dim getSeisakuIchiranHd As New SeisakuIchiranDaoImpl
            Dim tSeisakuHakouHdVo As New TSeisakuHakouHdVo
            tSeisakuHakouHdVo = getSeisakuIchiranHd.GetSeisakuIchiranHd(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            'ベース車情報
            baseList = Ichiran.GetTSeisakuIchiranBase(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            'ＷＢ車情報
            wbList = Ichiran.GetTSeisakuIchiranWb(seisakuHakouNo, seisakuHakouNoKaiteiNo)
            Dim param As New TShisakuEventBaseVo
            param.ShisakuEventCode = shisakuEventCode
            Dim vos As List(Of TShisakuEventBaseVo) = shisakuEventBaseTenkaiDao.FindBy(param)

            '追加及び削除データ処理
            For Each vo As TShisakuEventBaseVo In vos '試作イベントのVO
                Dim record As New TShisakuEventBaseVo
                record.SekkeiTenkaiKbn = Nothing

                '   設計展開前ならベース情報を更新する。
                If StringUtil.Equals(IsSekkeiTenkaiIkou, False) Then

                    If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then    'ホワイトボディ、削除は除く
                        '製作一覧から設定
                        For Each voSeisakuBase As TSeisakuIchiranBaseVo In baseList '製作一覧完成車シートのベース車情報
                            '' 開発符号をブランクに置き換える
                            'strGousya = vo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               vo.ShisakuGousya)

                            '2014/02/18
                            'Dim strSeisakuGousya As String = voSeisakuBase.Gousya.PadLeft(4, "0")
                            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               voSeisakuBase.Gousya)

                            '製作一覧の号車が試作イベントの号車を含むなら
                            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                                ''号車が同じなら
                                'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                                '表示順取得
                                If vo.HyojijunNo > HoldRowNo Then HoldRowNo = vo.HyojijunNo

                                '製作一覧から値をセット
                                record.ShisakuSyubetu = "" '完成車
                                record.ShisakuGousya = voSeisakuBase.KaihatsuFugo & voSeisakuBase.Gousya
                                record.BaseKaihatsuFugo = voSeisakuBase.KaihatsuFugo
                                record.BaseShiyoujyouhouNo = voSeisakuBase.ShiyoujyouhouNo
                                'アプライド№を７ケタ型式から取得
                                Dim getSobiKaitei As New EventEditBaseCarDaoImpl
                                Dim GetValue = getSobiKaitei.FindSobiKaitei(voSeisakuBase.KaihatsuFugo, _
                                                                            voSeisakuBase.ShiyoujyouhouNo, _
                                                                            voSeisakuBase.KatashikiScd7)
                                '存在しない場合にはAP№へブランクをセット
                                If StringUtil.IsNotEmpty(GetValue) Then
                                    record.BaseAppliedNo = GetValue.AppliedNo
                                Else
                                    record.BaseAppliedNo = ""
                                End If
                                record.BaseKatashiki = voSeisakuBase.KatashikiScd7
                                record.BaseShimuke = voSeisakuBase.KatashikiShimuke
                                record.BaseOp = voSeisakuBase.KatashikiOp.Replace(" ", "") 'ブランクを排除
                                record.BaseGaisousyoku = voSeisakuBase.Gaisousyoku
                                record.BaseNaisousyoku = voSeisakuBase.Naisousyoku
                                record.ShisakuBaseEventCode = voSeisakuBase.ShisakuEvent
                                record.ShisakuBaseGousya = ""

                                record.HyojijunNo = vo.HyojijunNo   '表示順№

                                _record.Add(vo.HyojijunNo, record)

                                Exit For
                            End If
                        Next
                    ElseIf StringUtil.Equals(vo.ShisakuSyubetu, "W") Then    'ホワイトボディのみ

                        record.SekkeiTenkaiKbn = Nothing
                        '製作一覧から設定
                        For Each voSeisakuBase As TSeisakuIchiranWbVo In wbList

                            '' 開発符号をブランクに置き換える
                            'strGousya = vo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               vo.ShisakuGousya)

                            '2014/02/18
                            'Dim strSeisakuGousya As String = voSeisakuBase.Gousya.PadLeft(4, "0")
                            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               voSeisakuBase.Gousya)

                            '製作一覧の号車が試作イベントの号車を含むなら
                            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                                ''号車が同じなら
                                'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                                '表示順取得
                                If vo.HyojijunNo > HoldRowNo Then HoldRowNo = vo.HyojijunNo
                                '製作一覧から値をセット
                                record.ShisakuSyubetu = "W" 'ホワイトボディー
                                record.ShisakuGousya = voSeisakuBase.KaihatsuFugo & voSeisakuBase.Gousya
                                record.BaseKaihatsuFugo = voSeisakuBase.KaihatsuFugo
                                record.BaseShiyoujyouhouNo = voSeisakuBase.ShiyoujyouhouNo
                                'アプライド№を７ケタ型式から取得
                                Dim getSobiKaitei As New EventEditBaseCarDaoImpl
                                Dim GetValue = getSobiKaitei.FindSobiKaitei(voSeisakuBase.KaihatsuFugo, _
                                                                            voSeisakuBase.ShiyoujyouhouNo, _
                                                                            voSeisakuBase.KatashikiScd7)
                                '存在しない場合にはAP№へブランクをセット
                                If StringUtil.IsNotEmpty(GetValue) Then
                                    record.BaseAppliedNo = GetValue.AppliedNo
                                Else
                                    record.BaseAppliedNo = ""
                                End If
                                record.BaseKatashiki = voSeisakuBase.KatashikiScd7
                                record.BaseShimuke = voSeisakuBase.KatashikiShimuke
                                record.BaseOp = voSeisakuBase.KatashikiOp.Replace(" ", "") 'ブランクを排除
                                record.BaseGaisousyoku = voSeisakuBase.Gaisousyoku
                                record.BaseNaisousyoku = voSeisakuBase.Naisousyoku
                                record.ShisakuBaseEventCode = ""
                                record.ShisakuBaseGousya = ""

                                record.HyojijunNo = vo.HyojijunNo   '表示順

                                _record.Add(vo.HyojijunNo, record)

                                Exit For
                            End If
                        Next
                    End If
                    'Else
                    '    Dim i As Long = 0
                    '    '' 開発符号をブランクに置き換える
                    '    'strGousya = vo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                    '    If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then    'ホワイトボディ、削除は除く
                    '        For Each voSeisakuBase As TSeisakuIchiranBaseVo In baseList '製作一覧完成車シートのベース車情報
                    '            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    '            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                    '                                                               vo.ShisakuGousya)
                    '            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                    '                                                               voSeisakuBase.Gousya)
                    '            '製作一覧の号車が試作イベントの号車を含むなら
                    '            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                    '                ''号車が同じなら
                    '                'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                    '                i = i + 1
                    '                Exit For
                    '            End If
                    '        Next
                    '    End If
                    '    If StringUtil.Equals(vo.ShisakuSyubetu, "W") Then    'ホワイトボディ、削除は除く
                    '        For Each voSeisakuBase As TSeisakuIchiranWbVo In wbList '製作一覧ＷＢ車シートのベース車情報
                    '            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                    '            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                    '            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                    '                                                               vo.ShisakuGousya)
                    '            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                    '                                                               voSeisakuBase.Gousya)
                    '            '製作一覧の号車が試作イベントの号車を含むなら
                    '            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                    '                ''号車が同じなら
                    '                'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                    '                i = i + 1
                    '                Exit For
                    '            End If
                    '        Next
                    '    End If
                    '    'データセット
                    '    VoUtil.CopyProperties(vo, record)
                    '    '１件も無ければ削除コードをセット
                    '    If i = 0 Then
                    '        record.ShisakuSyubetu = "D" '削除=D
                    '    End If
                    '    ''If record.SekkeiTenkaiKbn IsNot Nothing Then
                    '    'record.SekkeiTenkaiKbn = Convert.ToString(TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_GO.Equals(record.SekkeiTenkaiKbn))
                    '    ''End If
                    '    _record.Add(vo.HyojijunNo, record)
                End If

                '表示順取得
                If vo.HyojijunNo > HoldRowNo Then HoldRowNo = vo.HyojijunNo

            Next

            '設計展開以降か？
            '   設計展開前ならベース情報を追加（号車追加）する。
            If StringUtil.Equals(IsSekkeiTenkaiIkou, False) Then

                '削除号車の処理
                '   完成車及びＷＢ車情報
                '製作一覧から設定
                For Each vo As TShisakuEventBaseVo In vos '試作イベントのベース車情報
                    Dim i As Long = 0
                    '' 開発符号をブランクに置き換える
                    'strGousya = vo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                    If StringUtil.IsEmpty(vo.ShisakuSyubetu) Then    'ホワイトボディ、削除は除く
                        For Each voSeisakuBase As TSeisakuIchiranBaseVo In baseList '製作一覧完成車シートのベース車情報
                            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               vo.ShisakuGousya)

                            '2014/02/18
                            'Dim strSeisakuGousya As String = voSeisakuBase.Gousya.PadLeft(4, "0")
                            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               voSeisakuBase.Gousya)

                            '製作一覧の号車が試作イベントの号車を含むなら
                            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                                ''号車が同じなら
                                'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                                i = i + 1
                                Exit For
                            End If
                        Next
                    End If
                    If StringUtil.Equals(vo.ShisakuSyubetu, "W") Then    'ホワイトボディ、削除は除く
                        For Each voSeisakuBase As TSeisakuIchiranWbVo In wbList '製作一覧ＷＢ車シートのベース車情報
                            '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                            '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                            Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               vo.ShisakuGousya)

                            '2014/02/18
                            'Dim strSeisakuGousya As String = voSeisakuBase.Gousya.PadLeft(4, "0")
                            Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                                                                               voSeisakuBase.Gousya)

                            '製作一覧の号車が試作イベントの号車を含むなら
                            If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                                ''号車が同じなら
                                'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                                i = i + 1
                                Exit For
                            End If
                        Next
                    End If
                    '１件も無ければ
                    If i = 0 Then
                        '表示順取得
                        If vo.HyojijunNo > HoldRowNo Then HoldRowNo = vo.HyojijunNo
                        Dim record As New TShisakuEventBaseVo
                        VoUtil.CopyProperties(vo, record)
                        If record.SekkeiTenkaiKbn IsNot Nothing Then
                            record.SekkeiTenkaiKbn = Convert.ToString(TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_GO.Equals(record.SekkeiTenkaiKbn))
                        End If
                        record.ShisakuSyubetu = "D" '削除コード

                        ''レコードがあれば削除後、変更後の値を更新
                        'If StringUtil.IsNotEmpty(_record.Value(vo.HyojijunNo)) Then
                        '    _record.Remove(vo.HyojijunNo)
                        'End If
                        _record.Add(vo.HyojijunNo, record)

                    End If
                Next

                '設計展開校は号車の追加は不要

                ''完成車ベース情報の号車追加処理
                'If HoldRowNo <> 0 Then HoldRowNo = HoldRowNo + 1

                ''追加号車の処理
                'For Each voSeisakuBase As TSeisakuIchiranBaseVo In baseList '製作一覧完成車のベース車情報

                '    Dim i As Long = 0
                '    'イベントから設定
                '    For Each vo As TShisakuEventBaseVo In vos '試作イベントのベース車情報
                '        '' 開発符号をブランクに置き換える
                '        'strGousya = vo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                '        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                '        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                '        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                '                                                           vo.ShisakuGousya)
                '        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                '                                                           voSeisakuBase.Gousya)
                '        '製作一覧の号車が試作イベントの号車を含むなら
                '        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                '            ''号車が同じなら
                '            'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                '            i = i + 1
                '            Exit For
                '        End If
                '    Next

                '    '該当号車が１件も無い場合は追加
                '    If i = 0 Then
                '        Dim record As New TShisakuEventBaseVo

                '        record.SekkeiTenkaiKbn = Nothing
                '        '開発符号と号車を結合してセット
                '        record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & voSeisakuBase.Gousya
                '        record.BaseKaihatsuFugo = voSeisakuBase.KaihatsuFugo
                '        record.BaseShiyoujyouhouNo = voSeisakuBase.ShiyoujyouhouNo
                '        record.BaseKatashikiScd7 = voSeisakuBase.KatashikiScd7

                '        record.BaseKatashiki = voSeisakuBase.KatashikiScd7
                '        'アプライド№を７ケタ型式から取得
                '        Dim getSobiKaitei As New EventEditBaseCarDaoImpl
                '        Dim GetValue = getSobiKaitei.FindSobiKaitei(voSeisakuBase.KaihatsuFugo, _
                '                                                    voSeisakuBase.ShiyoujyouhouNo, _
                '                                                    voSeisakuBase.KatashikiScd7)
                '        '存在しない場合にはAP№へブランクをセット
                '        If StringUtil.IsNotEmpty(GetValue) Then
                '            record.BaseAppliedNo = GetValue.AppliedNo
                '        Else
                '            record.BaseAppliedNo = ""
                '        End If
                '        record.BaseShimuke = voSeisakuBase.KatashikiShimuke
                '        record.BaseOp = voSeisakuBase.KatashikiOp.Replace(" ", "") 'ブランクを排除
                '        record.BaseGaisousyoku = voSeisakuBase.Gaisousyoku
                '        record.BaseNaisousyoku = voSeisakuBase.Naisousyoku
                '        record.HyojijunNo = HoldRowNo

                '        _record.Add(HoldRowNo, record)

                '        HoldRowNo = HoldRowNo + 1

                '    End If

                'Next

                ''ＷＢ車ベース情報の号車追加処理
                ''追加号車の更新
                'For Each voSeisakuBase As TSeisakuIchiranWbVo In wbList '製作一覧のＷＢ車情報

                '    Dim i As Long = 0
                '    'イベントから設定
                '    For Each vo As TShisakuEventBaseVo In vos '試作イベントのベース車情報
                '        '' 開発符号をブランクに置き換える
                '        'strGousya = vo.ShisakuGousya.Replace(tSeisakuHakouHdVo.KaihatsuFugo, "")
                '        '試作イベントの号車から開発符号、#（完成車）、W（ＷＢ車）を取り除いて、
                '        '   製作一覧の号車と比較する。（４桁未満は頭0付きで比較）
                '        Dim strShisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                '                                                           vo.ShisakuGousya)
                '        Dim strSeisakuGousya As String = kaihatsuFugo4keta(tSeisakuHakouHdVo.KaihatsuFugo, _
                '                                                           voSeisakuBase.Gousya)
                '        '製作一覧の号車が試作イベントの号車を含むなら
                '        If 0 <= strShisakuGousya.IndexOf(strSeisakuGousya) Then
                '            ''号車が同じなら
                '            'If StringUtil.Equals(voSeisakuBase.Gousya, strGousya) Then
                '            i = i + 1
                '            Exit For
                '        End If
                '    Next

                '    '該当号車が１件も無い場合は追加
                '    If i = 0 Then
                '        Dim record As New TShisakuEventBaseVo

                '        record.ShisakuSyubetu = "W" 'ホワイトボディ
                '        record.SekkeiTenkaiKbn = Nothing
                '        '開発符号と号車を結合してセット
                '        record.ShisakuGousya = tSeisakuHakouHdVo.KaihatsuFugo & voSeisakuBase.Gousya
                '        record.BaseKaihatsuFugo = voSeisakuBase.KaihatsuFugo
                '        record.BaseShiyoujyouhouNo = voSeisakuBase.ShiyoujyouhouNo
                '        record.BaseKatashikiScd7 = voSeisakuBase.KatashikiScd7

                '        record.BaseKatashiki = voSeisakuBase.KatashikiScd7
                '        'アプライド№を７ケタ型式から取得
                '        Dim getSobiKaitei As New EventEditBaseCarDaoImpl
                '        Dim GetValue = getSobiKaitei.FindSobiKaitei(voSeisakuBase.KaihatsuFugo, _
                '                                                    voSeisakuBase.ShiyoujyouhouNo, _
                '                                                    voSeisakuBase.KatashikiScd7)
                '        '存在しない場合にはAP№へブランクをセット
                '        If StringUtil.IsNotEmpty(GetValue) Then
                '            record.BaseAppliedNo = GetValue.AppliedNo
                '        Else
                '            record.BaseAppliedNo = ""
                '        End If
                '        record.BaseShimuke = voSeisakuBase.KatashikiShimuke
                '        record.BaseOp = voSeisakuBase.KatashikiOp.Replace(" ", "") 'ブランクを排除
                '        record.BaseGaisousyoku = voSeisakuBase.Gaisousyoku
                '        record.BaseNaisousyoku = voSeisakuBase.Naisousyoku
                '        record.HyojijunNo = HoldRowNo

                '        _record.Add(HoldRowNo, record)

                '        HoldRowNo = HoldRowNo + 1

                '    End If

                'Next

            End If

        End Sub

#End Region

        Private Function IsAddMode() As Boolean
            Return StringUtil.IsEmpty(shisakuEventCode)
        End Function

        Public Sub Register(ByVal newShisakuEventCode As String)
            RegisterMain(newShisakuEventCode, True)
        End Sub

        Public Sub Save(ByVal newShisakuEventCode As String)
            RegisterMain(newShisakuEventCode, False)
        End Sub

        Private Sub RegisterMain(ByVal newShisakuEventCode As String, ByVal IsRegister As Boolean)
            '' 既存データを削除
            If Not IsAddMode() Then
                Dim param As New TShisakuEventBaseVo
                param.ShisakuEventCode = newShisakuEventCode
                shisakuEventBaseTenkaiDao.DeleteBy(param)
            End If

            For Each key As Integer In _record.Keys
                Dim dispValue As TShisakuEventBaseVo = _record.Value(key)
                Dim value As New TShisakuEventBaseVo
                VoUtil.CopyProperties(dispValue, value)

                '号車がNothingの列はインサートしない
                If StringUtil.IsEmpty(value.ShisakuGousya) Then
                    Continue For
                End If

                value.ShisakuEventCode = newShisakuEventCode
                value.HyojijunNo = key
                If value.SekkeiTenkaiKbn IsNot Nothing Then
                    value.SekkeiTenkaiKbn = Convert.ToString(IIf(Convert.ToBoolean(value.SekkeiTenkaiKbn), _
                                                TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_GO, _
                                                TShisakuEventBaseSeisakuIchiranVoHelper.SekkeiTenkaiKbn.JURYO_MAE))
                End If

                'NEW設計展開用に以下の値を取得しBASE情報に更新する。
                Dim getSobiKaitei As New EventEditBaseCarDaoImpl
                Dim GetValue = getSobiKaitei.FindSobiKaitei(value.BaseKaihatsuFugo, _
                                                                      value.BaseShiyoujyouhouNo, _
                                                                      value.BaseKatashiki)
                If Not StringUtil.IsEmpty(GetValue) Then
                    value.BaseKatashikiScd7 = GetValue.KatashikiScd7
                    value.BaseSobiKaiteiNo = GetValue.SobiKaiteiNo
                End If
                '
                '製作一覧DBでは”国内”で持っているので変換する。
                If StringUtil.Equals(dispValue.BaseShimuke, _
                                     TShisakuEventBaseSeisakuIchiranVoHelper.BaseShimukechiShimukeName.KOKUNAI) Then
                    value.BaseShimuke = ""
                Else
                    value.BaseShimuke = dispValue.BaseShimuke
                End If
                '2014/04/22 kabasawa'
                '空文字をnullにしないと設計展開でエラーが発生する'
                If StringUtil.IsEmpty(value.BaseShiyoujyouhouNo) Then
                    value.BaseShiyoujyouhouNo = Nothing
                End If



                If StringUtil.IsEmpty(value.CreatedUserId) Then
                    value.CreatedUserId = login.UserId
                    value.CreatedDate = aDate.CurrentDateDbFormat
                    value.CreatedTime = aDate.CurrentTimeDbFormat
                    dispValue.CreatedUserId = login.UserId
                    dispValue.CreatedDate = aDate.CurrentDateDbFormat
                    dispValue.CreatedTime = aDate.CurrentTimeDbFormat
                End If
                value.UpdatedUserId = login.UserId
                value.UpdatedDate = aDate.CurrentDateDbFormat
                value.UpdatedTime = aDate.CurrentTimeDbFormat

                shisakuEventBaseTenkaiDao.InsertBy(value)
            Next

            shisakuEventCode = newShisakuEventCode
        End Sub

        ''' <summary>
        ''' イベント情報コピー処理時の初期化など
        ''' </summary>
        ''' <param name="shisakuEventCode">元試作イベントコード</param>
        ''' <remarks></remarks>
        Friend Sub ProcessPostCopy(ByVal shisakuEventCode As String)
            Me.shisakuEventCode = shisakuEventCode
            ' 自身が登録ユーザーになるようにクリア
            For Each rowNo As Integer In GetInputRowNos()
                Records(rowNo).CreatedUserId = Nothing
                Records(rowNo).CreatedDate = Nothing
                Records(rowNo).CreatedTime = Nothing
            Next
        End Sub

#Region "公開プロパティ"
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


    End Class
End Namespace