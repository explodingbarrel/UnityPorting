using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System;
using System.Runtime.InteropServices;

namespace System
{
	/**
	 * Helpers for missing functions of classes, turned into extensions instead...
	*/
	public static class MissingExtensions
	{
		/**
		 * Helping Metro convert from System.Format(string,Object) to Windows App Store's System.Format(string,Object[]).
		 */
		public static string Format(string format, System.Object oneParam)
		{
			return System.String.Format(format, new System.Object[] { oneParam });
		}

		/**
		 * StringBuilder.AppendFormat(arg0,arg1,arg2) isn't implemented on WP8, so use this instead.
		 */
		public static StringBuilder AppendFormatEx(this StringBuilder sb, string format, System.Object arg0, System.Object arg1 = null, System.Object arg2 = null)
		{
			return sb.AppendFormat(format, new object[] { arg0, arg1, arg2 });
		}

		/**
		 * Missing IsSubclassOf, this works well
		 */
		public static bool IsSubclassOf(this System.Type type, System.Type parent)
		{
#if NETFX_CORE
			return parent.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
#else
			return type.IsSubclassOf(parent);
#endif
		}

		/**
		 * Just forward this call to the proper spot
		 */
		public static bool IsAssignableFrom(this System.Type type, System.Type other)
		{
#if NETFX_CORE
			return type.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
#else
			return type.IsAssignableFrom(other);
#endif
		}

		public static IAsyncResult BeginRead(this System.IO.Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return null;
		}

		public static IAsyncResult BeginWrite(this System.IO.Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return null;
		}

		public static int EndRead(this System.IO.Stream stream, IAsyncResult asyncResult)
		{
			return 0;
		}

		public static void EndWrite(this System.IO.Stream stream, IAsyncResult asyncResult)
		{
		}


	}
}

