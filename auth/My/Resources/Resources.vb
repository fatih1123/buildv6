Imports System
Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices

Namespace auth.My.Resources
	''' <summary>
	'''   A strongly-typed resource class, for looking up localized strings, etc.
	''' </summary>
	' Token: 0x02000005 RID: 5
	<GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")>
	<DebuggerNonUserCode()>
	<CompilerGenerated()>
	<HideModuleName()>
	Friend NotInheritable Module Resources
		''' <summary>
		'''   Returns the cached ResourceManager instance used by this class.
		''' </summary>
		' Token: 0x17000006 RID: 6
		' (get) Token: 0x0600000C RID: 12 RVA: 0x000021B8 File Offset: 0x000003B8
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Friend ReadOnly Property ResourceManager As ResourceManager
			Get
				Dim flag As Boolean = Object.ReferenceEquals(Resources.resourceMan, Nothing)
				If flag Then
					Dim temp As ResourceManager = New ResourceManager("auth.Resources", GetType(Resources).Assembly)
					Resources.resourceMan = temp
				End If
				Return Resources.resourceMan
			End Get
		End Property

		''' <summary>
		'''   Overrides the current thread's CurrentUICulture property for all
		'''   resource lookups using this strongly typed resource class.
		''' </summary>
		' Token: 0x17000007 RID: 7
		' (get) Token: 0x0600000D RID: 13 RVA: 0x00002200 File Offset: 0x00000400
		' (set) Token: 0x0600000E RID: 14 RVA: 0x00002217 File Offset: 0x00000417
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Friend Property Culture As CultureInfo
			Get
				Return Resources.resourceCulture
			End Get
			Set(value As CultureInfo)
				Resources.resourceCulture = value
			End Set
		End Property

		' Token: 0x04000006 RID: 6
		Private resourceMan As ResourceManager

		' Token: 0x04000007 RID: 7
		Private resourceCulture As CultureInfo
	End Module
End Namespace
