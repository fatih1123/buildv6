Imports System
Imports System.CodeDom.Compiler
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CompilerServices

Namespace auth.My
	' Token: 0x02000004 RID: 4
	<HideModuleName()>
	<GeneratedCode("MyTemplate", "11.0.0.0")>
	Friend NotInheritable Module MyProject
		' Token: 0x17000001 RID: 1
		' (get) Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		<HelpKeyword("My.Computer")>
		Friend ReadOnly Property Computer As MyComputer
			<DebuggerHidden()>
			Get
				Return MyProject.m_ComputerObjectProvider.GetInstance
			End Get
		End Property

		' Token: 0x17000002 RID: 2
		' (get) Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
		<HelpKeyword("My.Application")>
		Friend ReadOnly Property Application As MyApplication
			<DebuggerHidden()>
			Get
				Return MyProject.m_AppObjectProvider.GetInstance
			End Get
		End Property

		' Token: 0x17000003 RID: 3
		' (get) Token: 0x06000009 RID: 9 RVA: 0x00002164 File Offset: 0x00000364
		<HelpKeyword("My.User")>
		Friend ReadOnly Property User As User
			<DebuggerHidden()>
			Get
				Return MyProject.m_UserObjectProvider.GetInstance
			End Get
		End Property

		' Token: 0x17000004 RID: 4
		' (get) Token: 0x0600000A RID: 10 RVA: 0x00002180 File Offset: 0x00000380
		<HelpKeyword("My.Forms")>
		Friend ReadOnly Property Forms As MyProject.MyForms
			<DebuggerHidden()>
			Get
				Return MyProject.m_MyFormsObjectProvider.GetInstance
			End Get
		End Property

		' Token: 0x17000005 RID: 5
		' (get) Token: 0x0600000B RID: 11 RVA: 0x0000219C File Offset: 0x0000039C
		<HelpKeyword("My.WebServices")>
		Friend ReadOnly Property WebServices As MyProject.MyWebServices
			<DebuggerHidden()>
			Get
				Return MyProject.m_MyWebServicesObjectProvider.GetInstance
			End Get
		End Property

		' Token: 0x04000001 RID: 1
		Private m_ComputerObjectProvider As MyProject.ThreadSafeObjectProvider(Of MyComputer) = New MyProject.ThreadSafeObjectProvider(Of MyComputer)()

		' Token: 0x04000002 RID: 2
		Private m_AppObjectProvider As MyProject.ThreadSafeObjectProvider(Of MyApplication) = New MyProject.ThreadSafeObjectProvider(Of MyApplication)()

		' Token: 0x04000003 RID: 3
		Private m_UserObjectProvider As MyProject.ThreadSafeObjectProvider(Of User) = New MyProject.ThreadSafeObjectProvider(Of User)()

		' Token: 0x04000004 RID: 4
		Private m_MyFormsObjectProvider As MyProject.ThreadSafeObjectProvider(Of MyProject.MyForms) = New MyProject.ThreadSafeObjectProvider(Of MyProject.MyForms)()

		' Token: 0x04000005 RID: 5
		Private m_MyWebServicesObjectProvider As MyProject.ThreadSafeObjectProvider(Of MyProject.MyWebServices) = New MyProject.ThreadSafeObjectProvider(Of MyProject.MyWebServices)()

		' Token: 0x02000009 RID: 9
		<EditorBrowsable(EditorBrowsableState.Never)>
		<MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")>
		Friend NotInheritable Class MyForms
			' Token: 0x06000068 RID: 104 RVA: 0x000052B0 File Offset: 0x000034B0
			<DebuggerHidden()>
			Private Shared Function Create__Instance__(Of T As{Form, New})(Instance As T) As T
				Dim flag As Boolean = Instance Is Nothing OrElse Instance.IsDisposed
				If flag Then
					Dim flag2 As Boolean = MyProject.MyForms.m_FormBeingCreated IsNot Nothing
					If flag2 Then
						Dim flag3 As Boolean = MyProject.MyForms.m_FormBeingCreated.ContainsKey(GetType(!!0))
						If flag3 Then
							Throw New InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", New String(-1) {}))
						End If
					Else
						MyProject.MyForms.m_FormBeingCreated = New Hashtable()
					End If
					MyProject.MyForms.m_FormBeingCreated.Add(GetType(!!0), Nothing)
					Try
						Return Activator.CreateInstance(Of T)()
					Catch ex As TargetInvocationException When ex.InnerException IsNot Nothing
						Dim BetterMessage As String = Utils.GetResourceString("WinForms_SeeInnerException", New String() { ex.InnerException.Message })
						Throw New InvalidOperationException(BetterMessage, ex.InnerException)
					Finally
						MyProject.MyForms.m_FormBeingCreated.Remove(GetType(!!0))
					End Try
				End If
				Return Instance
			End Function

			' Token: 0x06000069 RID: 105 RVA: 0x000053D8 File Offset: 0x000035D8
			<DebuggerHidden()>
			Private Sub Dispose__Instance__(Of T As Form)(ByRef instance As T)
				instance.Dispose()
				instance = Nothing
			End Sub

			' Token: 0x0600006A RID: 106 RVA: 0x000053EF File Offset: 0x000035EF
			<DebuggerHidden()>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Sub New()
			End Sub

			' Token: 0x0600006B RID: 107 RVA: 0x000053FC File Offset: 0x000035FC
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Overrides Function Equals(o As Object) As Boolean
				Return MyBase.Equals(RuntimeHelpers.GetObjectValue(o))
			End Function

			' Token: 0x0600006C RID: 108 RVA: 0x0000541C File Offset: 0x0000361C
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Overrides Function GetHashCode() As Integer
				Return MyBase.GetHashCode()
			End Function

			' Token: 0x0600006D RID: 109 RVA: 0x00005434 File Offset: 0x00003634
			<EditorBrowsable(EditorBrowsableState.Never)>
			Friend Function [GetType]() As Type
				Return GetType(MyProject.MyForms)
			End Function

			' Token: 0x0600006E RID: 110 RVA: 0x00005450 File Offset: 0x00003650
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Overrides Function ToString() As String
				Return MyBase.ToString()
			End Function

			' Token: 0x1700001F RID: 31
			' (get) Token: 0x0600006F RID: 111 RVA: 0x00005468 File Offset: 0x00003668
			' (set) Token: 0x06000070 RID: 112 RVA: 0x00005483 File Offset: 0x00003683
			Public Property Form1 As Form1
				<DebuggerHidden()>
				Get
					Me.m_Form1 = MyProject.MyForms.Create__Instance__(Of Form1)(Me.m_Form1)
					Return Me.m_Form1
				End Get
				<DebuggerHidden()>
				Set(value As Form1)
					If value IsNot Me.m_Form1 Then
						If value IsNot Nothing Then
							Throw New ArgumentException("Property can only be set to Nothing")
						End If
						Me.Dispose__Instance__(Of Form1)(Me.m_Form1)
					End If
				End Set
			End Property

			' Token: 0x04000035 RID: 53
			<ThreadStatic()>
			Private Shared m_FormBeingCreated As Hashtable

			' Token: 0x04000036 RID: 54
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public m_Form1 As Form1
		End Class

		' Token: 0x0200000A RID: 10
		<EditorBrowsable(EditorBrowsableState.Never)>
		<MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")>
		Friend NotInheritable Class MyWebServices
			' Token: 0x06000071 RID: 113 RVA: 0x000054B0 File Offset: 0x000036B0
			<EditorBrowsable(EditorBrowsableState.Never)>
			<DebuggerHidden()>
			Public Overrides Function Equals(o As Object) As Boolean
				Return MyBase.Equals(RuntimeHelpers.GetObjectValue(o))
			End Function

			' Token: 0x06000072 RID: 114 RVA: 0x000054D0 File Offset: 0x000036D0
			<EditorBrowsable(EditorBrowsableState.Never)>
			<DebuggerHidden()>
			Public Overrides Function GetHashCode() As Integer
				Return MyBase.GetHashCode()
			End Function

			' Token: 0x06000073 RID: 115 RVA: 0x000054E8 File Offset: 0x000036E8
			<EditorBrowsable(EditorBrowsableState.Never)>
			<DebuggerHidden()>
			Friend Function [GetType]() As Type
				Return GetType(MyProject.MyWebServices)
			End Function

			' Token: 0x06000074 RID: 116 RVA: 0x00005504 File Offset: 0x00003704
			<EditorBrowsable(EditorBrowsableState.Never)>
			<DebuggerHidden()>
			Public Overrides Function ToString() As String
				Return MyBase.ToString()
			End Function

			' Token: 0x06000075 RID: 117 RVA: 0x0000551C File Offset: 0x0000371C
			<DebuggerHidden()>
			Private Shared Function Create__Instance__(Of T As New)(instance As T) As T
				Dim flag As Boolean = instance Is Nothing
				Dim Create__Instance__ As T
				If flag Then
					Create__Instance__ = Activator.CreateInstance(Of T)()
				Else
					Create__Instance__ = instance
				End If
				Return Create__Instance__
			End Function

			' Token: 0x06000076 RID: 118 RVA: 0x00005545 File Offset: 0x00003745
			<DebuggerHidden()>
			Private Sub Dispose__Instance__(Of T)(ByRef instance As T)
				instance = Nothing
			End Sub

			' Token: 0x06000077 RID: 119 RVA: 0x0000554F File Offset: 0x0000374F
			<DebuggerHidden()>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Sub New()
			End Sub
		End Class

		' Token: 0x0200000B RID: 11
		<EditorBrowsable(EditorBrowsableState.Never)>
		<ComVisible(False)>
		Friend NotInheritable Class ThreadSafeObjectProvider(Of T As New)
			' Token: 0x17000020 RID: 32
			' (get) Token: 0x06000078 RID: 120 RVA: 0x0000555C File Offset: 0x0000375C
			Friend ReadOnly Property GetInstance As T
				<DebuggerHidden()>
				Get
					Dim flag As Boolean = MyProject.ThreadSafeObjectProvider(Of !0).m_ThreadStaticValue Is Nothing
					If flag Then
						MyProject.ThreadSafeObjectProvider(Of !0).m_ThreadStaticValue = Activator.CreateInstance(Of T)()
					End If
					Return MyProject.ThreadSafeObjectProvider(Of !0).m_ThreadStaticValue
				End Get
			End Property

			' Token: 0x06000079 RID: 121 RVA: 0x0000558E File Offset: 0x0000378E
			<DebuggerHidden()>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Sub New()
			End Sub

			' Token: 0x04000037 RID: 55
			<CompilerGenerated()>
			<ThreadStatic()>
			Private Shared m_ThreadStaticValue As T
		End Class
	End Module
End Namespace
