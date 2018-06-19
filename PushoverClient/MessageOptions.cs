namespace PushoverClient
{
    public sealed class Priority
    {
        private readonly string _name;
        private readonly int _value;

        public static readonly Priority Lowest = new Priority(-2, "-2");
        public static readonly Priority Low = new Priority(-1, "-1");
        public static readonly Priority Normal = new Priority(0, "0");
        public static readonly Priority High = new Priority(1, "1");

        private Priority(int value, string name)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }
    }

    public sealed class NotificationSound
    {
        private readonly string _name;
        private readonly int _value;

        public static readonly NotificationSound PhoneDefault = new NotificationSound(-1, "");
        public static readonly NotificationSound Pushover = new NotificationSound(0, "pushover");
        public static readonly NotificationSound Bike = new NotificationSound(1, "bike");
        public static readonly NotificationSound Bugle = new NotificationSound(2, "bugle");
        public static readonly NotificationSound CashRegister = new NotificationSound(3, "cashregister");
        public static readonly NotificationSound Classical = new NotificationSound(4, "classical");
        public static readonly NotificationSound Cosmic = new NotificationSound(5, "cosmic");
        public static readonly NotificationSound Falling = new NotificationSound(6, "falling");
        public static readonly NotificationSound Gamelan = new NotificationSound(77, "gamelan");
        public static readonly NotificationSound Incoming = new NotificationSound(8, "incoming");
        public static readonly NotificationSound Intermission = new NotificationSound(9, "intermission");
        public static readonly NotificationSound Magic = new NotificationSound(10, "magic");
        public static readonly NotificationSound Mechanical = new NotificationSound(11, "mechanical");
        public static readonly NotificationSound PianoBar = new NotificationSound(12, "pianobar");
        public static readonly NotificationSound Siren = new NotificationSound(13, "siren");
        public static readonly NotificationSound SpaceAlarm = new NotificationSound(14, "spacealarm");
        public static readonly NotificationSound TugBoat = new NotificationSound(15, "tugboat");
        public static readonly NotificationSound AlienAlarm = new NotificationSound(16, "alien"); //(long)    
        public static readonly NotificationSound Climb = new NotificationSound(17, "climb"); //(long)    
        public static readonly NotificationSound Persistent = new NotificationSound(18, "persistent"); //(long)    
        public static readonly NotificationSound PushoverEcho = new NotificationSound(19, "echo"); //(long)    
        public static readonly NotificationSound UpDown = new NotificationSound(20, "updown"); //(long)    
        public static readonly NotificationSound None = new NotificationSound(21, "none"); //(silent)
        public static readonly NotificationSound Silent = new NotificationSound(22, "none"); //(silent)

        private NotificationSound(int value, string name)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}