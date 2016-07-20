Imports ShisakuCommon.Db.EBom.Vo.Helper
Imports ShisakuCommon.Db.EBom.Dao
Imports EventSakusei.YosanSetteiBuhinEdit.Dao
Imports ShisakuCommon
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Util


Namespace YosanSetteiBuhinEdit.Logic

    Public Class YosanSetteiBuhinEditHeader : Inherits Observable

        Private impl As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl
        Private header As New Observable

        Public Sub New(ByVal shisakuEventCode As String, ByVal shisakuListCode As String)

            'リストコードの取得'
            Dim listcodeVo As New TYosanSetteiListcodeVo
            listcodeVo = impl.FindByListCode(shisakuEventCode, shisakuListCode)

            'イベント情報の取得'
            If listcodeVo Is Nothing Then
                '例外仮対応
                MsgBox("リストコードに対応するレコードが取得出来ませんでした。")
                Throw New ShisakuException
                Exit Sub
            End If

            Dim eventVo As New TShisakuEventVo
            eventVo = impl.FindByEvent(shisakuEventCode)

            Me.shisakuEventCode = shisakuEventCode
            Me.shisakuListCode = shisakuListCode
            _listEventName = listcodeVo.YosanEventName

            _kaihatsuFugo = eventVo.ShisakuKaihatsuFugo

            _koujiShireiNo = listcodeVo.YosanKoujiShireiNo
            _seihinKbn = listcodeVo.YosanSeihinKbn
            _yosanCode = listcodeVo.YosanKoujiNo
            _koujiKbn = listcodeVo.YosanKoujiKbn

            BlockNoLabelValues = GetLabelValues_BlockNo()

            'ユニット区分
            _MTKbn = eventVo.UnitKbn
            '基本からとってくる'
            '_blockNo = ""

            SetChanged()

        End Sub

#Region "公開プロパティ"
        '' 試作イベントコード
        Private _shisakuEventCode As String
        '' 試作リストコード
        Private _shisakuListCode As String
        '' リストコードイベント名称
        Private _listEventName As String
        '' 開発符号
        Private _kaihatsuFugo As String
        '' 工事指令No
        Private _koujiShireiNo As String
        '' 工事区分
        Private _koujiKbn As String
        '' M/T区分
        Private _MTKbn As String
        '' 製品区分
        Private _seihinKbn As String
        '' 予算コード
        Private _yosanCode As String
        '' ブロックNo
        Private _blockNo As String
        '' メタル用M/T区分
        Private _metalMTKbn As String
#End Region

#Region "公開プロパティの設定"

        ''' <summary>試作イベントコード</summary>
        ''' <value>試作イベントコード</value>
        ''' <returns>試作イベントコード</returns>
        Public Property shisakuEventCode() As String
            Get
                Return _shisakuEventCode
            End Get
            Set(ByVal value As String)
                _shisakuEventCode = value
            End Set
        End Property

        ''' <summary>試作リストコード</summary>
        ''' <value>試作リストコード</value>
        ''' <returns>試作リストコード</returns>
        Public Property shisakuListCode() As String
            Get
                Return _shisakuListCode
            End Get
            Set(ByVal value As String)
                _shisakuListCode = value
            End Set
        End Property

        ''' <summary>リストコードイベント名称</summary>
        ''' <value>リストコードイベント名称</value>
        ''' <returns>リストコードイベント名称</returns>
        Public Property listEventName() As String
            Get
                Return _listEventName
            End Get
            Set(ByVal value As String)
                _listEventName = value
            End Set
        End Property

        ''' <summary>開発符号</summary>
        ''' <value>開発符号</value>
        ''' <returns>開発符号</returns>
        Public Property kaihatsuFugo() As String
            Get
                Return _kaihatsuFugo
            End Get
            Set(ByVal value As String)
                _kaihatsuFugo = value
            End Set
        End Property

        ''' <summary>工事指令No</summary>
        ''' <value>工事指令No</value>
        ''' <returns>工事指令No</returns>
        Public Property koujiShireiNo() As String
            Get
                Return _koujiShireiNo
            End Get
            Set(ByVal value As String)
                _koujiShireiNo = value
            End Set
        End Property

        ''' <summary>工事区分</summary>
        ''' <value>工事区分</value>
        ''' <returns>工事区分</returns>
        Public Property koujiKbn() As String
            Get
                Return _koujiKbn
            End Get
            Set(ByVal value As String)
                _koujiKbn = value
            End Set
        End Property

        ''' <summary>M/T区分</summary>
        ''' <value>M/T区分</value>
        ''' <returns>M/T区分</returns>
        Public Property MTKbn() As String
            Get
                Return _MTKbn
            End Get
            Set(ByVal value As String)
                _MTKbn = value
            End Set
        End Property

        ''' <summary>製品区分</summary>
        ''' <value>製品区分</value>
        ''' <returns>製品区分</returns>
        Public Property seihinKbn() As String
            Get
                Return _seihinKbn
            End Get
            Set(ByVal value As String)
                _seihinKbn = value
            End Set
        End Property

        ''' <summary>予算コード</summary>
        ''' <value>予算コード</value>
        ''' <returns>予算コード</returns>
        Public Property yosanCode() As String
            Get
                Return _yosanCode
            End Get
            Set(ByVal value As String)
                _yosanCode = value
            End Set
        End Property

        ''' <summary>ブロックNo</summary>
        ''' <value>ブロックNo</value>
        ''' <returns>ブロックNo</returns>
        Public Property blockNo() As String
            Get
                Return _blockNo
            End Get
            Set(ByVal value As String)
                _blockNo = value
            End Set
        End Property

        ''' <summary>メタル用M/T区分</summary>
        ''' <value>メタル用M/T区分</value>
        ''' <returns>メタル用M/T区分</returns>
        Public Property metalMTKbn() As String
            Get
                Return _metalMTKbn
            End Get
            Set(ByVal value As String)
                _metalMTKbn = value
            End Set
        End Property


#End Region

#Region "公開プロパティの選択値"

        '' ブロックNoの選択値
        Private _BlockNoLabelValues As List(Of LabelValueVo)

        ''' <summary>ブロックNoの選択値</summary>
        ''' <value>ブロックNoの選択値</value>
        ''' <returns>ブロックNoの選択値</returns>
        Public Property BlockNoLabelValues() As List(Of LabelValueVo)
            Get
                Return _BlockNoLabelValues
            End Get
            Set(ByVal value As List(Of LabelValueVo))
                _BlockNoLabelValues = value
            End Set
        End Property

#End Region

        Private Class BlockNoExtraction : Implements ILabelValueExtraction
            Public Sub Extraction(ByVal aLocator As ShisakuCommon.Util.LabelValue.LabelValueLocator) Implements ShisakuCommon.Util.LabelValue.ILabelValueExtraction.Extraction
                Dim vo As New TYosanSetteiBuhinVo
                aLocator.IsA(vo).Label(vo.YosanBlockNo).Value(vo.YosanBlockNo)
            End Sub
        End Class

        Public Function GetLabelValues_BlockNo() As List(Of LabelValueVo)
            Dim impl As YosanSetteiBuhinEditHeaderDao = New YosanSetteiBuhinEditHeaderDaoImpl
            Dim results As List(Of LabelValueVo) = _
                LabelValueExtracter(Of TYosanSetteiBuhinVo).Extract(impl.FindByTehaiKihon(shisakuEventCode, shisakuListCode), New BlockNoExtraction)
            results.Sort(New LabelValueComparer)
            Return results
        End Function

    End Class

End Namespace
