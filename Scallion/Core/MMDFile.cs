using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Scallion.Core
{
    public interface IMMDFile<T>
    {
        void Load(string path);
        void Save(string path);
    }

    internal abstract class MMDFile<T> : MMDObject, IMMDFile<T>
    {
        public void Load(string path)
        {
            using (var input = new FileStream(path, FileMode.Open))
            {
                this.Deserialize(new MoDeserializer(input));
            }
        }

        public void Save(string path)
        {
            using (var output = new FileStream(path, FileMode.Create))
            {
                this.Serialize(new MoSerializer(output));
            }
        }
    }
}
