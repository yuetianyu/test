﻿Imports ShisakuCommon.Db.EBom.Vo

Namespace TehaichoMenu.Vo
    Public Class TShisakuTehaiTeiseiKihonVoHelper : Inherits TShisakuTehaiTeiseiKihonVo

        Private _ShisakuGousya As String
        Private _ShisakuGousyaHyoujiJun As Integer
        Private _InsuSuryo As Integer
        Private _MFlag As String

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

        ''' <summary>試作号車表示順</summary>
        ''' <value>試作号車表示順</value>
        ''' <returns>試作号車表示順</returns>
        Public Property ShisakuGousyaHyoujiJun() As Integer
            Get
                Return _ShisakuGousyaHyoujiJun
            End Get
            Set(ByVal value As Integer)
                _ShisakuGousyaHyoujiJun = value
            End Set
        End Property

        ''' <summary>表示順変わったフラグ</summary>
        ''' <value>表示順変わったフラグ</value>
        ''' <returns>表示順変わったフラグ</returns>
        Public Property MFlag() As String
            Get
                Return _MFlag
            End Get
            Set(ByVal value As String)
                _MFlag = value
            End Set
        End Property


    End Class
End Namespace