using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.Converters
{
    public class TypeConverter : ValueConverter<Guid, byte[]>
    {
        public TypeConverter()
            : base (guid => guid.ToByteArray(),
                  bytes => new Guid(bytes))
        { }
    }
}
