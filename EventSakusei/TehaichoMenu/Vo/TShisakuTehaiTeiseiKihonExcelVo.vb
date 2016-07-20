
Imports ShisakuCommon.Db.EBom.Vo

Namespace TehaichoMenu.Vo
    '手配帳エラーチェック用のVOクラス'
    Public Class TShisakuTehaiTeiseiKihonExcelVo : Inherits TShisakuBuhinEditVoSekkeiHelper 'TShisakuTehaiKihonVo

        '''<summary>補助名称</summary>
        Private _HojoName As String

        '''<summary>材質</summary>
        Private _ZairyoKijutsu As String

        '''<summary>板厚</summary>部品サイズ
        Private _BankoSuryo As String

        '''<summary>部品サイズ</summary>
        Private _MaterialInfo As String

        '''<summary>部品サイズ０３</summary>
        Private _MaterialInfoHeight As String

        ''' <summary>補助名称</summary>
        ''' <value>補助名称o</value>
        ''' <returns>補助名称</returns>
        Public Property HojoName() As String
            Get
                Return _HojoName
            End Get
            Set(ByVal value As String)
                _HojoName = value
            End Set
        End Property

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

        ''' <summary>部品サイズ</summary>
        ''' <value>部品サイズ</value>
        ''' <returns>部品サイズ</returns>
        Public Property MaterialInfo() As String
            Get
                Return _MaterialInfo
            End Get
            Set(ByVal value As String)
                _MaterialInfo = value
            End Set
        End Property

        ''' <summary>部品サイズ０３</summary>
        ''' <value>部品サイズ０３</value>
        ''' <returns>部品サイズ０３</returns>
        Public Property MaterialInfoHeight() As String
            Get
                Return _MaterialInfoHeight
            End Get
            Set(ByVal value As String)
                _MaterialInfoHeight = value
            End Set
        End Property

    End Class
End Namespace