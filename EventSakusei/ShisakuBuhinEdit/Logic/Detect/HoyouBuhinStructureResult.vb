Imports ShisakuCommon.Db.EBom.Vo

Namespace ShisakuBuhinEdit.Logic.Detect
    ''' <summary>
    ''' 構成展開用の情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinStructureResult
        ''' <summary>構成情報が存在しない</summary>
        Public Shared ReadOnly NOT_EXIST As HoyouBuhinStructureResult = New HoyouBuhinStructureResult

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
        Public InstlVo As TShisakuSekkeiBlockInstlVo
        ''' <summary>試作データの場合の補用部品編集情報</summary>
        Public EditVo As THoyouBuhinEditVo
        ''' <summary>呼び出し元情報</summary>
        Public YobidashiMoto As String  '2012/02/03 
        ''' <summary>
        ''' 構成の情報がEBomにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewEBom(ByVal originalBuhinNo As String, ByVal BuhinNo As String, ByVal YobidashiMoto As String) As HoyouBuhinStructureResult
            Return New HoyouBuhinStructureResult(True, True, False, originalBuhinNo, BuhinNo, Nothing, Nothing, Nothing, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が試作システムにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="InstlVo">試作設計ブロックINSTL情報</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewShisaku(ByVal InstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal YobidashiMoto As String = Nothing) As HoyouBuhinStructureResult
            Return New HoyouBuhinStructureResult(True, False, True, Nothing, InstlVo.InstlHinban, InstlVo.InstlHinbanKbn, InstlVo, Nothing, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が試作システムにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="editVo">補用部品編集情報</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewShisaku(ByVal editVo As THoyouBuhinEditVo, Optional ByVal YobidashiMoto As String = Nothing) As HoyouBuhinStructureResult
            Return New HoyouBuhinStructureResult(True, False, True, Nothing, editVo.BuhinNo, Nothing, Nothing, editVo, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が試作システムにあった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="InstlVo">試作設計ブロックINSTL情報</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewShisakuEBOM(ByVal InstlVo As TShisakuSekkeiBlockInstlVo, Optional ByVal YobidashiMoto As String = Nothing) As HoyouBuhinStructureResult
            Return New HoyouBuhinStructureResult(True, True, True, Nothing, InstlVo.InstlHinban, InstlVo.InstlHinbanKbn, InstlVo, Nothing, YobidashiMoto)
        End Function

        ''' <summary>
        ''' 構成の情報が見つからなかった場合の「構成の情報」を返す
        ''' </summary>
        ''' <param name="BuhinNo">部品番号</param>
        ''' <returns>新しい構成の情報</returns>
        ''' <remarks></remarks>
        Public Shared Function NewNotExist(ByVal BuhinNo As String, Optional ByVal BuhinNoKbn As String = Nothing) As HoyouBuhinStructureResult
            Return New HoyouBuhinStructureResult(False, False, False, Nothing, BuhinNo, BuhinNoKbn, Nothing, Nothing, Nothing)
        End Function

        Public Sub New()
        End Sub

        Private Sub New(ByVal IsExist As Boolean, ByVal IsEBom As Boolean, ByVal IsShisaku As Boolean, ByVal OriginalBuhinNo As String, ByVal BuhinNo As String, ByVal BuhinNoKbn As String, ByVal InstlVo As TShisakuSekkeiBlockInstlVo, ByVal EditVo As THoyouBuhinEditVo, ByVal YobidashiMoto As String)
            Me.IsExist = IsExist
            Me.IsEBom = IsEBom
            Me.IsShisaku = IsShisaku
            Me.BuhinNo = BuhinNo
            Me.OriginalBuhinNo = OriginalBuhinNo
            Me.BuhinNoKbn = BuhinNoKbn
            Me.InstlVo = InstlVo
            Me.EditVo = EditVo
            Me.YobidashiMoto = YobidashiMoto  '2012/02/03
        End Sub
    End Class
End Namespace