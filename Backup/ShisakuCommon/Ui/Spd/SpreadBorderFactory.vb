Imports FarPoint.Win

Namespace Ui.Spd
    ''' <summary>
    ''' SpreadのBorderのFacotryクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpreadBorderFactory

        Private _UnderLine As ComplexBorder
        ''' <summary>
        ''' セル下部の線を返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetUnderLine() As ComplexBorder
            If _UnderLine Is Nothing Then
                _UnderLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine))
            End If
            Return _UnderLine
        End Function

        Private _UnderLineAndRightLine As ComplexBorder
        ''' <summary>
        ''' セル下部の線と、セル右部の線を返す
        ''' </summary>
        ''' <returns>セル下部の線と、セル右部の線</returns>
        ''' <remarks></remarks>
        Public Function GetUnderLineAndRightLine() As ComplexBorder
            If _UnderLineAndRightLine Is Nothing Then
                _UnderLineAndRightLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine))
            End If
            Return _UnderLineAndRightLine
        End Function

        Private _UnderLineAndRightWLine As ComplexBorder
        ''' <summary>
        ''' セル下部の線と、セル右部の二重線を返す
        ''' </summary>
        ''' <returns>セル下部の線と、セル右部の二重線</returns>
        ''' <remarks></remarks>
        Public Function GetUnderLineAndRightWLine() As ComplexBorder
            If _UnderLineAndRightWLine Is Nothing Then
                _UnderLineAndRightWLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.DoubleLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine))
            End If
            Return _UnderLineAndRightWLine
        End Function

        Private _UnderLineAndLeftWLine As ComplexBorder
        ''' <summary>
        ''' セル下部の線と、セル左部の二重線を返す
        ''' </summary>
        ''' <returns>セル下部の線と、セル左部の二重線</returns>
        ''' <remarks></remarks>
        Public Function GetUnderLineAndLeftWLine() As ComplexBorder
            If _UnderLineAndLeftWLine Is Nothing Then
                _UnderLineAndLeftWLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.DoubleLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine))
            End If
            Return _UnderLineAndLeftWLine
        End Function

        Private _RightWLine As ComplexBorder
        ''' <summary>
        ''' セル右部の二重線を返す
        ''' </summary>
        ''' <returns>セル右部の二重線</returns>
        ''' <remarks></remarks>
        Public Function GetRightWLine() As ComplexBorder
            If _RightWLine Is Nothing Then
                _RightWLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.DoubleLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            End If
            Return _RightWLine
        End Function

        Private _LeftWLine As ComplexBorder
        ''' <summary>
        ''' セル左部の二重線を返す
        ''' </summary>
        ''' <returns>セル左部の二重線</returns>
        ''' <remarks></remarks>
        Public Function GetLeftWLine() As ComplexBorder
            If _LeftWLine Is Nothing Then
                _LeftWLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.DoubleLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            End If
            Return _LeftWLine
        End Function

        Private _LeftLineAndRightWLine As ComplexBorder
        ''' <summary>
        ''' セル左部の線と、セル右部の二重線を返す
        ''' </summary>
        ''' <returns>セル左部の線と、セル右部の二重線</returns>
        ''' <remarks></remarks>
        Public Function GetLeftLineAndRightWLine() As ComplexBorder
            If _LeftLineAndRightWLine Is Nothing Then
                _LeftLineAndRightWLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.DoubleLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            End If
            Return _LeftLineAndRightWLine
        End Function
        '↓↓2014/10/21 酒井 ADD BEGIN
        Private _LeftLine As ComplexBorder
        ''' <summary>
        ''' セル左部の線と、セル右部の二重線を返す
        ''' </summary>
        ''' <returns>セル左部の線と、セル右部の二重線</returns>
        ''' <remarks></remarks>
        Public Function GetLeftLine() As ComplexBorder
            If _LeftLine Is Nothing Then
                _LeftLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            End If
            Return _LeftLine
        End Function
        '↑↑2014/10/21 酒井 ADD END
        Private _TopWLine As ComplexBorder
        ''' <summary>
        ''' セル上部の二重線を返す
        ''' </summary>
        ''' <returns>セル上部の二重線</returns>
        ''' <remarks></remarks>
        Public Function GetTopWLine() As ComplexBorder
            If _TopWLine Is Nothing Then
                _TopWLine = _
                    New FarPoint.Win.ComplexBorder( _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.DoubleLine), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), _
                                                   New FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None))
            End If
            Return _TopWLine
        End Function

    End Class
End Namespace