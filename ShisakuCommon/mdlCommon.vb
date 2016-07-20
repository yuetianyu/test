Imports EBom.Common
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports Microsoft.Office.Interop

''' <summary>
''' 試作システム グローバル共有モジュール
''' </summary>
''' <remarks></remarks>
Public Module SeisakuGlobal

    '項目の開始列
    Public CONT_START_COL As Long
    '項目の列位置
    'ベースのみ
    Public B_KFUGO_POS As Long
    Public B_SHIYONO_POS As Long
    Public B_SHASHU_POS As Long
    Public B_GRADE_POS As Long
    Public B_SHIMUKE_POS As Long
    Public B_HUNDLE_POS As Long
    Public B_HAIKIRYO_POS As Long
    Public B_KEISHIKI_POS As Long
    Public B_KAKYUKI_POS As Long
    Public B_KUDO_POS As Long
    Public B_MISSION_POS As Long
    Public B_KATASHIKI_POS As Long
    Public B_SHIMUKECODE_POS As Long
    Public B_OPCODE_POS As Long
    Public B_GAISOCODE_POS As Long
    Public B_GAISONAME_POS As Long
    Public B_NAISOCODE_POS As Long
    Public B_NAISONAME_POS As Long
    Public B_OPSPEC_POS As Long
    Public B_SYATAINO_POS As Long
    Public B_OPSPEC_COUNT As Long
    Public B_SHISAKUEVENT_POS As Long
    Public B_SHIYOMOKUTEKI_POS As Long
    Public B_SHUYOKAKUNIN_POS As Long
    Public B_BUSHO_POS As Long
    Public B_JUNJYO_POS As Long
    Public B_GROUP_POS As Long
    Public B_KANSEIKIBOBI_POS As Long
    Public B_MEMO_POS As Long

    '完成車
    Public C_SEIHO_POS As Long
    Public C_SYATAINO_POS As Long
    Public C_GOUSYA_POS As Long
    Public C_EG_POS As Long
    Public C_ISS_POS As Long
    Public C_TM_POS As Long
    Public C_RD_POS As Long
    Public C_KFUGO_POS As Long
    Public C_SHIYONO_POS As Long
    Public C_SHASHU_POS As Long
    Public C_GRADE_POS As Long
    Public C_SHIMUKE_POS As Long
    Public C_HUNDLE_POS As Long
    Public C_HAIKIRYO_POS As Long
    Public C_KEISHIKI_POS As Long
    Public C_KAKYUKI_POS As Long
    Public C_KUDO_POS As Long
    Public C_MISSION_POS As Long
    Public C_KATASHIKI_POS As Long
    Public C_SHIMUKECODE_POS As Long
    Public C_OPCODE_POS As Long
    Public C_GAISOCODE_POS As Long
    Public C_GAISONAME_POS As Long
    Public C_NAISOCODE_POS As Long
    Public C_NAISONAME_POS As Long
    Public C_OPSPEC_POS As Long

    Public C_OPSPEC_COUNT As Long

    '完成車情報開始列
    Public COMP_ST_COL As Long
    'ベース情報開始列
    Public BASE_ST_COL As Long
    '完成車情報OPスペック数
    Public COMP_OPSPEC_CNT As Long
    'ベース情報OPスペック数
    Public BASE_OPSPEC_CNT As Long

    'ベース車の特別織込みスタート列数及び項目列数
    Public BASE_TOKUBETU_ST_COL As Long
    Public BASE_TOKUBETU_CNT As Long
    '試作車の特別織込みスタート列数及び項目列数
    Public SHISAKU_TOKUBETU_ST_COL As Long
    Public SHISAKU_TOKUBETU_CNT As Long

    '以下はWB車の変動用として用意。
    Public WB_SHIYOUMOKUTEKI_POS As Long
    Public WB_SHUYOUKAKUNINKOUMOKU_POS As Long
    Public WB_SHIYOUBUSYO_POS As Long
    Public WB_SEISAKUJUNJYO_POS As Long
    Public WB_SEISAKUGROUP_POS As Long
    Public WB_KANSEIKIBOUBI_POS As Long
    Public WB_SHUYOUKAKUNINKBN_POS As Long

    Public WB_SHIYO_START_POS As Long

    Public WB_MEMO_POS As Long

    Public WB_MEMO As Long

    Public WB_SHIYOU_COUNT As Long

    '確認画面用
    Public frmNo5ParaModori As String
    Public frmNo5Para As String

    '******************************************************************************
    ' setContPosition
    ' 各項目の列番号を取得する
    '******************************************************************************
    ''' <summary>
    ''' 各項目の列番号を取得する
    ''' </summary>
    ''' <param name="cate">完成車情報orベース車情報</param>
    ''' <param name="lngStartPos">スタートカラム</param>
    ''' <param name="flgMode">モード（TRUE：印刷書式、FALSE：編集書式）</param>
    ''' <remarks></remarks>
    Public Sub setContPosition(ByVal cate As String, ByVal lngStartPos As Long, ByVal flgMode As Boolean)

        '項目の開始列
        CONT_START_COL = lngStartPos

        '完成車情報の列位置設定
        If cate = "COMP" Then
            If StringUtil.Equals(flgMode, True) Then
                '項目の列位置
                '   追加した項目
                C_GOUSYA_POS = CONT_START_COL + COMP_GOUSYA_POS
                C_EG_POS = CONT_START_COL + COMP_EG_POS
                C_ISS_POS = CONT_START_COL + COMP_ISS_POS
                C_TM_POS = CONT_START_COL + COMP_TM_POS
                C_RD_POS = CONT_START_COL + COMP_RD_POS
                C_SYATAINO_POS = CONT_START_COL + COMP_SYATAINO_POS

                C_KFUGO_POS = CONT_START_COL + COMP_KFUGO_POS
                C_SHIYONO_POS = CONT_START_COL + COMP_SHIYONO_POS
                C_SHASHU_POS = CONT_START_COL + COMP_SHASHU_POS
                C_GRADE_POS = CONT_START_COL + COMP_GRADE_POS
                C_SHIMUKE_POS = CONT_START_COL + COMP_SHIMUKE_POS
                C_HUNDLE_POS = CONT_START_COL + COMP_HUNDLE_POS
                C_HAIKIRYO_POS = CONT_START_COL + COMP_HAIKIRYO_POS
                C_KEISHIKI_POS = CONT_START_COL + COMP_KEISHIKI_POS
                C_KAKYUKI_POS = CONT_START_COL + COMP_KAKYUKI_POS
                C_KUDO_POS = CONT_START_COL + COMP_KUDO_POS
                C_MISSION_POS = CONT_START_COL + COMP_MISSION_POS
                C_KATASHIKI_POS = CONT_START_COL + COMP_KATASHIKI_POS
                C_SHIMUKECODE_POS = CONT_START_COL + COMP_SHIMUKECODE_POS
                C_OPCODE_POS = CONT_START_COL + COMP_OPCODE_POS
                C_GAISOCODE_POS = CONT_START_COL + COMP_GAISOCODE_POS
                C_GAISONAME_POS = CONT_START_COL + COMP_GAISONAME_POS
                C_NAISOCODE_POS = CONT_START_COL + COMP_NAISOCODE_POS
                C_NAISONAME_POS = CONT_START_COL + COMP_NAISONAME_POS
                C_OPSPEC_POS = CONT_START_COL + COMP_OPSPEC_POS
            Else
                '項目の列位置
                '   追加した項目
                C_GOUSYA_POS = CONT_START_COL + H_COMP_GOUSYA_POS
                C_EG_POS = CONT_START_COL + H_COMP_EG_POS
                C_ISS_POS = CONT_START_COL + H_COMP_ISS_POS
                C_TM_POS = CONT_START_COL + H_COMP_TM_POS
                C_RD_POS = CONT_START_COL + H_COMP_RD_POS
                C_SYATAINO_POS = CONT_START_COL + H_COMP_SYATAINO_POS

                C_KFUGO_POS = CONT_START_COL + H_COMP_KFUGO_POS
                C_SHIYONO_POS = CONT_START_COL + H_COMP_SHIYONO_POS
                C_SHASHU_POS = CONT_START_COL + H_COMP_SHASHU_POS
                C_GRADE_POS = CONT_START_COL + H_COMP_GRADE_POS
                C_SHIMUKE_POS = CONT_START_COL + H_COMP_SHIMUKE_POS
                C_HUNDLE_POS = CONT_START_COL + H_COMP_HUNDLE_POS
                C_HAIKIRYO_POS = CONT_START_COL + H_COMP_HAIKIRYO_POS
                C_KEISHIKI_POS = CONT_START_COL + H_COMP_KEISHIKI_POS
                C_KAKYUKI_POS = CONT_START_COL + H_COMP_KAKYUKI_POS
                C_KUDO_POS = CONT_START_COL + H_COMP_KUDO_POS
                C_MISSION_POS = CONT_START_COL + H_COMP_MISSION_POS
                C_KATASHIKI_POS = CONT_START_COL + H_COMP_KATASHIKI_POS
                C_SHIMUKECODE_POS = CONT_START_COL + H_COMP_SHIMUKECODE_POS
                C_OPCODE_POS = CONT_START_COL + H_COMP_OPCODE_POS
                C_GAISOCODE_POS = CONT_START_COL + H_COMP_GAISOCODE_POS
                C_GAISONAME_POS = CONT_START_COL + H_COMP_GAISONAME_POS
                C_NAISOCODE_POS = CONT_START_COL + H_COMP_NAISOCODE_POS
                C_NAISONAME_POS = CONT_START_COL + H_COMP_NAISONAME_POS
                C_OPSPEC_POS = CONT_START_COL + H_COMP_OPSPEC_POS
                '制作方法
                C_SEIHO_POS = CONT_START_COL + H_COMP_SEIHO_POS
            End If


            'ベース情報の列位置設定
        ElseIf cate = "BASE" Then
            If StringUtil.Equals(flgMode, True) Then
                '完成車情報のOP項目列はベースのスタート位置からOPSPEC_POSを引いた値になる。
                C_OPSPEC_COUNT = CONT_START_COL - C_OPSPEC_POS - 8  'ベース開発符号とＯＰ項目列の間に移動した列数分マイナスする。
                '完成車OP項目以降のセルなのでOPSPEC_COUNTもプラスする。
                B_SHIYOMOKUTEKI_POS = 2 + C_OPSPEC_COUNT + BASE_SHIYOMOKUTEKI_POS
                B_SHUYOKAKUNIN_POS = 2 + C_OPSPEC_COUNT + BASE_SHUYOKAKUNIN_POS
                B_BUSHO_POS = 2 + C_OPSPEC_COUNT + BASE_BUSHO_POS
                B_JUNJYO_POS = 2 + C_OPSPEC_COUNT + BASE_JUNJYO_POS
                B_GROUP_POS = 2 + C_OPSPEC_COUNT + BASE_GROUP_POS
                B_KANSEIKIBOBI_POS = 2 + C_OPSPEC_COUNT + BASE_KANSEIKIBOBI_POS
                B_MEMO_POS = 2 + C_OPSPEC_COUNT + BASE_MEMO_POS
                C_SEIHO_POS = 2 + C_OPSPEC_COUNT + COMP_SEIHO_POS
            Else
                '完成車情報のOP項目列はベースのスタート位置からOPSPEC_POSを引いた値になる。
                C_OPSPEC_COUNT = CONT_START_COL - C_OPSPEC_POS - 7  'ベース開発符号とＯＰ項目列の間に移動した列数分マイナスする。
                '完成車OP項目以降のセルなのでOPSPEC_COUNTもプラスする。
                B_SHIYOMOKUTEKI_POS = 2 + C_OPSPEC_COUNT + H_BASE_SHIYOMOKUTEKI_POS
                B_SHUYOKAKUNIN_POS = 2 + C_OPSPEC_COUNT + H_BASE_SHUYOKAKUNIN_POS
                B_BUSHO_POS = 2 + C_OPSPEC_COUNT + H_BASE_BUSHO_POS
                B_JUNJYO_POS = 2 + C_OPSPEC_COUNT + H_BASE_JUNJYO_POS
                B_GROUP_POS = 2 + C_OPSPEC_COUNT + H_BASE_GROUP_POS
                B_KANSEIKIBOBI_POS = 2 + C_OPSPEC_COUNT + H_BASE_KANSEIKIBOBI_POS
                B_MEMO_POS = 2 + C_OPSPEC_COUNT + H_BASE_MEMO_POS
            End If

            '項目の列位置
            '   追加した項目
            B_SYATAINO_POS = CONT_START_COL + BASE_SYATAINO_POS
            B_SHISAKUEVENT_POS = CONT_START_COL + BASE_SHISAKUEVENT_POS

            B_KFUGO_POS = CONT_START_COL + BASE_KFUGO_POS
            B_SHIYONO_POS = CONT_START_COL + BASE_SHIYONO_POS
            B_SHASHU_POS = CONT_START_COL + BASE_SHASHU_POS
            B_GRADE_POS = CONT_START_COL + BASE_GRADE_POS
            B_SHIMUKE_POS = CONT_START_COL + BASE_SHIMUKE_POS
            B_HUNDLE_POS = CONT_START_COL + BASE_HUNDLE_POS
            B_HAIKIRYO_POS = CONT_START_COL + BASE_HAIKIRYO_POS
            B_KEISHIKI_POS = CONT_START_COL + BASE_KEISHIKI_POS
            B_KAKYUKI_POS = CONT_START_COL + BASE_KAKYUKI_POS
            B_KUDO_POS = CONT_START_COL + BASE_KUDO_POS
            B_MISSION_POS = CONT_START_COL + BASE_MISSION_POS
            B_KATASHIKI_POS = CONT_START_COL + BASE_KATASHIKI_POS
            B_SHIMUKECODE_POS = CONT_START_COL + BASE_SHIMUKECODE_POS
            B_OPCODE_POS = CONT_START_COL + BASE_OPCODE_POS
            B_GAISOCODE_POS = CONT_START_COL + BASE_GAISOCODE_POS
            B_GAISONAME_POS = CONT_START_COL + BASE_GAISONAME_POS
            B_NAISOCODE_POS = CONT_START_COL + BASE_NAISOCODE_POS
            B_NAISONAME_POS = CONT_START_COL + BASE_NAISONAME_POS
            B_OPSPEC_POS = CONT_START_COL + BASE_OPSPEC_POS

            B_OPSPEC_COUNT = BASE_OPSPEC_CNT

        End If

    End Sub

    '******************************************************************************
    ' setContPositionSN_COMPL
    ' 各項目の列番号を取得する
    '******************************************************************************
    ''' <summary>
    ''' 各項目の列番号を取得する
    ''' </summary>
    ''' <param name="lngOP">OP項目列数</param>
    ''' <param name="flgMode">モード（TRUE：印刷印刷、FALSE：編集書式）</param>
    ''' <remarks></remarks>
    Sub setContPositionSN_COMPL(ByVal lngOP As Long, ByVal flgMode As Boolean)

        '===========================================================
        '使用目的～メモまでの列位置を再設定
        'ベースＯＰ列分をプラス
        If StringUtil.Equals(flgMode, True) Then
            B_SHIYOMOKUTEKI_POS = CONT_START_COL + lngOP + BASE_SHIYOMOKUTEKI_POS - 1
            B_SHUYOKAKUNIN_POS = CONT_START_COL + lngOP + BASE_SHUYOKAKUNIN_POS - 1
            B_BUSHO_POS = CONT_START_COL + lngOP + BASE_BUSHO_POS - 1
            B_JUNJYO_POS = CONT_START_COL + lngOP + BASE_JUNJYO_POS - 1
            B_GROUP_POS = CONT_START_COL + lngOP + BASE_GROUP_POS - 1
            B_KANSEIKIBOBI_POS = CONT_START_COL + lngOP + BASE_KANSEIKIBOBI_POS - 1
            B_MEMO_POS = CONT_START_COL + lngOP + BASE_MEMO_POS - 1
            '制作方法
            C_SEIHO_POS = CONT_START_COL + lngOP + COMP_SEIHO_POS - 1
        Else
            B_SHIYOMOKUTEKI_POS = CONT_START_COL + lngOP + H_BASE_SHIYOMOKUTEKI_POS - 1
            B_SHUYOKAKUNIN_POS = CONT_START_COL + lngOP + H_BASE_SHUYOKAKUNIN_POS - 1
            B_BUSHO_POS = CONT_START_COL + lngOP + H_BASE_BUSHO_POS - 1
            B_JUNJYO_POS = CONT_START_COL + lngOP + H_BASE_JUNJYO_POS - 1
            B_GROUP_POS = CONT_START_COL + lngOP + H_BASE_GROUP_POS - 1
            B_KANSEIKIBOBI_POS = CONT_START_COL + lngOP + H_BASE_KANSEIKIBOBI_POS - 1
            B_MEMO_POS = CONT_START_COL + lngOP + H_BASE_MEMO_POS - 1
        End If
        '===========================================================

    End Sub

    '******************************************************************************
    ' setContPositionSN_COMPL
    ' ベースは特別織込みの項目位置にも対応
    ' 各項目の列番号を取得する
    '******************************************************************************
    Sub setContPositionSN_COMPL_Base(ByVal lngOP As Long, ByVal lngBaseT As Long, ByVal lngShisakuT As Long)

        '===========================================================
        'ベース車情報のOPスペック項目数をセット
        BASE_OPSPEC_CNT = lngOP
        B_OPSPEC_COUNT = lngOP
        'ベース車の特別織込みスタート列数及び項目列数
        BASE_TOKUBETU_ST_COL = B_OPSPEC_POS + lngOP
        BASE_TOKUBETU_CNT = lngBaseT
        '試作車の特別織込みスタート列数及び項目列数
        SHISAKU_TOKUBETU_ST_COL = B_OPSPEC_POS + lngOP + lngBaseT + 1
        SHISAKU_TOKUBETU_CNT = lngShisakuT
        '===========================================================

    End Sub

    '******************************************************************************
    ' setCompReContPosition
    ' 完成車シートの各項目の列番号を取得する
    '   列挿入後の下位列の位置調整のため
    '   挿入した列№を返す。
    '******************************************************************************
    Public Function setCompReContPosition(ByVal strReStartPos As String) As Long

        Select Case strReStartPos
            Case "C"
                C_OPSPEC_COUNT = C_OPSPEC_COUNT + 1
                C_SEIHO_POS = C_SEIHO_POS + 1
                B_KFUGO_POS = B_KFUGO_POS + 1
                B_SHISAKUEVENT_POS = B_SHISAKUEVENT_POS + 1
                B_SHIYONO_POS = B_SHIYONO_POS + 1
                B_SHASHU_POS = B_SHASHU_POS + 1
                B_GRADE_POS = B_GRADE_POS + 1
                B_SHIMUKE_POS = B_SHIMUKE_POS + 1
                B_HUNDLE_POS = B_HUNDLE_POS + 1
                B_HAIKIRYO_POS = B_HAIKIRYO_POS + 1
                B_KEISHIKI_POS = B_KEISHIKI_POS + 1
                B_KAKYUKI_POS = B_KAKYUKI_POS + 1
                B_KUDO_POS = B_KUDO_POS + 1
                B_MISSION_POS = B_MISSION_POS + 1
                B_KATASHIKI_POS = B_KATASHIKI_POS + 1
                B_SHIMUKECODE_POS = B_SHIMUKECODE_POS + 1
                B_OPCODE_POS = B_OPCODE_POS + 1
                B_GAISOCODE_POS = B_GAISOCODE_POS + 1
                B_GAISONAME_POS = B_GAISONAME_POS + 1
                B_NAISOCODE_POS = B_NAISOCODE_POS + 1
                B_NAISONAME_POS = B_NAISONAME_POS + 1
                B_SYATAINO_POS = B_SYATAINO_POS + 1
                B_OPSPEC_POS = B_OPSPEC_POS + 1
                BASE_TOKUBETU_ST_COL = BASE_TOKUBETU_ST_COL + 1
                SHISAKU_TOKUBETU_ST_COL = SHISAKU_TOKUBETU_ST_COL + 1
                B_SHIYOMOKUTEKI_POS = B_SHIYOMOKUTEKI_POS + 1
                B_SHUYOKAKUNIN_POS = B_SHUYOKAKUNIN_POS + 1
                B_BUSHO_POS = B_BUSHO_POS + 1
                B_JUNJYO_POS = B_JUNJYO_POS + 1
                B_GROUP_POS = B_GROUP_POS + 1
                B_KANSEIKIBOBI_POS = B_KANSEIKIBOBI_POS + 1
                B_MEMO_POS = B_MEMO_POS + 1
                '挿入したOP列番号の最終列番号を返す。
                setCompReContPosition = B_SHIYOMOKUTEKI_POS
            Case "B"
                B_OPSPEC_COUNT = B_OPSPEC_COUNT + 1
                BASE_TOKUBETU_ST_COL = BASE_TOKUBETU_ST_COL + 1
                SHISAKU_TOKUBETU_ST_COL = SHISAKU_TOKUBETU_ST_COL + 1
                'B_SHIYOMOKUTEKI_POS = B_SHIYOMOKUTEKI_POS + 1
                'B_SHUYOKAKUNIN_POS = B_SHUYOKAKUNIN_POS + 1
                'B_BUSHO_POS = B_BUSHO_POS + 1
                'B_JUNJYO_POS = B_JUNJYO_POS + 1
                'B_GROUP_POS = B_GROUP_POS + 1
                'B_KANSEIKIBOBI_POS = B_KANSEIKIBOBI_POS + 1
                'B_MEMO_POS = B_MEMO_POS + 1
                '挿入した列番号を返す。
                setCompReContPosition = BASE_TOKUBETU_ST_COL
            Case "BO"
                BASE_TOKUBETU_CNT = BASE_TOKUBETU_CNT + 1
                SHISAKU_TOKUBETU_ST_COL = SHISAKU_TOKUBETU_ST_COL + 1
                'B_SHIYOMOKUTEKI_POS = B_SHIYOMOKUTEKI_POS + 1
                'B_SHUYOKAKUNIN_POS = B_SHUYOKAKUNIN_POS + 1
                'B_BUSHO_POS = B_BUSHO_POS + 1
                'B_JUNJYO_POS = B_JUNJYO_POS + 1
                'B_GROUP_POS = B_GROUP_POS + 1
                'B_KANSEIKIBOBI_POS = B_KANSEIKIBOBI_POS + 1
                'B_MEMO_POS = B_MEMO_POS + 1
                '挿入した列番号を返す。
                setCompReContPosition = SHISAKU_TOKUBETU_ST_COL - 1
            Case "SO"
                SHISAKU_TOKUBETU_CNT = SHISAKU_TOKUBETU_CNT + 1
                'B_SHIYOMOKUTEKI_POS = B_SHIYOMOKUTEKI_POS + 1
                'B_SHUYOKAKUNIN_POS = B_SHUYOKAKUNIN_POS + 1
                'B_BUSHO_POS = B_BUSHO_POS + 1
                'B_JUNJYO_POS = B_JUNJYO_POS + 1
                'B_GROUP_POS = B_GROUP_POS + 1
                'B_KANSEIKIBOBI_POS = B_KANSEIKIBOBI_POS + 1
                'B_MEMO_POS = B_MEMO_POS + 1
                ''挿入した列番号を返す。
                setCompReContPosition = SHISAKU_TOKUBETU_ST_COL + SHISAKU_TOKUBETU_CNT + 1
        End Select
    End Function

    '******************************************************************************
    ' setWbodyReContPosition
    ' ＷＢ車シートの各項目の列番号を取得する
    '   列挿入後の下位列の位置調整のため
    '   挿入した列№を返す。
    '******************************************************************************
    Public Function setWbodyReContPosition(ByVal strReStartPos As String) As Long

        Select Case strReStartPos
            Case "W"
                'ラインOP列追加により以下の項目移行は変動になる。
                WB_SHIYOUMOKUTEKI_POS = WB_SHIYOUMOKUTEKI_POS + 1
                WB_SHUYOUKAKUNINKOUMOKU_POS = WB_SHUYOUKAKUNINKOUMOKU_POS + 1
                WB_SHIYOUBUSYO_POS = WB_SHIYOUBUSYO_POS + 1
                WB_SEISAKUJUNJYO_POS = WB_SEISAKUJUNJYO_POS + 1
                WB_SEISAKUGROUP_POS = WB_SEISAKUGROUP_POS + 1
                WB_KANSEIKIBOUBI_POS = WB_KANSEIKIBOUBI_POS + 1
                WB_SHUYOUKAKUNINKBN_POS = WB_SHUYOUKAKUNINKBN_POS + 1
                WB_SHIYO_START_POS = WB_SHIYO_START_POS + 1

                WB_MEMO_POS = WB_MEMO_POS + 1
                '挿入した列番号を返す。
                setWbodyReContPosition = WB_SHIYOUMOKUTEKI_POS
            Case "S"
                '仕様情報のカウントを＋１
                WB_SHIYOU_COUNT = WB_SHIYOU_COUNT + 1
                '挿入した列番号を返す。
                setWbodyReContPosition = WB_SHIYO_START_POS + WB_SHIYOU_COUNT
        End Select

    End Function


    ''' <summary>
    ''' 製作一覧の列位置を取得（完成車シート）"
    ''' 改訂以降
    ''' </summary>
    ''' <param name="HopKoumokuList">OP項目列</param>
    ''' <param name="HtokubetuOrikomiList">特別織込み列</param>
    ''' <param name="flgMode">モード（TRUE：印刷印刷、FALSE：編集書式）</param>
    Public Sub getContPositionSN_COMPL(ByVal HopKoumokuList As List(Of TSeisakuIchiranOpkoumokuVo), _
                                       ByVal HtokubetuOrikomiList As List(Of TSeisakuTokubetuOrikomiVo), ByVal flgMode As Boolean)

        '初期設定
        Dim strGousya As String = Nothing
        '-------------------------------------------
        '=============================================================================================
        '列情報
        '=============================================================================================
        'OP項目列取得
        '   項目の開始列
        CONT_START_COL = 3
        '完成車情報各項目の列位置取得
        setContPosition("COMP", CONT_START_COL, flgMode)
        COMP_ST_COL = CONT_START_COL
        '   クリア
        COMP_OPSPEC_CNT = 0

        '   ＯＰ項目列（完成車）比較用
        For Each wbVo As TSeisakuIchiranOpkoumokuVo In HopKoumokuList
            '号車を取得
            If StringUtil.IsEmpty(strGousya) Then
                strGousya = wbVo.Gousya
            End If
            '取得した号車のOP列項目数を計算
            If wbVo.Gousya = strGousya And _
                wbVo.Syubetu = SYUBETU_COMP Then
                COMP_OPSPEC_CNT = COMP_OPSPEC_CNT + 1
                'Else
                '    Exit For
            End If
        Next
        'カラム位置を取得
        '   OP項目列はデフォルト１列あるので以下の処理を行う。
        If COMP_OPSPEC_CNT = 0 Then COMP_OPSPEC_CNT = 1
        setContPositionSN_COMPL(COMP_OPSPEC_CNT, flgMode)

        'ベースのスタート位置を設定（メモ欄が最終なので＋１
        BASE_ST_COL = B_MEMO_POS + 1

        'ベース情報各項目の列位置取得
        CONT_START_COL = BASE_ST_COL
        setContPosition("BASE", CONT_START_COL, flgMode)

        'OP項目列取得
        Dim o As Long
        Dim ob, os As Long  'ob:ベース車の特別装備仕様列数、os:試作車の特別装備仕様列数。
        'ベース車のラインOP項目列数以降を再取得する。
        '   ＯＰ項目列（ベース車）比較用
        strGousya = Nothing
        For Each wbVo As TSeisakuIchiranOpkoumokuVo In HopKoumokuList
            '号車を取得
            If StringUtil.IsEmpty(strGousya) Then
                strGousya = wbVo.Gousya
            End If
            '取得した号車のOP列項目数を計算
            If wbVo.Gousya = strGousya And _
                wbVo.Syubetu = SYUBETU_BASE Then
                o = o + 1
                'Else
                '    Exit For
            End If
        Next
        '   OP項目列はデフォルト１列あるので以下の処理を行う。
        If o = 0 Then o = 1
        strGousya = Nothing
        For Each wbVo As TSeisakuTokubetuOrikomiVo In HtokubetuOrikomiList
            '号車を取得
            If StringUtil.IsEmpty(strGousya) Then
                strGousya = wbVo.Gousya
            End If
            '取得した号車のOP列項目数を計算
            If wbVo.Gousya = strGousya And _
                wbVo.Syubetu = SYUBETU_BASE Then
                ob = ob + 1
                'Else
                '    Exit For
            End If
        Next
        strGousya = Nothing
        For Each wbVo As TSeisakuTokubetuOrikomiVo In HtokubetuOrikomiList
            '号車を取得
            If StringUtil.IsEmpty(strGousya) Then
                strGousya = wbVo.Gousya
            End If
            '取得した号車のOP列項目数を計算
            If wbVo.Gousya = strGousya And _
                wbVo.Syubetu = SYUBETU_SHISAKU Then
                os = os + 1
                'Else
                '    Exit For
            End If
        Next

        'カラム位置を取得
        setContPositionSN_COMPL_Base(o, ob, os)
        '=============================================================================================

    End Sub

    ''' <summary>
    ''' 製作一覧の列位置を取得（ＷＢ車シート）"
    ''' 改訂以降
    ''' </summary>
    ''' <param name="HopKoumokuList">OP項目列</param>
    ''' <param name="HwbSoubiList">装備仕様列</param>
    Public Sub getContPositionSN_WBODY(ByVal HopKoumokuList As List(Of TSeisakuIchiranOpkoumokuVo), _
                                       ByVal HwbSoubiList As List(Of TSeisakuWbSoubiShiyouVo))

        '-------------------------------------------
        'ExcelImportSheetName = SN_WBODY
        'xls.SetActiveSheet(ExcelImportSheetName)
        '-------------------------------------------
        '=============================================================================================
        '列情報
        '=============================================================================================
        'OP項目列取得
        'Dim xlsValueHD As Object = xls.GetValue(1, 7, 256, 7)   'StartCol,StartRow,EndCol,EndRow
        'Dim arryXlsHD As Array = CType(xlsValueHD, Array)
        Dim lngOpCnt As Long = 0
        Dim lngMemoCnt As Long = 0
        Dim strGousya As String = Nothing

        '   回す
        '   ＯＰ項目列（ＷＢ車）比較用
        For Each wbVo As TSeisakuIchiranOpkoumokuVo In HopKoumokuList
            'ＷＢ車の号車を取得
            If wbVo.Syubetu = SYUBETU_WB Then
                If StringUtil.IsEmpty(strGousya) Then
                    strGousya = wbVo.Gousya
                End If
                '取得した号車のOP列項目数を計算
                If wbVo.Gousya = strGousya Then
                    lngOpCnt = lngOpCnt + 1
                    'Else
                    '    Exit For
                End If
            End If
        Next

        'OP列数をプラスしてOP列数移行のセル位置を取得する。
        'If lngOpCnt = 0 Then lngOpCnt = 1
        WB_SHIYOUMOKUTEKI_POS = WB_K_SHIYOUMOKUTEKI_POS + lngOpCnt
        WB_SHUYOUKAKUNINKOUMOKU_POS = WB_K_SHUYOUKAKUNINKOUMOKU_POS + lngOpCnt
        WB_SHIYOUBUSYO_POS = WB_K_SHIYOUBUSYO_POS + lngOpCnt
        WB_SEISAKUJUNJYO_POS = WB_K_SEISAKUJUNJYO_POS + lngOpCnt
        WB_SEISAKUGROUP_POS = WB_K_SEISAKUGROUP_POS + lngOpCnt
        WB_KANSEIKIBOUBI_POS = WB_K_KANSEIKIBOUBI_POS + lngOpCnt
        WB_SHUYOUKAKUNINKBN_POS = WB_K_SHUYOUKAKUNINKBN_POS + lngOpCnt
        'メモ欄の位置
        WB_MEMO_POS = WB_K_MEMO_POS + lngOpCnt

        '装備仕様情報のスタート位置
        WB_SHIYO_START_POS = WB_K_SHIYO_START_POS + lngOpCnt

        '装備仕様情報列数を取得する。
        '   回す

        '   ＷＢ車装備仕様情報比較用
        strGousya = Nothing
        For Each wbVo As TSeisakuWbSoubiShiyouVo In HwbSoubiList
            '号車を取得
            If StringUtil.IsEmpty(strGousya) Then
                strGousya = wbVo.Gousya
            End If
            '取得した号車のOP列項目数を計算
            If wbVo.Gousya = strGousya Then
                lngMemoCnt = lngMemoCnt + 1
                'Else
                '    Exit For
            End If
        Next

        '仕様情報のカウント
        WB_SHIYOU_COUNT = lngMemoCnt
        '=============================================================================================

    End Sub

#Region "元の製作一覧の初期設定シート更新"
    ''' <summary>
    ''' 元の製作一覧の初期設定シート更新する。
    ''' </summary>
    ''' <param name="fileName">ファイル名</param>
    ''' <param name="strKaihatsuFugo">開発符号</param>
    ''' <param name="strEvent">イベント</param>
    ''' <param name="strEventName">イベント名</param>
    ''' <param name="strHakouNo">発行№</param>
    ''' <param name="strKaiteiNo">改訂№</param>
    ''' <param name="strHozonHensyu">編集書式保存先</param>
    ''' <param name="strHozonHakou">出力書式保存先</param>
    ''' <remarks></remarks>
    Public Function updSeisakuIchiran(ByVal fileName As String, ByVal strKaihatsuFugo As String, _
                                      ByVal strEvent As String, ByVal strEventName As String, _
                                      ByVal strHakouNo As String, ByVal strKaiteiNo As String, _
                                      ByVal strHozonHensyu As String, ByVal strHozonHakou As String) As Boolean
        'EXCELアプリケーションを初期化
        'Dim app As Excel.Application
        'Dim book As Excel.Workbook
        'Dim sheet As Excel.Worksheet

        '---------------------------------------
        '画面を綺麗に、実行中のカーソルへ変更。
        Application.DoEvents()
        Cursor.Current = Cursors.WaitCursor
        '---------------------------------------
        Try
            If Not ShisakuComFunc.IsFileOpen(fileName) Then

                Using xls As New ShisakuExcel(fileName)

                    xls.SetActiveSheet(SN_SETUP)

                    Dim xlsValue As Object = xls.GetValue(1, 1, 12, 11)
                    '            Dim xlsValue As Object = xls.GetValue(1, startXlsDataRow, xls.EndCol, xls.EndRow)
                    Dim arryXls As Array = CType(xlsValue, Array)

                    '上書きしたいセルを指定及び値をセットする。
                    '   開発符号
                    xls.SetValue(3, 3, 3, 3, strKaihatsuFugo) 'startCol ,startRow ,endCol ,endRow ,value
                    '   イベント
                    xls.SetValue(3, 5, 3, 5, strEvent) 'startCol ,startRow ,endCol ,endRow ,value
                    '   イベント名
                    xls.SetValue(3, 7, 3, 7, strEventName) 'startCol ,startRow ,endCol ,endRow ,value
                    '   発行№
                    xls.SetValue(3, 9, 3, 9, strHakouNo) 'startCol ,startRow ,endCol ,endRow ,value
                    '   改訂№（これはいらないかも・・・）
                    xls.SetValue(3, 11, 3, 11, strKaiteiNo) 'startCol ,startRow ,endCol ,endRow ,value
                    '   保存先・編集用
                    xls.SetValue(10, 9, 10, 9, strHozonHensyu) 'startCol ,startRow ,endCol ,endRow ,value
                    '   保存先・発行用
                    xls.SetValue(10, 11, 10, 11, strHozonHakou) 'startCol ,startRow ,endCol ,endRow ,value

                    '保存
                    xls.Save()

                End Using

            End If

            Return True
        Catch
            ComFunc.ShowErrMsgBox("製作一覧の初期設定シートの更新でエラーが発生しました。" & vbLf & vbLf & _
                                  "「製作一覧ファイル」をご確認ください。")
            Return False
            Exit Function
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Function
#End Region

#Region "EXCELファイルのコメント設定マクロをキックする"

    ''' <summary>
    ''' EXCELファイルのコメント設定マクロをキックする。
    ''' 完成車情報／ＷＢ車情報のコメントを付ける。
    ''' </summary>
    Public Function comeSet(ByVal fileName As String) As Boolean

        '---------------------------------------
        '画面を綺麗に、実行中のカーソルへ変更。
        Application.DoEvents()
        Cursor.Current = Cursors.WaitCursor
        '---------------------------------------

        Try
            Dim oExcel As New Excel.ApplicationClass
            Dim oBook As Excel.WorkbookClass
            Dim oBooks As Excel.Workbooks = oExcel.Workbooks
            Dim strPath As String

            strPath = fileName
            'Excelオブジェクトの設定
            oExcel.Visible = False
            oBook = oBooks.Open(strPath)

            'マクロを実行する
            oExcel.Run("'" & oBook.Name & "'!subCOMPcome")  '完成車シート
            oExcel.Run("'" & oBook.Name & "'!subWBcome")    'ＷＢ車シート

            '後処理
            oBook.Save()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook)
            oBook = Nothing
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks)
            oBooks = Nothing
            oExcel.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
            oExcel = Nothing

            comeSet = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK)
            comeSet = False
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Function

#End Region

#Region "EXCELファイルの開発符号の入力規則設定マクロをキックする"

    ''' <summary>
    ''' EXCELファイルの開発符号の入力規則設定マクロをキックする。
    ''' 完成車情報／ＷＢ車情報の開発符号の入力規則の設定。
    ''' </summary>
    Public Function kaihatsuFugoSet(ByVal fileName As String) As Boolean

        '---------------------------------------
        '画面を綺麗に、実行中のカーソルへ変更。
        Application.DoEvents()
        Cursor.Current = Cursors.WaitCursor
        '---------------------------------------

        Try
            Dim oExcel As New Excel.ApplicationClass
            Dim oBook As Excel.WorkbookClass
            Dim oBooks As Excel.Workbooks = oExcel.Workbooks
            Dim strPath As String

            strPath = fileName
            'Excelオブジェクトの設定
            oExcel.Visible = False
            oBook = oBooks.Open(strPath)

            'マクロを実行する
            oExcel.Run("'" & oBook.Name & "'!AutoKaihatsuFugoSet")  '完成車／ＷＢ車シート

            '後処理
            oBook.Save()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook)
            oBook = Nothing
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks)
            oBooks = Nothing
            oExcel.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel)
            oExcel = Nothing

            kaihatsuFugoSet = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK)
            kaihatsuFugoSet = False
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Function

#End Region

#Region "　Right メソッド　"

    ''' -----------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の右端から指定された文字数分の文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iLength">
    '''     取り出す文字数。</param>
    ''' <returns>
    '''     右端から指定された文字数分の文字列。
    '''     文字数を超えた場合は、文字列全体が返されます。</returns>
    ''' -----------------------------------------------------------------------------------
    Public Function cRight(ByVal stTarget As String, ByVal iLength As Integer) As String
        If iLength <= stTarget.Length Then
            Return stTarget.Substring(stTarget.Length - iLength)
        End If

        Return stTarget
    End Function

#End Region
End Module
