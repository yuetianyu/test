Imports System.Xml
Imports EBom.Data

Namespace Db.Sql
    Public Class ShisakuSqlAnalyzer
        Private ReadOnly sql As String
        Private ReadOnly bindParamValue As Object

        Private tagAnalyzer As SqlXmlAnalyzer
        Private bindAnalyzer As SqlBindAnalyzer
        ''' <summary>解析後のSQL</summary>
        ''' <returns>解析後のSQL</returns>
        Public ReadOnly Property AnalyzedSql() As String
            Get
                Return tagAnalyzer.AnalyzedSql
            End Get
        End Property

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="sql">SQL</param>
        ''' <param name="bindParamValue">埋め込みパラメータを持つobject</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal sql As String, ByVal bindParamValue As Object)
            Me.sql = sql
            Me.bindParamValue = bindParamValue
        End Sub

        ''' <summary>
        ''' SQLを解析する
        ''' </summary>
        ''' <remarks>解析の結果、SQL文の構成・埋め込みパラメータの抽出</remarks>
        Public Sub Analyze()
            'Dim doc As New XmlDocument
            Dim encloser As New SqlCDataEncloser("if", "where", "set")
            Dim sqlWithTag As String = "<sql>" & encloser.Enclose(sql) & "</sql>"
            tagAnalyzer = New SqlXmlAnalyzer(sqlWithTag, bindParamValue, New SqlExpressionEvaluator)
            tagAnalyzer.Analyze()
            bindAnalyzer = New SqlBindAnalyzer(tagAnalyzer.AnalyzedSql, bindParamValue)
            bindAnalyzer.Analyze()
        End Sub

        ''' <summary>
        ''' パラメータを SqlAccess に追加設定する
        ''' </summary>
        ''' <param name="db">パラメータを追加する SqlAccess</param>
        ''' <remarks></remarks>
        Public Sub AddParametersTo(ByVal db As SqlAccess)
            bindAnalyzer.AddAllTo(db)
        End Sub
    End Class
End Namespace