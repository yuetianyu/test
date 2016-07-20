'↓↓2014/10/16 酒井 ADD BEGIN
Imports ShisakuCommon
Imports ShisakuCommon.Ui
Imports ShisakuCommon.Db.EBom.Vo
Imports ShisakuCommon.Db.EBom.Dao
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports EventSakusei.EventEdit.Dao
Imports EventSakusei.ShisakuBuhinEdit
Imports EventSakusei.TehaichoEdit

Public Class frm41SaiKakunin
    Public frm41 As Frm41DispShisakuBuhinEdit00
    Public frm20 As frm20DispTehaichoEdit
    '↓↓2014/10/28 酒井 ADD BEGIN
    Private maxlen As Integer
    '↑↑2014/10/28 酒井 ADD END

    Public Function GetSai(ByVal shisakuEventCode As String) As Boolean
        Dim shisakuEventDao As TShisakuEventDao = New TShisakuEventDaoImpl
        Dim shisakuEventVo As TShisakuEventVo = shisakuEventDao.FindByPk(shisakuEventCode)

        '設計展開済み号車取得
        Dim lstGousha As New ArrayList
        Dim shisakuEventBaseDao As TShisakuEventBaseDao = New TShisakuEventBaseDaoImpl
        Dim param As New TShisakuEventBaseVo
        param.ShisakuEventCode = shisakuEventCode
        Dim st As String
        For Each vo As TShisakuEventBaseVo In shisakuEventBaseDao.FindBy(param)
            Dim p As Integer = vo.ShisakuGousya.IndexOf("#")
            If p > 0 Then
                st = vo.ShisakuGousya.Substring(p + 1)
            Else
                st = vo.ShisakuGousya
            End If
            lstGousha.Add(st)
        Next


        If shisakuEventVo Is Nothing Then
            Return False
        ElseIf StringUtil.IsEmpty(shisakuEventVo.SeisakuichiranHakouNo) Then
            Return False
        End If

        Dim seisakuIchiranDao As SeisakuIchiranDao = New SeisakuIchiranDaoImpl
        Dim tmp As String
        Dim seisakuIchiranBaseVos As List(Of TSeisakuIchiranBaseVo)
        Dim seisakuIchiranKanseiVos As List(Of TSeisakuIchiranKanseiVo)
        If StringUtil.IsEmpty(shisakuEventVo.SeisakuichiranHakouNoKai) Then
            seisakuIchiranBaseVos = seisakuIchiranDao.GetTSeisakuIchiranBase(shisakuEventVo.SeisakuichiranHakouNo, shisakuEventVo.SeisakuichiranHakouNoKai)
            seisakuIchiranKanseiVos = seisakuIchiranDao.GetTSeisakuIchiranKansei(shisakuEventVo.SeisakuichiranHakouNo, shisakuEventVo.SeisakuichiranHakouNoKai)
        Else
            tmp = "00" & CStr(shisakuEventVo.SeisakuichiranHakouNoKai)
            tmp = Mid(tmp, tmp.Length - 1)
            seisakuIchiranBaseVos = seisakuIchiranDao.GetTSeisakuIchiranBase(shisakuEventVo.SeisakuichiranHakouNo, tmp)
            seisakuIchiranKanseiVos = seisakuIchiranDao.GetTSeisakuIchiranKansei(shisakuEventVo.SeisakuichiranHakouNo, tmp)
        End If

        Me.txtKakunin3.Text = ""
        Dim flg As Boolean = False
        '↓↓2014/10/28 酒井 ADD BEGIN
        maxlen = 0
        Dim blackStartList As New List(Of Integer)
        Dim blackLenList As New List(Of Integer)
        Dim rowNo As Integer = 0
        '↑↑2014/10/28 酒井 ADD END
        For Each seisakuIchiranBaseVo As TSeisakuIchiranBaseVo In seisakuIchiranBaseVos
            Dim tmpBase As String = ""
            Dim tmpKansei As String = ""
            If lstGousha.Contains(seisakuIchiranBaseVo.Gousya) Then
                '設計展開された号車の場合、比較処理

                For Each seisakuIchiranKanseiVo As TSeisakuIchiranKanseiVo In seisakuIchiranKanseiVos
                    If seisakuIchiranBaseVo.Gousya = seisakuIchiranKanseiVo.Gousya Then

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.Syasyu) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.Syasyu) Then
                            If seisakuIchiranBaseVo.Syasyu <> seisakuIchiranKanseiVo.Syasyu Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.Syasyu & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.Syasyu & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.Grade) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.Grade) Then
                            If seisakuIchiranBaseVo.Grade <> seisakuIchiranKanseiVo.Grade Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.Grade & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.Grade & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.Shimuke) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.Shimuke) Then
                            If seisakuIchiranBaseVo.Shimuke <> seisakuIchiranKanseiVo.Shimuke Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.Shimuke & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.Shimuke & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.Handoru) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.Handoru) Then
                            If seisakuIchiranBaseVo.Handoru <> seisakuIchiranKanseiVo.Handoru Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.Handoru & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.Handoru & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.EgHaikiryou) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.EgHaikiryou) Then
                            If seisakuIchiranBaseVo.EgHaikiryou <> seisakuIchiranKanseiVo.EgHaikiryou Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.EgHaikiryou & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.EgHaikiryou & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.EgKatashiki) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.EgKatashiki) Then
                            If seisakuIchiranBaseVo.EgKatashiki <> seisakuIchiranKanseiVo.EgKatashiki Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.EgKatashiki & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.EgKatashiki & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.EgKakyuuki) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.EgKakyuuki) Then
                            If seisakuIchiranBaseVo.EgKakyuuki <> seisakuIchiranKanseiVo.EgKakyuuki Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.EgKakyuuki & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.EgKakyuuki & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.TmKudou) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.TmKudou) Then
                            If seisakuIchiranBaseVo.TmKudou <> seisakuIchiranKanseiVo.TmKudou Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.TmKudou & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.TmKudou & "、"
                            End If
                        End If

                        If StringUtil.IsNotEmpty(seisakuIchiranBaseVo.TmHensokuki) And StringUtil.IsNotEmpty(seisakuIchiranKanseiVo.TmHensokuki) Then
                            If seisakuIchiranBaseVo.TmHensokuki <> seisakuIchiranKanseiVo.TmHensokuki Then
                                tmpBase = tmpBase & seisakuIchiranBaseVo.TmHensokuki & "、"
                                tmpKansei = tmpKansei & seisakuIchiranKanseiVo.TmHensokuki & "、"
                            End If
                        End If

                        If StringUtil.IsEmpty(tmpBase) Then
                            Exit For
                        Else

                            'tmpBase.Substring(0, tmpBase.Length - 1)
                            'tmpKansei.Substring(0, tmpKansei.Length - 1)
                            '↓↓2014/10/21 酒井 ADD BEGIN
                            Dim wk As String = ""
                            If Mid(seisakuIchiranBaseVo.Gousya, 1, 1) = "#" Then
                            Else
                                wk = "#"
                            End If
                            '↓↓2014/10/28 酒井 ADD BEGIN
                            Dim i As Integer = Len(Me.txtKakunin3.Text)
                            '↑↑2014/10/28 酒井 ADD END
                            'Me.txtKakunin3.Text = Me.txtKakunin3.Text _
                            '                        & seisakuIchiranBaseVo.Gousya & " " _
                            '                        & tmpBase.Substring(0, tmpBase.Length - 1) & " → " _
                            '                        & tmpKansei.Substring(0, tmpKansei.Length - 1) & vbCrLf
                            Me.txtKakunin3.Text = Me.txtKakunin3.Text _
                                & wk & seisakuIchiranBaseVo.Gousya & " " _
                                & tmpBase.Substring(0, tmpBase.Length - 1) & " → " _
                                & tmpKansei.Substring(0, tmpKansei.Length - 1) & vbCrLf
                            '↑↑2014/10/21 酒井 ADD END
                            '↓↓2014/10/28 酒井 ADD BEGIN
                            Dim aLen As Integer = Len(wk & seisakuIchiranBaseVo.Gousya & " " _
                                & tmpBase.Substring(0, tmpBase.Length - 1) & " → " _
                                & tmpKansei.Substring(0, tmpKansei.Length - 1))
                            If maxlen < aLen Then
                                maxlen = aLen
                            End If

                            blackStartList.Add(i - rowNo)
                            blackLenList.Add(Len(wk & seisakuIchiranBaseVo.Gousya))

                            blackStartList.Add(i + Len(wk & seisakuIchiranBaseVo.Gousya & " " _
                                & tmpBase.Substring(0, tmpBase.Length - 1)) - rowNo)
                            blackLenList.Add(Len(" → "))

                            rowNo = rowNo + 1
                            '↑↑2014/10/28 酒井 ADD END
                            flg = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
        '↓↓2014/10/28 酒井 ADD BEGIN
        '号車と→は黒字に。
        For i As Integer = 0 To blackStartList.Count - 1
            txtKakunin3.Select(blackStartList(i), blackLenList(i))
            txtKakunin3.SelectionColor = Color.Black
        Next
        '↑↑2014/10/28 酒井 ADD END
        Return flg
    End Function
    Private Sub BtnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        If Not frm41 Is Nothing Then
            frm41.UpdateBtnSai()
        End If
        If Not frm20 Is Nothing Then
            frm20.UpdateBtnSai()
        End If
        '↓↓2014/10/28 酒井 ADD BEGIN
        'frm41SaiKakuninMin画面を親画面と同じ位置に表示
        'frm41SaiKakuninMin.Height = 71
        'frm41SaiKakuninMin.Width = 61
        frm41SaiKakuninMin.Location = New Point(Me.Left + Me.Width - frm41SaiKakuninMin.Width, Me.Top)
        '↑↑2014/10/28 酒井 ADD END
        frm41SaiKakuninMin.frm41 = Me.frm41
        frm41SaiKakuninMin.frm20 = Me.frm20
        frm41SaiKakuninMin.Show()
        Me.Hide()
    End Sub

    Private Sub frm41SaiKakunin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'バージョンをセット
        ShisakuFormUtil.setTitleVersion(Me)
    End Sub
    '↓↓2014/10/21 酒井 ADD BEGIN
    Private Sub frm41SaiKakunin_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        txtKakunin3.Height = Me.Height - 91
        txtKakunin3.Width = Me.Width - 6
    End Sub
    '↑↑2014/10/21 酒井 ADD END

    Public Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Private Sub frm41SaiKakunin_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        '↓↓2014/10/28 酒井 ADD BEGIN
        '概ね文字全体が表示されるサイズに変更する
        Me.Width = maxlen * 12
        '↑↑2014/10/28 酒井 ADD END
    End Sub
End Class
'↑↑2014/10/16 酒井 ADD END
