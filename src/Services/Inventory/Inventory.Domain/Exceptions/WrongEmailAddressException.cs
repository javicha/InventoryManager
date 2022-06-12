namespace Inventory.Domain.Exceptions
{
    [Serializable()]
    public class WrongEmailAddressException : Exception
    {
        public WrongEmailAddressException() : base() { }
        public WrongEmailAddressException(string message) : base(message) { }
        public WrongEmailAddressException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected WrongEmailAddressException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
