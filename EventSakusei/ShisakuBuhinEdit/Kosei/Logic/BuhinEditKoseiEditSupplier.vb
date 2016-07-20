Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Matrix
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Logic
    ''' <summary>
    ''' 試作部品編集情報の作成データを供給するクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BuhinEditKoseiEditSupplier
        Private _blockKeyVo As TShisakuSekkeiBlockVo
        Private _matrix As BuhinKoseiMatrix

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="blockKeyVo">試作設計ブロック情報（キー情報）</param>
        ''' <param name="matrix">部品表</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blockKeyVo As TShisakuSekkeiBlockVo, ByVal matrix As BuhinKoseiMatrix)
            Me._blockKeyVo = blockKeyVo
            Me._matrix = matrix
        End Sub

        ''' <summary>
        ''' 登録用のデータを作成する
        ''' </summary>
        ''' <returns>登録用のデータ</returns>
        ''' <remarks></remarks>
        Public Function MakeValues() As List(Of TShisakuBuhinEditVo)
            Dim results As New List(Of TShisakuBuhinEditVo)
            Dim BuhinNoHyoujiJun As Integer = 0
            Dim flag As Boolean = False

            'このあたりで登録時に員数の無い行を飛ばさせる必要がある'
            For Each rowIndex As Integer In _matrix.GetInputRowIndexes()
                For Each colIndex As Integer In _matrix.GetInputInsuColumnIndexes
                    '員数の存在チェック'
                    If Not _matrix.InsuSuryo(rowIndex, colIndex) Is Nothing Then
                        flag = True

                        'NULLだと手配帳で問題が発生するので'
                        If _matrix.Record(rowIndex).ShukeiCode Is Nothing Then
                            _matrix.Record(rowIndex).ShukeiCode = ""
                        End If
                        If _matrix.Record(rowIndex).SiaShukeiCode Is Nothing Then
                            _matrix.Record(rowIndex).SiaShukeiCode = ""
                        End If
                        If _matrix.Record(rowIndex).GencyoCkdKbn Is Nothing Then
                            _matrix.Record(rowIndex).GencyoCkdKbn = ""
                        End If
                        If _matrix.Record(rowIndex).MakerCode Is Nothing Then
                            _matrix.Record(rowIndex).MakerCode = ""
                        End If
                        If _matrix.Record(rowIndex).MakerName Is Nothing Then
                            _matrix.Record(rowIndex).MakerName = ""
                        End If
                        If _matrix.Record(rowIndex).BuhinNoKbn Is Nothing Then
                            _matrix.Record(rowIndex).BuhinNoKbn = ""
                        End If
                        If _matrix.Record(rowIndex).BuhinNoKaiteiNo Is Nothing Then
                            _matrix.Record(rowIndex).BuhinNoKaiteiNo = ""
                        End If
                        If _matrix.Record(rowIndex).EdaBan Is Nothing Then
                            _matrix.Record(rowIndex).EdaBan = ""
                        End If
                        If _matrix.Record(rowIndex).Saishiyoufuka Is Nothing Then
                            _matrix.Record(rowIndex).Saishiyoufuka = ""
                        End If
                        '2012/01/23 供給セクション追加
                        If _matrix.Record(rowIndex).KyoukuSection Is Nothing Then
                            _matrix.Record(rowIndex).KyoukuSection = ""
                        End If
                        If _matrix.Record(rowIndex).ShutuzuYoteiDate Is Nothing Then
                            _matrix.Record(rowIndex).ShutuzuYoteiDate = 99999999
                        End If
                        If _matrix.Record(rowIndex).ShisakuBuhinHi Is Nothing Then
                            _matrix.Record(rowIndex).ShisakuBuhinHi = 0
                        End If
                        If _matrix.Record(rowIndex).ShisakuKataHi Is Nothing Then
                            _matrix.Record(rowIndex).ShisakuKataHi = 0
                        End If
                        '2012/01/25 部品ノート追加 
                        If _matrix.Record(rowIndex).BuhinNote Is Nothing Then
                            _matrix.Record(rowIndex).BuhinNote = ""
                        End If
                        '20140818 Sakai Add
                        If _matrix.Record(rowIndex).TsukurikataSeisaku Is Nothing Then
                            _matrix.Record(rowIndex).TsukurikataSeisaku = ""
                        End If
                        If _matrix.Record(rowIndex).TsukurikataKatashiyou1 Is Nothing Then
                            _matrix.Record(rowIndex).TsukurikataKatashiyou1 = ""
                        End If
                        If _matrix.Record(rowIndex).TsukurikataKatashiyou2 Is Nothing Then
                            _matrix.Record(rowIndex).TsukurikataKatashiyou2 = ""
                        End If
                        If _matrix.Record(rowIndex).TsukurikataKatashiyou3 Is Nothing Then
                            _matrix.Record(rowIndex).TsukurikataKatashiyou3 = ""
                        End If
                        If _matrix.Record(rowIndex).TsukurikataTigu Is Nothing Then
                            _matrix.Record(rowIndex).TsukurikataTigu = ""
                        End If
                        If _matrix.Record(rowIndex).TsukurikataNounyu Is Nothing Then
                            ''2015/05/14 修正E.Ubukata
                            '' 値を入れていない場合勝手に0にせず空白にする
                            '_matrix.Record(rowIndex).TsukurikataNounyu = 0
                            _matrix.Record(rowIndex).TsukurikataNounyu = Nothing
                        End If
                        If _matrix.Record(rowIndex).TsukurikataKibo Is Nothing Then
                            _matrix.Record(rowIndex).TsukurikataKibo = ""
                        End If


                        ''↓↓2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD BEGIN
                        '材料情報・製品長
                        If _matrix.Record(rowIndex).MaterialInfoLength Is Nothing Then
                            ''2015/05/14 修正E.Ubukata
                            '' 値を入れていない場合勝手に0にせず空白にする
                            '_matrix.Record(rowIndex).MaterialInfoLength = 0
                            _matrix.Record(rowIndex).MaterialInfoLength = Nothing
                        End If
                        '' 材料情報・製品幅
                        If _matrix.Record(rowIndex).MaterialInfoWidth Is Nothing Then
                            ''2015/05/14 修正E.Ubukata
                            '' 値を入れていない場合勝手に0にせず空白にする
                            '_matrix.Record(rowIndex).MaterialInfoWidth = 0
                            _matrix.Record(rowIndex).MaterialInfoWidth = Nothing
                        End If
                        '' データ項目・改訂№
                        If _matrix.Record(rowIndex).DataItemKaiteiNo Is Nothing Then
                            _matrix.Record(rowIndex).DataItemKaiteiNo = ""
                        End If
                        '' データ項目・エリア名
                        If _matrix.Record(rowIndex).DataItemAreaName Is Nothing Then
                            _matrix.Record(rowIndex).DataItemAreaName = ""
                        End If
                        '' データ項目・セット名
                        If _matrix.Record(rowIndex).DataItemSetName Is Nothing Then
                            _matrix.Record(rowIndex).DataItemSetName = ""
                        End If
                        '' データ項目・改訂情報
                        If _matrix.Record(rowIndex).DataItemKaiteiInfo Is Nothing Then
                            _matrix.Record(rowIndex).DataItemKaiteiInfo = ""
                        End If
                        ''↑↑2014/12/26 メタル対応追加フィールド (DANIEL)柳沼 ADD END


                        'ある列が存在すれば追加'
                        With _matrix.Record(rowIndex)
                            .ShisakuEventCode = _blockKeyVo.ShisakuEventCode
                            .ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
                            .ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
                            .ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
                            .BuhinNoHyoujiJun = rowIndex
                        End With
                        '存在チェックしてから追加'
                        If Not results.Contains(_matrix.Record(rowIndex)) Then
                            results.Add(_matrix.Record(rowIndex))
                        End If

                        ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
                        If StringUtil.IsEmpty(_matrix.Record(rowIndex).BaseBuhinFlg) Then
                            _matrix.Record(rowIndex).BaseBuhinFlg = "0"
                        End If
                        ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END
                        Exit For
                    End If
                Next
            Next

            '順番を飛ばさない'
            For index As Integer = 0 To results.Count - 1
                results(index).BuhinNoHyoujiJun = index
            Next



            Return results
        End Function


        ''' <summary>
        ''' 更新する
        ''' </summary>
        ''' <param name="login">ログイン情報</param>
        ''' <param name="editDao">試作部品編集Dao</param>
        ''' <param name="aDate">試作日付</param>
        ''' <remarks></remarks>
        Public Sub Update(ByVal login As LoginInfo, ByVal editDao As TShisakuBuhinEditDao, ByVal aDate As ShisakuDate)

            Dim param As New TShisakuBuhinEditVo
            param.ShisakuEventCode = _blockKeyVo.ShisakuEventCode
            param.ShisakuBukaCode = _blockKeyVo.ShisakuBukaCode
            param.ShisakuBlockNo = _blockKeyVo.ShisakuBlockNo
            param.ShisakuBlockNoKaiteiNo = _blockKeyVo.ShisakuBlockNoKaiteiNo
            editDao.DeleteBy(param)

            For Each vo As TShisakuBuhinEditVo In MakeValues()

                Dim editVo As New TShisakuBuhinEditVo
                editVo.ShisakuEventCode = vo.ShisakuEventCode
                editVo.ShisakuBukaCode = vo.ShisakuBukaCode
                editVo.ShisakuBlockNo = vo.ShisakuBlockNo
                editVo.ShisakuBlockNoKaiteiNo = vo.ShisakuBlockNoKaiteiNo
                editVo.BuhinNoHyoujiJun = vo.BuhinNoHyoujiJun
                editVo.Level = vo.Level
                editVo.KyoukuSection = vo.KyoukuSection
                editVo.ShukeiCode = vo.ShukeiCode
                editVo.SiaShukeiCode = vo.SiaShukeiCode
                editVo.GencyoCkdKbn = vo.GencyoCkdKbn
                editVo.MakerCode = vo.MakerCode
                editVo.MakerName = vo.MakerName
                editVo.BuhinNo = vo.BuhinNo
                editVo.BuhinNoKbn = vo.BuhinNoKbn
                editVo.BuhinNoKaiteiNo = vo.BuhinNoKaiteiNo
                editVo.EdaBan = vo.EdaBan
                editVo.BuhinName = vo.BuhinName
                editVo.Saishiyoufuka = vo.Saishiyoufuka
                editVo.ShutuzuYoteiDate = vo.ShutuzuYoteiDate
                editVo.ZaishituKikaku1 = vo.ZaishituKikaku1
                editVo.ZaishituKikaku2 = vo.ZaishituKikaku2
                editVo.ZaishituKikaku3 = vo.ZaishituKikaku3
                editVo.ZaishituMekki = vo.ZaishituMekki
                ''↓↓2014/07/23 Ⅰ.2.管理項目追加_y) (TES)張 ADD BEGIN
                editVo.TsukurikataSeisaku = vo.TsukurikataSeisaku
                editVo.TsukurikataKatashiyou1 = vo.TsukurikataKatashiyou1
                editVo.TsukurikataKatashiyou2 = vo.TsukurikataKatashiyou2
                editVo.TsukurikataKatashiyou3 = vo.TsukurikataKatashiyou3
                editVo.TsukurikataTigu = vo.TsukurikataTigu
                editVo.TsukurikataNounyu = vo.TsukurikataNounyu
                editVo.TsukurikataKibo = vo.TsukurikataKibo
                ''↑↑2014/07/23 Ⅰ.2.管理項目追加_y) (TES)張 ADD END

                editVo.ShisakuBankoSuryo = vo.ShisakuBankoSuryo
                editVo.ShisakuBankoSuryoU = vo.ShisakuBankoSuryoU
                '↓↓↓2014/12/25 メタル項目を追加 TES)張 ADD BEGIN
                editVo.MaterialInfoLength = vo.MaterialInfoLength
                editVo.MaterialInfoWidth = vo.MaterialInfoWidth
                editVo.DataItemKaiteiNo = vo.DataItemKaiteiNo
                editVo.DataItemAreaName = vo.DataItemAreaName
                editVo.DataItemSetName = vo.DataItemSetName
                editVo.DataItemKaiteiInfo = vo.DataItemKaiteiInfo
                '↑↑↑2014/12/25 メタル項目を追加 TES)張 ADD END
                editVo.ShisakuBuhinHi = vo.ShisakuBuhinHi
                editVo.ShisakuKataHi = vo.ShisakuKataHi
                editVo.Bikou = vo.Bikou
                editVo.BuhinNote = vo.BuhinNote
                editVo.KaiteiHandanFlg = vo.KaiteiHandanFlg
                editVo.ShisakuListCode = vo.ShisakuListCode
                If StringUtil.IsEmpty(editVo.CreatedUserId) Then
                    With editVo
                        .CreatedUserId = login.UserId
                        .CreatedDate = aDate.CurrentDateDbFormat
                        .CreatedTime = aDate.CurrentTimeDbFormat
                    End With
                End If
                editVo.EditTourokubi = DateUtil.ConvDateToIneteger(aDate.CurrentDateTime)
                editVo.EditTourokujikan = DateUtil.ConvTimeToIneteger(aDate.CurrentDateTime)
                editVo.UpdatedUserId = login.UserId
                editVo.UpdatedDate = aDate.CurrentDateDbFormat
                editVo.UpdatedTime = aDate.CurrentTimeDbFormat
                ''↓↓2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD BEGIN
                editVo.BaseBuhinFlg = vo.BaseBuhinFlg
                ''↑↑2014/08/22 1)EBOM-新試作手配システム過去データの組み合わせ抽出 酒井 ADD END

                ''2015/09/03
                editVo.BaseBuhinSeq = vo.BaseBuhinSeq

                editDao.InsertBy(editVo)
            Next
        End Sub
    End Class
End Namespace