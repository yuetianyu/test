Namespace Vo
    Public Class YakanSekkeiGetForCreateTsuchishoAndZairyouVo
        '' 部品番号  
        Private _BuhinNo As String

        '' 図面番号 
        Private _ZumenNo As String = ""
        '' 図面改訂No. 
        Private _ZumenKaiteiNo As String = ""

        '' 板厚 
        Private _BankoSuryo As String = ""
        '' 材料・材質 
        Private _ZairyoKijutsu As String = ""

        '' 設計通知書番号
        Private _TsuchishoNo As String = ""

        ''' <summary>部品番号</summary>
        ''' <value>部品番号</value>
        ''' <returns>部品番号</returns>
        Public Property BuhinNo() As String
            Get
                Return _BuhinNo
            End Get
            Set(ByVal value As String)
                _BuhinNo = value
            End Set
        End Property



        ''' <summary>図面番号</summary>
        ''' <value>図面番号</value>
        ''' <returns>図面番号</returns>
        Public Property ZumenNo() As String
            Get
                Return _ZumenNo
            End Get
            Set(ByVal value As String)
                _ZumenNo = value
            End Set
        End Property

        ''' <summary>図面改訂No.</summary>
        ''' <value>図面改訂No.</value>
        ''' <returns>図面改訂No.</returns>
        Public Property ZumenKaiteiNo() As String
            Get
                Return _ZumenKaiteiNo
            End Get
            Set(ByVal value As String)
                _ZumenKaiteiNo = value
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



        ''' <summary>材料・材質</summary>
        ''' <value>材料・材質</value>
        ''' <returns>材料・材質</returns>
        Public Property ZairyoKijutsu() As String
            Get
                Return _ZairyoKijutsu
            End Get
            Set(ByVal value As String)
                _ZairyoKijutsu = value
            End Set
        End Property



        ''' <summary>設計通知書番号</summary>
        ''' <value>設計通知書番号</value>
        ''' <returns>設計通知書番号</returns>
        Public Property TsuchishoNo() As String
            Get
                Return _TsuchishoNo
            End Get
            Set(ByVal value As String)
                _TsuchishoNo = value
            End Set
        End Property
    End Class
End Namespace

