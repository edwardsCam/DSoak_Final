using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Messages
{
    public class NetByteStream
    {
        #region Private Data Members
        private MemoryStream memoryStream = new MemoryStream();
        private Stack<Int16> _readLimitStack = new Stack<Int16>();
        #endregion

        #region Constructors
        public NetByteStream() { }

                /// <summary>
        /// Constructur from a list of objects
        /// </summary>
        /// <param name="items"></param>
        public NetByteStream(params object[] items)
        {
            WriteObjects(items);
        }
        #endregion

        #region Write Methods

        public void WriteObjects(params object[] items)
        {
            foreach (object item in items)
                WriteObject(item);
        }

        public void WriteObject(object item)
        {
            if (item != null)
            {
                Type type = item.GetType();

                if (type.Equals(typeof(NetByteStream)))
                    Write((NetByteStream) item);
                else if (type.Equals(typeof(bool)))
                    Write((bool)item);
                else if (type.Equals(typeof(byte)))
                    Write((byte)item);
                else if (type.Equals(typeof(char)))
                    Write((char)item);
                else if (type.Equals(typeof(short)) || type.Equals(typeof(Int16)))
                    Write((Int16)item);
                else if (type.Equals(typeof(int)) || type.Equals(typeof(Int32)))
                    Write((Int32)item);
                else if (type.Equals(typeof(long)) || type.Equals(typeof(Int64)))
                    Write((Int64)item);
                else if (type.Equals(typeof(double)))
                    Write((double)item);
                else if (type.Equals(typeof(float)))
                    Write((float)item);
                else if (type.Equals(typeof(string)))
                    Write((string)item);
                else if (item is byte[])
                    Write((byte[])item);
                else
                    throw new NotImplementedException();
            }

        }

        public void Write(NetByteStream value)
        {
            if (value != null)
            {
                int currentPosition = Position;
                value.memoryStream.Seek(0, SeekOrigin.Begin);
                value.memoryStream.CopyTo(memoryStream);
                value.memoryStream.Seek(currentPosition, SeekOrigin.Begin);
            }
        }

        public void Write(byte value)
        {
            Write(new byte[] { value });
        }

        public void Write(bool value)
        {
            if (value)
                Write(new byte[] { 1 });
            else
                Write(new byte[] { 0 });
        }

        public void Write(char value)
        {
            Write(BitConverter.GetBytes(value));
        }

        public void Write(Int16 value)
        {
            Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void Write(Int32 value)
        {
            Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void Write(Int64 value)
        {
            Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void Write(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            Write(bytes);
        }

        public void Write(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            Write(bytes);
        }

        public void Write(string value)
        {
            if (value != null)
            {
                byte[] bytes = SwapWordBytes(Encoding.Unicode.GetBytes(value));
                Write((Int16)bytes.Length);
                Write(bytes);
            }
            else
                Write((Int16)0);
        }

        public void Write(byte[] value)
        {
            if (value != null)
                memoryStream.Write(value, 0, value.Length);
        }

        public void Write(byte[] value, int offset, int length)
        {
            if (value != null)
                memoryStream.Write(value, this.Length, length);
        }

        public void WriteBackLength(Int32 writePosition, Int16 length)
        {
            if (writePosition >= 0 && writePosition < Length - 2)
            {
                int currentPosition = Position;
                memoryStream.Seek(writePosition, SeekOrigin.Begin);
                memoryStream.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(length)), 0, 2);
                memoryStream.Seek(currentPosition, SeekOrigin.Begin);
            }
        }

        public void Rewrite(byte[] bytes)
        {
            Clear();
            Write(bytes);
        }

        #endregion

        #region Read Method

        public void ResetRead()
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }

        public byte ReadByte()
        {
            return ReadBytes(1)[0];
        }

        public bool ReadBool()
        {
            return (ReadBytes(1)[0] == 0) ? false : true;
        }

        public char ReadChar()
        {
            return BitConverter.ToChar(ReadBytes(2), 0);
        }

        public Int16 ReadInt16()
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(ReadBytes(2), 0));
        }

        public Int16 PeekInt16()
        {
            Int16 result = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(ReadBytes(2), 0));
            memoryStream.Seek(-2, SeekOrigin.Current);
            return result;
        }

        public Int32 ReadInt32()
        {
            byte[] bytes = ReadBytes(4);
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bytes, 0));
        }

        public long ReadInt64()
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(ReadBytes(8), 0));
        }

        public double ReadDouble()
        {
            byte[] bytes = ReadBytes(8);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public float ReadFloat()
        {
            byte[] bytes = ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public string ReadString()
        {
            string result = string.Empty;
            Int16 length = ReadInt16();
            if (length > 0)
                result = Encoding.Unicode.GetString(SwapWordBytes(ReadBytes(length)));
            return result;
        }

        public byte[] ReadBytes(int length)
        {
            if (_readLimitStack.Count > 0 && memoryStream.Position + length > _readLimitStack.Peek())
                throw new ApplicationException("Attempt to read beyond read limit");

            byte[] result = new byte[length];            
            memoryStream.Read(result, 0, length);

            return result;
        }

        virtual public byte[] ToBytes()
        {
            byte[] bytes = new byte[memoryStream.Length];
            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.Read(bytes, 0, Length);

            return bytes;
        }

        #endregion

        #region Read Limit Set, Restore, and Clear Methods

        public void SetNewReadLimit(Int16 length)
        {
            _readLimitStack.Push(Convert.ToInt16(memoryStream.Position + length));
        }

        public void RestorePreviosReadLimit()
        {
            if (_readLimitStack.Count > 0)
                _readLimitStack.Pop();
        }

        public void ClearMaxReadPosition()
        {
            _readLimitStack.Clear();
        }

        #endregion

        #region Other Public Methods

        public void Clear()
        {
            memoryStream = new MemoryStream();
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                byte result = 0;
                int currentPosition = Position;
                memoryStream.Seek(index, SeekOrigin.Begin);
                result = Convert.ToByte(memoryStream.ReadByte());
                memoryStream.Seek(currentPosition, SeekOrigin.Begin);
                return result;
            }

            set
            {
                if (index < 0)
                    throw new IndexOutOfRangeException();

                int currentPosition = Position;
                memoryStream.Seek(index, SeekOrigin.Begin);
                memoryStream.WriteByte(value);
                memoryStream.Seek(currentPosition, SeekOrigin.Begin);
            }
        }

        public Int16 Length
        {
            get
            {
                return Convert.ToInt16(memoryStream.Length);
            }
        }

        public Int16 Position
        {
            get
            {
                return Convert.ToInt16(memoryStream.Position);
            }
        }

        public int RemainingToRead
        {
            get
            {
                int tmpMax = (_readLimitStack.Count == 0) ? this.Length : _readLimitStack.Peek();
                return (Position >= tmpMax) ? 0 : tmpMax - Position;
            }
        }

        public bool IsMore()
        {
            int tmpMax = (_readLimitStack.Count == 0) ? this.Length : _readLimitStack.Peek();
            return (Position >= tmpMax) ? false : true;
        }

        public string CreateLogString()
        {
            StringBuilder result = new StringBuilder(this.Length * 18);
            for (int i = 0; i < this.Length; i++)
            {
                result.AppendFormat("{0}:{1}  ", i.ToString().Trim(), this[i].ToString().Trim());
            }

            return result.ToString();
        }

        #endregion

        # region Static Byte Parsing Functions
        private static byte[] SwapWordBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i += 2)
            {
                byte tmp = bytes[i];
                bytes[i] = bytes[i+1];
                bytes[i+1] = tmp;
            }

            return bytes;
        }

        static public string GetString(byte[] bytes, ref int offset, bool isNullTerminated)
        {
            Int16 length = BitConverter.ToInt16(bytes, offset);
            offset += 2;
            string result = Encoding.ASCII.GetString(bytes, offset, length);
            offset += length;
            if (isNullTerminated)
                offset++;
            return result;
        }
        # endregion

    }
}
