namespace HotelAPI.Enums
{
    public static class ResponseStrings
    {
        public const string AMOUNT_OVER_ZERO = "Amount must be over zero.";
        public const string DRINK_NOT_FOUND = "Drink was not found in the database.";
        public const string GAME_NOT_FOUND = "Game was not found in the database.";
        public const string GAME_OR_DRINK_EMPTY = "Either Game or Drink parameters must be provided.";
        public const string PASS_EMPTY = "Pass parameter was empty.";
        public const string ROOM_EMPTY = "Room parameter was empty.";
        public const string ROOM_NOT_FOUND = "Room was not found in the database.";
        public const string ROOM_NO_LAST_MOVEMENT = "Room has no last movement.";
        public const string USER_EMPTY = "User parameter was empty.";
        public const string USER_NOT_FOUND = "User was not found in the database.";
    }
}
