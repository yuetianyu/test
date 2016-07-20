Namespace Db.EBom.Vo
    ''' <summary>ユーザー電話番号情報</summary>
    Public Class TShisakuTelNoVo
        ''' <summary>ユーザーID</summary>
        Private _UserId As String
        ''' <summary>電話番号</summary>
        Private _TelNo As String

        ''' <summary>ユーザーID</summary>
        ''' <value>ユーザーID</value>
        ''' <returns>ユーザーID</returns>
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

    End Class
End Namespace