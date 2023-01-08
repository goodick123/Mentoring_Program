using System;
using System.Runtime.Serialization;

namespace Own.Models
{
    [Serializable]
    public class SimpleClass : ISerializable
    {
        public string Name;
        public int Age;

        public SimpleClass()
        { 
        }

        protected SimpleClass(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("name");
            Age = info.GetInt32("age");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name);
            info.AddValue("age", Age);
        }
    }
}
