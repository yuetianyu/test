Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports EventSakusei.EventEdit.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Logic
    Public Class EventEditReferenceCar : Inherits Observable

        Private shisakuEventCode As String
        Private ReadOnly login As LoginInfo
        Private ReadOnly baseCarDao As EventEditBaseCarDao
        Private ReadOnly referenceCarDao As EventEditReferenceCarDao
        Private ReadOnly aBaseCar As EventEditBaseCar

        Public Sub New(ByVal shisakuEventCode As String, _
                       ByVal login As LoginInfo, _
                       ByVal baseCarDao As EventEditBaseCarDao, _
                       ByVal referenceCarDao As EventEditReferenceCarDao, _
                       ByVal aBaseCar As EventEditBaseCar)
            Me.shisakuEventCode = shisakuEventCode
            Me.login = login
            Me.baseCarDao = baseCarDao
            Me.referenceCarDao = referenceCarDao
            Me.aBaseCar = aBaseCar

            setChanged()
        End Sub

#Region "参考車情報のDelegateプロパティ"
        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuSyubetu = value
                setChanged()
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
                setChanged()
            End Set
        End Property

        ''' <summary>試作型式</summary>
        ''' <value>試作型式</value>
        ''' <returns>試作型式</returns>
        Public Property ShisakuKatashiki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuKatashiki
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuKatashiki = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作OP</summary>
        ''' <value>試作OP</value>
        ''' <returns>試作OP</returns>
        Public Property ShisakuOp(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuOp
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuOp = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作仕向け</summary>
        ''' <value>試作仕向け</value>
        ''' <returns>試作仕向け</returns>
        Public Property ShisakuShimuke(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuShimuke
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuShimuke = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作ハンドル</summary>
        ''' <value>試作ハンドル</value>
        ''' <returns>試作ハンドル</returns>
        Public Property ShisakuHandoru(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuHandoru
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuHandoru = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作車型</summary>
        ''' <value>試作車型</value>
        ''' <returns>試作車型</returns>
        Public Property ShisakuSyagata(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSyagata
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuSyagata = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作グレード</summary>
        ''' <value>試作グレード</value>
        ''' <returns>試作グレード</returns>
        Public Property ShisakuGrade(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGrade
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuGrade = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作車台№</summary>
        ''' <value>試作車台№</value>
        ''' <returns>試作車台№</returns>
        Public Property ShisakuSyadaiNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSyadaiNo
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuSyadaiNo = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作外装色</summary>
        ''' <value>試作外装色</value>
        ''' <returns>試作外装色</returns>
        Public Property ShisakuGaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGaisousyoku
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuGaisousyoku = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作内装色</summary>
        ''' <value>試作内装色</value>
        ''' <returns>試作内装色</returns>
        Public Property ShisakuNaisousyoku(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuNaisousyoku
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuNaisousyoku = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作グループ</summary>
        ''' <value>試作グループ</value>
        ''' <returns>試作グループ</returns>
        Public Property ShisakuGroup(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuGroup
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuGroup = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作工指№</summary>
        ''' <value>試作工指№</value>
        ''' <returns>試作工指№</returns>
        Public Property ShisakuKoushiNo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuKoushiNo
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuKoushiNo = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作完成日</summary>
        ''' <value>試作完成日</value>
        ''' <returns>試作完成日</returns>
        Public Property ShisakuKanseibi(ByVal rowNo As Integer) As Nullable(Of Int32)
            Get
                Return Records(rowNo).ShisakuKanseibi
            End Get
            Set(ByVal value As Nullable(Of Int32))
                Records(rowNo).ShisakuKanseibi = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作E/G型式</summary>
        ''' <value>試作E/G型式</value>
        ''' <returns>試作E/G型式</returns>
        Public Property ShisakuEgKatashiki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgKatashiki
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuEgKatashiki = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作E/G排気量</summary>
        ''' <value>試作E/G排気量</value>
        ''' <returns>試作E/G排気量</returns>
        Public Property ShisakuEgHaikiryou(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgHaikiryou
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuEgHaikiryou = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作E/Gシステム</summary>
        ''' <value>試作E/Gシステム</value>
        ''' <returns>試作E/Gシステム</returns>
        Public Property ShisakuEgSystem(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgSystem
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuEgSystem = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作E/G過給機</summary>
        ''' <value>試作E/G過給機</value>
        ''' <returns>試作E/G過給機</returns>
        Public Property ShisakuEgKakyuuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuEgKakyuuki
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuEgKakyuuki = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作T/M駆動</summary>
        ''' <value>試作T/M駆動</value>
        ''' <returns>試作T/M駆動</returns>
        Public Property ShisakuTmKudou(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmKudou
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuTmKudou = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作T/M変速機</summary>
        ''' <value>試作T/M変速機</value>
        ''' <returns>試作T/M変速機</returns>
        Public Property ShisakuTmHensokuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmHensokuki
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuTmHensokuki = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作T/M副変速機</summary>
        ''' <value>試作T/M副変速機</value>
        ''' <returns>試作T/M副変速機</returns>
        Public Property ShisakuTmFukuHensokuki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuTmFukuHensokuki
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuTmFukuHensokuki = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作使用部署</summary>
        ''' <value>試作使用部署</value>
        ''' <returns>試作使用部署</returns>
        Public Property ShisakuSiyouBusyo(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuSiyouBusyo
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuSiyouBusyo = value
                setChanged()
            End Set
        End Property

        ''' <summary>試作試験目的</summary>
        ''' <value>試作試験目的</value>
        ''' <returns>試作試験目的</returns>
        Public Property ShisakuShikenMokuteki(ByVal rowNo As Integer) As String
            Get
                Return Records(rowNo).ShisakuShikenMokuteki
            End Get
            Set(ByVal value As String)
                Records(rowNo).ShisakuShikenMokuteki = value
                setChanged()
            End Set
        End Property
#End Region

#Region "行情報取得・操作"
        Private _record As New IndexedList(Of EventEditCompleteCarVo)
        Private _recordBase As New IndexedList(Of EventEditBaseCarVo)

        ''' <summary>完成車情報</summary>
        ''' <returns>完成車情報</returns>
        Private ReadOnly Property Records(ByVal rowNo As Integer) As EventEditCompleteCarVo
            Get
                Return _record.Value(rowNo)
            End Get
        End Property

        ''' <summary>完成車情報</summary>
        ''' <returns>完成車情報</returns>
        Private ReadOnly Property RecordBases(ByVal rowNo As Integer) As EventEditBaseCarVo
            Get
                Return _recordBase.Value(rowNo)
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
            _recordBase.Insert(rowNo)
        End Sub

        ''' <summary>
        ''' 行を削除する
        ''' </summary>
        ''' <param name="rowNo">削除する行No</param>
        ''' <remarks></remarks>
        Public Sub DeleteRow(ByVal rowNo As Integer)
            _record.Remove(rowNo)
            _recordBase.Remove(rowNo)
        End Sub

        Private Sub ReadRecords()
            'Dim param As New TShisakuEventKanseiVo
            'param.ShisakuEventCode = shisakuEventCode
            'Dim vos As List(Of TShisakuEventKanseiVo) = kanseiDao.FindBy(param)
            'For Each vo As TShisakuEventKanseiVo In vos
            '    Dim record As New EventEditCompleteCarVo
            '    VoUtil.CopyProperties(vo, record)
            '    _record.Add(vo.HyojijunNo, record)
            'Next
        End Sub

#End Region
        Public Sub Apply()
            Dim updateRowNos As New List(Of Integer)
            For Each rowNo As Integer In aBaseCar.GetInputRowNos
                Dim updated As Boolean
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseKaihatsuFugo, aBaseCar.BaseKaihatsuFugo(rowNo)) Then
                    RecordBases(rowNo).BaseKaihatsuFugo = aBaseCar.BaseKaihatsuFugo(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseShiyoujyouhouNo, aBaseCar.BaseShiyoujyouhouNo(rowNo)) Then
                    RecordBases(rowNo).BaseShiyoujyouhouNo = aBaseCar.BaseShiyoujyouhouNo(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseAppliedNo, aBaseCar.BaseAppliedNo(rowNo)) Then
                    RecordBases(rowNo).BaseAppliedNo = aBaseCar.BaseAppliedNo(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseKatashiki, aBaseCar.BaseKatashiki(rowNo)) Then
                    RecordBases(rowNo).BaseKatashiki = aBaseCar.BaseKatashiki(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseShimuke, aBaseCar.BaseShimuke(rowNo)) Then
                    RecordBases(rowNo).BaseShimuke = aBaseCar.BaseShimuke(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseOp, aBaseCar.BaseOp(rowNo)) Then
                    RecordBases(rowNo).BaseOp = aBaseCar.BaseOp(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseGaisousyoku, aBaseCar.BaseGaisousyoku(rowNo)) Then
                    RecordBases(rowNo).BaseGaisousyoku = aBaseCar.BaseGaisousyoku(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).BaseNaisousyoku, aBaseCar.BaseNaisousyoku(rowNo)) Then
                    RecordBases(rowNo).BaseNaisousyoku = aBaseCar.BaseNaisousyoku(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).ShisakuBaseEventCode, aBaseCar.ShisakuBaseEventCode(rowNo)) Then
                    RecordBases(rowNo).ShisakuBaseEventCode = aBaseCar.ShisakuBaseEventCode(rowNo)
                    updated = True
                End If
                If EzUtil.IsNotEqualIfNull(RecordBases(rowNo).ShisakuBaseGousya, aBaseCar.ShisakuBaseGousya(rowNo)) Then
                    RecordBases(rowNo).ShisakuBaseGousya = aBaseCar.ShisakuBaseGousya(rowNo)
                    updated = True
                End If
                If updated Then
                    updateRowNos.Add(rowNo)
                End If
            Next

            Dim emptyCompleteVo As New TShisakuEventKanseiVo
            For Each rowNo As Integer In updateRowNos
                VoUtil.CopyProperties(emptyCompleteVo, Records(rowNo))
                setChanged()
                If Not EzUtil.ContainsNull(RecordBases(rowNo).BaseKaihatsuFugo, RecordBases(rowNo).BaseShiyoujyouhouNo, RecordBases(rowNo).BaseAppliedNo, RecordBases(rowNo).BaseKatashiki, RecordBases(rowNo).BaseShimuke, RecordBases(rowNo).BaseOp, RecordBases(rowNo).BaseGaisousyoku, RecordBases(rowNo).BaseNaisousyoku) Then
                    Dim vo As Rhac0230Vo = baseCarDao.FindRhac0230By(RecordBases(rowNo).BaseKaihatsuFugo, RecordBases(rowNo).BaseShiyoujyouhouNo, RecordBases(rowNo).BaseKatashiki, RecordBases(rowNo).BaseAppliedNo, RecordBases(rowNo).BaseShimuke, RecordBases(rowNo).BaseOp, RecordBases(rowNo).BaseGaisousyoku, Rhac0430VoHelper.NaigaisoKbn.Gaiso)
                    If vo IsNot Nothing Then
                        Records(rowNo).ShisakuKatashiki = vo.KatashikiFugo7
                        Records(rowNo).ShisakuShimuke = RecordBases(rowNo).BaseShimuke
                        Records(rowNo).ShisakuOp = RecordBases(rowNo).BaseOp
                        Records(rowNo).ShisakuHandoru = vo.HandlePos
                        Records(rowNo).ShisakuSyagata = vo.DoorSu
                        Records(rowNo).ShisakuGrade = vo.GradeCode
                        Records(rowNo).ShisakuGaisousyoku = RecordBases(rowNo).BaseGaisousyoku
                        Records(rowNo).ShisakuNaisousyoku = RecordBases(rowNo).BaseNaisousyoku
                        Records(rowNo).ShisakuEgKatashiki = vo.GendokiKatasiki
                        Records(rowNo).ShisakuEgHaikiryou = vo.EgHaikiryo
                        Records(rowNo).ShisakuEgSystem = vo.DobenkeiCode
                        Records(rowNo).ShisakuEgKakyuuki = vo.KakyukiCode
                        Records(rowNo).ShisakuTmKudou = vo.KudoHosiki
                        Records(rowNo).ShisakuTmHensokuki = vo.TransMission
                    End If
                ElseIf Not EzUtil.ContainsNull(RecordBases(rowNo).ShisakuBaseEventCode, RecordBases(rowNo).ShisakuBaseGousya) Then
                    Dim vo As TShisakuEventKanseiVo = referenceCarDao.FindKanseiBy(RecordBases(rowNo).ShisakuBaseEventCode, RecordBases(rowNo).ShisakuBaseGousya)
                    If vo IsNot Nothing Then
                        VoUtil.CopyProperties(vo, Records(rowNo))
                    End If
                End If
            Next
        End Sub
    End Class
End Namespace