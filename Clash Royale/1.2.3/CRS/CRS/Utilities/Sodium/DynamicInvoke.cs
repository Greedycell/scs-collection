using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace UCS.Utilities.Sodium
{
	// Token: 0x02000038 RID: 56
	internal static class DynamicInvoke
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000D824 File Offset: 0x0000BA24
		public static T GetDynamicInvoke<T>(string function, string library)
		{
			TypeBuilder typeBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynamicDllInvoke"), AssemblyBuilderAccess.Run).DefineDynamicModule("DynamicDllModule").DefineType("DynamicDllInvokeType", TypeAttributes.Public | TypeAttributes.UnicodeClass);
			MethodInfo method = typeof(T).GetMethod("Invoke");
			Type[] array = (from param in method.GetParameters()
				select param.ParameterType).ToArray<Type>();
			MethodBuilder methodBuilder = typeBuilder.DefinePInvokeMethod(function, library, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, method.ReturnType, array, CallingConvention.Cdecl, CharSet.Ansi);
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			MethodInfo method2 = typeBuilder.CreateType().GetMethod(function);
			return (T)((object)Delegate.CreateDelegate(typeof(T), method2, true));
		}
	}
}
