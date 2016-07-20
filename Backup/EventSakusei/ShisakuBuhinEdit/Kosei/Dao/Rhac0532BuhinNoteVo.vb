Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Kosei.Dao
    ''' <summary>
    ''' 部品情報に部品ノートを追加したVO
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Rhac0532BuhinNoteVo : Inherits Rhac0532MakerNameVo

        '' 部品ノート
        Private _BuhinNote As String

        ''' <summary>部品ノート</summary>
        ''' <value>部品ノート</value>
        ''' <returns>部品ノート</returns>
        Public Property BuhinNote() As String
            '2012/01/25 親クラスであるRhac0532voのChukiKijutsuを返します。
            Get
                Return Me.ChukiKijutsu
            End Get
            Set(ByVal value As String)
                _BuhinNote = value
            End Set
        End Property
    End Class
End Namespace