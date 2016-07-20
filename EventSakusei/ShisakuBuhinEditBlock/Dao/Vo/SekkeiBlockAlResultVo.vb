Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEditBlock.Dao
    Public Class SekkeiBlockAlResultVo : Inherits Rhac1500Vo
        '' 部コード
        Private _BuCode As String
        '' 課コード
        Private _KaCode As String
        '' 試作号車
        Private _ShisakuGousya As String
        '' 装備改訂№
        Private _SobiKaiteiNo As String
        '' 7桁型式識別コード
        Private _KatashikiScd7 As String
        '' 試作イベントコード
        Private _shisakuEventCode As String
        '' 組合せコード
        Private _KumiawaseCodeSo As String
        '' TOP_COLOR_KAITEI_NO
        Private _TopColorKaiteiNo As String
        '' FUKA_NO
        Private _FukaNo As String

        ''' <summary>部コード</summary>
        ''' <value>部コード</value>
        ''' <returns>部コード</returns>
        Public Property BuCode() As String
            Get
                Return _BuCode
            End Get
            Set(ByVal value As String)
                _BuCode = value
            End Set
        End Property

        ''' <summary>課コード</summary>
        ''' <value>課コード</value>
        ''' <returns>課コード</returns>
        Public Property KaCode() As String
            Get
                Return _KaCode
            End Get
            Set(ByVal value As String)
                _KaCode = value
            End Set
        End Property

        ''' <summary>試作号車</summary>
        ''' <value>試作号車</value>
        ''' <returns>試作号車</returns>
        Public Property ShisakuGousya() As String
            Get
                Return _ShisakuGousya
            End Get
            Set(ByVal value As String)
                _ShisakuGousya = value
            End Set
        End Property

        ''' <summary>装備改訂№</summary>
        ''' <value>装備改訂№</value>
        ''' <returns>装備改訂№</returns>
        Public Property SobiKaiteiNo() As String
            Get
                Return _SobiKaiteiNo
            End Get
            Set(ByVal value As String)
                _SobiKaiteiNo = value
            End Set
        End Property

        ''' <summary>7桁型式識別コード</summary>
        ''' <value>7桁型式識別コード</value>
        ''' <returns>7桁型式識別コード</returns>
        Public Property KatashikiScd7() As String
            Get
                Return _KatashikiScd7
            End Get
            Set(ByVal value As String)
                _KatashikiScd7 = value
            End Set
        End Property

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

        ''' <summary>組合せコード</summary>
        ''' <value>組合せコード</value>
        ''' <returns>組合せコード</returns>
        Public Property KumiawaseCodeSo() As String
            Get
                Return _KumiawaseCodeSo
            End Get
            Set(ByVal value As String)
                _KumiawaseCodeSo = value
            End Set
        End Property

        ''' <summary>TopColorKaiteiNo</summary>
        ''' <value>TopColorKaiteiNo</value>
        ''' <returns>TopColorKaiteiNo</returns>
        Public Property TopColorKaiteiNo() As String
            Get
                Return _TopColorKaiteiNo
            End Get
            Set(ByVal value As String)
                _TopColorKaiteiNo = value
            End Set
        End Property

        ''' <summary>FukaNo</summary>
        ''' <value>FukaNo</value>
        ''' <returns>FukaNo</returns>
        Public Property FukaNo() As String
            Get
                Return _FukaNo
            End Get
            Set(ByVal value As String)
                _FukaNo = value
            End Set
        End Property
    End Class
End Namespace