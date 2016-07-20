Imports ShisakuCommon.Db.EBom.Vo

Namespace YosanBuhinEdit.Logic.Detect
    ''' <summary>
    ''' 構成展開用の情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StructureResult
        ''' <summary>構成情報が存在しない</summary>
        Public Shared ReadOnly NOT_EXIST As StructureResult = New StructureResult

        ''' <summary>存在する部品番号か？</summary>
        Public IsExist As Boolean
        ''' <summary>E-BOMデータか？</summary>
        Public IsEBom As Boolean
        ''' <summary>試作データか？</summary>
        Public IsShisaku As Boolean
        ''' <summary>元部品番号</summary>
        Public OriginalBuhinNo As String
        ''' <summary>部品番号</summary>
        Public BuhinNo As String
        ''' <summary>部品番号区分</summary>
        Public BuhinNoKbn As String
        ''' <summary>試作データの場合の試作設計ブロックINSTL情報</summary>
        Public InstlVo As TYosanBuhinEditPatternVo
        ''' <summary>試作データの場合の補用部品編集情報</summary>
        Public EditVo As TYosanBuhinEditVo
        ''' <summary>呼び出し元情報</summary>
        Public YobidashiMoto As String  '2012/02/03 
        ''' <summary>
        ''' 構成の情報がEBomにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewEBom(ByVal originalBuhinNo As String, ByVal BuhinNo As String, ByVal YobidashiMoto As String) As StructureResult
            Return New StructureResult(True, True, False, originalBuhinNo, BuhinNo, Nothing, Nothing, Nothing, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が試作システムにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="InstlVo">試作設計ブロックINSTL情報</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewShisaku(ByVal InstlVo As TYosanBuhinEditPatternVo, Optional ByVal YobidashiMoto As String = Nothing) As StructureResult
            Return New StructureResult(True, False, True, Nothing, InstlVo.PatternName, Nothing, InstlVo, Nothing, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が試作システムにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="editVo">補用部品編集情報</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewShisaku(ByVal editVo As TYosanBuhinEditVo, Optional ByVal YobidashiMoto As String = Nothing) As StructureResult
            Return New StructureResult(True, False, True, Nothing, editVo.YosanBuhinNo, Nothing, Nothing, editVo, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が試作システムにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="InstlVo">試作設計ブロックINSTL情報</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewShisakuEBOM(ByVal InstlVo As TYosanBuhinEditPatternVo, Optional ByVal YobidashiMoto As String = Nothing) As StructureResult
            Return New StructureResult(True, True, True, Nothing, InstlVo.PatternName, Nothing, InstlVo, Nothing, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が見つからなかった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewNotExist(ByVal BuhinNo As String, Optional ByVal BuhinNoKbn As String = Nothing) As StructureResult
            Return New StructureResult(False, False, False, Nothing, BuhinNo, BuhinNoKbn, Nothing, Nothing, Nothing)
        End Function

        Public Sub New()
        End Sub

        Private Sub New(ByVal IsExist As Boolean, ByVal IsEBom As Boolean, ByVal IsShisaku As Boolean, ByVal OriginalBuhinNo As String, ByVal BuhinNo As String, ByVal BuhinNoKbn As String, ByVal InstlVo As TYosanBuhinEditPatternVo, ByVal EditVo As TYosanBuhinEditVo, ByVal YobidashiMoto As String)
            Me.IsExist = IsExist
            Me.IsEBom = IsEBom
            Me.IsShisaku = IsShisaku
            Me.BuhinNo = BuhinNo
            Me.OriginalBuhinNo = OriginalBuhinNo
            Me.BuhinNoKbn = BuhinNoKbn
            Me.InstlVo = InstlVo
            Me.EditVo = EditVo
            Me.YobidashiMoto = YobidashiMoto
        End Sub
    End Class
End Namespace