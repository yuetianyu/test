Imports EventSakusei.ShisakuBuhinEdit.Kosei.Logic.Tree

Namespace ShisakuBuhinEdit.Kosei.Logic.Merge
    ''' <summary>
    ''' Nodeアクセッサinterface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IBuhinNodeAccessor(Of K, B)
        ''' <summary>
        ''' 部品番号を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>部品番号</returns>
        ''' <remarks></remarks>
        Function GetBuhinNoFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 部品名称を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>部品名称</returns>
        ''' <remarks></remarks>
        Function GetBuhinNameFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 部品番号試作区分を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>部品番号試作区分</returns>
        ''' <remarks></remarks>
        Function GetBuhinNoKbnFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 改訂を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>改訂</returns>
        ''' <remarks></remarks>
        Function GetBuhinNoKaiteiNoFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 枝番を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>枝番</returns>
        ''' <remarks></remarks>
        Function GetEdaBanFrom(ByVal node As BuhinNode(Of K, B)) As String

        ''' <summary>
        ''' メーカーコードを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>メーカーコード</returns>
        ''' <remarks></remarks>
        Function GetMakerCodeFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' メーカー名を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>メーカー名</returns>
        ''' <remarks></remarks>
        Function GetMakerNameFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 国内集計コードを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>国内集計コード</returns>
        ''' <remarks></remarks>
        Function GetShukeiCodeFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 海外SIA集計コードを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>海外SIA集計コード</returns>
        ''' <remarks></remarks>
        Function GetSiaShukeiCodeFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 現調CKD区分を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>現調CKD区分</returns>
        ''' <remarks></remarks>
        Function GetGencyoCkdKbnFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 国内集計コードを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>国内集計コード</returns>
        ''' <remarks></remarks>
        Function GetBuhinShukeiCodeFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 海外SIA集計コードを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>海外SIA集計コード</returns>
        ''' <remarks></remarks>
        Function GetBuhinSiaShukeiCodeFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 現調CKD区分を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>現調CKD区分</returns>
        ''' <remarks></remarks>
        Function GetBuhinGencyoCkdKbnFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 出図予定年月日を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>出図予定年月日</returns>
        ''' <remarks></remarks>
        Function GetShutuzuYoteiDateFrom(ByVal node As BuhinNode(Of K, B)) As Nullable(Of Int32)
        ''' <summary>
        ''' 再使用不可を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>再使用不可</returns>
        ''' <remarks></remarks>
        Function GetSaishiyoufuka(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 供給セクションを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>供給セクション</returns>
        ''' <remarks>2012/01/23 供給セクション追加</remarks>
        Function GetKyoukuSection(ByVal node As BuhinNode(Of K, B)) As String

        '2014/09/23 酒井 ADD
        Function GetBaseBuhinFlg(ByVal node As BuhinNode(Of K, B)) As String

        '2015/09/08 ADD
        Function GetBaseBuhinSeq(ByVal node As BuhinNode(Of K, B)) As Nullable(Of Int32)

        Function GetInstlDataKbn(ByVal node As BuhinNode(Of K, B)) As String

        ''' <summary>
        ''' 部品ノートを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>部品ノート</returns>
        ''' <remarks>2012/02/10 部品ノート追加</remarks>
        Function GetBuhinNote(ByVal node As BuhinNode(Of K, B)) As String

    End Interface
End Namespace