Imports System
Imports System.CodeDom.Compiler
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices

Namespace auth.My
	' Token: 0x02000002 RID: 2
	<GeneratedCode("MyTemplate", "11.0.0.0")>
	<EditorBrowsable(EditorBrowsableState.Never)>
	Friend Class MyApplication
		Inherits WindowsFormsApplicationBase

		' Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		<STAThread()>
		<DebuggerHidden()>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		<MethodImpl(MethodImplOptions.NoInlining Or MethodImplOptions.NoOptimization)>
		Friend Shared Sub Main(Args As String())
			Try
				Application.SetCompatibleTextRenderingDefault(WindowsFormsApplicationBase.UseCompatibleTextRendering)
			Finally
			End Try
			MyProject.Application.Run(Args)
		End Sub

		' Token: 0x06000002 RID: 2 RVA: 0x0000208C File Offset: 0x0000028C
		<DebuggerStepThrough()>
		Public Sub New()
			MyBase.New(AuthenticationMode.Windows)
			MyBase.IsSingleInstance = False
			MyBase.EnableVisualStyles = True
			MyBase.SaveMySettingsOnExit = True
			MyBase.ShutdownStyle = ShutdownMode.AfterMainFormCloses
		End Sub

		' Token: 0x06000003 RID: 3 RVA: 0x000020B7 File Offset: 0x000002B7
		<DebuggerStepThrough()>
		Protected Overrides Sub OnCreateMainForm()
			MyBase.MainForm = MyProject.Forms.Form1
		End Sub

		' Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		<DebuggerStepThrough()>
		Protected Overrides Function OnInitialize(commandLineArgs As ReadOnlyCollection(Of String)) As Boolean
			MyBase.MinimumSplashScreenDisplayTime = 0
			Return MyBase.OnInitialize(commandLineArgs)
		End Function
	End Class
End Namespace
