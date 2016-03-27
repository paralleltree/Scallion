using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scallion.Core
{
    internal interface IMoSerializable
    {
        void Serialize(MoSerializer archive);
        void Deserialize(MoDeserializer archive);
    }

    internal abstract class MMDObject : IMoSerializable
    {
        public abstract void Serialize(MoSerializer archive);
        public abstract void Deserialize(MoDeserializer archive);
    }

    internal abstract class MoIO
    {
        public Encoding FileEncoding { get; set; }

        public MoIO()
            : this(Encoding.GetEncoding("shift_jis"))
        {
        }

        public MoIO(Encoding enc)
        {
            FileEncoding = enc;
        }
    }
}
