using CommandLine;

namespace SendPush
{
    public class Options
    {
        [Option('t', "title", HelpText = "The message title")]
        public string Title { get; set; }

        [Option('m', "message", HelpText = "The message")]
        public string Message { get; set; }

        /// <summary>
        /// The Pushover.NET api key to send the message from
        /// </summary>
        [Option('f', "from", HelpText = "The Pushover api key to send the message from")]
        public string From { get; set; }

        /// <summary>
        /// The Pushover.NET api key to send the message to
        /// </summary>
        [Option('u', "user", HelpText = "The Pushover api key to send the message to")]
        public string User { get; set; }

        /// <summary>
        /// The Pushover.NET api key to send the message to
        /// </summary>
        [Option('p', "plaintext", HelpText = "Send message as plaintext, this is the default option")]
        public string Plaintext
        {
            get => this._plaintext ?? "true";
            set
            {
                switch (value)
                {
                    case "false":
                    case "no":
                    case "0":
                    case "html":
                        this._plaintext = "false";
                        break;

                    default:
                        this._plaintext = "true";
                        break;
                }
            }
        }

        private string _plaintext;

    }
}