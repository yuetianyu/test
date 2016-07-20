Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Vo

Namespace Db.EBom.Exclusion
    ''' <summary>
    ''' 補用イベント情報テーブル用の排他制御クラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ThoyouEventExclusion : Inherits TableExclusion(Of THoyouEventVo)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New(New THoyouEventDaoImpl)
        End Sub

        ''' <summary>
        ''' 排他制御に必要な情報を保存します
        ''' </summary>
        ''' <param name="hoyouEventCode">補用イベントコード</param>
        ''' <remarks></remarks>
        Public Overloads Sub Save(ByVal hoyouEventCode As String)
            If Not hoyouEventCode.Equals(String.Empty) Then
                MyBase.Save(hoyouEventCode)
            End If
        End Sub

        ''' <summary>
        ''' ステータスが変更されたかを返す
        ''' </summary>
        ''' <returns>変更された場合、true</returns>
        ''' <remarks></remarks>
        Public Function WasChangedStatus() As Boolean
            If saveVo Is Nothing OrElse checkVo Is Nothing Then
                Throw New InvalidOperationException("#WasUpdatedBySomeone() で評価しないと使用できません.")
            End If
            Return EzUtil.IsNotEqualIfNull(saveVo.Status, checkVo.Status)
        End Function
    End Class
End Namespace