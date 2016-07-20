Imports EBom.Common

''' <summary>
''' 試作システム グローバル定数モジュール
''' </summary>
''' <remarks></remarks>
Public Module ShisakuGlobal

#Region "太田工場でテスト"
    Public Const isOhtaTest As Boolean = False
#End Region

#Region "アドミンユーザーの、ID/PW定数"
    Public Const ADMIN_USER_ID As String = "admin"
    Public Const ADMIN_USER_PW As String = "admin"
#End Region

    ''↓↓2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_ax) (TES)張 ADD BEGIN
#Region "補用部品検索状況の定数"
    Public Const HOYOU_TANPIN As String = "H_TANPIN"
    Public Const HOYOU_NOMAL As String = "H_NOMAL"
    Public Const HOYOU_NOMAL_LESS As String = "H_NOMAL_LESS"
    Public Const HOYOU_NOMAL_ALL As String = "H_NOMAL_ALL"
    '
    Public Const HOYOU_TANPIN_SHISAKU As String = "H_TANPIN_SHISAKU"
    Public Const HOYOU_NOMAL_SHISAKU As String = "H_NOMAL_SHISAKU"
    Public Const HOYOU_NOMAL_LESS_SHISAKU As String = "H_NOMAL_LESS_SHISAKU"
    Public Const HOYOU_NOMAL_ALL_SHISAKU As String = "H_NOMAL_ALL_SHISAKU"
    'かな
    Public Const KANA_HOYOU_TANPIN As String = "単品"
    Public Const KANA_HOYOU_NOMAL As String = "通常"
    Public Const KANA_HOYOU_NOMAL_LESS As String = "通常(LESS)"
    Public Const KANA_HOYOU_NOMAL_ALL As String = "通常(ALL)"
    '
    Public Const KANA_HOYOU_TANPIN_SHISAKU As String = "単品(試作)"
    Public Const KANA_HOYOU_NOMAL_SHISAKU As String = "通常(試作)"
    Public Const KANA_HOYOU_NOMAL_LESS_SHISAKU As String = "通常(試作)(LESS)"
    Public Const KANA_HOYOU_NOMAL_ALL_SHISAKU As String = "通常(試作)(ALL)"
#End Region
#Region "補用部品検索選択方法の定数"
    Public Const HOYOU_SELECT_EBOM_SYSTEM_DAI As String = "E-BOMシステム大区分"
    Public Const HOYOU_SELECT_EBOM_SYSTEM As String = "E-BOMシステム区分"
    Public Const HOYOU_SELECT_EBOM_BLOCK As String = "E-BOMブロック"
    Public Const HOYOU_SELECT_EBOM_SOUBISHIYOU As String = "E-BOM仕様情報"
    Public Const HOYOU_SELECT_MBOM_SHISAKUTEHAI As String = "試作手配システム"

    ''↓↓2015/02/10 予算書部品検索追加 TES)劉 ADD BEGIN
    Public Const HOYOU_SELECT_VER1 As String = "試作開発管理表Ver1"
    Public Const HOYOU_SELECT_VER2 As String = "試作開発管理表Ver2"
    ''↑↑2015/02/10 予算書部品検索追加 TES)劉 ADD END

    Public Const HOYOU_SELECT_FINAL As String = "FINAL指定"
#End Region

#Region "供給セクションの変換"
    Public Const KYOKYU_SECTION_SHISAKU As String = "9SH10"
    Public Const KYOKYU_SECTION_JIKKEN As String = "9SX00"
    #End Region

#Region "編集モード"
    Public Const HoyouHensyuMode = "0" '編集モード
    Public Const HoyouKaiteiHensyuMode = "1" '改定編集モード
    Public Const HoyouKanryoViewMode = "2" '完了閲覧モード           *****完了閲覧モード対応*****
#End Region

#Region "編集モード(1:手配担当モード、2:予算担当モード)"
    Public Const TehaiTantoMode = "1"
    Public Const YosanTantoMode = "2"
#End Region

#Region "画面編集表示モード"
    Public Const EXCEL_MODE = "2" 'Excel出力専用モード
#End Region

#Region "部品表の集計コード"
    'MsgBox("選択した担当は下記の方が編集中に異常終了したもようです。編集できます" & vbCrLf & vbCrLf & _
    '    "担当者「" & tExclusiveControlTantoVo.EditUserId & _
    '             "：" & tanTousya & "」", MsgBoxStyle.OkOnly, "警告")

    ''X ： 任意管理単位（INSTL,ASSY等の社内加工・移動部品）
    Public Const SHUKEI_X = "X"
    Public Const SHUKEI_X_NAME = "任意管理単位（INSTL,ASSY等の社内加工・移動部品）"
    ''A ： 納入部品,内製製品
    Public Const SHUKEI_A = "A"
    Public Const SHUKEI_A_NAME = "納入部品,内製製品"
    ''E ： 支給部品（支給先が本品と同じブロック）
    Public Const SHUKEI_E = "E"
    Public Const SHUKEI_E_NAME = "支給部品（支給先が本品と同じブロック）"
    ''Y ： 支給部品（支給先が本品と別のブロック）
    Public Const SHUKEI_Y = "Y"
    Public Const SHUKEI_Y_NAME = "支給部品（支給先が本品と別のブロック）"
    ''R ： 参考呼出し部品
    Public Const SHUKEI_R = "R"
    Public Const SHUKEI_R_NAME = "参考呼出し部品"
    ''J ： 自給部品
    Public Const SHUKEI_J = "J"
    Public Const SHUKEI_J_NAME = "自給部品"

#End Region
    ''↑↑2014/07/29 Ⅰ.3.設計編集 ベース車改修専用化_ax) (TES)張 ADD END

#Region "DBケー名"
    ''' <summary>DBケー名</summary>
    Public Const DB_KEY_KOSEI As String = "KOSEI_DB"
    ''' <summary>DBケー名</summary>
    Public Const DB_KEY_EBOM As String = "EBOM_DB"
    ''' <summary>DBケース名</summary>
    Public Const DB_KEY_mBOM As String = "mBOM_DB"
#End Region

#Region "SKE1課の部課コード"
    Public Const SkeBukaCode As String = "8401"
#End Region

#Region "試作設計ブロック情報　状態"
    '2013/06/24 追加 -----------------------------------------------------------
    '　ＡＬ再展開を行うと以下のステータスとする。
    Public Const ShishakuSekkeiBlockStatusSaiChuusyutuSumi = "30"   '再抽出済み
    '---------------------------------------------------------------------------
    Public Const ShishakuSekkeiBlockStatusShouchiChuu = "31"   '編集中  
    Public Const ShishakuSekkeiBlockStatusIchiji = "32" '一時保存中
    Public Const ShishakuSekkeiBlockStatusSumi = "33" '登録済み
    Public Const ShishakuSekkeiBlockStatusShouchiKanryou = "34" '完了
    Public Const ShishakuSekkeiBlockStatusShounin1 = "35" '承認1
    Public Const ShishakuSekkeiBlockStatusShounin2 = "36" '承認2
#End Region
    
#Region "ブロック不要"
    Public Const ShishakuSekkeiBlockHitsuyou = "0" '必要
    Public Const ShishakuSekkeiBlockFuyou = "1" '不要
#End Region

#Region "試作編集モード"
    Public Const ShishakuHensyuMode = "0" '編集モード
    Public Const ShishakuKaiteiHensyuMode = "1" '改定編集モード
    Public Const ShishakuKanryoViewMode = "2" '完了閲覧モード           *****完了閲覧モード対応*****
#End Region

#Region "権限用定数"
    ''' <summary>権限有り</summary>
    Public Const M_AUTHORITY_USER_KBN As Integer = 1 '権限有り

    '　車系/開発符号マスター権限有る
    Public Const M_AUTHORITY_APP_NO As String = "1000"
    Public Const M_AUTHORITY_KINO_ID_1 As String = "MASTER006"
    Public Const M_AUTHORITY_KINO_ID_2 As String = "OPEN"


    'M_AUTHORITY_USERテブルの機能ID1の判断値
    Public Const M_AUTHORITY_USER_KINO1ID As String = "MENU"
    'M_AUTHORITY_USERテブルの機能ID2の判断値
    Public Const M_AUTHORITY_USER_KINO2ID_BLANK As Integer = 0
    Public Const M_AUTHORITY_USER_KINO2ID_SEKKEI As Integer = 1
    Public Const M_AUTHORITY_USER_KINO2ID_SISAKU As Integer = 2
    '↓↓↓2014/12/23 試作１課の権限チェックを追加 TES)張 ADD BEGIN
    Public Const M_AUTHORITY_USER_KINO2ID_SISAKU_1KA As Integer = 3
    '↑↑↑2014/12/23 試作１課の権限チェックを追加 TES)張 ADD END

    'M_AUTHORITYテブルの大区分の判断値
    Public Const M_AUTHORITY_MENU_DAI_KBN As Integer = 100 '新試作手配システム起動権限があります


    'M_AUTHORITYテブルの区分の判断値
    Public Const M_AUTHORITY_MENU_KBN_SEKKEI As Integer = 1 '設計課権限値
    Public Const M_AUTHORITY_MENU_KBN_SISAKU As Integer = 2 '試作課権限値
    '↓↓↓2014/12/23 試作１課の権限チェックを追加 TES)張 ADD BEGIN
    Public Const M_AUTHORITY_MENU_KBN_SISAKU_1KA As Integer = 3
    '↑↑↑2014/12/23 試作１課の権限チェックを追加 TES)張 ADD END

    'M_AUTHORITYテブルの権限の判断値
    Public Const M_AUTHORITY_AUTHORITY_KBN As Integer = 1 '利用可

    '2012/01/22
    'M_AUTHORITYテーブルの承認権限の判断値
    Public Const M_AUTHORITY_KINO_SHONIN As String = "SHONIN"

    Public Enum LOGIN_AUTHORITY
        USE_NG = -1 '権限無し

        USE_NODATE = 0 '権限無し
        USE_SISAKU = 2 '試作課権限有り
        USE_SEKKEI = 1 '設計課権限有り
        '↓↓↓2014/12/23 試作１課の権限チェックを追加 TES)張 ADD BEGIN
        USE_SISAKU_1KA = 3 '試作１課権限有り
        '↑↑↑2014/12/23 試作１課の権限チェックを追加 TES)張 ADD END
    End Enum
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum USERMAST_AUTHORITY
        UNKNWON = -1 '権限不明
        ADMIN = 0 '管理者
        SEKKEI = 1 '設計者
    End Enum
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum KENGEN
        USE_NG = 0 '無し
        USE_OK = 1 '有り
    End Enum
#End Region

#Region "プログラム、機能のIDと名"
    ''' <summary>プログラムID（管理者ログイン）</summary>
    Public PROGRAM_ID_01 = "LOGIN049"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_01 = "管理者ログイン"

    ''' <summary>プログラムID（ログイン）</summary>
    Public PROGRAM_ID_02 = "LOGIN001"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_02 = "ログイン"

    ''' <summary>プログラムID（ログインマスター）</summary>
    Public PROGRAM_ID_03 = "MASTER005"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_03 = "ログインマスター"

    ''' <summary>プログラムID（権限マスター）</summary>
    Public PROGRAM_ID_04 = "MASTER051"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_04 = "権限マスター"

    ''' <summary>プログラムID（車系／開発符号マスター）</summary>
    Public PROGRAM_ID_05 = "MASTER006"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_05 = "車系／開発符号マスター"

    ''' <summary>プログラムID（試作部品表 編集一覧）</summary>
    Public PROGRAM_ID_06 = "EVENT_EDIT35"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_06 = "試作部品表 編集一覧"

    ''' <summary>プログラムID（試作部品表 改訂編集一覧）</summary>
    Public PROGRAM_ID_07 = "EVENT_EDIT36"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_07 = "試作部品表 改訂編集一覧"

    ''' <summary>プログラムID（試作部品表作成メニュー）</summary>
    Public PROGRAM_ID_08 = "EVENT008"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_08 = "試作部品表作成メニュー"

    ''' <summary>プログラムID（イベント情報登録・編集）</summary>
    Public PROGRAM_ID_09 = "EVENT009"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_09 = "イベント情報登録・編集"

    ''' <summary>プログラムID（イベント情報登録・編集）</summary>
    Public PROGRAM_ID_18 = "EVENT018"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_18 = "手配帳作成"

    ''' <summary>プログラムID（イベント情報登録・編集）</summary>
    Public PROGRAM_ID_19 = "EVENT019"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_19 = "手配帳編集"

    ''' <summary>プログラムID（手配帳作成・編集）</summary>
    Public PROGRAM_ID_20 = "EVENT020"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_20 = "手配帳編集画面"

    ''' <summary>プログラムID（管理者メニュー）</summary>
    Public PROGRAM_ID_MENU01 = "MENU050"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_MENU01 = "管理者メニュー"

    ''' <summary>プログラムID（試作計画１課メニュー）</summary>
    Public PROGRAM_ID_MENU02 = "MENU002"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_MENU02 = "試作計画１課メニュー"

    ''' <summary>プログラムID（設計メニュー）</summary>
    Public PROGRAM_ID_MENU03 = "MENU003"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_MENU03 = "設計メニュー"

    '*****完了閲覧モード対応*****
    ''' <summary>プログラムID（試作部品表 完了イベント一覧）</summary>
    Public PROGRAM_ID_21 = "EVENT_EDIT62"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_21 = "試作部品表 完了イベント一覧"

    ''' <summary>プログラムID（設計メニュー）</summary>
    Public PROGRAM_ID_GOUSYABETSU = "GSBT001"
    ''' <summary>プログラム名</summary>
    Public PROGRAM_NAME_GOUSYABETSU = "号車別仕様書"


    ''' <summary>機能ID（承認押下）</summary>
    Public KINO_ID_01 = "APPROVAL"
    ''' <summary>機能ID（使用権限）</summary>
    Public KINO_ID_02 = "OPEN"
    ''' <summary>機能名</summary>
    Public KINO_NAME_01 = "承認押下"
    ''' <summary>機能名</summary>
    Public KINO_NAME_02 = "使用権限"
#End Region

#Region "プログラム、機能のIDと名"
    Public PROGRAM_ID = New String() {"", "LOGIN049", "LOGIN001", "MASTER005", _
                                      "MASTER051", "MASTER006"}
    Public PROGRAM_NAME = New String() {"", "管理者ログイン", "ログイン", "ログインマスター", _
                                      "権限マスター", "車系／開発符号マスター"}
    Public PROGRAM_ID_MENU = New String() {"", "MENU002", "MENU003"}

    Public PROGRAM_NAME_MENU = New String() {"", "設計課メニュー", "SKE1メニュー", "試作１課メニュー"}

    '2012/01/24 承認アラート機能の為、SHONINと承認部課を追加
    '   2014/02/05 試作日程管理表の権限追加のため、NITTEI、試作日程管理表を追加
    '   2015/02/05 オーダーシート権限追加
    Public KINO_ID = New String() {"", "APPROVAL", "OPEN", "SHONIN", "NITTEI", "ORDERSHEET"}
    Public KINO_NAME = New String() {"", "承認押下", "使用権限", "承認通知", "試作日程管理表", "現調品手配システム"}

    Public KENGEN_ID = New String() {"", "0", "1"}
    Public KENGEN_NAME = New String() {"", "なし", "あり"}

    Public NITTEI_ID = New String() {"", "0", "1", "2"}
    Public NITTEI_NAME = New String() {"", "参照権限", "更新権限", "管理者権限"}

    Public ORDERSHEET_ID = New String() {"", "OPEN", "EDIT"}
    Public ORDERSHEET_NAME = New String() {"", "使用権限", "編集権限"}

#End Region

#Region " バックカラー"
    Public ERROR_COLOR As Color = Color.Red
#End Region


#Region "ユーザー区分と区分名"
    '↓↓↓2014/12/23 試作１課の権限チェックを追加 TES)張 CHG BEGIN
    'Public UserKubunCd = New String() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
    'Public UserKubunName = New String() {"管理部署", "設計部署", "試作部", "その他", "購買", "原価", "生技", "その他", "商企", "その他"}
    Public UserKubunCd = New String() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A"}
    Public UserKubunName = New String() {"管理部署", "設計部署", "試作部", "その他", "購買", "原価", "生技", "その他", "商企", "その他", "試作１課"}
    '↑↑↑2014/12/23 試作１課の権限チェックを追加 TES)張 CHG END
#End Region

#Region "Ⅰ.8.号車別仕様書 作成機能 excelテンプレート格納フォルダ名"
    '↓↓2014/09/30 酒井 ADD BEGIN
    'Public Const ExcelGousyaTemplateDir = "D:\テンプレート"
    Public Const ExcelGousyaTemplateDir = "D:\新試作手配システム\号車別仕様書\テンプレート"
    Public Const ExcelGousyaOutPutDir = "D:\新試作手配システム\号車別仕様書\出力"
    '↑↑2014/09/30 酒井 ADD END
    '↓↓2014/10/17 酒井 ADD BEGIN
    'Public Const ExcelSaiTemplateFile = "D:\新試作手配システム\製作一覧差異抽出\テンプレート\製作一覧差異抽出テンプレート.xls"
    Public Function ExcelSaiTemplateFile() As String
        Return My.Application.Info.DirectoryPath & "\テンプレート\製作一覧差異抽出テンプレート.xls"
    End Function
    '↑↑2014/10/17 酒井 ADD END
#End Region

#Region "excel出力ファイル"
    Public Const ExcelOutPut = "spreadwin_save.xls"
    Public Const ExcelOutPutDir = "E:\新試作手配システム\Excel出力一時フォルダ"
    '↓↓↓2014/12/30 材料手配リスト作成を追加 TES)張 ADD BEGIN
    '    Public Const ExcelZairyoListTemplate = "D:\新試作手配システム\材料手配リスト\テンプレート\材料手配リスト.xls"
    'Public Const ExcelZairyoListTemplate = "D:\テンプレート\材料手配リスト(新フォーマット案).xls"
    Public Function ExcelZairyoListTemplate() As String
        Return My.Application.Info.DirectoryPath & "\テンプレート\材料手配リスト(新フォーマット案).xls"
    End Function


    Public Const ExcelZairyoListOutPut = "材料手配リスト.xls"
    'Public Const ExcelZairyoListOutPutDir = "D:\テンプレート\ExcelOutput\材料手配リスト\出力"
    Public Const ExcelZairyoListOutPutDir = "E:\新試作手配システム\Excel出力一時フォルダ"
    '↑↑↑2014/12/30 材料手配リスト作成を追加 TES)張 ADD END
    Public Const YosanListExcelImportDir = "E:\新試作手配システム\Excel出力一時フォルダ"
    Public Const YosanListExcelImportFile = "（財務実績EXCEL）14.08 12試験研究費集計表(確定).xls"
#End Region

#Region "excel取込シート"
    Public ExcelImportSheetName As String
#End Region

    ''↓↓2015/01/21 手配帳編集画面CSV取込追加 TES)劉 ADD BEGIN
#Region "手配帳編集画面CSV取込"
    'Public Const WordInPut = "D:\テンプレート\申請書.doc"
    Public Function WordInPut() As String
        Return My.Application.Info.DirectoryPath & "\テンプレート\申請書.doc"
    End Function

    'Public Const WordOutPutDir = "D:\テンプレート\ExcelOutput\データ支給依頼書\出力"
    Public Const WordOutPutDir = "D:\新試作手配システム\Excel出力一時フォルダ"

    'Public Const MAKER_CSV_FILE = "D:\テンプレート\csv\MakerInfo.csv"
    Public Function MAKER_CSV_FILE() As String
        Return My.Application.Info.DirectoryPath & "\テンプレート\MakerInfo.csv"
    End Function

    'Public Const TANTO_CSV_FILE = "D:\テンプレート\csv\TantoInfo.csv"
    Public Function TANTO_CSV_FILE() As String
        Return My.Application.Info.DirectoryPath & "\テンプレート\TantoInfo.csv"
    End Function


    '工事指令書
    'Public Const ExcelKoujiShireiTemplate = "D:\テンプレート\工事指令原紙 .xls"
    Public Function ExcelKoujiShireiTemplate() As String
        Return My.Application.Info.DirectoryPath & "\テンプレート\工事指令原紙 .xls"
    End Function
    Public Const ExcelKoujiShireiOutput = "工事指令書.xls"
    'Public Const ExcelKoujiShireiOutputDir = "D:\テンプレート\ExcelOutput\工事指令書"
    Public Const ExcelKoujiShireiOutputDir = "D:\新試作手配システム\Excel出力一時フォルダ"


    '注文書
    'Public Const ExcelOrderTemplate = "D:\テンプレート\注文書原紙.xls"
    Public Function ExcelOrderTemplate() As String
        Return My.Application.Info.DirectoryPath & "\テンプレート\注文書原紙.xls"
    End Function
    Public Const ExcelOrderOutput = "注文書.xls"
    'Public Const ExcelOrderOutputDir = "D:\テンプレート\ExcelOutput\注文書"
    Public Const ExcelOrderOutputDir = "D:\新試作手配システム\Excel出力一時フォルダ"
#End Region
    ''↑↑2015/01/21 手配帳編集画面CSV取込追加 TES)劉 ADD END

#Region "ステータス"
    Public Const StatusSekkei As String = "21"
    Public Const StatusKaitei As String = "23"
    Public Const StatusKanryou As String = "24"
#End Region

#Region "ブロック課長主査権限"
    Public Const AuthorityOK As String = "1"
    Public Const AuthorityNG As String = "0"
#End Region

#Region "画面編集表示モード"
    Public Const EDIT_MODE = "0" '編集表示モード
    Public Const VIEW_MODE = "1" '閲覧表示モード
#End Region

#Region "スプレッドの最大行数の指定"
    Public Const SPREAD_MAX_ROWS = 1000
#End Region

#Region "接続DB名"
    Public RHACLIBF_DB_NAME As String
    Public MBOM_DB_NAME As String
    Public EBOM_DB_NAME As String
    ''2015/10/21 追加 E.Ubukata
    Public BRAKU_DB_NAME As String
#End Region

    '2012年下期案件分
    '製作一覧システム取込
#Region "ステータス／状態"
    Public Const STATUS_A = "10" '未承認
    Public Const STATUS_B = "20" '承認済
    Public Const STATUS_A_NAME = "未承認" '未承認
    Public Const STATUS_B_NAME = "承認済み" '承認済

    Public Const JYOTAI_A = "1" '中止
    Public Const JYOTAI_B = "" '
    Public Const JYOTAI_A_NAME = "中止" '中止
    Public Const JYOTAI_B_NAME = "　　　"     '
#End Region


#Region "試作イベント修正状態"
    Public baseUpdateFlg As String = ""
    Public kanseiUpdateFlg As String = ""
    Public basicUpdateFlg As String = ""
    Public specialUpdateFlg As String = ""
    Public Const baseUpdateMsg As String = "ベース車情報が更新されました。"
    Public Const kanseiUpdateMsg As String = "完成車情報が更新されました。"
    Public Const basicUpdateMsg As String = "基本装備仕様情報が更新されました。"
    Public Const specialUpdateMsg As String = "特別装備仕様情報が更新されました。"
#End Region

    '20140225 追加分

#Region "製作一覧編集書式用ファイル"
    Public Const ExcelEditFile = "製作一覧_編集書式.xls"
    Public Const ExcelEditDir = "\\FGNT08\pt\全車共通\E-BOMアプリ\VBA\"
#End Region

#Region "製作一覧発行書式用ファイル"
    Public Const ExcelNewFile = "製作一覧_新規作成.xls"
    Public Const ExcelNewDir = "\\FGNT08\pt\全車共通\E-BOMアプリ\VBA\"
#End Region

#Region "WB用装備仕様ファイル"
    Public Const ExcelWbFile = "製作一覧WB用装備仕様マスタ.xls"
    Public Const ExcelWbDir = "\\FGNT08\pt\全車共通\E-BOMアプリ\VBA\"
#End Region

#Region "シート名"
    '初期設定シートの名称
    Public Const SN_SETUP = "初期設定"
    '完成車製作一覧シートの名称
    Public Const SN_COMPL = "完成車"
    'W/B一覧のシート名称
    Public Const SN_WBODY = "ＷＢ"
#End Region

#Region "位置情報"
    '完成車製作一覧のデータ開始行
    Public Const DATA_START_ROW = 11
    Public Const DATA_END_ROW = 210
    'WBシート_項目の開始列
    Public Const WB_CONT_START_ROW As Long = 11
    Public Const WB_CONT_END_ROW As Long = 210
#End Region

#Region "シンボル"
    Public Const SYMBOL_A = "追加" '追加
    Public Const SYMBOL_C = "変更" '変更
    Public Const SYMBOL_D = "削除" '削除
#End Region

#Region "種別"
    Public Const SYUBETU_COMP = "C" '
    Public Const SYUBETU_BASE = "B" '
    Public Const SYUBETU_SHISAKU = "S" '
    Public Const SYUBETU_WB = "W" '
#End Region

#Region "製作方法区分と区分名"
    Public SeihoCd = New String() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"}
    Public SeihoName = New String() {"ﾌﾙ組み＋ﾌﾙｷﾞｿｰ", "移管車改修", "既存車流用", "フルライン流し", _
                                     "ﾒﾀﾙﾗｲﾝ（専用織込）＋ﾄﾘﾑﾗｲﾝ（ﾍﾞｰｽ）後改修", "ﾒﾀﾙﾗｲﾝ（専用織込）＋ﾌﾙｷﾞｿｰ", _
                                     "ﾍﾟｲﾝﾄﾎﾞﾃﾞｨ＋ﾌﾙｷﾞｿｰ", "ﾍﾟｲﾝﾄﾎﾞﾃﾞｨ改修＋ﾌﾙｷﾞｿｰ", "完成報告のみ", "ﾒﾀﾙﾌﾙ組み+ﾄﾘﾑﾗｲﾝ流し", _
                                     "その他"}
#End Region

#Region "出力形態"
    Public Const SYUTURYOKU_KEITAI_HAKOU = "1"
    Public Const SYUTURYOKU_KEITAI_HENSYU = "2"
    Public Const SYUTURYOKU_KEITAI_REFERENCE_OUTPUT = "5"
    Public Const SYUTURYOKU_KEITAI_HAKOU_NAME = "印刷書式"
    Public Const SYUTURYOKU_KEITAI_HENSYU_NAME = "編集書式"
#End Region

#Region "非表示フラグ"
    Public Const HIDE_FLG = "H" '
#End Region

#Region "完成車シートのカラム位置"

    '完成車情報項目の開始列からの相対位置
    Public Const COMP_KFUGO_POS = 0
    Public Const COMP_SHIYONO_POS = 1
    Public Const COMP_GOUSYA_POS = 2
    Public Const COMP_SHASHU_POS = 3
    Public Const COMP_GRADE_POS = 4
    Public Const COMP_SHIMUKE_POS = 5
    Public Const COMP_HUNDLE_POS = 6
    Public Const COMP_HAIKIRYO_POS = 7
    Public Const COMP_KEISHIKI_POS = 8
    Public Const COMP_KAKYUKI_POS = 9
    Public Const COMP_EG_POS = 10
    Public Const COMP_ISS_POS = 11
    Public Const COMP_KUDO_POS = 12
    Public Const COMP_MISSION_POS = 13
    Public Const COMP_TM_POS = 14
    Public Const COMP_RD_POS = 15
    Public Const COMP_KATASHIKI_POS = 16
    Public Const COMP_SHIMUKECODE_POS = 17
    Public Const COMP_OPCODE_POS = 18
    Public Const COMP_GAISOCODE_POS = 19
    Public Const COMP_GAISONAME_POS = 20
    Public Const COMP_NAISOCODE_POS = 21
    Public Const COMP_NAISONAME_POS = 22
    Public Const COMP_SYATAINO_POS = 23
    Public Const COMP_OPSPEC_POS = 24
    '   仕様変更（2012･12･26）以下の項目はベース情報の前に移動
    Public Const BASE_SHIYOMOKUTEKI_POS = 25
    Public Const BASE_SHUYOKAKUNIN_POS = 26
    Public Const BASE_BUSHO_POS = 27
    Public Const BASE_GROUP_POS = 28
    Public Const BASE_JUNJYO_POS = 29
    Public Const BASE_KANSEIKIBOBI_POS = 30
    Public Const COMP_SEIHO_POS = 31
    Public Const BASE_MEMO_POS = 32

    'ベース情報項目の開始列からの相対位置
    Public Const BASE_KFUGO_POS = 0
    Public Const BASE_SHISAKUEVENT_POS = 1
    Public Const BASE_SHIYONO_POS = 2
    Public Const BASE_SHASHU_POS = 3
    Public Const BASE_GRADE_POS = 4
    Public Const BASE_SHIMUKE_POS = 5
    Public Const BASE_HUNDLE_POS = 6
    Public Const BASE_HAIKIRYO_POS = 7
    Public Const BASE_KEISHIKI_POS = 8
    Public Const BASE_KAKYUKI_POS = 9
    Public Const BASE_KUDO_POS = 10
    Public Const BASE_MISSION_POS = 11
    Public Const BASE_KATASHIKI_POS = 12
    Public Const BASE_SHIMUKECODE_POS = 13
    Public Const BASE_OPCODE_POS = 14
    Public Const BASE_GAISOCODE_POS = 15
    Public Const BASE_GAISONAME_POS = 16
    Public Const BASE_NAISOCODE_POS = 17
    Public Const BASE_NAISONAME_POS = 18
    Public Const BASE_SYATAINO_POS = 19
    Public Const BASE_OPSPEC_POS = 20

#End Region

#Region "完成車シートのカラム位置（編集用）"

    '完成車情報項目の開始列からの相対位置
    Public Const H_COMP_KFUGO_POS = 0
    Public Const H_COMP_SHIYONO_POS = 1
    Public Const H_COMP_SEIHO_POS = 2
    Public Const H_COMP_GOUSYA_POS = 3
    Public Const H_COMP_SHASHU_POS = 4
    Public Const H_COMP_GRADE_POS = 5
    Public Const H_COMP_SHIMUKE_POS = 6
    Public Const H_COMP_HUNDLE_POS = 7
    Public Const H_COMP_HAIKIRYO_POS = 8
    Public Const H_COMP_KEISHIKI_POS = 9
    Public Const H_COMP_KAKYUKI_POS = 10
    Public Const H_COMP_EG_POS = 11
    Public Const H_COMP_ISS_POS = 12
    Public Const H_COMP_KUDO_POS = 13
    Public Const H_COMP_MISSION_POS = 14
    Public Const H_COMP_TM_POS = 15
    Public Const H_COMP_RD_POS = 16
    Public Const H_COMP_KATASHIKI_POS = 17
    Public Const H_COMP_SHIMUKECODE_POS = 18
    Public Const H_COMP_OPCODE_POS = 19
    Public Const H_COMP_GAISOCODE_POS = 20
    Public Const H_COMP_GAISONAME_POS = 21
    Public Const H_COMP_NAISOCODE_POS = 22
    Public Const H_COMP_NAISONAME_POS = 23
    Public Const H_COMP_SYATAINO_POS = 24
    Public Const H_COMP_OPSPEC_POS = 25

    Public Const H_BASE_SHIYOMOKUTEKI_POS = 26
    Public Const H_BASE_SHUYOKAKUNIN_POS = 27
    Public Const H_BASE_BUSHO_POS = 28
    Public Const H_BASE_GROUP_POS = 29
    Public Const H_BASE_JUNJYO_POS = 30
    Public Const H_BASE_KANSEIKIBOBI_POS = 31
    Public Const H_BASE_MEMO_POS = 32

#End Region

#Region "ＷＢ車シートのカラム位置"

    'WBシート_項目の列位置
    Public Const WB_OLDGOUSYA_POS As Long = 1
    Public Const WB_SHINBORU_POS As Long = 2
    Public Const WB_KAIHATUFUGO_POS As Long = 3
    Public Const WB_SHIYOJYOHONO_POS As Long = 4
    Public Const WB_GOUSYA_POS As Long = 5
    Public Const WB_SYAKEI_POS As Long = 6
    Public Const WB_GREAD_POS As Long = 7
    Public Const WB_SHIMUKE_POS As Long = 8
    Public Const WB_HUNDLE_POS As Long = 9
    Public Const WB_EG_POS As Long = 10
    Public Const WB_TM_POS As Long = 11
    Public Const WB_KATASHIKI_POS As Long = 12
    Public Const WB_SHIMUKECODE_POS As Long = 13
    Public Const WB_OPCODE_POS As Long = 14
    Public Const WB_GAISOCODE_POS As Long = 15
    Public Const WB_GAISONAME_POS As Long = 16
    Public Const WB_NAISOCODE_POS As Long = 17
    Public Const WB_NAISONAME_POS As Long = 18
    Public Const WB_SYATAINO_POS As Long = 19

    'ラインOP機能追加
    Public Const WB_OP_START_POS As Long = 20

    'ラインOP列追加により以下の項目移行は変動になる。
    '   "_K_"は仮のポジションのしるし
    Public Const WB_K_SHIYOUMOKUTEKI_POS As Long = 21
    Public Const WB_K_SHUYOUKAKUNINKOUMOKU_POS As Long = 22
    Public Const WB_K_SHIYOUBUSYO_POS As Long = 23
    Public Const WB_K_SEISAKUGROUP_POS As Long = 24
    Public Const WB_K_SEISAKUJUNJYO_POS As Long = 25
    Public Const WB_K_KANSEIKIBOUBI_POS As Long = 26
    Public Const WB_K_MEMO_POS As Long = 27
    Public Const WB_K_SHUYOUKAKUNINKBN_POS As Long = 28

    Public Const WB_K_SHIYO_START_POS As Long = 29

#End Region


#Region "3Dデータファイル"
    'GJ1指定フォルダに変更
#If DEBUG Then
    Public Const XVLFileDir As String = "D:\新試作手配システム\temp"
#Else
        Public Const XVLFileDir As String = "\\Gj1np26n\Public\share\SKE1\2013-10-08"
#End If

#If DEBUG Then
    Public Const XVLFileDir1 As String = "\\emcsv0\public\DATAV5\"
    Public Const XVLFileDir2 As String = "\3D\VIEWER\SDAM"
#Else
    Public Const XVLFileDir1 As String = "\\emcsv0\public\DATAV5\"
    Public Const XVLFileDir2 As String = "\3D\VIEWER\SDAM"
#End If

    Public Const XVLFileSubDir As String = "\Bf4ModelXvl"

    Public Const XVLFileBodyDir As String = "\\fgnt07\pt\新試作手配システム\ボディXVLファイル\"
    'Public Const XVLFileBodyDir As String = "\\Gj1np26n\Public\share\SKE1\2013-11-01\"

    '計測ファイル名
    Public Const XVLKeisokuXls As String = "最小の箱寸法.xls"
    Public Const XVLKeisokuXlsSheet As String = "Sheet1"
#End Region

    ''↓↓2014/12/23 4試作１課メニュー (TES)張 ADD BEGIN
#Region "試作１課メニューフラグ"
    'その他
    Public Const SHISAKU1KA_BLANK As Integer = 0
    '試作１課
    Public Const SHISAKU1KA_SISAKU As Integer = 1
#End Region
    ''↑↑2014/12/23 4試作１課メニュー (TES)張 ADD END

#Region "材料寸法ファイル"
    Public Const ZairyoSunpoFile = "材料寸法.xls"
    Public Const ZairyoSunpoDir = "D:\"
#End Region

#Region "設通情報"
    Public Const ECS_DATA_LINK = "http://zumen-settu.gkh.subaru-fhi.co.jp:122/logon.htm"
#End Region


End Module
