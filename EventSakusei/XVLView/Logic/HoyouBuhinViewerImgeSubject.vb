Imports System.IO
Imports ShisakuCommon

Namespace XVLView.Logic

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HoyouBuhinViewerImgeSubject

#Region "メンバー変数"
        'ShisakuCommonをインポートしたため不要.
        '#Region "3Dデータファイル"
        '        'GJ1指定フォルダに変更
        '        Public Const XVLFileDir As String = "\\Gj1np26n\Public\share\SKE1\2013-10-08"
        '        Public Const XVLFileSubDir As String = "\Bf4ModelXvl"
        '        '計測ファイル名
        '        Public Const XVLKeisokuXls As String = "最小の箱寸法.xls"
        '        Public Const XVLKeisokuXlsSheet As String = "Sheet1"
        '#End Region

        ''' <summary>XVLPLAYERコントロール - ビューコントロールのみ利用可能</summary>
        Private XV_API_VIEW = &H20000000

        ''' <summary>XVLPLAYERコントロール - ファイル操作</summary>
        Private XV_API_OPE_FILE = &H2000000

        ''' <summary>XVLPLAYERコントロール - param1 : 整数．XvReadType 参照 param2 : 文字列．XVL ファイルパス</summary>
        Private XV_VIEW_EXE_READ_MODEL = XV_API_VIEW Or XV_API_OPE_FILE Or &H1

        ' - 指定された XVL ファイルをモデル上に読み込む

        ' -- 指定されたファイルでモデルを開く
        Private XV_READ_OPEN = 1

        ' -- 指定されたファイルをモデルにインポートする
        Private XV_READ_IMPORT = 2

        ''''<summary>前画面引継ぎ項目</summary>>
        'Private _viewVo As New ViewerImgeVo

        ''' <summary>３Ｄデータ表示</summary>
        Private _frmVeiwerImge As HoyouBuhinFrmVeiwerImge

#End Region

#Region "コンストラクタ"

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="aFileName"></param>
        ''' <param name="frm"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal aFileName As String, ByVal frm As HoyouBuhinFrmVeiwerImge)

            _frmVeiwerImge = frm

            'Dim iFullPath As String = XVLFileDir & XVLFileSubDir & "\" & aFileName
            Dim iFullPath As String = aFileName

            '３Ｄデータ表示
            _frmVeiwerImge.AxXVLPlayer.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_OPEN, iFullPath, Nothing, Nothing)

        End Sub

        ''' <summary>
        ''' ファイルの追加.
        ''' </summary>
        ''' <param name="aFileName"></param>
        ''' <remarks></remarks>
        Public Sub IMPORT(ByVal aFileName As String)

            Dim obje As Object = Nothing
            'obje = _frmVeiwerImge.AxXVLPlayer.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, XVLFileDir & XVLFileSubDir & "\" & aFileName, Nothing, Nothing)
            obje = _frmVeiwerImge.AxXVLPlayer.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aFileName, Nothing, Nothing)


        End Sub

#End Region

        '#Region "3Dデータ表示"

        '        '''' <summary>
        '        '''' 3Dデータ表示
        '        '''' </summary>
        '        '''' <remarks></remarks>
        '        'Public Sub setViewerData()
        '        '    Dim aDir As String = ShisakuCommon.ShisakuGlobal.XVLFileDir & ShisakuCommon.ShisakuGlobal.XVLFileSubDir
        '        '    Dim emp As System.Object = Nothing
        '        '    Dim aFileName As String = getFileName(aDir)
        '        '    If aFileName.Trim <> "" Then
        '        '        '3Dデータ表示
        '        '        _frmVeiwerImge.AxXVLPlayer.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_OPEN, aFileName, emp, emp)
        '        '    End If
        '        'End Sub

        '        '''' <summary>
        '        '''' ファイル名取得
        '        '''' </summary>
        '        '''' <returns></returns>
        '        '''' <remarks></remarks>
        '        'Private Function getFileName(ByVal aDir As String) As String
        '        '    Dim aFileName As String = ""
        '        '    Try
        '        '        Dim afileParam As String = "*" & _viewVo.BuhinNo & "*.xv*"
        '        '        Dim files As String() = System.IO.Directory.GetFiles(aDir, afileParam, System.IO.SearchOption.AllDirectories)
        '        '        If files.Length > 1 Then
        '        '            'ブロックで絞り込む
        '        '            Dim aKaihatsuFugoFiles As String() = Array.FindAll(files, Function(str As String) str.Contains(_viewVo.KaihatsuFugo))
        '        '            If aKaihatsuFugoFiles.Length > 1 Then
        '        '                '補助名称で絞り込む
        '        '                Dim aHojyoNameFiles As String() = Array.FindAll(aKaihatsuFugoFiles, Function(str As String) str.Contains(_viewVo.HojyoName))
        '        '                If aHojyoNameFiles.Length >= 1 Then
        '        '                    For Each strFile In aHojyoNameFiles
        '        '                        aFileName = Path.GetFullPath(strFile)
        '        '                        Exit For
        '        '                    Next
        '        '                End If
        '        '            Else
        '        '                aFileName = Path.GetFullPath(aKaihatsuFugoFiles(0))
        '        '            End If
        '        '        Else
        '        '            aFileName = Path.GetFullPath(files(0))
        '        '        End If
        '        '    Catch ex As Exception
        '        '        MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        '    End Try
        '        '    Return aFileName
        '        'End Function

        '#End Region


        'Public Sub setBody()
        '    Dim aDir As String = XVLFileDir & XVLFileSubDir
        '    Dim emp As System.Object = Nothing
        '    With _frmVeiwerImge.AxXVLPlayer

        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_OPEN, aDir & "\GBF4_50825AL02A____02____GUSSET-FT-SD-OUT-_508W_50825AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_50825AL03A____01____GUSSET-FT-SD-IN--_508W_50825AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_50866AL06A____00____STAY-FT-SD-------_508W_50825AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_50866AL08A____00____STAY-R-FRAME-US--_508W_50866AL04A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_50866AL09A____00____STAY-R-FRAME-US--_508W_50866AL04A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51021AL00A____00____FRAME-SD-UPR-OUT-_510A_51021AL00A_00_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51021AL01A____00____FRAME-SD-UPR-OUT-_510A_51021AL00A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51130AL00A____02____REINF-SD-SL-IN-F-_511A_51150AL08A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51130AL01A____02____REINF-SD-SL-IN-F-_511A_51150AL08A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51150AL10A____05____SD-SILL-IN-F-----_511A_51150AL08A_05_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51150AL11A____05____SD-SILL-IN-F-----_511A_51150AL08A_05_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51151AL06A____01____PLR-F-IN-LWR-----_511A_51151AL00A_01_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51151AL07A____01____PLR-F-IN-LWR-----_511A_51151AL00A_01_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51157AL00A____01____SEPARATOR-SD-SILL_511A_51150AL08A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51157AL01A____01____SEPARATOR-SD-SILL_511A_51150AL08A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51231AL01A____02____FRAME-RAD-LWR-LWR_512A_51231AL00A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51405AL02A____06____PNL-SD-OUT-SDN---_513A_51405AL02A_06_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51405AL03A____06____PNL-SD-OUT-SDN---_513A_51405AL02A_06_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51447AL04A____01____QTR-PNL-R-END-SDN_513A_51445AL06A_01_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51447AL05A____01____QTR-PNL-R-END-SDN_513A_51445AL06A_01_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51453AL06A____03____GUSSET-FRM-SD-U-A_513A_51453AL00A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51453AL07A____03____GUSSET-FRM-SD-U-A_513A_51453AL00A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51453AL08A____02____GUSSET-FRM-SD-U-B_513A_51453AL00A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51453AL09A____02____GUSSET-FRM-SD-U-B_513A_51453AL00A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51456AL09A____00____REINF-RL-SD-O-F--_513A_51456AL20A_01_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51456AL11A____00____REINF-RL-SD-PATCH_513A_51456AL20A_01_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51456AL20A____00____REINF-RL-SD-O-F--_513A_51456AL20A_01_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51456AL28A____01____REINF-EXT-PLR-F-O_513A_51455AL02A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51456AL29A____01____REINF-EXT-PLR-F-O_513A_51455AL02A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51456AL30A____02____REINF-PLR-F-O-A--_513A_51455AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51462AL01A____01____BRKT-TRUNK-HOOK--_513A_51445AL06A_01_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51462AL02A____01____BRKT-TRUNK-HOOK--_513A_51445AL06A_01_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51465AL00A____02____PLATE-NUT-HINGE--_513A_51477AL00A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51465AL01A____02____PLATE-NUT-HINGE--_513A_51477AL00A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL10A____00____REINF-SILL-SD-O-A_513A_51472AL10A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL11A____00____REINF-SILL-SD-O-A_513A_51472AL10A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL16A____02____REINF-RAIL-SD-O-R_513A_51472AL16A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL17A____02____REINF-RAIL-SD-O-R_513A_51472AL16A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL18A____00____REINF-SL-SD-OUT-F_513A_51472AL18A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL19A____00____REINF-SL-SD-OUT-F_513A_51472AL18A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL20A____03____REINF-SL-SD-OUT-R_513A_51472AL20A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL21A____03____REINF-SL-SD-OUT-R_513A_51472AL20A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL26A____02____REINF-PLR-F-OUT--_513A_51455AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL27A____02____REINF-PLR-F-OUT--_513A_51455AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL28A____00____REINF-HNG-F-PL-U-_513A_51455AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL30A____00____REINF-HNG-F-PL-L-_513A_51455AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL32A____00____REINF-PLR-CTR-O--_513A_51456AL22A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL33A____00____REINF-PLR-CTR-O--_513A_51456AL22A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL34A____00____REINF-PLR-C-O-A-U_513A_51456AL22A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL35A____00____REINF-PLR-C-O-A-U_513A_51456AL22A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL36A____01____REINF-PLR-C-O-A-L_513A_51456AL22A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL37A____01____REINF-PLR-C-O-A-L_513A_51456AL22A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL38A____00____REINF-PLR-CTR-O-B_513A_51456AL22A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL39A____00____REINF-PLR-CTR-O-B_513A_51456AL22A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL40A____02____REINF-STRKR-R----_513A_51472AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL41A____02____REINF-STRKR-R----_513A_51472AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL42A____01____REINF-STRG-SPRT--_513A_51455AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51472AL43A____01____REINF-STRG-SPRT--_513A_51455AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51477AL02A____03____PATCH-R-PLR------_513A_51477AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51477AL03A____03____PATCH-R-PLR------_513A_51477AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51522AL04A____03____PLR-F-IN-CTR-----_513A_51520AL06A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51522AL05A____03____PLR-F-IN-CTR-----_513A_51520AL06A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51522AL06A____01____PLR-F-IN-UPR-----_513A_51520AL08A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51522AL07A____01____PLR-F-IN-UPR-----_513A_51520AL08A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51526AL06A____01____RAIL-SD-IN-SDN---_513A_51525AL06A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51526AL07A____01____RAIL-SD-IN-SDN---_513A_51525AL06A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51532AL00A____00____PLR-CTR-IN-------_513A_51530AL02A_00_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51532AL01A____00____PLR-CTR-IN-------_513A_51530AL02A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51541AL00A____02____PLR-R-IN-SIA-----_513A_51541AL00A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51541AL01A____02____PLR-R-IN-SIA-----_513A_51541AL00A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51560AL02A____04____ARCH-R-IN--------_513A_51560AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51560AL03A____04____ARCH-R-IN--------_513A_51560AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51562AL06A____01____BRKT-ASSIST-F----_513A_51525AL04A_03_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51562AL07A____01____BRKT-ASSIST-R----_513A_51525AL04A_03_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51562AL10A____00____BRKT-RETR-F------_513A_51530AL02A_00_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51562AL15A____00____BRKT-CSTR-ARCH---_513A_51562AL00A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51562AL17A____01____BRKT-TRIM--------_513A_51520AL08A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51562AL18A____01____BRKT-TRIM--------_513A_51520AL08A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51572AL12A____02____REINF-SILL-SD----_513A_51572AL12A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51572AL13A____02____REINF-SILL-SD----_513A_51572AL12A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51572AL28A____02____REINF-R-PLR------_513A_51572AL22A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51572AL29A____02____REINF-R-PLR------_513A_51572AL22A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51572AL32A____02____REINF-RETR-UPR---_513A_51572AL22A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51572AL33A____02____REINF-RETR-UPR---_513A_51572AL22A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51575AL04A____02____EXTENSION-F-RAIL-_513A_51520AL08A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51575AL05A____02____EXTENSION-F-RAIL-_513A_51520AL08A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51615AL04A____02____CLOSING-PLT-FR---_516A_51615AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51615AL05A____02____CLOSING-PLT-FR---_516A_51615AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51615AL06A____00____CLOSING-PLT-S-U-F_516A_51630AL04A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51615AL07A____00____CLOSING-PLT-S-U-F_516A_51630AL04A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51615AL09A____00____CLOSING-PLT-FF-A-_516A_51615AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51620AL16A____02____FRAME-SD-UPR-IN--_516A_51620AL04A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51620AL17A____02____FRAME-SD-UPR-IN--_516A_51620AL04A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51620AL24A____02____FRAME-SD-UPR-F---_516A_51630AL04A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51620AL25A____02____FRAME-SD-UPR-F---_516A_51630AL04A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51620AL28A____03____FRAME-SD-FF-A----_516A_51620AL22A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51620AL30A____03____FRAME-SD-FF-B----_516A_51620AL22A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51620AL31A____03____FRAME-SD-FF-B----_516A_51620AL22A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL18A____04____BRKT-F-SUS-UPR---_516A_51625AL04A_05_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL19A____04____BRKT-F-SUS-UPR---_516A_51625AL04A_05_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL22A____01____BRKT-F-SUS-LWR---_516A_51625AL04A_05_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL23A____01____BRKT-F-SUS-LWR---_516A_51625AL04A_05_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL26A____01____BRKT-H-U---------_516A_51625AL04A_05_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL32A____00____BRKT-BEAM-F-IN---_516A_51620AL22A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL33A____00____BRKT-BEAM-F-IN---_516A_51620AL22A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL36A____00____BRKT-BEAM-F-OUT--_516A_51615AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL37A____00____BRKT-BEAM-F-OUT--_516A_51615AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL40A____00____BRKT-BEAM-F-IN-U-_516A_51620AL22A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL41A____00____BRKT-BEAM-F-IN-U-_516A_51620AL22A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL44A____00____BRKT-WASH-TANK---_516A_51630AL04A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL45A____00____BRKT-FUSE--------_516A_51630AL04A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL48A____00____BRKT-H-L-SD------_516A_51625AL00A_00_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL49A____00____BRKT-H-L-SD------_516A_51625AL00A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL50A____00____BRKT-APRON-R-LWR-_516B_51625AL08A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL51A____00____BRKT-APRON-R-LWR-_516B_51625AL08A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL54A____02____BRKT-LUGG-HOOK---_516B_51625AL12A_02_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL56A____00____BRKT-CSTR-APRON-F_516B_51625AL14A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51625AL57A____00____BRKT-CSTR-APRON-R_516B_51625AL15A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL08A____03____REINF-TB-LWR-----_516A_51630AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL09A____03____REINF-TB-LWR-----_516A_51630AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL12A____01____REINF-F-SUS-UPR--_516A_51625AL04A_05_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL13A____01____REINF-F-SUS-UPR--_516A_51625AL04A_05_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL16A____02____REINF-F-SUS-LWR--_516A_51625AL04A_05_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL17A____02____REINF-F-SUS-LWR--_516A_51625AL04A_05_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL22A____02____REINF-APR-F------_516A_51630AL04A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL23A____02____REINF-APR-F------_516A_51630AL04A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51630AL24A____01____REINF-BATTERY----_516A_51630AL04A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51632AL00A____01____SEPARATOR-FR-SD-U_516A_51620AL04A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51632AL01A____01____SEPARATOR-FR-SD-U_516A_51620AL04A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51632AL05A____00____SEPARATOR-FF-F---_516A_51620AL22A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51633AL04A____00____DBLR-FF-F--------_516A_51620AL22A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51633AL05A____02____DOUBLER-FF-F-----_516A_51620AL22A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51633AL08A____00____DOUBLER-TB-LWR---_516A_51630AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51633AL09A____00____DOUBLER-TB-LWR---_516A_51630AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51634AL00A____00____PATCH-TOE-BOARD--_516A_51630AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51635AL00A____00____PLATE-FF---------_516A_51620AL00A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51635AL01A____00____PLATE-FF---------_516A_51620AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51635AL03A____00____PLATE-CLS-PLT-FF-_516A_51615AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51636AL00A____02____GUSSET-BATTERY---_516A_51615AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51636AL01A____00____GUSSET-BAT-LWR---_516A_51615AL02A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51636AL02A____00____GUSSET-APR-F-----_516A_51615AL02A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51660AL00A____02____WHEEL-APRON-R----_516B_51660AL00A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51660AL01A____02____WHEEL-APRON-R----_516B_51660AL00A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51676AL04A____01____BRKT-FEND-UP-F---_516A_51676AL00A_01_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51676AL06A____03____BRKT-FEND-UP-R---_516A_51676AL02A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51676AL12A____01____BRKT-FEND-UP-F---_516A_51676AL00A_01_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51676AL14A____03____BRKT-FEND-UP-R---_516A_51676AL02A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51680AL04A____03____REINF-R-APRON-OUT_516B_51680AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_51680AL05A____03____REINF-R-APRON-OUT_516B_51680AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52120AL01A____01____FLOOR-PAN-F-A----_521A_52120AL00A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52120AL02A____01____FLOOR-PAN-F-B----_521A_52120AL00A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52120AL03A____01____FLOOR-PAN-F-B----_521A_52120AL00A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52136AL02A____01____PATCH-CRM-B------_521C_52140AJ16A_12_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52140AL14A____02____CROSS-MBR-A-R----_521C_52140AL13A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52143AL03A____00____REINF-CRM-A-R-SD-_521C_52140AL13A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52143AL04A____00____REINF-CRM-A-R-SD-_521C_52140AL13A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52145AL00A____00____REINF-FRM-SD-R-F-_521C_52146AL01A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52146AL00A____00____DBLR-FRM-SD-R-F--_521C_52146AL00A_00_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52146AL04A____00____DBLR-FRM-SD-R-F--_521C_52146AL00A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52153AL08A____00____BRKT-SELECT-LEVER_521A_52140AL04A_00_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52154AL06A____01____SIDE-SILL-IN-R---_521C_52154AL00A_01_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52154AL07A____01____SIDE-SILL-IN-R---_521C_52154AL00A_01_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52157AL02A____00____SEPARATE-SILL-R-U_521C_52154AL00A_01_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52157AL03A____00____SEPARATE-SILL-R-U_521C_52154AL00A_01_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52210AL00A____02____TOE-BOARD-SIA----_522A_52200AL00A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52230AL00A____00____BRKT-ACCEL-SIA---_522A_52200AL00A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52230AL04A____00____BRKT-BLOWER-SIA--_522A_52200AL00A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52421AL04A____01____SKIRT-R-OUT-SDN--_524B_52401AL00A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52421AL05A____01____SKIRT-R-IN-SDN---_524B_52401AL00A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52432AL00A____00____PLATE-NUT-STRIKER_524B_52401AL00A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52433AL01A____00____REINF-STRIKER-SDN_524B_52401AL00A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52436AL00A____00____BRKT-BMPR-LWR-CTR_524B_52401AL01A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52436AL01A____00____BRKT-BMPR-LWR-SD-_524B_52401AL01A_03_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52710AL00A____01____BULK-HEAD-F------_511C_51121AL00A_04_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52720AL00A____01____SEPARATOR-BULK-C-_511C_51121AL00A_04_CT_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52721AL00A____01____GUSSET-R-PNL-F---_527B_52704AL02A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52721AL01A____01____GUSSET-R-PNL-F---_527B_52704AL02A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52721AL02A____02____GUSSET-R-PNL-R---_527B_52704AL02A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52721AL03A____02____GUSSET-R-PNL-R---_527B_52704AL02A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52730AL00A____01____BRKT-WIPER-A-----_511C_51121AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52730AL02A____01____BRKT-WIPER-B-----_511C_51121AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52730AL05A____02____BRKT-STAY-HOOD---_511C_51121AL00A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52730AL07A____01____BRKT-PEDAL-UPR-A-_511C_51121AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52730AL13A____01____BRKT-PDL-BRAKE---_511C_51121AL00A_04_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52730AL16A____02____BRKT-STAY-HOOD---_511C_51121AL00A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL00A____02____REINF-F-PNL-SD---_511C_51121AL00A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL01A____02____REINF-F-PNL-SD---_511C_51121AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL04A____02____REINF-WIPER-SD---_511C_51121AL00A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL05A____02____REINF-WIPER-SD---_511C_51121AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL08A____02____REINF-BULK-SD----_511C_51121AL00A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL09A____02____REINF-BULK-SD----_511C_51121AL00A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL12A____00____REINF-R-APRON-4D-_527B_52704AL02A_02_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL13A____00____REINF-R-APRON-4D-_527B_52704AL02A_02_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL20A____01____REINF-R-BULK-----_527B_52704AL02A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52740AL21A____01____REINF-R-BULK-----_527B_52704AL02A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52760AL00A____02____FRAME-F-BULK-----_511C_51121AL00A_04_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_52760AL01A____02____FRAME-F-BULK-----_511C_51121AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53020AL00A____02____RAD-PNL-SD-------_530A_53010AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53020AL01A____02____RAD-PNL-SD-------_530A_53010AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53040AL00A____02____STAY-HOOD-LOCK---_530A_53010AL00A_03_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53051AL00A____02____BRKT-RAD-SD------_530A_53010AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53051AL01A____02____BRKT-RAD-SD------_530A_53010AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53051AL02A____01____BRKT-AB-SNSR-----_530A_53010AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53051AL03A____01____BRKT-AB-SNSR-----_530A_53010AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53060AL02A____02____FRAME-RAD-UPR-SIA_530A_53010AL00A_03_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53060AL06A____02____FRAME-RAD-PNL-SD-_530A_53010AL00A_03_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53060AL07A____02____FRAME-RAD-PNL-SD-_530A_53010AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53110AL00A____04____PANEL-F----------_511C_51121AL00A_05_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53115AL00A____05____PANEL-R-CTR------_531B_53105AL02A_07_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53125AL00A____02____REINF-R-PNL-F----_531B_53105AL02A_07_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53125AL04A____02____REINF-R-PNL-R----_531B_53105AL02A_07_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53125AL06A____00____REINF-R-PANEL----_531B_53105AL02A_07_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53146AL01A____01____PLATE-CHILD-ANCH-_531B_53146AL01A_01_CT_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53405AL01A____01____RAIL-F-UPR-------_510A_53400AL00A_03_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53405AL02A____02____RAIL-F-LWR-SDN---_510A_53400AL00A_03_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53415AL03A____00____RAIL-R-LWR-SDN---_510A_53415AL03A_00_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53601AL10A____03____ROOF-PNL-SDN-----_536A_53601AL10A_03_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53701AL13A____01____BRACE-F-SDN------_536A_53701AL13A_01_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53701AL14A____01____BRACE-R-A-SDN----_536A_53701AL14A_01_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53701AL15A____01____BRACE-R-B-SDN----_536A_53701AL15A_01_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_53701AL19A____01____BRACE-CTR-SDN----_510A_53700AL01A_01_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_56117AL00A____02____HOOK-NUT---------_561A_56103AL00A_03_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_56120AL00A____01____BATTERY-PAN-A----_561A_56103AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_56120AL01A____01____BATTERY-PAN-B----_561A_56103AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_56122AL00A____01____BRKT-BAT---------_561A_56103AL00A_03_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57120AL00A____06____FENDER-F---------_571A_57120AL00A_06_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57120AL01A____06____FENDER-F---------_571A_57120AL00A_06_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57221AL00A____00____REINF-HOOD-LK----_572A_57221AL00A_00_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57221AL06A____00____REINF-HOOD-STAY--_572A_57221AL06A_00_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57221AL08A____00____REINF-HOOD-HINGE-_572A_57221AL02A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57221AL09A____00____REINF-HOOD-HINGE-_572A_57221AL02A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57230AL00A____01____HOOD-PNL-F-OUT---_572A_57230AL00A_01_CT_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57230AL02A____02____HOOD-PNL-F-IN----_572A_57230AL02A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57231AL00A____01____PLATE-HOOD-LK----_572A_57232AL00A_01_CT_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57231AL02A____00____PLATE-HOOD-HG----_572A_57221AL02A_00_LH_LR.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57232AL02A____01____BRKT-HOOD-LOCK---_572A_57232AL00A_01_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57261AL00A____00____HINGE-BRKT-UPR---_572A_57260AL00A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57261AL01A____00____HINGE-BRKT-UPR---_572A_57260AL00A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57261AL04A____00____HINGE-BRKT-LWR---_572A_57260AL00A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57261AL05A____00____HINGE-BRKT-LWR---_572A_57260AL00A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57510AL01A____03____PNL-OUT-TRK-LWR--_575A_57510AL01A_03_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57510AL02A____02____PNL-OUT-TRK-UPR--_575A_57510AL02A_02_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57511AL00A____05____PNL-INNER-TRK-LID_575A_57511AL00A_05_CT_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57512AL01A____02____REINF-TRUNK-HNG--_575A_57512AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57512AL03A____02____REINF-TRUNK-HNG--_575A_57512AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57512AL08A____02____REINF-TRUNK-BRKT-_575A_57512AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57512AL09A____02____REINF-TRUNK-BRKT-_575A_57512AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57602AL05A____01____FLAP-OUT-SDN-----_576A_57601AL06A_01_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_57602AL16A____01____FLAP-INNER-SDN---_576A_57601AL06A_01_RH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60030AL00A____06____PANEL-IN-DR-F----_600H_60030AL00A_06_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60030AL01A____06____PANEL-IN-DR-F----_600H_60030AL00A_06_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60040AL00A____02____PNL-OUT-DR-F-SDN-_600H_60040AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60040AL01A____02____PNL-OUT-DR-F-SDN-_600H_60040AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60060AL00A____03____REINF-HNG-DR-F---_600H_60060AL00A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60060AL01A____03____REINF-HNG-DR-F---_600H_60060AL00A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60063AL00A____02____REINF-DR-F-OUT-UP_600H_60053AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60063AL01A____02____REINF-DR-F-OUT-UP_600H_60053AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60064AL00A____02____REINF-DR-F-OUT---_600H_60064AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60064AL01A____02____REINF-DR-F-OUT---_600H_60064AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60068AL00A____02____REINF-A-F-OUT-UP-_600H_60053AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60068AL01A____02____REINF-A-F-OUT-UP-_600H_60053AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60068AL02A____02____REINF-MIR-DR-F---_600H_60053AL00A_02_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60068AL03A____02____REINF-MIR-DR-F---_600H_60053AL00A_02_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60430AL00A____04____PANEL-IN-DR-R----_604A_60430AL00A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60430AL01A____04____PANEL-IN-DR-R----_604A_60430AL00A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60440AL00A____04____PNL-OUT-DR-R-SDN-_604A_60440AL00A_04_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60440AL01A____04____PNL-OUT-DR-R-SDN-_604A_60440AL00A_04_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60460AL00A____03____REINF-HNG-DR-R---_604A_60460AL00A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60460AL01A____03____REINF-HNG-DR-R---_604A_60460AL00A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60463AL00A____03____REINF-DR-R-OUT-UP_604A_60463AL00A_03_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60463AL01A____03____REINF-DR-R-OUT-UP_604A_60463AL00A_03_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60464AL00A____00____REINF-DR-R-OUT-C-_604A_60464AL00A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60464AL01A____00____REINF-DR-R-OUT-C-_604A_60464AL00A_00_LH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60484AL00A____00____REINF-PTN-DR-R-LW_604A_60484AL00A_00_RH_SY.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_60484AL01A____00____REINF-PTN-DR-R-LW_604A_60484AL00A_00_LH_SY.xv2", emp, emp)

        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20202AL000_00_--_ARM_ASSY_F-------_RH-.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20202AL010_00_--_ARM_ASSY_F-------_LH-.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20204ASL00_00_--_RUB_BUSH_ARM_F_R-_---.xv2", emp, emp)

        '        '.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20204SG000_00_--_RUB_BUSH_ARM_F_F-_---.xv2", emp, emp)

        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20206AJ000_--_--_BALL_JOINT_COMPL-_---.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20207ASL00_00_--_SPACER_ARM_F-----_---.xv2", emp, emp)

        '        '.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20540AA090_--_--_ADJUSTING_BOLT---_M14.xv2", emp, emp)
        '        '.Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20540AA100_--_--_FLANG_BOLT_M14---_---.xv2", emp, emp)

        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20560AA040_--_--_WASHER_(ADJUST)--_14-.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\2013-12-23_Review\SBF4_200A_20560AA04A_--_--_WASHER_ADJUST----_---.xv2", emp, emp)

        '    End With
        'End Sub

        'Private Sub setBody2()
        '    Dim aDir As String = XVLFileDir & XVLFileSubDir
        '    Dim emp As System.Object = Nothing
        '    With _frmVeiwerImge.AxXVLPlayer
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_OPEN, aDir & "\GBF4_50825AL02A____02____GUSSET-FT-SD-OUT-_508W_50825AL00A_04_LH_AS.xv2", emp, emp)
        '        .Execute(XV_VIEW_EXE_READ_MODEL, XV_READ_IMPORT, aDir & "\GBF4_50825AL03A____01____GUSSET-FT-SD-IN--_508W_50825AL00A_04_LH_AS.xv2", emp, emp)

        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_50866AL06A____00____STAY-FT-SD-------_508W_50825AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_50866AL08A____00____STAY-R-FRAME-US--_508W_50866AL04A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_50866AL09A____00____STAY-R-FRAME-US--_508W_50866AL04A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_50882AL02A____02____BRKT-LUGG-HK-D-PL_508W_50882AL00A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_50882AL03A____02____BRKT-LUGG-HK-D-PL_508W_50882AL00A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51021AL00A____00____FRAME-SD-UPR-OUT-_510A_51021AL00A_00_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51021AL01A____00____FRAME-SD-UPR-OUT-_510A_51021AL00A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51130AL00A____02____REINF-SD-SL-IN-F-_511A_51150AL08A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51130AL01A____02____REINF-SD-SL-IN-F-_511A_51150AL08A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51150AL10A____05____SD-SILL-IN-F-----_511A_51150AL08A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51150AL11A____05____SD-SILL-IN-F-----_511A_51150AL08A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51151AL06A____01____PLR-F-IN-LWR-----_511A_51151AL00A_01_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51151AL07A____01____PLR-F-IN-LWR-----_511A_51151AL00A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51157AL00A____01____SEPARATOR-SD-SILL_511A_51150AL08A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51157AL01A____01____SEPARATOR-SD-SILL_511A_51150AL08A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51231AL01A____02____FRAME-RAD-LWR-LWR_512A_51231AL00A_02_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51336AL01A____03____PLATE-NUT-R-BELT-_513A_51336AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51405AL00A____04____PNL-SD-OUT-OBK---_513A_51405AL00A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51405AL01A____04____PNL-SD-OUT-OBK---_513A_51405AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51447AL02A____03____QTR-PNL-R-END-OBK_513A_51445AL04A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51447AL03A____03____QTR-PNL-R-END-OBK_513A_51445AL04A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51453AL06A____03____GUSSET-FRM-SD-U-A_513A_51453AL00A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51453AL07A____03____GUSSET-FRM-SD-U-A_513A_51453AL00A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51453AL08A____02____GUSSET-FRM-SD-U-B_513A_51453AL00A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51453AL09A____02____GUSSET-FRM-SD-U-B_513A_51453AL00A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51456AL08A____00____REINF-RL-SD-O-F--_513A_51456AL18A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51456AL10A____00____REINF-RL-SD-PATCH_513A_51456AL18A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51456AL18A____00____REINF-RL-SD-O-F--_513A_51456AL18A_01_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51456AL28A____01____REINF-EXT-PLR-F-O_513A_51455AL02A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51456AL29A____01____REINF-EXT-PLR-F-O_513A_51455AL02A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51456AL30A____02____REINF-PLR-F-O-A--_513A_51455AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL10A____00____REINF-SILL-SD-O-A_513A_51472AL10A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL11A____00____REINF-SILL-SD-O-A_513A_51472AL10A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL14A____03____REINF-RAIL-SD-O-R_513A_51472AL14A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL15A____03____REINF-RAIL-SD-O-R_513A_51472AL14A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL18A____00____REINF-SL-SD-OUT-F_513A_51472AL18A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL19A____00____REINF-SL-SD-OUT-F_513A_51472AL18A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL20A____03____REINF-SL-SD-OUT-R_513A_51472AL20A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL21A____03____REINF-SL-SD-OUT-R_513A_51472AL20A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL26A____02____REINF-PLR-F-OUT--_513A_51455AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL27A____02____REINF-PLR-F-OUT--_513A_51455AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL28A____00____REINF-HNG-F-PL-U-_513A_51455AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL30A____00____REINF-HNG-F-PL-L-_513A_51455AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL32A____00____REINF-PLR-CTR-O--_513A_51456AL22A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL33A____00____REINF-PLR-CTR-O--_513A_51456AL22A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL34A____00____REINF-PLR-C-O-A-U_513A_51456AL22A_02_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL35A____00____REINF-PLR-C-O-A-U_513A_51456AL22A_02_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL36A____01____REINF-PLR-C-O-A-L_513A_51456AL22A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL37A____01____REINF-PLR-C-O-A-L_513A_51456AL22A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL38A____00____REINF-PLR-CTR-O-B_513A_51456AL22A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL39A____00____REINF-PLR-CTR-O-B_513A_51456AL22A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL40A____02____REINF-STRKR-R----_513A_51472AL00A_02_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL41A____02____REINF-STRKR-R----_513A_51472AL00A_02_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL42A____01____REINF-STRG-SPRT--_513A_51455AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51472AL43A____01____REINF-STRG-SPRT--_513A_51455AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51475AL02A____01____EXT-D-PLR--------_513A_51445AL04A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51475AL03A____01____EXT-D-PLR--------_513A_51445AL04A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51512AL02A____03____QTR-PNL-R-IN-----_513A_51512AL02A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51512AL03A____03____QTR-PNL-R-IN-----_513A_51512AL02A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51522AL04A____03____PLR-F-IN-CTR-----_513A_51520AL06A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51522AL05A____03____PLR-F-IN-CTR-----_513A_51520AL06A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51522AL06A____01____PLR-F-IN-UPR-----_513A_51520AL08A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51522AL07A____01____PLR-F-IN-UPR-----_513A_51520AL08A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51526AL04A____02____RAIL-SD-IN-OBK---_513A_51525AL04A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51526AL05A____02____RAIL-SD-IN-OBK---_513A_51525AL04A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51532AL00A____00____PLR-CTR-IN-------_513A_51530AL02A_00_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51532AL01A____00____PLR-CTR-IN-------_513A_51530AL02A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51551AL00A____02____PLR-D-IN-UPR-----_513A_51572AL18A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51551AL01A____02____PLR-D-IN-UPR-----_513A_51572AL18A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51551AL04A____03____PLR-D-IN-LWR-----_513A_51572AL18A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51551AL05A____03____PLR-D-IN-LWR-----_513A_51572AL18A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51560AL02A____04____ARCH-R-IN--------_513A_51560AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51560AL03A____04____ARCH-R-IN--------_513A_51560AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL06A____01____BRKT-ASSIST-F----_513A_51525AL04A_03_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL07A____01____BRKT-ASSIST-R----_513A_51525AL04A_03_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL10A____00____BRKT-RETR-F------_513A_51530AL02A_00_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL11A____00____BRKT-STRIKER-----_513A_51572AL20A_01_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL12A____00____BRKT-STRIKER-----_513A_51572AL20A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL15A____00____BRKT-CSTR-ARCH---_513A_51562AL00A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL16A____00____BRKT-HOOK--------_513A_51572AL18A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL17A____01____BRKT-TRIM--------_513A_51520AL08A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51562AL18A____01____BRKT-TRIM--------_513A_51520AL08A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51571AL00A____00____SEPARATOR-D-PLR--_513A_51572AL18A_05_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51571AL01A____00____SEPARATOR-D-PLR--_513A_51572AL18A_05_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL12A____02____REINF-SILL-SD----_513A_51572AL12A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL13A____02____REINF-SILL-SD----_513A_51572AL12A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL36A____03____REINF-D-PLR------_513A_51572AL18A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL37A____03____REINF-D-PLR------_513A_51572AL18A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL40A____02____REINF-C-PLR------_513A_51572AL16A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL41A____02____REINF-C-PLR------_513A_51572AL16A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL44A____01____REINF-R-APRON----_513A_51572AL20A_01_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51572AL45A____01____REINF-R-APRON----_513A_51572AL20A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51575AL04A____02____EXTENSION-F-RAIL-_513A_51520AL08A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51575AL05A____02____EXTENSION-F-RAIL-_513A_51520AL08A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51583AL00A____00____PATCH-D-PLR------_513A_51572AL18A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51583AL01A____00____PATCH-D-PLR------_513A_51572AL18A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51615AL04A____02____CLOSING-PLT-FR---_516A_51615AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51615AL05A____02____CLOSING-PLT-FR---_516A_51615AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51615AL06A____00____CLOSING-PLT-S-U-F_516A_51630AL04A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51615AL07A____00____CLOSING-PLT-S-U-F_516A_51630AL04A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51615AL09A____00____CLOSING-PLT-FF-A-_516A_51615AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51620AL16A____02____FRAME-SD-UPR-IN--_516A_51620AL04A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51620AL17A____02____FRAME-SD-UPR-IN--_516A_51620AL04A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51620AL24A____02____FRAME-SD-UPR-F---_516A_51630AL04A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51620AL25A____02____FRAME-SD-UPR-F---_516A_51630AL04A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51620AL28A____03____FRAME-SD-FF-A----_516A_51620AL22A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51620AL30A____03____FRAME-SD-FF-B----_516A_51620AL22A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51620AL31A____03____FRAME-SD-FF-B----_516A_51620AL22A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL18A____04____BRKT-F-SUS-UPR---_516A_51625AL04A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL19A____04____BRKT-F-SUS-UPR---_516A_51625AL04A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL22A____01____BRKT-F-SUS-LWR---_516A_51625AL04A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL23A____01____BRKT-F-SUS-LWR---_516A_51625AL04A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL26A____01____BRKT-H-U---------_516A_51625AL04A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL32A____00____BRKT-BEAM-F-IN---_516A_51620AL22A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL33A____00____BRKT-BEAM-F-IN---_516A_51620AL22A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL36A____00____BRKT-BEAM-F-OUT--_516A_51615AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL37A____00____BRKT-BEAM-F-OUT--_516A_51615AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL40A____00____BRKT-BEAM-F-IN-U-_516A_51620AL22A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL41A____00____BRKT-BEAM-F-IN-U-_516A_51620AL22A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL44A____00____BRKT-WASH-TANK---_516A_51630AL04A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL45A____00____BRKT-FUSE--------_516A_51630AL04A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL48A____00____BRKT-H-L-SD------_516A_51625AL00A_00_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL49A____00____BRKT-H-L-SD------_516A_51625AL00A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL50A____00____BRKT-APRON-R-LWR-_516B_51625AL08A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL51A____00____BRKT-APRON-R-LWR-_516B_51625AL08A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL54A____02____BRKT-LUGG-HOOK---_516B_51625AL12A_02_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL56A____00____BRKT-CSTR-APRON-F_516B_51625AL14A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51625AL57A____00____BRKT-CSTR-APRON-R_516B_51625AL15A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL08A____03____REINF-TB-LWR-----_516A_51630AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL09A____03____REINF-TB-LWR-----_516A_51630AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL12A____01____REINF-F-SUS-UPR--_516A_51625AL04A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL13A____01____REINF-F-SUS-UPR--_516A_51625AL04A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL16A____02____REINF-F-SUS-LWR--_516A_51625AL04A_05_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL17A____02____REINF-F-SUS-LWR--_516A_51625AL04A_05_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL22A____02____REINF-APR-F------_516A_51630AL04A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL23A____02____REINF-APR-F------_516A_51630AL04A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51630AL24A____01____REINF-BATTERY----_516A_51630AL04A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51632AL00A____01____SEPARATOR-FR-SD-U_516A_51620AL04A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51632AL01A____01____SEPARATOR-FR-SD-U_516A_51620AL04A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51632AL05A____00____SEPARATOR-FF-F---_516A_51620AL22A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51633AL04A____00____DBLR-FF-F--------_516A_51620AL22A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51633AL05A____02____DOUBLER-FF-F-----_516A_51620AL22A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51633AL08A____00____DOUBLER-TB-LWR---_516A_51630AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51633AL09A____00____DOUBLER-TB-LWR---_516A_51630AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51634AL00A____00____PATCH-TOE-BOARD--_516A_51630AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51635AL00A____00____PLATE-FF---------_516A_51620AL00A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51635AL01A____00____PLATE-FF---------_516A_51620AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51635AL03A____00____PLATE-CLS-PLT-FF-_516A_51615AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51636AL00A____02____GUSSET-BATTERY---_516A_51615AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51636AL01A____00____GUSSET-BAT-LWR---_516A_51615AL02A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51636AL02A____00____GUSSET-APR-F-----_516A_51615AL02A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51660AL00A____02____WHEEL-APRON-R----_516B_51660AL00A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51660AL01A____02____WHEEL-APRON-R----_516B_51660AL00A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51676AL04A____01____BRKT-FEND-UP-F---_516A_51676AL00A_01_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51676AL06A____03____BRKT-FEND-UP-R---_516A_51676AL02A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51676AL12A____01____BRKT-FEND-UP-F---_516A_51676AL00A_01_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51676AL14A____03____BRKT-FEND-UP-R---_516A_51676AL02A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51676AL20A____00____BRKT-SEAT-HINGE--_516B_51676AL16A_01_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51676AL21A____00____BRKT-SEAT-HINGE--_516B_51676AL16A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51680AL04A____03____REINF-R-APRON-OUT_516B_51680AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_51680AL05A____03____REINF-R-APRON-OUT_516B_51680AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52120AL01A____01____FLOOR-PAN-F-A----_521A_52120AL00A_02_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52120AL02A____01____FLOOR-PAN-F-B----_521A_52120AL00A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52120AL03A____01____FLOOR-PAN-F-B----_521A_52120AL00A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52136AL02A____01____PATCH-CRM-B------_521C_52140AJ16A_12_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52140AL14A____02____CROSS-MBR-A-R----_521C_52140AL13A_02_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52143AL03A____00____REINF-CRM-A-R-SD-_521C_52140AL13A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52143AL04A____00____REINF-CRM-A-R-SD-_521C_52140AL13A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52145AL00A____00____REINF-FRM-SD-R-F-_521C_52146AL01A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52146AL00A____00____DBLR-FRM-SD-R-F--_521C_52146AL00A_00_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52146AL04A____00____DBLR-FRM-SD-R-F--_521C_52146AL00A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52153AL08A____00____BRKT-SELECT-LEVER_521A_52140AL04A_00_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52154AL06A____01____SIDE-SILL-IN-R---_521C_52154AL00A_01_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52154AL07A____01____SIDE-SILL-IN-R---_521C_52154AL00A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52157AL02A____00____SEPARATE-SILL-R-U_521C_52154AL00A_01_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52157AL03A____00____SEPARATE-SILL-R-U_521C_52154AL00A_01_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52210AL00A____02____TOE-BOARD-SIA----_522A_52200AL00A_02_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52230AL00A____00____BRKT-ACCEL-SIA---_522A_52200AL00A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52230AL04A____00____BRKT-BLOWER-SIA--_522A_52200AL00A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52421AL00A____01____SKIRT-R-OUT-OBK--_524B_52401AL01A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52421AL01A____01____SKIRT-R-IN-OBK---_524B_52401AL01A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52421AL02A____01____SKIRT-R-IN-SD-OBK_524B_52401AL01A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52421AL03A____01____SKIRT-R-IN-SD-OBK_524B_52401AL01A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52433AL00A____00____REINF-STRIKER-OBK_524B_52401AL01A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52436AL00A____00____BRKT-BMPR-LWR-CTR_524B_52401AL01A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52436AL01A____00____BRKT-BMPR-LWR-SD-_524B_52401AL01A_03_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52710AL00A____01____BULK-HEAD-F------_511C_51121AL00A_04_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52720AL00A____01____SEPARATOR-BULK-C-_511C_51121AL00A_04_CT_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52730AL00A____01____BRKT-WIPER-A-----_511C_51121AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52730AL02A____01____BRKT-WIPER-B-----_511C_51121AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52730AL05A____02____BRKT-STAY-HOOD---_511C_51121AL00A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52730AL07A____01____BRKT-PEDAL-UPR-A-_511C_51121AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52730AL13A____01____BRKT-PDL-BRAKE---_511C_51121AL00A_04_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52730AL16A____02____BRKT-STAY-HOOD---_511C_51121AL00A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52740AL00A____02____REINF-F-PNL-SD---_511C_51121AL00A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52740AL01A____02____REINF-F-PNL-SD---_511C_51121AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52740AL04A____02____REINF-WIPER-SD---_511C_51121AL00A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52740AL05A____02____REINF-WIPER-SD---_511C_51121AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52740AL08A____02____REINF-BULK-SD----_511C_51121AL00A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52740AL09A____02____REINF-BULK-SD----_511C_51121AL00A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52760AL00A____02____FRAME-F-BULK-----_511C_51121AL00A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_52760AL01A____02____FRAME-F-BULK-----_511C_51121AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53020AL00A____02____RAD-PNL-SD-------_530A_53010AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53020AL01A____02____RAD-PNL-SD-------_530A_53010AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53040AL00A____02____STAY-HOOD-LOCK---_530A_53010AL00A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53051AL00A____02____BRKT-RAD-SD------_530A_53010AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53051AL01A____02____BRKT-RAD-SD------_530A_53010AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53051AL02A____01____BRKT-AB-SNSR-----_530A_53010AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53051AL03A____01____BRKT-AB-SNSR-----_530A_53010AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53060AL02A____02____FRAME-RAD-UPR-SIA_530A_53010AL00A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53060AL06A____02____FRAME-RAD-PNL-SD-_530A_53010AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53060AL07A____02____FRAME-RAD-PNL-SD-_530A_53010AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53110AL00A____04____PANEL-F----------_511C_51121AL00A_05_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53405AL00A____02____RAIL-F-LWR-OBK---_510A_53400AL00A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53405AL01A____01____RAIL-F-UPR-------_510A_53400AL00A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53415AL00A____02____RAIL-R-UPR-------_510A_53410AL00A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53415AL01A____03____RAIL-R-LWR-------_510A_53410AL00A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53420AL00A____01____REINF-HINGE------_510A_53410AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53420AL01A____01____REINF-HINGE------_510A_53410AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53440AL00A____00____BRKT-RETR-R------_510A_53410AL00A_03_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53601AL08A____03____ROOF-PNL---------_536A_53601AL08A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53701AL08A____01____BRACE-F-OBK------_536A_53701AL08A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53701AL09A____01____BRACE-A-OBK------_536A_53701AL09A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53701AL10A____01____BRACE-B-OBK------_536A_53701AL10A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53701AL11A____01____BRACE-C-OBK------_536A_53701AL11A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53701AL12A____01____BRACE-D-OBK------_536A_53701AL12A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_53701AL17A____01____BRACE-CTR-OBK----_510A_53700AL00A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_56117AL00A____02____HOOK-NUT---------_561A_56103AL00A_03_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_56120AL00A____01____BATTERY-PAN-A----_561A_56103AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_56120AL01A____01____BATTERY-PAN-B----_561A_56103AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_56122AL00A____01____BRKT-BAT---------_561A_56103AL00A_03_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57120AL02A____06____FENDER-F---------_571A_57120AL00A_06_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57120AL03A____06____FENDER-F---------_571A_57120AL00A_06_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57221AL00A____00____REINF-HOOD-LK----_572A_57221AL00A_00_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57221AL06A____00____REINF-HOOD-STAY--_572A_57221AL06A_00_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57221AL08A____00____REINF-HOOD-HINGE-_572A_57221AL02A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57221AL09A____00____REINF-HOOD-HINGE-_572A_57221AL02A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57230AL00A____01____HOOD-PNL-F-OUT---_572A_57230AL00A_01_CT_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57230AL02A____02____HOOD-PNL-F-IN----_572A_57230AL02A_02_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57231AL00A____01____PLATE-HOOD-LK----_572A_57232AL00A_01_CT_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57231AL02A____00____PLATE-HOOD-HG----_572A_57221AL02A_00_LH_LR.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57232AL02A____01____BRKT-HOOD-LOCK---_572A_57232AL00A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57261AL00A____00____HINGE-BRKT-UPR---_572A_57260AL00A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57261AL01A____00____HINGE-BRKT-UPR---_572A_57260AL00A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57261AL04A____00____HINGE-BRKT-LWR---_572A_57260AL00A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57261AL05A____00____HINGE-BRKT-LWR---_572A_57260AL00A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57602AL06A____02____FLAP-OUT-OBK-----_576A_57601AL07A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_57602AL17A____02____FLAP-INNER-OBK---_576A_57601AL07A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60030AL00A____06____PANEL-IN-DR-F----_600H_60030AL00A_06_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60030AL01A____06____PANEL-IN-DR-F----_600H_60030AL00A_06_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60040AL02A____02____PNL-OUT-DR-F-OBK-_600H_60040AL00A_02_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60040AL03A____02____PNL-OUT-DR-F-OBK-_600H_60040AL00A_02_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60060AL00A____03____REINF-HNG-DR-F---_600H_60060AL00A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60060AL01A____03____REINF-HNG-DR-F---_600H_60060AL00A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60063AL00A____02____REINF-DR-F-OUT-UP_600H_60053AL00A_02_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60063AL01A____02____REINF-DR-F-OUT-UP_600H_60053AL00A_02_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60064AL00A____02____REINF-DR-F-OUT---_600H_60064AL00A_02_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60064AL01A____02____REINF-DR-F-OUT---_600H_60064AL00A_02_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60068AL00A____02____REINF-A-F-OUT-UP-_600H_60053AL00A_02_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60068AL01A____02____REINF-A-F-OUT-UP-_600H_60053AL00A_02_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60068AL02A____02____REINF-MIR-DR-F---_600H_60053AL00A_02_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60068AL03A____02____REINF-MIR-DR-F---_600H_60053AL00A_02_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60430AL00A____04____PANEL-IN-DR-R----_604A_60430AL00A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60430AL01A____04____PANEL-IN-DR-R----_604A_60430AL00A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60440AL02A____04____PNL-OUT-DR-R-OBK-_604A_60440AL00A_04_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60440AL03A____04____PNL-OUT-DR-R-OBK-_604A_60440AL00A_04_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60460AL00A____03____REINF-HNG-DR-R---_604A_60460AL00A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60460AL01A____03____REINF-HNG-DR-R---_604A_60460AL00A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60463AL00A____03____REINF-DR-R-OUT-UP_604A_60463AL00A_03_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60463AL01A____03____REINF-DR-R-OUT-UP_604A_60463AL00A_03_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60464AL00A____00____REINF-DR-R-OUT-C-_604A_60464AL00A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60464AL01A____00____REINF-DR-R-OUT-C-_604A_60464AL00A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60484AL00A____00____REINF-PTN-DR-R-LW_604A_60484AL00A_00_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60484AL01A____00____REINF-PTN-DR-R-LW_604A_60484AL00A_00_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60830AL00A____06____PANEL-IN-R-GATE--_608A_60830AL00A_06_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60840AL00A____03____PANEL-OUT-R-G----_608A_60840AL00A_03_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60860AL00A____04____REINF-HNG-R-GATE-_608A_60850AL00A_04_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60860AL01A____04____REINF-HNG-R-GATE-_608A_60850AL00A_04_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60861AL00A____01____RNF-LATCH-R-G----_608A_60861AL00A_01_CT_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60867AL00A____02____RNF-STPR-R-G-----_608A_60857AL00A_02_RH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60867AL01A____02____RNF-STPR-R-G-----_608A_60857AL00A_02_LH_AS.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60885AL00A____01____BRKT-FINISHER-R-G_608A_60885AL00A_01_RH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60885AL01A____01____BRKT-FINISHER-R-G_608A_60885AL00A_01_LH_SY.xv2;
        '        '\\Gj1np26n\Public\share\SKE1\2013-10-08\Bf4ModelXvl\GBF4_60885AL03A____01____BRKT-GRIP-HDL-RG-_608A_60885AL02A_01_RH_AS.xv2

        '    End With
        'End Sub

    End Class

End Namespace
