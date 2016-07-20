Namespace Db.EBom.Vo.Helper
    Public Class TShisakuSekkeiBlockVoHelper
        ''' <summary>試作ブロック№表示順</summary>
        Public Class ShisakuBlockNoHyoujiJun
            ''' <summary>開始値</summary>
            Public Const START_VALUE As Integer = 0
        End Class
        ''' <summary>状態</summary>
        Public Class Jyoutai
            ''' <summary>編集中</summary>
            Public Const EDIT As String = "31"
            ''' <summary>一時保存</summary>
            Public Const SAVE As String = "32"
            ''' <summary>登録</summary>
            Public Const REGISTER As String = "33"
            ''' <summary>完了</summary>
            Public Const FINISHED As String = "34"
            ''' <summary>再抽出済み</summary>
            Public Const REALBUHIN As String = "30"
        End Class
        ''' <summary>状態</summary>
        Public Class JyoutaiMoji
            ''' <summary>編集中</summary>
            Public Const EDIT As String = "編集中"
            ''' <summary>一時保存</summary>
            Public Const SAVE As String = "一時保存"
            ''' <summary>登録済み</summary>
            Public Const REGISTER As String = "登録済み"
            ''' <summary>完了</summary>
            Public Const FINISHED As String = "完了"
            ''' <summary>再抽出済み</summary>
            Public Const REALBUHIN As String = "再抽出済み"
        End Class

        ''' <summary>担当承認状態</summary>
        Public Class TantoJyoutai
            ''' <summary>承認１</summary>
            Public Const APPROVAL As String = "35"
        End Class
        ''' <summary>担当承認状態</summary>
        Public Class TantoJyoutaiMoji
            ''' <summary>承認１</summary>
            Public Const APPROVAL As String = "承認１"
        End Class
        '仕様変更により追加
        '''<summary>担当承認状態２</summary>
        Public Class TantoJyoutai2
            ''' <summary>承認済</summary>
            Public Const OK As String = "承認済"
        End Class

        ''' <summary>課長承認状態</summary>
        Public Class KachouJyoutai
            ''' <summary>承認２</summary>
            Public Const APPROVAL As String = "36"
        End Class
        ''' <summary>課長承認状態</summary>
        Public Class KachouJyoutaiMoji
            ''' <summary>承認２</summary>
            Public Const APPROVAL As String = "承認２"
        End Class
        '仕様変更により追加
        '''<summary>課長承認状態２</summary>
        Public Class KachouJyoutai2
            ''' <summary>承認済</summary>
            Public Const OK As String = "承認済"
        End Class

        ''' <summary>試作ブロック№改訂№</summary>
        Public Class ShisakuBlockNoKaiteiNo
            ''' <summary>初期値</summary>
            Public Const DEFAULT_VALUE As String = "000"
        End Class
        ''' <summary>ブロック不要</summary>
        Public Class BlockFuyou
            ''' <summary>必要</summary>
            Public Const NECESSARY As String = "0"
            ''' <summary>不要</summary>
            Public Const UNNECESSARY As String = "1"
        End Class


#Region "local member 拡張"
        '' 最終更新日 
        Private _SaisyuuKoushinbi As String
        '' 最終更新時間 
        Private _SaisyuuKoushinjikan As String
        '' 担当承認日 
        Private _TantoSyouninHi As String
        '' 担当承認時間
        Private _TantoSyouninJikan As String
        '' 課長承認日 
        Private _KachouSyouninHi As String
        '' 課長承認時間
        Private _KachouSyouninJikan As String

#End Region

#Region "local member 拡張 getとset"
        ''' <summary>最終更新日</summary>
        ''' <value>最終更新日</value>
        ''' <returns>最終更新日</returns>
        Public ReadOnly Property SaisyuuKoushinbi() As String
            Get
                If Not vo Is Nothing Then
                    _SaisyuuKoushinbi = ShisakuComFunc.moji8Convert2Date(vo.SaisyuKoushinbi)
                Else
                    _SaisyuuKoushinbi = ""
                End If
                Return _SaisyuuKoushinbi
            End Get
        End Property

        ''' <summary>最終更新時間</summary>
        ''' <value>最終更新時間</value>
        ''' <returns>最終更新時間</returns>
        Public ReadOnly Property SaisyuuKoushinjikan() As String
            Get
                If Not vo Is Nothing Then
                    Dim timeStr = vo.SaisyuKoushinjikan.ToString()
                    If (timeStr.Length = 5) Then
                        timeStr = "0" + timeStr
                    End If
                    _SaisyuuKoushinjikan = ShisakuComFunc.moji6Convert2Time(timeStr)
                Else
                    _SaisyuuKoushinjikan = ""
                End If
                Return _SaisyuuKoushinjikan
            End Get
        End Property

        ''' <summary>担当承認日</summary>
        ''' <value>担当承認日</value>
        ''' <returns>担当承認日</returns>
        Public ReadOnly Property TantoSyouninHi() As String
            Get
                If Not vo Is Nothing Then
                    _TantoSyouninHi = ShisakuComFunc.moji8Convert2Date(vo.TantoSyouninHi)
                Else
                    _TantoSyouninHi = ""
                End If
                Return _TantoSyouninHi
            End Get
        End Property

        ''' <summary>担当承認時間</summary>
        ''' <value>担当承認時間</value>
        ''' <returns>担当承認時間</returns>
        Public ReadOnly Property TantoSyouninJikan() As String
            Get
                If Not vo Is Nothing Then
                    Dim timeStr = vo.TantoSyouninJikan.ToString()
                    If (timeStr.Length = 5) Then
                        timeStr = "0" + timeStr
                    End If
                    _TantoSyouninJikan = ShisakuComFunc.moji6Convert2Time(timeStr)
                Else
                    _TantoSyouninJikan = ""
                End If
                Return _TantoSyouninJikan
            End Get
        End Property




        ''' <summary>課長承認日</summary>
        ''' <value>課長承認日</value>
        ''' <returns>課長承認日</returns>
        Public ReadOnly Property KachouSyouninHi() As String
            Get
                If Not vo Is Nothing Then
                    _KachouSyouninHi = ShisakuComFunc.moji8Convert2Date(vo.KachouSyouninHi)
                Else
                    _KachouSyouninHi = ""
                End If
                Return _KachouSyouninHi
            End Get
        End Property

        ''' <summary>課長承認時間</summary>
        ''' <value>課長承認時間</value>
        ''' <returns>課長承認時間</returns>
        Public ReadOnly Property KachouSyouninJikan() As String
            Get
                If Not vo Is Nothing Then
                    Dim timeStr = vo.KachouSyouninJikan.ToString()
                    If (timeStr.Length = 5) Then
                        timeStr = "0" + timeStr
                    End If
                    _KachouSyouninJikan = ShisakuComFunc.moji6Convert2Time(timeStr)
                Else
                    _KachouSyouninJikan = ""
                End If
                Return _KachouSyouninJikan
            End Get
        End Property

#End Region

        ''' <summary>試作設計ブロックVo</summary>
        Private vo As TShisakuSekkeiBlockVo
        ''' <summary>
        ''' Construct
        ''' </summary>
        ''' <param name="vo">試作設計ブロックVo</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal vo As TShisakuSekkeiBlockVo)
            Me.vo = vo
        End Sub
    End Class
End Namespace