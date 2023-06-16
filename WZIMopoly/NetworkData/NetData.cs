using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable SYSLIB0011

namespace WZIMopoly.NetworkData
{
    /// <summary>
    /// Represents a base class for network data serialization.
    /// </summary>
    [Serializable]
    internal abstract class NetData
    {
        /// <summary>
        /// Converts the current instance to a byte array.
        /// </summary>
        /// <returns>
        /// A byte array representing the serialized form of the object.
        /// </returns>
        public virtual byte[] ToByteArray()
        {
            using var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, this);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Deserializes the specified byte array into
        /// an instance of the derived class.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the derived class.
        /// </typeparam>
        /// <param name="bytes">
        /// The byte array to deserialize.
        /// </param>
        /// <returns>
        /// The instance of the derived class
        /// representing the deserialized object.
        /// </returns>
        public static T FromByteArray<T>(byte[] bytes)
            where T : NetData
        {
            using var memoryStream = new MemoryStream(bytes);
            var binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(memoryStream);
        }
    }
}
