﻿Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinMenu.Dao
    Public Class SekkeiBlockInstlResultVo : Inherits TShisakuSekkeiBlockInstlVo
        '' ブロック名称
        Private _ShisakuBlockName As String
        ' ベース車情報の号車
        Private _baseShisakuGousya As String

        Private _ShisakuKaihatsuFugo As String

        ''' <summary>ブロック名称</summary>
        ''' <value>ブロック名称</value>
        ''' <returns>ブロック名称</returns>
        Public Property ShisakuBlockName() As String
            Get
                Return _ShisakuBlockName
            End Get
            Set(ByVal value As String)
                _ShisakuBlockName = value
            End Set
        End Property

        ''' <summary>ベース車情報の号車</summary>
        ''' <value>ベース車情報の号車</value>
        ''' <returns>ベース車情報の号車</returns>
        Public Property BaseShisakuGousya() As String
            Get
                Return _baseShisakuGousya
            End Get
            Set(ByVal value As String)
                _baseShisakuGousya = value
            End Set
        End Property


        ''' <summary>ベース車情報の号車</summary>
        ''' <value>ベース車情報の号車</value>
        ''' <returns>ベース車情報の号車</returns>
        Public Property ShisakuKaihatsuFugo() As String
            Get
                Return _ShisakuKaihatsuFugo
            End Get
            Set(ByVal value As String)
                _ShisakuKaihatsuFugo = value
            End Set
        End Property
    End Class
End Namespace