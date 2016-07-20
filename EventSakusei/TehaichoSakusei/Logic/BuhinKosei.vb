Imports ShisakuCommon.Db.EBom.Vo
Imports EventSakusei.TehaichoSakusei.Dao

Namespace TehaichoSakusei.Logic
    Public Class BuhinKosei

        Private impl As BuhinKoseiDao
        Private kaihatsuFugo As String

        Public Sub New(ByVal kaihatsuFugo As String)
            impl = New BuhinKoseiDaoImpl
            Me.kaihatsuFugo = kaihatsuFugo
        End Sub

        '部品構成を取得する'
        Public Function BuhinKosei(ByVal buhinEditTmpVo As TShisakuBuhinEditTmpVo) As TShisakuTehaiKihonVo
            '手配帳作成用なので部品構成は部品番号(子)で検索する'
            Dim rhac0551Vo As New Rhac0551Vo
            Dim rhac0552Vo As New Rhac0552Vo
            Dim rhac0553Vo As New Rhac0553Vo
            Dim rhac0530Vo As New Rhac0530Vo
            Dim rhac0532Vo As New Rhac0532Vo
            Dim rhac0533Vo As New Rhac0533Vo

            Dim resultKihon As New TShisakuTehaiKihonVo

            rhac0552Vo = impl.FindByRhac0552BuhinNoOya(buhinEditTmpVo.BuhinNo)

            If rhac0552Vo Is Nothing Then

                rhac0551Vo = impl.FindByRhac0551BuhinNoOya(buhinEditTmpVo.BuhinNo)

                If rhac0551Vo Is Nothing Then

                    rhac0553Vo = impl.FindByRhac0553BuhinNoOya(kaihatsuFugo, buhinEditTmpVo.BuhinNo)

                    If rhac0553Vo Is Nothing Then
                        '存在しない部品番号'
                        Return resultKihon
                    Else
                        '存在する部品番号'
                        resultKihon.BuhinNoOya = rhac0553Vo.BuhinNoOya
                        rhac0533Vo = impl.FindByRhac0533(resultKihon.BuhinNoOya)

                        If rhac0533Vo Is Nothing Then
                            Return resultKihon
                        Else
                            '区分はどこ？'
                            Return resultKihon
                        End If
                    End If

                Else
                    'RHAC0551に存在した'
                    resultKihon.BuhinNoOya = rhac0551Vo.BuhinNoOya

                End If

            Else
                'RHAC0552に存在した場合１つ上の情報が取得できた'
                resultKihon.BuhinNoOya = rhac0552Vo.BuhinNoOya

            End If
            Return resultKihon
        End Function





    End Class
End Namespace
