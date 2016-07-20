Imports ShisakuCommon.Ui.Access

Namespace Ui.Valid.Impl


    Public MustInherit Class AbstractBaseValidator : Implements BaseValidator, IValidator
        Private Class Operand
            Friend OrOperator As Boolean
            Friend aValidator As IValidator
            Friend Sub New(ByVal aValidator As IValidator, Optional ByVal OrOperator As Boolean = False)
                Me.OrOperator = OrOperator
                Me.aValidator = aValidator
            End Sub
        End Class

        Private validators As New List(Of Operand)
        Private _errorValidators As New List(Of IValidator)
        Private _errorControls As New List(Of ErrorControl)
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(Nothing)
        End Sub
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="errorMessage">エラー発生時のメッセージ</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal errorMessage As String)
            _baseInfo.ErrorMessage = errorMessage
        End Sub

        Public Sub Add(ByVal aValidator As IValidator) Implements BaseValidator.Add
            validators.Add(New Operand(aValidator))
        End Sub
        Public Sub AddOr(ByVal aValidator As IValidator)
            If validators.Count = 0 Then
                Throw New InvalidOperationException("1要素目は、ORにできません.")
            End If
            validators.Add(New Operand(aValidator, True))
        End Sub

        Public Function Operation(Of T)(ByVal accessor As ControlAccessor(Of T)) As ValidateOperation(Of T)
            Return New ValidateOperationImpl(Of T)(Me, accessor, _baseInfo)
        End Function
        Public Function Collaborate() As ValidateCollaborator
            Return New ValidateCollaboratorImpl(Me)
        End Function

        Public Sub AssertValidate(ByVal ParamArray indexes As Integer()) Implements BaseValidator.AssertValidate
            If Not Validate(indexes) Then
                Throw New IllegalInputException(ErrorMessage, ErrorControls)
            End If
        End Sub

        Private Sub ClearError()
            _errorValidators.Clear()
            _errorControls.Clear()
        End Sub

        Private Sub AddError(ByVal aValidator As IValidator)
            _errorValidators.Add(aValidator)
            _errorControls.AddRange(aValidator.ErrorControls)
        End Sub

        Public Function Validate(ByVal ParamArray indexes As Integer()) As Boolean Implements IValidator.Validate
            ClearError()
            Dim result As Boolean = True

            If indexes.Length = 0 Then
                result = Validate(-1, result)
            Else
                For Each index As Integer In indexes
                    result = Validate(index, result)
                Next
            End If

            If result Then
                ClearError()
            End If
            Return result
        End Function

        Private Shared ReadOnly EMPTY_INDEX_PARAMS As Integer() = New Integer() {}
        Private Function Validate(ByVal index As Integer, Optional ByVal result As Boolean = True) As Boolean
            For Each anOperand As Operand In validators
                If anOperand.OrOperator Then
                    If result And Not CheckAll Then
                        ' OR は、True -> False にならないから、評価しない（CheckAll=Trueは例外）
                    ElseIf Not anOperand.aValidator.Validate(index) Then
                        AddError(anOperand.aValidator)
                        result = False
                    Else
                        result = True
                    End If
                Else
                    If Not result AndAlso Not CheckAll Then
                        ' AND は、False -> True にならないから、評価しない（CheckAll=Trueは例外）
                    ElseIf Not anOperand.aValidator.Validate(index) Then
                        AddError(anOperand.aValidator)
                        result = False
                    End If
                End If
            Next
            Return result
        End Function

        Public ReadOnly Property ErrorMessage() As String Implements IValidator.ErrorMessage
            Get
                If 0 < _errorValidators.Count Then
                    If _baseInfo.ErrorMessage IsNot Nothing And Not IsTransmitMessageChildren Then
                        Return _baseInfo.ErrorMessage
                    Else
                        Dim results As New List(Of String)
                        For Each aValidator As IValidator In _errorValidators
                            results.Add(aValidator.ErrorMessage)
                        Next
                        Return Join(results.ToArray, vbCrLf)
                    End If
                End If
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property ErrorControls() As ErrorControl() Implements IValidator.ErrorControls
            Get
                Return _errorControls.ToArray
            End Get
        End Property

#Region "プロパティ"
        '' 全てのValidateを実行する
        Private _checkAll As Boolean
        Private _baseInfo As New BaseValidatorInfo

        ''' <summary>全てのValidateを実行する</summary>
        ''' <value>全てのValidateを実行する</value>
        ''' <returns>全てのValidateを実行する</returns>
        Public Property CheckAll() As Boolean
            Get
                Return _checkAll
            End Get
            Set(ByVal value As Boolean)
                _checkAll = value
            End Set
        End Property

        ''' <summary>エラーメッセージを子要素に引き継ぐ</summary>
        ''' <value>エラーメッセージを子要素に引き継ぐ</value>
        ''' <returns>エラーメッセージを子要素に引き継ぐ</returns>
        Public Property IsTransmitMessageChildren() As Boolean
            Get
                Return _baseInfo.IsTransmitMessageChildren
            End Get
            Set(ByVal value As Boolean)
                _baseInfo.IsTransmitMessageChildren = value
            End Set
        End Property
#End Region
    End Class
End Namespace