Imports FarPoint.Win.Spread.CellType
Imports EventSakusei.EventEdit.Logic
Imports ShisakuCommon
Imports ShisakuCommon.Util
Imports FarPoint.Win.Spread
Imports EventSakusei.EventEdit.Ui
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Ui.Valid
Imports ShisakuCommon.Ui.Spd
Imports System.Text

Namespace EventEdit
    Public Class SpdCompleteCarObserver : Implements Frm9SpdObserver, Frm9SpdSetter(Of EventEditCompleteCar)
#Region "各列のTag"
        Private Const TAG_SHUBETSU As String = "SHUBETSU"
        Private Const TAG_GOSHA As String = "GOSHA"
        Public Const TAG_SHAGATA As String = "SHAGATA"
        Public Const TAG_GRADE As String = "GRADE"
        Private Const TAG_SHIMUKECHI_SHIMUKE As String = "SHIMUKECHI_SHIMUKE"
        Private Const TAG_HANDLE As String = "HANDLE"
        Private Const TAG_EG_KATASHIKI As String = "EG_KATASHIKI"
        Private Const TAG_EG_HAIKIRYO As String = "EG_HAIKIRYO"
        Private Const TAG_EG_SYSTEM As String = "EG_SYSTEM"
        Private Const TAG_EG_KAKYUKI As String = "EG_KAKYUKI"
        Public Const TAG_EG_MEMO_1 As String = "EG_MEMO_1"
        Public Const TAG_EG_MEMO_2 As String = "EG_MEMO_2"
        Public Const TAG_TM_KUDO As String = "TM_KUDO"
        Public Const TAG_TM_HENSOKUKI As String = "TM_HENSOKUKI"
        Private Const TAG_TM_FUKU_HENSOKUKI As String = "TM_FUKU_HENSOKUKI"
        Public Const TAG_TM_MEMO_1 As String = "TM_MEMO_1"
        Public Const TAG_TM_MEMO_2 As String = "TM_MEMO_2"
        Private Const TAG_KATASHIKI As String = "KATASHIKI"
        Private Const TAG_SHIMUKE As String = "SHIMUKE"
        Private Const TAG_OP As String = "OP"
        Private Const TAG_GAISO_SHOKU As String = "GAISO_SHOKU"
        Public Const TAG_GAISO_SHOKU_NAME As String = "GAISO_SHOKU_NAME"
        Private Const TAG_NAISO_SHOKU As String = "NAISO_SHOKU"
        Public Const TAG_NAISO_SHOKU_NAME As String = "NAISO_SHOKU_NAME"
        Private Const TAG_SHADAI_NO As String = "SHADAI_NO"
        Public Const TAG_SHIYOU_MOKUTEKI As String = "SHIYOU_MOKUTEKI"
        Public Const TAG_SHIKEN_MOKUTEKI As String = "SHIKEN_MOKUTEKI"
        Public Const TAG_SHIYO_BUSHO As String = "SHIYO_BUSHO"
        Private Const TAG_GROUP As String = "GROUP"
        Private Const TAG_SEISAKU_JUNJYO As String = "SEISAKU_JUNJYO"
        Private Const TAG_KANSEIBI As String = "KANSEIBI"
        Private Const TAG_KOSHI_NO As String = "KOSHI_NO"
        Private Const TAG_SEISAKU_HOUHOU_KBN As String = "SEISAKU_HOUHOU_KBN"
        Public Const TAG_SEISAKU_HOUHOU As String = "SEISAKU_HOUHOU"
        Public Const TAG_SHISAKU_MEMO As String = "SHISAKU_MEMO"

#End Region

        Private Shared ReadOnly DEFAULT_LOCK_TAGS As String() = {TAG_SHUBETSU, TAG_SHAGATA, TAG_GRADE, TAG_SHIMUKECHI_SHIMUKE, _
                                                                TAG_HANDLE, TAG_EG_KATASHIKI, TAG_EG_HAIKIRYO, TAG_EG_SYSTEM, _
                                                                TAG_EG_KAKYUKI, TAG_EG_MEMO_1, TAG_EG_MEMO_2, TAG_TM_KUDO, _
                                                                TAG_TM_HENSOKUKI, TAG_TM_FUKU_HENSOKUKI, TAG_TM_MEMO_1, TAG_TM_MEMO_2, _
                                                                TAG_KATASHIKI, TAG_SHIMUKE, TAG_OP, TAG_GAISO_SHOKU, TAG_GAISO_SHOKU_NAME, _
                                                                TAG_NAISO_SHOKU, TAG_NAISO_SHOKU_NAME, TAG_SHADAI_NO, TAG_SHIYOU_MOKUTEKI, _
                                                                TAG_SHIKEN_MOKUTEKI, TAG_SHIYO_BUSHO, TAG_GROUP, TAG_SEISAKU_JUNJYO, _
                                                                TAG_KANSEIBI, TAG_KOSHI_NO, TAG_SEISAKU_HOUHOU_KBN, TAG_SEISAKU_HOUHOU, _
                                                                TAG_SHISAKU_MEMO}

        ' DEFAULT_LOCK_TAGS のうち、工指No 以外
        Private Shared ReadOnly UNLOCKABLE_TAGS As String() = {TAG_SHUBETSU, TAG_SHAGATA, TAG_GRADE, TAG_SHIMUKECHI_SHIMUKE, _
                                                                TAG_HANDLE, TAG_EG_KATASHIKI, TAG_EG_HAIKIRYO, TAG_EG_SYSTEM, _
                                                                TAG_EG_KAKYUKI, TAG_EG_MEMO_1, TAG_EG_MEMO_2, TAG_TM_KUDO, _
                                                                TAG_TM_HENSOKUKI, TAG_TM_FUKU_HENSOKUKI, TAG_TM_MEMO_1, TAG_TM_MEMO_2, _
                                                                TAG_KATASHIKI, TAG_SHIMUKE, TAG_OP, TAG_GAISO_SHOKU, TAG_GAISO_SHOKU_NAME, _
                                                                TAG_NAISO_SHOKU, TAG_NAISO_SHOKU_NAME, TAG_SHADAI_NO, TAG_SHIYOU_MOKUTEKI, _
                                                                TAG_SHIKEN_MOKUTEKI, TAG_SHIYO_BUSHO, TAG_GROUP, TAG_SEISAKU_JUNJYO, _
                                                                TAG_KANSEIBI, TAG_SEISAKU_HOUHOU_KBN, TAG_SEISAKU_HOUHOU, _
                                                                TAG_SHISAKU_MEMO}

        Private ReadOnly spread As FpSpread
        Private ReadOnly sheet As SheetView
        Private subject As EventEditCompleteCar
        Private ReadOnly titleRows As Integer
        Private validator As SpreadValidator
        '号車DELETEﾁｪｯｸ用に追加
        Private GousyaDeleteCheck As String = ""

#Region "各列のCellTypeプロパティとGet/Set"
        '' 車型CellType
        Private _shagataCellType As TextCellType
        '' グレードCellType
        Private _gradeCellType As TextCellType
        '' 仕向地・仕向けCellType
        Private _shimukechiShimukeCellType As ComboBoxCellType
        '' ハンドルCellType
        Private _handleCellType As TextCellType
        '' E/G・型式CellType
        Private _egKatashikiCellType As TextCellType
        '' E/G・排気量CellType
        Private _egHaikiryoCellType As TextCellType
        '' E/G・システムCellType
        Private _egSystemCellType As TextCellType
        '' E/G・過給器CellType
        Private _egKakyukiCellType As TextCellType
        '' E/G・メモ１CellType
        Private _egMemo1CellType As TextCellType
        '' E/G・メモ２CellType
        Private _egMemo2CellType As TextCellType
        '' T/M・駆動CellType
        Private _tmKudoCellType As TextCellType
        '' T/M・変速機CellType
        Private _tmHensokukiCellType As TextCellType
        '' T/M・副変則CellType
        Private _tmFukuHensokukiCellType As TextCellType
        '' T/M・メモ１CellType
        Private _tmMemo1CellType As TextCellType
        '' T/M・メモ２CellType
        Private _tmMemo2CellType As TextCellType
        '' 型式CellType
        Private _katashikiCellType As TextCellType
        '' 仕向CellType
        Private _shimukeCellType As ComboBoxCellType
        '' OP CellType
        Private _opCellType As TextCellType
        '' 外装色CellType
        Private _gaisoShokuCellType As TextCellType
        '' 外装色名CellType
        Private _gaisoShokuNameCellType As TextCellType
        '' 内装色CellType
        Private _naisoShokuCellType As TextCellType
        '' 内装色名CellType
        Private _naisoShokuNameCellType As TextCellType
        '' 車台№CellType
        Private _shadaiNoCellType As TextCellType
        '' 使用目的CellType
        Private _shiyouMokutekiCellType As TextCellType
        '' 試験目的CellType
        Private _shikenMokutekiCellType As TextCellType
        '' 使用部署CellType
        Private _shiyoBushoCellType As TextCellType
        '' グループCellType
        Private _groupCellType As TextCellType
        '' 製作順序CellType
        Private _seisakuJunjyoCellType As TextCellType
        '' 完成日CellType
        Private _kanseibiCellType As DateTimeCellType
        '' 工指№CellType
        Private _koshiNoCellType As TextCellType
        '' 製作方法区分CellType
        Private _seisakuHouhouKbnCellType As TextCellType
        '' 製作方法CellType
        Private _seisakuHouhouCellType As TextCellType
        '' メモCellType
        Private _memoCellType As TextCellType


        ''' <summary>車型CellType</summary>
        ''' <value>車型CellType</value>
        ''' <returns>車型CellType</returns>
        Public ReadOnly Property ShagataCellType() As TextCellType
            Get
                If _shagataCellType Is Nothing Then
                    _shagataCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _shagataCellType.MaxLength = 20
                    _shagataCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _shagataCellType
            End Get
        End Property

        ''' <summary>グレードCellType</summary>
        ''' <value>グレードCellType</value>
        ''' <returns>グレードCellType</returns>
        Public ReadOnly Property GradeCellType() As TextCellType
            Get
                If _gradeCellType Is Nothing Then
                    _gradeCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _gradeCellType.MaxLength = 20
                    _gradeCellType.CharacterSet = CharacterSet.AllIME  '' グレードは半角カナもできる。
                End If
                Return _gradeCellType
            End Get
        End Property

        ''' <summary>仕向地・仕向CellType</summary>
        ''' <value>仕向地・仕向CellType</value>
        ''' <returns>仕向地・仕向CellType</returns>
        Public ReadOnly Property ShimukechiShimukeCellType() As ComboBoxCellType
            Get
                If _shimukechiShimukeCellType Is Nothing Then
                    _shimukechiShimukeCellType = SpreadUtil.CreateComboBoxCellType(Logic.EventEdit.GetLabelValues_Shimuke, False)
                    _shimukechiShimukeCellType.MaxLength = 6
                    _shimukechiShimukeCellType.Editable = True
                    _shimukechiShimukeCellType.EditorValue = EditorValue.String
                    _shimukechiShimukeCellType.CharacterCasing = CharacterCasing.Upper
                End If
                Return _shimukechiShimukeCellType
            End Get
        End Property

        ''' <summary>ハンドルCellType</summary>
        ''' <value>ハンドルCellType</value>
        ''' <returns>ハンドルCellType</returns>
        Public ReadOnly Property HandleCellType() As TextCellType
            Get
                If _handleCellType Is Nothing Then
                    _handleCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _handleCellType.MaxLength = 3
                End If
                Return _handleCellType
            End Get
        End Property

        ''' <summary>E/G・型式CellType</summary>
        ''' <value>E/G・型式CellType</value>
        ''' <returns>E/G・型式CellType</returns>
        Public ReadOnly Property EgKatashikiCellType() As TextCellType
            Get
                If _egKatashikiCellType Is Nothing Then
                    _egKatashikiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _egKatashikiCellType.MaxLength = 3
                End If
                Return _egKatashikiCellType
            End Get
        End Property

        ''' <summary>E/G・排気量CellType</summary>
        ''' <value>E/G・排気量CellType</value>
        ''' <returns>E/G・排気量CellType</returns>
        Public ReadOnly Property EgHaikiryoCellType() As TextCellType
            Get
                If _egHaikiryoCellType Is Nothing Then
                    _egHaikiryoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _egHaikiryoCellType.MaxLength = 4
                    _egHaikiryoCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _egHaikiryoCellType
            End Get
        End Property

        ''' <summary>E/G・システムCellType</summary>
        ''' <value>E/G・システムCellType</value>
        ''' <returns>E/G・システムCellType</returns>
        Public ReadOnly Property EgSystemCellType() As TextCellType
            Get
                If _egSystemCellType Is Nothing Then
                    _egSystemCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _egSystemCellType.MaxLength = 4
                End If
                Return _egSystemCellType
            End Get
        End Property

        ''' <summary>E/G・過給器CellType</summary>
        ''' <value>E/G・過給器CellType</value>
        ''' <returns>E/G・過給器CellType</returns>
        Public ReadOnly Property EgKakyukiCellType() As TextCellType
            Get
                If _egKakyukiCellType Is Nothing Then
                    _egKakyukiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _egKakyukiCellType.MaxLength = 4
                    _egKakyukiCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _egKakyukiCellType
            End Get
        End Property

        ''' <summary>E/G・メモ１CellType</summary>
        ''' <value>E/G・メモ１CellType</value>
        ''' <returns>E/G・メモ１CellType</returns>
        Public ReadOnly Property EgMemo1CellType() As TextCellType
            Get
                If _egMemo1CellType Is Nothing Then
                    _egMemo1CellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _egMemo1CellType.MaxLength = 50
                    _egMemo1CellType.CharacterSet = CharacterSet.AllIME  '' 試験目的は全角も半角も入力出来る
                End If
                Return _egMemo1CellType
            End Get
        End Property

        ''' <summary>E/G・メモ２CellType</summary>
        ''' <value>E/G・メモ２CellType</value>
        ''' <returns>E/G・メモ２CellType</returns>
        Public ReadOnly Property EgMemo2CellType() As TextCellType
            Get
                If _egMemo2CellType Is Nothing Then
                    _egMemo2CellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _egMemo2CellType.MaxLength = 50
                    _egMemo2CellType.CharacterSet = CharacterSet.AllIME  '' 試験目的は全角も半角も入力出来る
                End If
                Return _egMemo2CellType
            End Get
        End Property

        ''' <summary>T/M・駆動CellType</summary>
        ''' <value>T/M・駆動CellType</value>
        ''' <returns>T/M・駆動CellType</returns>
        Public ReadOnly Property TmKudoCellType() As TextCellType
            Get
                If _tmKudoCellType Is Nothing Then
                    _tmKudoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _tmKudoCellType.MaxLength = 4
                    _tmKudoCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _tmKudoCellType
            End Get
        End Property

        ''' <summary>T/M・変速機CellType</summary>
        ''' <value>T/M・変速機CellType</value>
        ''' <returns>T/M・変速機CellType</returns>
        Public ReadOnly Property TmHensokukiCellType() As TextCellType
            Get
                If _tmHensokukiCellType Is Nothing Then
                    _tmHensokukiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _tmHensokukiCellType.MaxLength = 8
                    _tmHensokukiCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _tmHensokukiCellType
            End Get
        End Property

        ''' <summary>T/M・副変則CellType</summary>
        ''' <value>T/M・副変則CellType</value>
        ''' <returns>T/M・副変則CellType</returns>
        Public ReadOnly Property TmFukuHensokukiCellType() As TextCellType
            Get
                If _tmFukuHensokukiCellType Is Nothing Then
                    _tmFukuHensokukiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _tmFukuHensokukiCellType.MaxLength = 3
                End If
                Return _tmFukuHensokukiCellType
            End Get
        End Property

        ''' <summary>T/M・メモ１CellType</summary>
        ''' <value>T/M・メモ１CellType</value>
        ''' <returns>T/M・メモ１CellType</returns>
        Public ReadOnly Property TmMemo1CellType() As TextCellType
            Get
                If _tmMemo1CellType Is Nothing Then
                    _tmMemo1CellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _tmMemo1CellType.MaxLength = 50
                    _tmMemo1CellType.CharacterSet = CharacterSet.AllIME  '' 試験目的は全角も半角も入力出来る
                End If
                Return _tmMemo1CellType
            End Get
        End Property

        ''' <summary>T/M・メモ２CellType</summary>
        ''' <value>T/M・メモ２CellType</value>
        ''' <returns>T/M・メモ２CellType</returns>
        Public ReadOnly Property TmMemo2CellType() As TextCellType
            Get
                If _tmMemo2CellType Is Nothing Then
                    _tmMemo2CellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _tmMemo2CellType.MaxLength = 50
                    _tmMemo2CellType.CharacterSet = CharacterSet.AllIME  '' 試験目的は全角も半角も入力出来る
                End If
                Return _tmMemo2CellType
            End Get
        End Property

        ''' <summary>型式CellType</summary>
        ''' <value>型式CellType</value>
        ''' <returns>型式CellType</returns>
        Public ReadOnly Property KatashikiCellType() As TextCellType
            Get
                If _katashikiCellType Is Nothing Then
                    _katashikiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _katashikiCellType.MaxLength = 7
                End If
                Return _katashikiCellType
            End Get
        End Property

        ''' <summary>仕向CellType</summary>
        ''' <value>仕向CellType</value>
        ''' <returns>仕向CellType</returns>
        Public ReadOnly Property ShimukeCellType() As ComboBoxCellType
            Get
                If _shimukeCellType Is Nothing Then
                    _shimukeCellType = SpreadUtil.CreateComboBoxCellType(Logic.EventEdit.GetLabelValues_Shimuke, False)
                    _shimukeCellType.MaxLength = 4
                    _shimukeCellType.Editable = True
                    _shimukeCellType.EditorValue = EditorValue.String
                    _shimukeCellType.CharacterCasing = CharacterCasing.Upper
                End If
                Return _shimukeCellType
            End Get
        End Property

        ''' <summary>OP CellType</summary>
        ''' <value>OP CellType</value>
        ''' <returns>OP CellType</returns>
        Public ReadOnly Property OpCellType() As TextCellType
            Get
                If _opCellType Is Nothing Then
                    _opCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _opCellType.MaxLength = 4
                End If
                Return _opCellType
            End Get
        End Property

        ''' <summary>外装色CellType</summary>
        ''' <value>外装色CellType</value>
        ''' <returns>外装色CellType</returns>
        Public ReadOnly Property GaisoShokuCellType() As TextCellType
            Get
                If _gaisoShokuCellType Is Nothing Then
                    _gaisoShokuCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _gaisoShokuCellType.MaxLength = 3
                End If
                Return _gaisoShokuCellType
            End Get
        End Property

        ''' <summary>外装色名CellType</summary>
        ''' <value>外装色名CellType</value>
        ''' <returns>外装色名CellType</returns>
        Public ReadOnly Property GaisoShokuNameCellType() As TextCellType
            Get
                If _gaisoShokuNameCellType Is Nothing Then
                    _gaisoShokuNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _gaisoShokuNameCellType.MaxLength = 50
                    _gaisoShokuNameCellType.CharacterSet = CharacterSet.AllIME  '' 試験目的は全角も半角も入力出来る
                End If
                Return _gaisoShokuNameCellType
            End Get
        End Property

        ''' <summary>内装色CellType</summary>
        ''' <value>内装色CellType</value>
        ''' <returns>内装色CellType</returns>
        Public ReadOnly Property NaisoShokuCellType() As TextCellType
            Get
                If _naisoShokuCellType Is Nothing Then
                    _naisoShokuCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _naisoShokuCellType.MaxLength = 3
                End If
                Return _naisoShokuCellType
            End Get
        End Property

        ''' <summary>内装色名CellType</summary>
        ''' <value>内装色名CellType</value>
        ''' <returns>内装色名CellType</returns>
        Public ReadOnly Property NaisoShokuNameCellType() As TextCellType
            Get
                If _naisoShokuNameCellType Is Nothing Then
                    _naisoShokuNameCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _naisoShokuNameCellType.MaxLength = 50
                    _naisoShokuNameCellType.CharacterSet = CharacterSet.AllIME  '' 試験目的は全角も半角も入力出来る
                End If
                Return _naisoShokuNameCellType
            End Get
        End Property

        ''' <summary>車台№CellType</summary>
        ''' <value>車台№CellType</value>
        ''' <returns>車台№CellType</returns>
        Public ReadOnly Property ShadaiNoCellType() As TextCellType
            Get
                If _shadaiNoCellType Is Nothing Then
                    _shadaiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _shadaiNoCellType.MaxLength = 20
                End If
                Return _shadaiNoCellType
            End Get
        End Property

        ''' <summary>使用目的CellType</summary>
        ''' <value>使用目的CellType</value>
        ''' <returns>使用目的CellType</returns>
        Public ReadOnly Property ShiyouMokutekiCellType() As TextCellType
            Get
                If _shiyouMokutekiCellType Is Nothing Then
                    _shiyouMokutekiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _shiyouMokutekiCellType.MaxLength = 12 ''全角も半角も入力出来る
                    _shiyouMokutekiCellType.CharacterSet = CharacterSet.AllIME  '' 全角も半角も入力出来る
                End If
                Return _shiyouMokutekiCellType
            End Get
        End Property

        ''' <summary>試験目的CellType</summary>
        ''' <value>試験目的CellType</value>
        ''' <returns>試験目的CellType</returns>
        Public ReadOnly Property ShikenMokutekiCellType() As TextCellType
            Get
                If _shikenMokutekiCellType Is Nothing Then
                    _shikenMokutekiCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _shikenMokutekiCellType.MaxLength = 256 ''全角も半角も入力出来る
                    _shikenMokutekiCellType.CharacterSet = CharacterSet.AllIME  '' 試験目的は全角も半角も入力出来る
                End If
                Return _shikenMokutekiCellType
            End Get
        End Property

        ''' <summary>使用部署CellType</summary>
        ''' <value>使用部署CellType</value>
        ''' <returns>使用部署CellType</returns>
        Public ReadOnly Property ShiyoBushoCellType() As TextCellType
            Get
                If _shiyoBushoCellType Is Nothing Then
                    _shiyoBushoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _shiyoBushoCellType.MaxLength = 10
                    _shiyoBushoCellType.CharacterSet = CharacterSet.AllIME  '' 使用部署は全角も半角も入力出来る
                End If
                Return _shiyoBushoCellType
            End Get
        End Property

        ''' <summary>グループCellType</summary>
        ''' <value>グループCellType</value>
        ''' <returns>グループCellType</returns>
        Public ReadOnly Property GroupCellType() As TextCellType
            Get
                If _groupCellType Is Nothing Then
                    _groupCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _groupCellType.MaxLength = 3
                End If
                Return _groupCellType
            End Get
        End Property

        ''' <summary>製作順序CellType</summary>
        ''' <value>製作順序CellType</value>
        ''' <returns>製作順序CellType</returns>
        Public ReadOnly Property SeisakuJunjyoCellType() As TextCellType
            Get
                If _seisakuJunjyoCellType Is Nothing Then
                    _seisakuJunjyoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _seisakuJunjyoCellType.MaxLength = 3
                End If
                Return _seisakuJunjyoCellType
            End Get
        End Property

        ''' <summary>完成日CellType</summary>
        ''' <value>完成日CellType</value>
        ''' <returns>完成日CellType</returns>
        Public ReadOnly Property KanseibiCellType() As DateTimeCellType
            Get
                If _kanseibiCellType Is Nothing Then
                    _kanseibiCellType = ShisakuSpreadUtil.NewGeneralDateTimeCellType
                End If
                Return _kanseibiCellType
            End Get
        End Property

        ''' <summary>工指№CellType</summary>
        ''' <value>工指№CellType</value>
        ''' <returns>工指№CellType</returns>
        Public ReadOnly Property KoshiNoCellType() As TextCellType
            Get
                If _koshiNoCellType Is Nothing Then
                    _koshiNoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    '' 入力しないから最大桁数は設定しない
                End If
                Return _koshiNoCellType
            End Get
        End Property

        ''' <summary>製作方法区分CellType</summary>
        ''' <value>製作方法区分CellType</value>
        ''' <returns>製作方法区分CellType</returns>
        Public ReadOnly Property SeisakuHouhouKbnCellType() As TextCellType
            Get
                If _seisakuHouhouKbnCellType Is Nothing Then
                    _seisakuHouhouKbnCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _seisakuHouhouKbnCellType.MaxLength = 2
                End If
                Return _seisakuHouhouKbnCellType
            End Get
        End Property

        ''' <summary>製作方法CellType</summary>
        ''' <value>製作方法CellType</value>
        ''' <returns>製作方法CellType</returns>
        Public ReadOnly Property SeisakuHouhouCellType() As TextCellType
            Get
                If _seisakuHouhouCellType Is Nothing Then
                    _seisakuHouhouCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _seisakuHouhouCellType.MaxLength = 100 ''全角も半角も入力出来る
                    _seisakuHouhouCellType.CharacterSet = CharacterSet.AllIME  '' 製作方法は全角も半角も入力出来る
                End If
                Return _seisakuHouhouCellType
            End Get
        End Property

        ''' <summary>メモCellType</summary>
        ''' <value>メモCellType</value>
        ''' <returns>メモCellType</returns>
        Public ReadOnly Property MemoCellType() As TextCellType
            Get
                If _memoCellType Is Nothing Then
                    _memoCellType = ShisakuSpreadUtil.NewGeneralTextCellType
                    _memoCellType.MaxLength = 256 ''全角も半角も入力出来る
                    _memoCellType.CharacterSet = CharacterSet.AllIME  '' メモは全角も半角も入力出来る
                End If
                Return _memoCellType
            End Get
        End Property
#End Region

        Public Sub New(ByVal spread As FpSpread, ByVal subject As EventEditCompleteCar)
            Me.spread = spread
            If spread.Sheets.Count = 0 Then
                Throw New ArgumentException
            End If
            Me.sheet = spread.Sheets(0)
            Me.subject = subject

            Me.titleRows = EventSpreadUtil.GetTitleRows(sheet)

            subject.addObserver(Me)
        End Sub

        Private Sub Spread_ChangeEventHandlable(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs)
            If e.Row > 2 Then
                OnChange(e.Row, e.Column)
            End If
        End Sub

        Private Sub Spread_VisibleChangedEventHandlable(ByVal sender As Object, ByVal e As System.EventArgs)
            If spread.Visible Then
                subject.notifyObservers()
            End If
        End Sub

        Public Sub SupersedeSubject(ByVal subject As EventEditCompleteCar) Implements Frm9SpdSetter(Of EventEditCompleteCar).SupersedeSubject
            Me.subject = subject
            Me.subject.addObserver(Me)
            SpreadUtil.RemoveHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
            ClearSheetData()
            ReInitialize()
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        Public Sub ClearSheetBackColor() Implements Frm9SpdObserver.ClearSheetBackColor
            sheet.Cells(titleRows, 0, sheet.RowCount - 1, sheet.ColumnCount - 1).ResetBackColor()
        End Sub
        Public Sub ClearSheetData() Implements Frm9SpdObserver.ClearSheetData
            ''''最大行を定数指定に修正
            sheet.RowCount = titleRows + 200
            sheet.ClearRange(titleRows, 0, sheet.RowCount - titleRows, sheet.ColumnCount, False)
        End Sub

        Public Sub Initialize() Implements Frm9SpdObserver.Initialize

            EventSpreadUtil.InitializeFrm9(spread)

            Dim index As Integer = 0
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHUBETSU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GOSHA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHAGATA
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GRADE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIMUKECHI_SHIMUKE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_HANDLE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_KATASHIKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_HAIKIRYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_SYSTEM
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_KAKYUKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_MEMO_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_EG_MEMO_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_KUDO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_HENSOKUKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_FUKU_HENSOKUKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_MEMO_1
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_TM_MEMO_2
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KATASHIKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIMUKE
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_OP
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GAISO_SHOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GAISO_SHOKU_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NAISO_SHOKU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_NAISO_SHOKU_NAME
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHADAI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIYOU_MOKUTEKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIKEN_MOKUTEKI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHIYO_BUSHO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_GROUP
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_JUNJYO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KANSEIBI
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_KOSHI_NO
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_HOUHOU_KBN
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SEISAKU_HOUHOU
            sheet.Columns(EzUtil.Increment(index)).Tag = TAG_SHISAKU_MEMO

            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHUBETSU, EventControlFactory.GetShubetsuCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_GOSHA, EventControlFactory.GetGoshaCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHAGATA, ShagataCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_GRADE, GradeCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHIMUKECHI_SHIMUKE, ShimukechiShimukeCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_HANDLE, HandleCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_EG_KATASHIKI, EgKatashikiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_EG_HAIKIRYO, EgHaikiryoCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_EG_SYSTEM, EgSystemCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_EG_KAKYUKI, EgKakyukiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_EG_MEMO_1, EgMemo1CellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_EG_MEMO_2, EgMemo2CellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_TM_KUDO, TmKudoCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_TM_HENSOKUKI, TmHensokukiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_TM_FUKU_HENSOKUKI, TmFukuHensokukiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_TM_MEMO_1, TmMemo1CellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_TM_MEMO_2, TmMemo2CellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_KATASHIKI, KatashikiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHIMUKE, ShimukeCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_OP, OpCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_GAISO_SHOKU, GaisoShokuCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_GAISO_SHOKU_NAME, GaisoShokuNameCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_NAISO_SHOKU, NaisoShokuCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_NAISO_SHOKU_NAME, NaisoShokuNameCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHADAI_NO, ShadaiNoCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHIYOU_MOKUTEKI, ShiyouMokutekiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHIKEN_MOKUTEKI, ShikenMokutekiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHIYO_BUSHO, ShiyoBushoCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_GROUP, GroupCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SEISAKU_JUNJYO, SeisakuJunjyoCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_KANSEIBI, KanseibiCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_KOSHI_NO, KoshiNoCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SEISAKU_HOUHOU_KBN, SeisakuHouhouKbnCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SEISAKU_HOUHOU, SeisakuHouhouCellType)
            SpreadUtil.BindCellTypeToColumn(sheet, TAG_SHISAKU_MEMO, MemoCellType)

            'InitializeValidator()

            AddHandler spread.LeaveCell, AddressOf OnLeaveCell

            AddHandler spread.VisibleChanged, AddressOf Spread_VisibleChangedEventHandlable
            '' 通常の Spread_Changed()では、CTRL+V/CTRL+ZでChengedイベントが発生しない
            ''（編集モードではない状態で変更された場合は発生しない仕様とのこと。）
            '' CTRL+V/CTRL+Zでもイベントが発生するハンドラを設定する
            SpreadUtil.AddHandlerSheetDataModelChanged(sheet, AddressOf Spread_ChangeEventHandlable)
        End Sub

        'Private Sub InitializeValidator()

        'validator = New SpreadValidator(spread)

        'For Each rowNo As Integer In subject.GetInputRowNos

        '    Dim row As Integer = titleRows + rowNo

        '    If Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value) _
        '       And StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_GROUP).Index).Value) Then
        '        ' validatorA に
        '        '   「グループ」」はRequired
        '        Dim validA As New SpreadValidator(spread)
        '        validA.CheckAll = True
        '        validA.Add(TAG_GROUP).Required()

        '        Dim validAorBorC As New SpreadValidator(spread, "グループを入力してください。")
        '        validAorBorC.Add(validA)

        '        validator.Add(TAG_GOSHA, "号車").Required()
        '        validator.Add(validAorBorC)
        '    End If

        'Next

        'End Sub

        Public Sub ReInitialize() Implements Frm9SpdObserver.ReInitialize
            ' nop
        End Sub

        ''' <summary>
        ''' Spread.LeaveCellEventHandler IME制御
        ''' </summary>
        ''' <param name="sender">LeaveCellEventHandlerに従う</param>
        ''' <param name="e">LeaveCellEventHandlerに従う</param>
        ''' <remarks></remarks>
        Private Sub OnLeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

            'If e.NewColumn = sheet.Columns(TAG_SHIKEN_MOKUTEKI).Index _
            '        AndAlso Not sheet.GetStyleInfo(e.NewRow, e.NewColumn).Locked Then
            '    spread.ImeMode = Windows.Forms.ImeMode.Hiragana
            'ElseIf e.NewColumn = sheet.Columns(TAG_GRADE).Index Then
            '    spread.ImeMode = Windows.Forms.ImeMode.Hiragana
            'Else
            '    spread.ImeMode = Windows.Forms.ImeMode.Disable
            'End If

            'IME制御がイマイチ良くないので以下にしてみる。
            ' 2011/03/19 柳沼
            'spread.ImeMode = Windows.Forms.ImeMode.Disable

        End Sub

        Public Sub Update(ByVal observable As ShisakuCommon.Util.Observable, ByVal args As Object) Implements Observer.Update
            If args Is Nothing Then
                LockSheetIfViewerChange()
                For Each key As Integer In subject.GetInputRowNos
                    Update(Nothing, key)
                Next
            Else
                If Not IsNumeric(args) Then
                    Return
                End If
                Dim rowNo As Integer = Convert.ToInt32(args)
                Dim row As Integer = titleRows + rowNo
                sheet.Cells(row, sheet.Columns(TAG_SHUBETSU).Index).Value = subject.ShisakuSyubetu(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = subject.ShisakuGousya(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHAGATA).Index).Value = subject.ShisakuSyagata(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GRADE).Index).Value = subject.ShisakuGrade(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIMUKECHI_SHIMUKE).Index).Value = subject.ShisakuShimukechiShimuke(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_HANDLE).Index).Value = subject.ShisakuHandoru(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_KATASHIKI).Index).Value = subject.ShisakuEgKatashiki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_HAIKIRYO).Index).Value = subject.ShisakuEgHaikiryou(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_SYSTEM).Index).Value = subject.ShisakuEgSystem(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_KAKYUKI).Index).Value = subject.ShisakuEgKakyuuki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_MEMO_1).Index).Value = subject.ShisakuEgMemo1(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_EG_MEMO_2).Index).Value = subject.ShisakuEgMemo2(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_KUDO).Index).Value = subject.ShisakuTmKudou(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_HENSOKUKI).Index).Value = subject.ShisakuTmHensokuki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_FUKU_HENSOKUKI).Index).Value = subject.ShisakuTmFukuHensokuki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_MEMO_1).Index).Value = subject.ShisakuTmMemo1(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_TM_MEMO_2).Index).Value = subject.ShisakuTmMemo2(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_KATASHIKI).Index).Value = subject.ShisakuKatashiki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIMUKE).Index).Value = subject.ShisakuShimuke(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_OP).Index).Value = subject.ShisakuOp(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU).Index).Value = subject.ShisakuGaisousyoku(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GAISO_SHOKU_NAME).Index).Value = subject.ShisakuGaisousyokuName(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU).Index).Value = subject.ShisakuNaisousyoku(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_NAISO_SHOKU_NAME).Index).Value = subject.ShisakuNaisousyokuName(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHADAI_NO).Index).Value = subject.ShisakuSyadaiNo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIYOU_MOKUTEKI).Index).Value = subject.ShisakuShiyouMokuteki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIKEN_MOKUTEKI).Index).Value = subject.ShisakuShikenMokuteki(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHIYO_BUSHO).Index).Value = subject.ShisakuSiyouBusyo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_GROUP).Index).Value = subject.ShisakuGroup(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_JUNJYO).Index).Value = subject.ShisakuSeisakuJunjyo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_KANSEIBI).Index).Value = DateUtil.ConvYyyymmddToDate(subject.ShisakuKanseibi(rowNo))
                sheet.Cells(row, sheet.Columns(TAG_KOSHI_NO).Index).Value = subject.ShisakuKoushiNo(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_HOUHOU_KBN).Index).Value = subject.ShisakuSeisakuHouhouKbn(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SEISAKU_HOUHOU).Index).Value = subject.ShisakuSeisakuHouhou(rowNo)
                sheet.Cells(row, sheet.Columns(TAG_SHISAKU_MEMO).Index).Value = subject.ShisakuMemo(rowNo)

                'E/G、T/Mのメモらカラムヘッダー
                If StringUtil.IsNotEmpty(subject.ShisakuEgMemo1Label(rowNo)) Then
                    sheet.Cells(2, sheet.Columns(TAG_EG_MEMO_1).Index).Value =  subject.ShisakuEgMemo1Label(rowNo)
                End If
                If StringUtil.IsNotEmpty(subject.ShisakuEgMemo2Label(rowNo)) Then
                    sheet.Cells(2, sheet.Columns(TAG_EG_MEMO_2).Index).Value =  subject.ShisakuEgMemo2Label(rowNo)
                End If
                If StringUtil.IsNotEmpty(subject.ShisakuTmMemo1Label(rowNo)) Then
                    sheet.Cells(2, sheet.Columns(TAG_TM_MEMO_1).Index).Value =  subject.ShisakuTmMemo1Label(rowNo)
                End If
                If StringUtil.IsNotEmpty(subject.ShisakuTmMemo2Label(rowNo)) Then
                    sheet.Cells(2, sheet.Columns(TAG_TM_MEMO_2).Index).Value =  subject.ShisakuTmMemo2Label(rowNo)
                End If

                OnRowLock(row)

            End If
        End Sub

        Private backIsViewer As Boolean?
        Private Sub LockSheetIfViewerChange()

            If Not subject.IsViewerMode.Equals(backIsViewer) Then
                backIsViewer = subject.IsViewerMode
                If subject.IsViewerMode Then
                    LockAllRowsByRule(True)
                    SpreadUtil.LockAllColumns(sheet)
                Else
                    SpreadUtil.UnlockAllColumns(sheet)
                    InitializeColumnsLock()
                    LockAllRowsByRule(False)
                End If
            End If
        End Sub

        Private Sub InitializeColumnsLock()

            For Each Tag As String In DEFAULT_LOCK_TAGS
                sheet.Columns(Tag).Locked = True
            Next
            sheet.Columns(TAG_GOSHA).Locked = subject.IsSekkeiTenkaiIkou
        End Sub

        Private Sub LockAllRowsByRule(ByVal isLocked As Boolean)

            For Each rowNo As Integer In subject.GetInputRowNos
                Dim row As Integer = titleRows + rowNo
                LockRowByRule(row, isLocked)
            Next
        End Sub

        Private Sub LockRowByRule(ByVal row As Integer, ByVal IsLocked As Boolean)
            For Each Tag As String In UNLOCKABLE_TAGS
                sheet.Cells(row, sheet.Columns(Tag).Index).Locked = IsLocked
                '号車をデリートした場合、全項目削除する。※コピー、EXCEL取込時は以下のイベントを無効にする。
                'If IsLocked = True Then
                '    sheet.Cells(row, sheet.Columns(Tag).Index).Value = Nothing
                'End If
                If sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value = Nothing Then
                    sheet.Cells(row, sheet.Columns(Tag).Index).Value = Nothing
                End If
            Next
        End Sub
        Private Sub OnRowLock(ByVal row As Integer)
            Dim rowNo As Integer = row - titleRows
            If subject.IsEditModes(rowNo) Then
                LockRowByRule(row, False)
            Else
                LockRowByRule(row, True)
            End If
        End Sub

        Public Sub OnChange(ByVal row As Integer, ByVal columnTag As String)
            If row > 2 Then
                OnChange(row, sheet.Columns(columnTag).Index)
            End If
        End Sub
        Public Sub OnChange(ByVal row As Integer, ByVal column As Integer) Implements Frm9SpdObserver.OnChange
            Dim rowNo As Integer = row - EventSpreadUtil.GetTitleRows(sheet)
            Dim value As Object = sheet.Cells(row, column).Value

            Select Case Convert.ToString(sheet.Columns(column).Tag)
                Case TAG_SHUBETSU
                    subject.ShisakuSyubetu(rowNo) = Convert.ToString(value)
                Case TAG_GOSHA
                    'DELETEされたのか？チェックする。
                    If Convert.ToString(value) = "" And subject.ShisakuGousya(rowNo) <> "" Then
                        '確認を促し、OKなら処理続行。
                        If GousyaDeleteCheck = "" _
                           AndAlso frm01Kakunin.ConfirmOkCancel("号車を削除して宜しいですか？") = MsgBoxResult.Ok Then
                            subject.ShisakuGousya(rowNo) = Convert.ToString(value)
                        Else
                            '１回だけ画面を出すようにフラグをチェンジ
                            If GousyaDeleteCheck = "" Then
                                GousyaDeleteCheck = "Back"
                            Else
                                GousyaDeleteCheck = ""
                            End If
                            '確認を促し、CANCELなら号車をDELETE前に戻す。
                            sheet.Cells(row, column).Text = subject.ShisakuGousya(rowNo)
                        End If
                    Else
                        '通常処理。
                        subject.ShisakuGousya(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_SHAGATA
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuSyagata(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_GRADE
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuGrade(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_SHIMUKECHI_SHIMUKE
                    subject.ShisakuShimukechiShimuke(rowNo) = Convert.ToString(value)
                Case TAG_HANDLE
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuHandoru(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_EG_KATASHIKI
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuEgKatashiki(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_EG_HAIKIRYO
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuEgHaikiryou(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_EG_SYSTEM
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuEgSystem(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_EG_KAKYUKI
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuEgKakyuuki(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_EG_MEMO_1
                    subject.ShisakuEgMemo1(rowNo) = Convert.ToString(value)
                Case TAG_EG_MEMO_2
                    subject.ShisakuEgMemo2(rowNo) = Convert.ToString(value)
                Case TAG_TM_KUDO
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuTmKudou(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_TM_HENSOKUKI
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuTmHensokuki(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_TM_FUKU_HENSOKUKI
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuTmFukuHensokuki(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_TM_MEMO_1
                    subject.ShisakuTmMemo1(rowNo) = Convert.ToString(value)
                Case TAG_TM_MEMO_2
                    subject.ShisakuTmMemo2(rowNo) = Convert.ToString(value)
                Case TAG_KATASHIKI
                    '半角のみ対応'
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuKatashiki(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_SHIMUKE
                    subject.ShisakuShimuke(rowNo) = Convert.ToString(value)
                Case TAG_OP
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuOp(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_GAISO_SHOKU
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuGaisousyoku(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_GAISO_SHOKU_NAME
                    subject.ShisakuGaisousyokuName(rowNo) = Convert.ToString(value)
                Case TAG_NAISO_SHOKU
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuNaisousyoku(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_NAISO_SHOKU_NAME
                    subject.ShisakuNaisousyokuName(rowNo) = Convert.ToString(value)
                Case TAG_SHADAI_NO
                    'If SpellCheck(Convert.ToString(value)) Then
                    subject.ShisakuSyadaiNo(rowNo) = Convert.ToString(value)
                    'End If
                Case TAG_SHIYOU_MOKUTEKI
                    subject.ShisakuShiyouMokuteki(rowNo) = Convert.ToString(value)
                Case TAG_SHIKEN_MOKUTEKI
                    subject.ShisakuShikenMokuteki(rowNo) = Convert.ToString(value)
                Case TAG_SHIYO_BUSHO
                    subject.ShisakuSiyouBusyo(rowNo) = Convert.ToString(value)
                Case TAG_GROUP
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuGroup(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_SEISAKU_JUNJYO
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuSeisakuJunjyo(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_KANSEIBI
                    subject.ShisakuKanseibi(rowNo) = DateUtil.ConvDateValueToIneteger(value)
                Case TAG_KOSHI_NO
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuKoushiNo(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_SEISAKU_HOUHOU_KBN
                    If SpellCheck(Convert.ToString(value)) Then
                        subject.ShisakuSeisakuHouhouKbn(rowNo) = Convert.ToString(value)
                    End If
                Case TAG_SEISAKU_HOUHOU
                    subject.ShisakuSeisakuHouhou(rowNo) = Convert.ToString(value)
                Case TAG_SHISAKU_MEMO
                    subject.ShisakuMemo(rowNo) = Convert.ToString(value)

            End Select
            subject.NotifyObservers(rowNo)
        End Sub

        ''' <summary>
        ''' 文字列チェック
        ''' </summary>
        ''' <param name="value">対象文字列</param>
        ''' <returns>ALL半角ならTrue</returns>
        ''' <remarks></remarks>
        Private Function SpellCheck(ByVal value As String) As Boolean
            '文字列の長さ
            Dim valueLength As Integer = value.Length
            Dim Enc As Encoding = Encoding.GetEncoding("Shift_JIS")


            For i As Integer = 0 To valueLength
                Dim c As String = Mid(value, i + 1, 1)

                '半角か全角かチェック'
                If Not StringUtil.IsEmpty(c) Then
                    If Enc.GetByteCount(c) = 1 Then

                    Else
                        Return False
                    End If
                End If
            Next
            Return True
        End Function


        Public Sub AssertValidateRegister()

            For Each rowNo As Integer In subject.GetInputRowNos
                Dim row As Integer = titleRows + rowNo

                validator = New SpreadValidator(spread)

                If Not StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_GOSHA).Index).Value) _
                   And StringUtil.IsEmpty(sheet.Cells(row, sheet.Columns(TAG_GROUP).Index).Value) Then
                    ' validatorA に
                    '   「グループ」」はRequired
                    Dim validA As New SpreadValidator(spread)
                    validA.CheckAll = True
                    validA.Add(TAG_GROUP).Required()

                    Dim validAorBorC As New SpreadValidator(spread, "グループを入力してください。")
                    validAorBorC.Add(validA)

                    validator.Add(TAG_GOSHA, "号車").Required()
                    validator.Add(validAorBorC)

                    validator.AssertValidate(row)
                End If
            Next
        End Sub
    End Class
End Namespace