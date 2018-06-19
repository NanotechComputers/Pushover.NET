using System;
using ServiceStack;
using System.Net;
using System.Threading.Tasks;

namespace PushoverClient
{
    /// <summary>
    /// Client library for using Pushover for push notifications.  
    /// See https://pushover.net/api for more information
    /// </summary>
    public class Pushover
    {
        /// <summary>
        /// Base url for the API
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private const string BASE_API_URL = "https://api.pushover.net/1/messages.json";

        /// <summary>
        /// The application key
        /// </summary>
        private string AppKey { get; set; }

        /// <summary>
        /// The default user or group key to send messages to
        /// </summary>
        private string DefaultUserGroupSendKey { get; set; }

        /// <summary>
        /// Create a pushover client using a source application key.
        /// </summary>
        /// <param name="appKey"></param>
        public Pushover(string appKey)
        {
            AppKey = appKey;
        }

        /// <summary>
        /// Create a pushover client using both a source 
        /// application key and a default send key
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="defaultSendKey"></param>
        [Obsolete("This constructor is deprecated")]
        public Pushover(string appKey, string defaultSendKey) : this(appKey)
        {
            DefaultUserGroupSendKey = defaultSendKey;
        }

        private PushResponse SendPush(object args)
        {
            try
            {
                var limit = 0;
                var remaining = 0;
                var reset = "";

                var response = BASE_API_URL.PostToUrl(args, responseFilter: httpRes =>
                    {
                        int.TryParse(httpRes.Headers["X-Limit-App-Limit"], out limit);
                        int.TryParse(httpRes.Headers["X-Limit-App-Remaining"], out remaining);
                        reset = httpRes.Headers["X-Limit-App-Reset"] ?? "";
                    })
                    .FromJson<PushResponse>();
                response.Limits = new Limitations
                {
                    Limit = limit,
                    Remaining = remaining,
                    Reset = reset,
                };
                return response;
            }
            catch (WebException webEx)
            {
                return webEx.GetResponseBody().FromJson<PushResponse>();
            }
        }

        private async Task<PushResponse> SendPushAsync(object args)
        {
            try
            {
                var limit = 0;
                var remaining = 0;
                var reset = "";

                var asyncResponse = await BASE_API_URL.PostToUrlAsync(args, responseFilter: httpRes =>
                {
                    int.TryParse(httpRes.Headers["X-Limit-App-Limit"], out limit);
                    int.TryParse(httpRes.Headers["X-Limit-App-Remaining"], out remaining);
                    reset = httpRes.Headers["X-Limit-App-Reset"] ?? "";
                });

                var response = asyncResponse.FromJson<PushResponse>();
                response.Limits = new Limitations
                {
                    Limit = limit,
                    Remaining = remaining,
                    Reset = reset,
                };
                return response;
            }
            catch (WebException webEx)
            {
                return webEx.GetResponseBody().FromJson<PushResponse>();
            }
        }

        /// <summary>
        /// Push a message
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="message">The body of the message</param>
        /// <param name="userKey">The user or group key (optional if you have set a default already)</param>
        /// <param name="device">Send to a specific device</param>
        /// <param name="html"></param>
        /// <returns></returns>
        [Obsolete("This method is deprecated")]
        public PushResponse Push(string title, string message, string userKey = "", string device = "", bool html = false)
        {
            var args = CreateArgs(title, message, userKey, device, html);
            return SendPush(args);
        }

        /// <summary>
        /// Push a message. Title will be the application name
        /// </summary>
        /// <param name="message">The body of the message</param>
        /// <returns>Response received by Pushover server</returns>
        public PushResponse Push(string message)
        {
            var args = CreateArgs("", message, new Options());
            return SendPush(args);
        }

        /// <summary>
        /// Push a message
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="message">The body of the message</param>
        /// <returns></returns>
        public PushResponse Push(string title, string message)
        {
            var args = CreateArgs(title, message, new Options());
            return SendPush(args);
        }

        /// <summary>
        /// Push a message
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="message">The body of the message</param>
        /// <param name="options">Optional message sending options</param>
        /// <returns></returns>
        public PushResponse Push(string title, string message, Options options)
        {
            var args = CreateArgs(title, message, options);
            return SendPush(args);
        }

        /// <summary>
        /// Push a message
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="message">The body of the message</param>
        /// <param name="userKey">The user or group key (optional if you have set a default already)</param>
        /// <param name="device">Send to a specific device</param>
        /// <param name="html"></param>
        /// <returns></returns>
        [Obsolete("This method is deprecated")]
        public async Task<PushResponse> PushAsync(string title, string message, string userKey = "", string device = "", bool html = false)
        {
            var args = CreateArgs(title, message, userKey, device, html);
            return await SendPushAsync(args);
        }

        /// <summary>
        /// Push a message. Title will be the application name
        /// </summary>
        /// <param name="message">The body of the message</param>
        /// <returns>Response received by Pushover server</returns>
        public async Task<PushResponse> PushAsync(string message)
        {
            var args = CreateArgs("", message, new Options());
            return await SendPushAsync(args);
        }

        /// <summary>
        /// Push a message
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="message">The body of the message</param>
        /// <returns></returns>
        public async Task<PushResponse> PushAsync(string title, string message)
        {
            var args = CreateArgs(title, message, new Options());
            return await SendPushAsync(args);
        }

        /// <summary>
        /// Push a message
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="message">The body of the message</param>
        /// <param name="options">Optional message sending options</param>
        /// <returns></returns>
        public async Task<PushResponse> PushAsync(string title, string message, Options options)
        {
            var args = CreateArgs(title, message, options);
            return await SendPushAsync(args);
        }

        private object CreateArgs(string title, string message, string userKey, string device, bool html)
        {
            // Try the passed user key or fall back to default
            var userGroupKey = string.IsNullOrEmpty(userKey) ? DefaultUserGroupSendKey : userKey;

            if (string.IsNullOrEmpty(userGroupKey))
                throw new ArgumentException("User key must be supplied", nameof(userKey));

            return new
            {
                token = AppKey,
                user = userGroupKey,
                device,
                html = html ? "1" : "0",
                title,
                message
            };
        }

        private object CreateArgs(string title, string message, Options options)
        {
   
            if (string.IsNullOrEmpty(options.Recipients))
            {
                throw new ArgumentException("Recipients must be supplied", nameof(options.Recipients));
            }

            //if (options.Recipients.IndexOf(",", StringComparison.OrdinalIgnoreCase) != -1)
            //{
                //We have a list of recipients, max is 50.
                var recipientCount = options.Recipients.Split(',').Length;
                if (recipientCount > 50)
                {
                    throw new ArgumentException("Recipients exceeded the maximum of 50", nameof(options.Recipients));
                }
            //}

            if (!string.IsNullOrWhiteSpace(title))
            {
                if (title.Length > 250)
                {
                    throw new ArgumentException("Title is limited to 250 characters", nameof(title));
                }
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message must be supplied", nameof(message));
            }

            if (message.Length > 1024)
            {
                throw new ArgumentException("Message is limited to 1024 characters", nameof(message));
            }

            if (string.IsNullOrWhiteSpace(options.Url) && !string.IsNullOrWhiteSpace(options.UrlTitle))
            {
                throw new ArgumentException("Url must be supplied when UrlTitle is set", nameof(options.Url));
            }


            if (!string.IsNullOrWhiteSpace(options.UrlTitle))
            {
                if (options.UrlTitle.Length > 100)
                {
                    throw new ArgumentException("UrlTitle is limited to 100 characters", nameof(options.UrlTitle));
                }
            }

            if (!string.IsNullOrWhiteSpace(options.Url))
            {
                if (options.Url.Length > 512)
                {
                    throw new ArgumentException("Url is limited to 512 characters", nameof(options.Url));
                }
            }

            var priority = options.Priority?.ToString() ?? Priority.Normal.ToString();
            var sound = options.Notification?.ToString() ?? NotificationSound.Pushover.ToString();

            return new
            {
                token = AppKey,
                user = options.Recipients,
                title,
                message,
                html = options.Html ? "1" : "0",
                url = options.Url,
                url_title = options.UrlTitle,
                priority,
                sound
            };
        }
    }
}