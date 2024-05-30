namespace JungleMCTS.Exceptions
{
    public class InvalidGameStateException : Exception
    {
        private const string _mainInfo = "Invalid game state. ";
        public InvalidGameStateException(string message) : base(_mainInfo + message) { }
    }
}
