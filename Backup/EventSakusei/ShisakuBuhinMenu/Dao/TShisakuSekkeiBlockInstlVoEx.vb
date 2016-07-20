Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class TShisakuSekkeiBlockInstlVoEx : Inherits TShisakuSekkeiBlockInstlVo



        '' INSTL元データ区分
        Private _InstlDataKbn2 As String
        '' ベース情報フラグ
        Private _BaseInstlFlg2 As String

        Private _OldInstlHinbanHyoujiJun As Integer

        ''' <summary>INSTL元データ区分</summary>
        ''' <value>INSTL元データ区分</value>
        ''' <returns>INSTL元データ区分</returns>
        Public Property InstlDataKbn2() As String
            Get
                Return _InstlDataKbn2
            End Get
            Set(ByVal value As String)
                _InstlDataKbn2 = value
            End Set
        End Property

        ''' <summary>ベース情報フラグ</summary>
        ''' <value>ベース情報フラグ</value>
        ''' <returns>ベース情報フラグ</returns>
        Public Property BaseInstlFlg2() As String
            Get
                Return _BaseInstlFlg2
            End Get
            Set(ByVal value As String)
                _BaseInstlFlg2 = value
            End Set
        End Property

        Public Property OldInstlHinbanHyoujiJun() As Integer
            Get
                Return _OldInstlHinbanHyoujiJun
            End Get
            Set(ByVal value As Integer)
                _OldInstlHinbanHyoujiJun = value
            End Set
        End Property
    End Class

End Namespace

