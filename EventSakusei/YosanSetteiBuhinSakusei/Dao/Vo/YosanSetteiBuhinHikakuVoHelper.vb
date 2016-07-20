Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanSetteiBuhinSakusei.Dao
    Public Class YosanSetteiBuhinHikakuVoHelper : Inherits TShisakuBuhinEditTmpVo

        '部品編集かベースとなる部品編集かの判定'
        Private _EditBaseFlag As Integer

        '員数数量'
        Private _InsuSuryo As Integer

        '試作号車表示順'
        Private _ShisakuGousyaHyoujiJun As Integer

        ''' <summary>部品編集かベースとなる部品編集かの判定</summary>
        ''' <value>部品編集かベースとなる部品編集かの判定</value>
        ''' <returns>部品編集かベースとなる部品編集かの判定</returns>
        Public Property EditBaseFlag() As String
            Get
                Return _EditBaseFlag
            End Get
            Set(ByVal value As String)
                _EditBaseFlag = value
            End Set
        End Property

        ''' <summary>員数数量</summary>
        ''' <value>員数数量</value>
        ''' <returns>員数数量</returns>
        Public Property InsuSuryo() As String
            Get
                Return _InsuSuryo
            End Get
            Set(ByVal value As String)
                _InsuSuryo = value
            End Set
        End Property

        ''' <summary>試作号車表示順</summary>
        ''' <value>試作号車表示順</value>
        ''' <returns>試作号車表示順</returns>
        Public Property ShisakuGousyaHyoujiJun() As String
            Get
                Return _ShisakuGousyaHyoujiJun
            End Get
            Set(ByVal value As String)
                _ShisakuGousyaHyoujiJun = value
            End Set
        End Property

    End Class
End Namespace