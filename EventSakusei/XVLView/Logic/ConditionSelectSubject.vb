Imports ShisakuCommon
Imports ShisakuCommon.Db
Imports ShisakuCommon.Db.EBom


Imports ShisakuCommon.Db.Impl
Imports ShisakuCommon.Db.EBom.Exclusion
Imports ShisakuCommon.Db.EBom.Dao.Impl
Imports ShisakuCommon.Db.EBom.Dao

Imports ShisakuCommon.Util
Imports ShisakuCommon.Db.EBom.Vo

Imports EventSakusei.TehaichoSakusei.Logic
Imports EventSakusei.TehaichoSakusei.Dao
Imports ShisakuCommon.Util.LabelValue
Imports EventSakusei.TehaichoSakusei.Vo


Public Class ConditionSelectSubject



#Region "メンバ変数"

    'GUID
    Private mGUID As Guid

    'イベントコード.
    Private mEventCode As String
    '開発符号.
    Private mKaihatsuFugo As String
    ' 試作イベントコード
    Private mShisakuEventCode As String
    ' グループNo
    Private mGroupNo As String

#End Region

#Region "プロパティー"
    ''' <summary>
    ''' イベントコード.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EventCode() As String
        Get
            Return mEventCode
        End Get
        Set(ByVal value As String)
            mEventCode = value
        End Set
    End Property

    ''' <summary>
    ''' 開発符号.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property KaihatsuFugo() As String
        Get
            Return mKaihatsuFugo
        End Get
    End Property



    ''' <summary>グループNo</summary>
    ''' <returns>グループNo</returns>
    Public Property GroupNo() As String
        Get
            Return mGroupNo
        End Get
        Set(ByVal value As String)
            mGroupNo = value

        End Set
    End Property

    ''' <summary>
    ''' GUID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GUID() As Guid
        Get
            Return mGUID
        End Get
        Set(ByVal value As Guid)
            mGUID = value
        End Set
    End Property

#End Region


#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Initialize()

    End Sub

#End Region

#Region "初期化"

    ''' <summary>
    ''' 初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Initialize()

        'GUIDの作成.
        Dim guidValue As Guid = System.Guid.NewGuid()
        mGUID = guidValue

    End Sub

#End Region

#Region "チェックボックス状態変更"
    Public Sub changeCheckBox(ByVal aGroup As GroupBox)
    End Sub
#End Region

#Region "号車チェックボックスコントロールの変更."
    '有効無効の切り替え.


#End Region

#Region "コンボボックスにアイテム設定."

    ''' <summary>
    ''' イベントコードをコンボボックスに設定する.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setEventCodeToComboBox(ByRef aComboBox As System.Windows.Forms.ComboBox)
        Dim iDao As New Dao.Impl.TShisakuEventDaoImpl
        '全てのイベントコードを取得.
        Dim iVos As List(Of Vo.TShisakuEventVo) = iDao.FindByAll()

        'アイテムを削除.
        aComboBox.Items.Clear()

        ''取得したボディ名をコンボボックスに設定.
        For Each lItem In iVos
            Dim itemStr As String = String.Format("{0}:{1}", lItem.ShisakuEventCode, lItem.ShisakuEventName)
            'グループを追加.
            aComboBox.Items.Add(itemStr)
        Next
        'イベントコードを設定.
        If 0 < iVos.Count Then
            aComboBox.Text = aComboBox.Items(0).ToString
            mEventCode = iVos(0).ShisakuEventCode
        End If


    End Sub

    ''' <summary>
    ''' 試作イベント完成車情報からグループ情報を抽出.
    ''' </summary>
    ''' <param name="aComboBox"></param>
    ''' <remarks></remarks>
    Public Sub setGroupItemtoComboBox(ByRef aComboBox As System.Windows.Forms.ComboBox)

        'イベントコードが未設定の場合はエラーを返す.
        If EventCode Is Nothing Then Throw New ShisakuException("イベントコードが選択されていません.")

        Dim iDao As New Dao.Impl.TShisakuEventKanseiDaoImpl
        Dim iVos As List(Of Vo.TShisakuEventKanseiVo) = iDao.FindByAll()

        'アイテムを削除.
        aComboBox.Items.Clear()
        aComboBox.Text = ""

        ''取得したボディ名をコンボボックスに設定.
        For Each lItem In iVos

            'イベントコードが異なる場合は次のレコードへ.
            If lItem.ShisakuEventCode <> EventCode Then Continue For
            If lItem.ShisakuGroup Is Nothing Then Continue For
            '同じグループ№の場合
            If aComboBox.Items.Contains(lItem.ShisakuGroup) Then Continue For

            'グループを追加.
            aComboBox.Items.Add(lItem.ShisakuGroup)

        Next

        If 0 < aComboBox.Items.Count Then aComboBox.Text = aComboBox.Items(0).ToString

    End Sub

    ''' <summary>
    ''' 号車情報の更新.
    ''' </summary>
    ''' <param name="aCheckList"></param>
    ''' <remarks></remarks>
    Public Sub setGousyatoComboBox(ByRef aCheckList As System.Windows.Forms.CheckedListBox)
        aCheckList.Items.Clear()

        Dim iDao As New XVLView.Dao.Impl.ConditionSelectDaoImpl
        Dim iVOs As List(Of XVLView.Dao.Vo.ConditionSelectVo) = iDao.GetGousya(mEventCode, mGroupNo)

        If iVOs Is Nothing OrElse 0 >= iVOs.Count Then Exit Sub

        For Each lItem In iVOs
            aCheckList.Items.Add(lItem.ShisakuGousya)
        Next

    End Sub

#End Region

    
End Class

