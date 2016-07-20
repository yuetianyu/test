Imports YosansyoTool.YosanBuhinEdit.Kosei.Logic.Tree

Namespace YosanBuhinEdit.Kosei.Logic.Merge
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

        '''' <summary>
        '''' 部課コードを返す
        '''' </summary>
        '''' <param name="node">データ元</param>
        '''' <returns>部課コード</returns>
        '''' <remarks></remarks>
        'Function GetBukaCodeFrom(ByVal node As BuhinNode(Of K, B)) As String
        '''' <summary>
        '''' 部品番号区分を返す
        '''' </summary>
        '''' <param name="node">データ元</param>
        '''' <returns>部品番号区分</returns>
        '''' <remarks></remarks>
        'Function GetBuhinNoKbnFrom(ByVal node As BuhinNode(Of K, B)) As String
        '''' <summary>
        '''' ブロックを返す
        '''' </summary>
        '''' <param name="node">データ元</param>
        '''' <returns>ブロック</returns>
        '''' <remarks></remarks>
        'Function GetBlockNoFrom(ByVal node As BuhinNode(Of K, B)) As String
        '''' <summary>
        '''' レベルを返す
        '''' </summary>
        '''' <param name="node">データ元</param>
        '''' <returns>レベル</returns>
        '''' <remarks></remarks>
        'Function GetLevelFrom(ByVal node As BuhinNode(Of K, B)) As String
        ''' <summary>
        ''' 員数を返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>員数</returns>
        ''' <remarks></remarks>
        Function GetInsuVoFrom(ByVal node As BuhinNode(Of K, B)) As String
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
        '''' <summary>
        '''' 現調CKD区分を返す
        '''' </summary>
        '''' <param name="node">データ元</param>
        '''' <returns>現調CKD区分</returns>
        '''' <remarks></remarks>
        'Function GetBuhinGencyoCkdKbnFrom(ByVal node As BuhinNode(Of K, B)) As String

        ''' <summary>
        ''' 供給セクションを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>供給セクション</returns>
        ''' <remarks>2012/01/23 供給セクション追加</remarks>
        Function GetKyoukuSection(ByVal node As BuhinNode(Of K, B)) As String

        ''' <summary>
        ''' 部品ノートを返す
        ''' </summary>
        ''' <param name="node">データ元</param>
        ''' <returns>部品ノート</returns>
        ''' <remarks>2012/02/10 部品ノート追加</remarks>
        Function GetBuhinNote(ByVal node As BuhinNode(Of K, B)) As String

    End Interface
End Namespace