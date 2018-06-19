namespace PushoverClient
{
    public class Options
    {
        public string Recipients { get; set; } = "";
        public bool Html { get; set; }
        public string Url { get; set; } = "";
        public string UrlTitle { get; set; } = "";
        public Priority Priority { get; set; }
        public NotificationSound Notification { get; set; }
    }
}