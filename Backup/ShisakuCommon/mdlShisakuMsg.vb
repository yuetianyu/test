
''' <summary>
''' 試作システム メッセージ定義モジュール
''' </summary>
''' <remarks></remarks>
Public Module ShisakuMsg

#Region " インフォメーションメッセージ "
    Public Const I0001 As String = "{0}を入力してください。"
#End Region

#Region " エラーメッセージ "
    Public Const E0001 As String = "パスワードが違います。"
    Public Const E0002 As String = "ユーザーには管理者権限がありません。"
    Public Const E0003 As String = "削除対象の車系／開発符号の削除へチェックを付けてください。"
    Public Const E0004 As String = "イベント登録済みです。削除できません。"
    Public Const E0005 As String = "「PW」を入力してください"
    Public Const E0006 As String = "「PW再入力」を入力してください"
    Public Const E0007 As String = "「PW」、「PW再入力」が一致しませんでした。再入力してください"
    Public Const E0008 As String = "「権限の設定」を正しく指定してください"
    Public Const E0009 As String = "「車系」を入力してください。"
    Public Const E0010 As String = "「開発符号」を入力してください。"
    Public Const E0011 As String = "一つ以上の「フェーズ」、「イベント」を入力してください。"
    Public Const E0012 As String = "「イベント」を入力してください。"
    Public Const E0013 As String = "「フェーズ」を入力してください。"
    Public Const E0014 As String = "イベントが重複しています。ご確認ください。"
    Public Const E0015 As String = "車系、開発符号、イベントは既に登録されています。ご確認ください。"
    Public Const E0016 As String = "イベント登録済みです。更新できません。"
    Public Const E0017 As String = "「メニュー」を選択してください。"
    Public Const E0018 As String = "「システム使用権限」を正しく指定してください。"
    Public Const E0019 As String = "イベントが全て登録済みです。削除できません。"
    Public Const E0020 As String = "フェーズの桁数は1です。"
    Public Const E0021 As String = "イベントの桁数は、全角10文字です。"
    Public Const E0022 As String = "車系の桁数は2です。"
    Public Const E0023 As String = "開発符号の桁数は4です。"
    Public Const E0024 As String = "システム使用権限で、同じ画面、同じ機能が選ばれています。"
    Public Const E0025 As String = "既に登録済みです。"

    Public Const EL001 As String = "ユーザーが登録されていません。GJ1(内線3611)までご連絡ください。"
    Public Const EL002 As String = "ユーザーにはシステム使用権限がありません。"
    Public Const EL003 As String = "ユーザーが登録されていません。"
    Public Const EL004 As String = "\nGJ1(内線3611)までご連絡ください。"


    '試作部品表作成一覧
    Public Const E0026 As String = "状態が設計メンテ中です。削除できません。"
    Public Const E0027 As String = "状態が改訂受付中です。削除できません。"
    Public Const E0028 As String = "状態が完了しています。削除できません。"

    'Excelファイルから取り込み
    Public Const E0029 As String = "取込元のEXCELにエラーが発生しました。取り込みを中止します。"
#End Region

#Region " TIPメッセージ "
    Public Const T0001 As String = "削除を実行しますか？"
    Public Const T0002 As String = "新規登録もしくは表中ダブルクリックで選択してください。"
    Public Const T0003 As String = "入力後、「登録」ボタンをクリックしてください。"
    Public Const T0004 As String = "登録しますか？"
    Public Const T0005 As String = "変更を更新せずに終了しますか？"
    Public Const T0006 As String = "更新を実行しますか？"
    Public Const T0007 As String = "ダブルクリックで選択してください。"
    Public Const T0008 As String = "登録せずに終了しますか？"
    Public Const T0009 As String = "設定を変更後、「更新」ボタンをクリックしてください。"
    Public Const T0010 As String = "Excel出力しました。"

    '37番
    Public Const T0011 As String = "新試作手配システムを終了します。"
    Public Const T0012 As String = "試作部品表を呼出します。"
    Public Const T0013 As String = "ダブルクリック、またはイベント選択し「呼出し」をクリックしてください。"

#End Region

#Region " DBメッセージ "
    Public Const M0001 As String = "登録しました。"
    Public Const M0002 As String = "更新しました。"
    Public Const M0003 As String = "削除しました。"
#End Region

#Region " アーランメッセージ "

    '試作部品表 編集一覧35と試作部品表　改訂編集一覧36
    Public Const A0001 As String = "処置期限が過ぎています。"
    Public Const A0002 As String = "処置〆切り間近です。"
    Public Const A0003 As String = "受付〆切り間近です。"
    Public Const A0004 As String = "検索したデータが存在しません。"
    Public Const A0005 As String = "イベントは終了しています。"

#End Region


End Module
