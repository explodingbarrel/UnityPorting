#if NETFX_CORE || WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net
{
		public class IPAddress
		{
			public static bool TryParse(string h, out IPAddress p)
			{
				throw new NotImplementedException();
				p = new IPAddress;
				return false;
			}
		}

		public class Dns
		{
			public static System.IAsyncResult BeginGetHostAddresses(string h, System.AsyncCallback cb, Object state)
			{
				throw new NotImplementedException();
				return null;
			}
			public static IPAddress[] EndGetHostAddresses(System.IAsyncResult asyncResult)
			{
				throw new NotImplementedException();
				return new IPAddress[0];
			}
		}
}
#endif