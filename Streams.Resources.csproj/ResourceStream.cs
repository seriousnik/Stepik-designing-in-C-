using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Streams.Resources
{
    public class ResourceReaderStream : Stream
    {
        Stream stream;
        byte[] keyInBytes;
        bool keyFound = false;
        bool isValueRead = false;
        int offset = 0;
        byte[] buffer1024 = new byte[1024];
        public ResourceReaderStream(Stream stream, string key)
        {
            this.stream = stream;
            keyInBytes = Encoding.ASCII.GetBytes(key);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.offset % 1024 == 0 || this.offset == 0) 
            {
                var countFromTestStream = stream.Read(buffer1024, 0, 1024);
                this.offset = 0;
            }              
            if (!keyFound)
                SeekValue();
            if (!isValueRead && keyFound)
                return ReadFieldValue(buffer);
            return 0;
        }

        private void SeekValue()
        {           
            for (var i = 0; i < 1024; i++)
            {
                for (var j = 0; j < keyInBytes.Length; j++)
                {
                    if (j == keyInBytes.Length - 1 && buffer1024[i] == keyInBytes[j])
                    {
                        offset = i + 3; keyFound = true; 
                    }
                    if (buffer1024[i] == keyInBytes[j])
                    {
                        i++;
                    }                  
                }
                if (offset != 0)
                    break;
            }
        }

        private int ReadFieldValue(byte[] buffer)
        {
            var length = 0;
            var i = 0;
            for (var l = offset; l < 1024 && i < buffer.Length; l++)
            {
                if (buffer1024[l] == 0 && buffer1024[l - 1] == 0)
                    continue;
                if (buffer1024[l] == 0 && buffer1024[l + 1] == 1)
                {
                    isValueRead = true;
                    break;
                }
                buffer[i] = buffer1024[l];
                length++; i++;
            }
            offset += length;
            return length;
        }

        public override void Flush()
        {
            // nothing to do
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

}


//Например, допустим, что вы разрабатываете компьютерную игру с множеством мелких файлов.
//Очевидно, что хотелось бы эти файлы убрать в один. Допустим, что вы по какой-то причине не хотите использовать zip-сжатие 
//(что было бы самым адекватным подходом к этой ситуации), и вместо этого хотите изобрести свой формат.

//Ваша задача — по известному формату написать стрим, который читает секцию файла.
//    Ваш стрим будет получать другой, базовый стрим, который содержит данные, и ваша задача — найти нужную секцию и прочитать.
//    Эта задача осмысленна, поскольку, например, Bitmap.FromStream принимает именно Stream, и вы можете подставить туда ваш стрим для того, чтобы все работало.

//Дополнительное ограничение: из базового стрима нужно читать порциями ровно по 1024 байт.Число произвольное, 
//    но это ограничение нужно: плохо слишком часто обращаться к стриму (сам факт чтения несет дополнительные расходы,
//    в некоторых случаях не зависящие от количества прочитанных байт), и плохо читать все сразу, 
//    поскольку стрим может быть очень большой и не поместиться в памяти.Найдите стандартный способ обеспечить это условие.

//private int ReadFieldValue(byte[] buffer)
//{
//    var length = 0;
//    for (var l = offset + 2; l < 1024; l++)
//    {
//        if (buffer1024[l] == 0 && buffer1024[l + 1] == 1)
//        {
//            length = l - offset - 2;
//            break;
//        }

//    }
//    var j = 0;
//    var i = 0;
//    for (; i < length; i++, j++)
//    {
//        if (buffer1024[offset + 3 + j] == 0 && buffer1024[offset + 4 + j] == 0)
//            j++;
//        buffer[i] = buffer1024[offset + 3 + j];
//    }
//    isValueRead = true;
//    return length - 1 - (j - i);
//}