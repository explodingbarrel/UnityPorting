#if NETFX_CORE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.Store;

namespace System.IO
{
    internal class EncryptedStreamReader : StreamReader
    {
        public EncryptedStreamReader(Stream stream)
            : base(stream)
        {
        }

        public override string ReadToEnd()
        {
            var sb = new StringBuilder();

            string line;
            try
            {
                while ((line = ReadLine()) != null)
                {
                    sb.Append(System.IO.EncryptionProvider.Decrypt(line, CurrentApp.AppId.ToString()));
                }
            }
            catch { }

            return sb.ToString();
        }
    }
}
#endif