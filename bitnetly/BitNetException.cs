using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace bitnetly
{
        public enum Reason
        {
            CallForbidden,
            MethodNotFound,
            UnableToParseResponse
        }

        [Serializable]
        public class BitNetException : Exception, ISerializable
        {
            public Reason Reason { get; private set; }

            private BitNetException()
            {
            }

            public BitNetException(Reason reason)
                : base()
            {
                Reason = reason;
            }

            public BitNetException(Reason reason, string message)
                : base(message)
            {
                Reason = reason;
            }

            public BitNetException(Reason reason, string message, Exception inner)
                : base(message, inner)
            {
                Reason = reason;
            }

            protected BitNetException(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                    throw new System.ArgumentNullException("info");

                Reason = (Reason)info.GetValue("Reason", typeof(Reason));
            }

            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            {
                if (info == null)
                    throw new System.ArgumentNullException("info");

                info.AddValue("Reason", Reason, typeof(Reason));
            }
        }
}
