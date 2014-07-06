#if NETFX_CORE || WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net.Sockets
{
		public class NetworkStream : System.IO.Stream
		{
			public override bool CanRead { get { return false; } }
			public override bool CanSeek { get { return false; } }
			public override bool CanWrite { get { return false; } }
			public override long Length { get { return 0; } }
			public override long Position { get; set; }

			public bool DataAvailable
			{
				get 
				{ 
					throw new NotImplementedException();
					return false; 
				}
			}

			public override void Flush() 
			{
			}

			public override long Seek(long i, System.IO.SeekOrigin j) 
			{ 
				return 0; 
			}

			public override void SetLength(long i) 
			{
			}

			public override int Read(byte[] b, int i, int j)
			{ 
				return 0;
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{ 
				return null; 
			}
	
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{ 
				return null; 
			}

		}
}
#endif