Imports System
Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Configuration
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.CompilerServices

Namespace auth.My
	' Token: 0x02000006 RID: 6
	<CompilerGenerated()>
	<GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.12.0.0")>
	<EditorBrowsable(EditorBrowsableState.Advanced)>
	Friend NotInheritable Partial Class MySettings
		Inherits ApplicationSettingsBase

		' Token: 0x06000011 RID: 17 RVA: 0x00002250 File Offset: 0x00000450
		<DebuggerNonUserCode()>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Private Shared Sub AutoSaveSettings(sender As Object, e As EventArgs)
			Dim saveMySettingsOnExit As Boolean = MyProject.Application.SaveMySettingsOnExit
			If saveMySettingsOnExit Then
				MySettingsProperty.Settings.Save()
			End If
		End Sub

		' Token: 0x17000008 RID: 8
		' (get) Token: 0x06000012 RID: 18 RVA: 0x0000227C File Offset: 0x0000047C
		Public Shared ReadOnly Property [Default] As MySettings
			Get
				Dim flag As Boolean = Not MySettings.addedHandler
				If flag Then
					Dim obj As Object = MySettings.addedHandlerLockObject
					ObjectFlowControl.CheckForSyncLockOnValueType(obj)
					SyncLock obj
						Dim flag3 As Boolean = Not MySettings.addedHandler
						If flag3 Then
							AddHandler MyProject.Application.Shutdown, AddressOf MySettings.AutoSaveSettings
							MySettings.addedHandler = True
						End If
					End SyncLock
				End If
				Return MySettings.defaultInstance
			End Get
		End Property

		' Token: 0x04000008 RID: 8
		Private Shared defaultInstance As MySettings = CType(SettingsBase.Synchronized(New MySettings()), MySettings)

		' Token: 0x04000009 RID: 9
		Private Shared addedHandler As Boolean

		' Token: 0x0400000A RID: 10
		Private Shared addedHandlerLockObject As Object = RuntimeHelpers.GetObjectValue(New Object())
	End Class
End Namespace
