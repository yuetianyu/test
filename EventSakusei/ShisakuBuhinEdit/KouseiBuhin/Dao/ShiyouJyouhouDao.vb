Imports EventSakusei.ShisakuBuhinEdit.KouseiBuhin.Dao.Vo
Imports ShisakuCommon.Util.LabelValue
Imports ShisakuCommon.Db.EBom.Vo

Namespace KouseiBuhin.Dao

    ''' <summary>
    ''' 仕様情報
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ShiyouJyouhouDao

        ''' <summary>仕様情報№を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShiyouJyouhouNoLabelValues(ByVal KaihatsuFugo As String) As List(Of LabelValueVo)

        ''' <summary>仕様情報№の最新を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByNewShiyouJyouhouNo(ByVal KaihatsuFugo As String) As Rhac0030Vo

        ''' <summary>車型を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetBySyagataLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>ｸﾞﾚｰﾄﾞを取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByGradeLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>仕向地・仕向けを取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShimukechiShimukeLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>仕向地・ﾊﾝﾄﾞﾙを取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByShimukechiHandleLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>E/G・排気量を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByEgHaikiryouLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>E/G・形式を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByEgKeishikiLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>E/G・過給器を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByEgKakyukiLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>T/M・駆動方式を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByTmKudouLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>T/M・変速機を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByTmHensokukiLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>７桁型式</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByKatashiki7LabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>仕向地コード</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByKataShimukeLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>仕向けコード(HELP)を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByKataShimukeHelpValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>ＯＰコード</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByKataOpLabelValues(ByVal KaihatsuFugo As String, _
                                                ByVal ShiyouJyouhouNo As String, _
                                                ByVal syagata As String, _
                                                ByVal Grade As String, _
                                                ByVal Handle As String, _
                                                ByVal Haikiryo As String, _
                                                ByVal Keishiki As String, _
                                                ByVal Kakyuki As String, _
                                                ByVal Kudou As String, _
                                                ByVal Mission As String, _
                                                ByVal Katashiki7 As String, _
                                                ByVal Shimukechi As String, _
                                                ByVal OpCode As String) As List(Of LabelValueVo)

        ''' <summary>ＯＰ項目列を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByOpKoumokuRetuValues(ByVal KaihatsuFugo As String) As List(Of OpListVo)

        ''' <summary>ＯＰスペック情報を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByOpSpecValues(ByVal KaihatsuFugo As String, ByVal ShiyouJyouhouNo As String) As List(Of OpSpecListVo)

        ''↓↓2014/07/31 Ⅰ.3.設計編集 ベース車改修専用化_ba) (TES)張 CHG BEGIN
        'Function GetByFinalHinbanValues(ByVal KaihatsuFugo As String, _
        '                              ByVal BlockNo As String, _
        '                              ByVal ShiyouJyouhouNo As String, _
        '                              ByVal syagata As String, _
        '                              ByVal Grade As String, _
        '                              ByVal Handle As String, _
        '                              ByVal Haikiryo As String, _
        '                              ByVal Keishiki As String, _
        '                              ByVal Kakyuki As String, _
        '                              ByVal Kudou As String, _
        '                              ByVal Mission As String, _
        '                              ByVal Katashiki7 As String, _
        '                              ByVal Shimukechi As String, _
        '                              ByVal OpCode As String) As List(Of FinalHinbanListVo)
        ''' <summary>ファイナル品番を取得</summary>
        ''' <param name="KaihatsuFugo"></param>
        ''' <param name="BlockNo"></param>
        ''' <param name="ShiyouJyouhouNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByFinalHinbanValues(ByVal KaihatsuFugo As String, _
                                        ByVal BlockNo As String, _
                                        ByVal fBuhinNo As String, _
                                        ByVal ShiyouJyouhouNo As String, _
                                        ByVal syagata As String, _
                                        ByVal Grade As String, _
                                        ByVal Handle As String, _
                                        ByVal Haikiryo As String, _
                                        ByVal Keishiki As String, _
                                        ByVal Kakyuki As String, _
                                        ByVal Kudou As String, _
                                        ByVal Mission As String, _
                                        ByVal Katashiki7 As String, _
                                        ByVal Shimukechi As String, _
                                        ByVal OpCode As String) As List(Of FinalHinbanListVo)
        ''↑↑2014/07/31 Ⅰ.3.設計編集 ベース車改修専用化_ba) (TES)張 CHG END

        ''' <summary>RHAC0533を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByRHAC0533Values(ByVal BuhinNo As String) As List(Of Rhac0533Vo)

        ''' <summary>RHAC0532を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByRHAC0532Values(ByVal BuhinNo As String) As List(Of Rhac0532Vo)

        ''' <summary>RHAC0553を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByRHAC0553Values(ByVal KaihatsuFugo As String, ByVal BuhinNo As String) As List(Of Rhac0553Vo)

        ''' <summary>RHAC0532を取得</summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetByRHAC0552Values(ByVal BuhinNo As String) As List(Of Rhac0552Vo)

    End Interface

End Namespace
