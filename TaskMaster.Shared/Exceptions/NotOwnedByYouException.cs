namespace TaskMaster.Shared.Exceptions
{
    public class NotOwnedByYouException : Exception
    {
       
        public NotOwnedByYouException()
            : base("You do not have access to work with this entity")
        {
        }

       
        public NotOwnedByYouException(string message)
            : base(message)
        {
        }
    }
}
