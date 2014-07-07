#if NETFX_CORE || WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace System.Net.Sockets
{
	public class TcpClient
	{
		private StreamSocket _socket = null;
		DataWriter _writer;

		private async Task EnsureSocket(string hostName, int port)
		{
			try
			{
				var host = new HostName(hostName);
				_socket = new StreamSocket();
				await _socket.ConnectAsync(host, port.ToString(), SocketProtectionLevel.SslAllowNullEncryption);
			}
			catch (Exception ex)
			{
				// If this is an unknown status it means that the error is fatal and retry will likely fail.
				if (global::Windows.Networking.Sockets.SocketError.GetStatus(ex.HResult) == global::Windows.Networking.Sockets.SocketErrorStatus.Unknown)
				{
					// TODO abort any retry attempts on Unity side
					throw;
				}
			}
		}

		private async Task WriteToOutputStreamAsync(byte[] bytes)
		{

			if (_socket == null) return;
			_writer = new DataWriter(_socket.OutputStream);
			_writer.WriteBytes(bytes);

			var debugString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

			try
			{
				await _writer.StoreAsync();
				await _socket.OutputStream.FlushAsync();

				_writer.DetachStream();
				_writer.Dispose();
			}
			catch (Exception exception)
			{
				// If this is an unknown status it means that the error if fatal and retry will likely fail.
				if (global::Windows.Networking.Sockets.SocketError.GetStatus(exception.HResult) == global::Windows.Networking.Sockets.SocketErrorStatus.Unknown)
				{
					// TODO abort any retry attempts on Unity side
					throw;
				}
			}
		}

		public int SendTimeout { get; set; }
		public int ReceiveTimeout { get; set; }

		public void Connect(string hostName, int port)
		{
			var thread = EnsureSocket(hostName, port);
			thread.Wait();
		}

		public Stream GetStream()
		{
			if (_socket == null) return null;
			return _socket.InputStream.AsStreamForRead();
		}

		public Stream GetOutputStream()
		{
			if (_socket == null) return null;
			return _socket.OutputStream.AsStreamForWrite();
		}

		public void Close()
		{
			if (_socket != null)
			{
				_socket.Dispose();
			}
		}

		public void WriteToOutputStream(byte[] bytes)
		{
			var thread = WriteToOutputStreamAsync(bytes);
			thread.Wait();
		}

		public bool Connected
		{
			get { return _socket != null; }
		}

		public bool NoDelay
		{
			get; set;
		}

		public virtual IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state)
		{
			return null;
		}

		public virtual void EndConnect(IAsyncResult asyncResult)
		{
		}
	}
}
#endif