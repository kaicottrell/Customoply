namespace CustomMonopoly.Server.Exceptions
{
    public class InvalidPropertyHouseCountException : Exception
    {
        // Default constructor with a default error message
        public InvalidPropertyHouseCountException()
            : base("The property house count is invalid.")
        {
        }
        public InvalidPropertyHouseCountException(string message) : base(message)
        {

        }

        public class InvalidPropertyHotelCountException : Exception
        {
            // Default constructor with a default error message
            public InvalidPropertyHotelCountException()
                : base("The property hotel count is invalid.")
            {
            }
            public InvalidPropertyHotelCountException(string message) : base(message)
            {

            }
        }
    }
}