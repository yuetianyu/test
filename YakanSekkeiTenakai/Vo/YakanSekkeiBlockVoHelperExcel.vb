Imports ShisakuCommon.Db.EBom.Vo

Namespace Vo
    Public Class YakanSekkeiBlockVoHelperExcel : Inherits YakanSekkeiBlockVo
        ''' <summary>INSTL品番表示順</summary>
        Private _InstlHinbanHyoujiJun As String

        ''' <summary>試作号車</summary>
        Private _ShisakuGousya As String

        ''' <summary>員数数量</summary>
        Private _InsuSuryo As Integer

        ''' <summary>号車表示順</summary>
        Private _HyojijunNo As Integer

        ''' <summary>担当ユーザーID</summary>
        Private _UserId As String

        ''' <summary>電話番号</summary>
        Private _TelNo As String

        '''' <summary>フラグ</summary>
        'Private _Flag As Boolean

        '''<summary>材質</summary>
        Private _ZairyoKijutsu As String

        '''<summary>板厚</summary>
        Private _BankoSuryo As String

        '''<summary>改訂</summary>
        Private _ZumenKaiteiNo As String

        ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        '''<summary>改訂</summary>
        Private _TsuchishoNo As String
        ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END

        '''<summary>シンボル</summary>
        Private _Symbol As String

        ''' <summary>INSTL品番表示順</summary>
        ''' <value>INSTL品番表示順</value>
        ''' <returns>INSTL品番表示順</returns>
        Public Property InstlHinbanHyoujiJun() As String
            Get
                Return _InstlHinbanHyoujiJun
            End Get
            Set(ByVal value As String)
                _InstlHinbanHyoujiJun = value
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

        ''' <summary>担当ユーザーID</summary>
        ''' <value>担当ユーザーID</value>
        ''' <returns>担当ユーザーID</returns>
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        ''' <summary>電話番号</summary>
        ''' <value>電話番号</value>
        ''' <returns>電話番号</returns>
        Public Property TelNo() As String
            Get
                Return _TelNo
            End Get
            Set(ByVal value As String)
                _TelNo = value
            End Set
        End Property

        ''' <summary>員数数量</summary>
        ''' <value>員数数量</value>
        ''' <returns>員数数量</returns>
        Public Property InsuSuryo() As Integer
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As Integer)
                _InsuSuryo = value
            End Set
        End Property

        ''' <summary>号車表示順</summary>
        ''' <value>号車表示順</value>
        ''' <returns>号車表示順</returns>
        Public Property HyojijunNo() As Integer
            Get
                Return _HyojijunNo
            End Get
            Set(ByVal value As Integer)
                _HyojijunNo = value
            End Set
        End Property

        'Public Property Flag() As Boolean
        '    Get
        '        Return _Flag
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _Flag = value
        '    End Set
        'End Property

        ''' <summary>材質</summary>
        ''' <value>材質</value>
        ''' <returns>材質</returns>
        Public Property ZairyoKijutsu() As String
            Get
                Return _ZairyoKijutsu
            End Get
            Set(ByVal value As String)
                _ZairyoKijutsu = value
            End Set
        End Property

        ''' <summary>板厚</summary>
        ''' <value>板厚</value>
        ''' <returns>板厚</returns>
        Public Property BankoSuryo() As String
            Get
                Return _BankoSuryo
            End Get
            Set(ByVal value As String)
                _BankoSuryo = value
            End Set
        End Property

        ''' <summary>改訂</summary>
        ''' <value>改訂</value>
        ''' <returns>改訂</returns>
        Public Property ZumenKaiteiNo() As String
            Get
                Return _ZumenKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZumenKaiteiNo = value
            End Set
        End Property

        ''↓↓2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD BEGIN
        Public Property TsuchishoNo() As String
            Get
                Return _TsuchishoNo
            End Get
            Set(ByVal value As String)
                _TsuchishoNo = value
            End Set
        End Property
        ''↑↑2014/08/28 Ⅰ.5.EBOM差分出力 be) 酒井 ADD END

        ''' <summary>シンボル</summary>
        ''' <value>シンボル</value>
        ''' <returns>シンボル</returns>
        Public Property Symbol() As String
            Get
                Return _Symbol
            End Get
            Set(ByVal value As String)
                _Symbol = value
            End Set
        End Property

    End Class
End Namespace
