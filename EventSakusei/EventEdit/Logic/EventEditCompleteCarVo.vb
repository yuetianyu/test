Imports ShisakuCommon.Db.EBom.Vo

Namespace EventEdit.Logic
    Public Class EventEditCompleteCarVo : Inherits TShisakuEventKanseiVo

        '' 試作種別
        Private _ShisakuSyubetu As String
        '' 試作号車
        Private _ShisakuGousya As String

        '' 試作E/Gメモ１のラベル
        Private _ShisakuEgMemo1Label As String
        '' 試作E/Gメモ２のラベル
        Private _ShisakuEgMemo2Label As String

        '' 試作T/Mメモ１のラベル
        Private _ShisakuTmMemo1Label As String
        '' 試作T/Mメモ２のラベル
        Private _ShisakuTmMemo2Label As String


        ''' <summary>試作種別</summary>
        ''' <value>試作種別</value>
        ''' <returns>試作種別</returns>
        Public Property ShisakuSyubetu() As String
            Get
                Return _ShisakuSyubetu
            End Get
            Set(ByVal value As String)
                _ShisakuSyubetu = value
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

        ''' <summary>試作E/Gメモ１のラベル</summary>
        ''' <value>試作E/Gメモ１のラベル</value>
        ''' <returns>試作E/Gメモ１のラベル</returns>
        Public Property ShisakuEgMemo1Label() As String
            Get
                Return _ShisakuEgMemo1Label
            End Get
            Set(ByVal value As String)
                _ShisakuEgMemo1Label = value
            End Set
        End Property

        ''' <summary>試作E/Gメモ２のラベル</summary>
        ''' <value>試作E/Gメモ２のラベル</value>
        ''' <returns>試作E/Gメモ２のラベル</returns>
        Public Property ShisakuEgMemo2Label() As String
            Get
                Return _ShisakuEgMemo2Label
            End Get
            Set(ByVal value As String)
                _ShisakuEgMemo2Label = value
            End Set
        End Property

        ''' <summary>試作T/Mメモ１のラベル</summary>
        ''' <value>試作T/Mメモ１のラベル</value>
        ''' <returns>試作T/Mメモ１のラベル</returns>
        Public Property ShisakuTmMemo1Label() As String
            Get
                Return _ShisakuTmMemo1Label
            End Get
            Set(ByVal value As String)
                _ShisakuTmMemo1Label = value
            End Set
        End Property

        ''' <summary>試作T/Mメモ２のラベル</summary>
        ''' <value>試作T/Mメモ２のラベル</value>
        ''' <returns>試作T/Mメモ２のラベル</returns>
        Public Property ShisakuTmMemo2Label() As String
            Get
                Return _ShisakuTmMemo2Label
            End Get
            Set(ByVal value As String)
                _ShisakuTmMemo2Label = value
            End Set
        End Property

    End Class
End Namespace