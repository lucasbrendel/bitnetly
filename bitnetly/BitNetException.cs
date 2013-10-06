using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace bitnetly
{
        /// <summary>
        /// Defines the possible reasons for the library to throw an exception
        /// </summary>
        public enum Reason
        {
            CallForbidden,
            MethodNotFound,
            UnableToParseResponse
        }

        /// <summary>
        /// Represents an exception that can be thrown by the library when a critical
        /// condition prevents a successful communication with the API.
        /// </summary>
        [Serializable]
        public class BitNetException : Exception, ISerializable
        {
             #region Custom Exception Properties
        
                /// <summary>
                /// Gets the reason behind the creation of this exception
                /// </summary>
                public Reason Reason { get; private set; }

                #endregion

             #region Construction

                private BitNetException()
                {
                }

                public BitNetException(Reason reason) : base()
                {
                        Reason = reason;
                }

                public BitNetException(Reason reason, string message) : base(message)
                {
                        Reason = reason;
                }

                public BitNetException(Reason reason, string message, Exception inner) : base(message, inner)
                {
                        Reason = reason;
                }

                #endregion

            #region ISerializable Members

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

                #endregion
        }
}
