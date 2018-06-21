============

.NET Standard Wrapper for the [Pushover](http://pushover.net) API.  Pushover makes it easy to send real-time notifications to your Android and iOS devices.

### Quick Start

Install the [NuGet package](http://www.nuget.org/packages/PushoverNET.Standard/)
```powershell
Install-Package PushoverNET.Standard
```

Next, you will need to provide Pushover.NET with your API key in code.  Need help finding your API key?  Check here: https://pushover.net/faq

In your application, call:

```CSharp
var pclient = new Pushover("Your-pushover-API-Key-here");
var options = new PushoverClient.Options
{
    Recipients = "Your-pushover-user-here", //User, group or comma separated values
    Priority = Priority.High,
    Notification = NotificationSound.SpaceAlarm,
    Html = true,
    Url = "http://www.google.com"
};
pclient.Push("Message Title", "Message text", options);
```

### Examples

##### Pushing a message:

```CSharp
using PushoverClient;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var pclient = new Pushover("Your-pushover-API-Key-here");
            var options = new PushoverClient.Options
            {
                Recipients = "Your-pushover-user-here", //User, group or comma separated values
                Priority = Priority.High,
                Notification = NotificationSound.SpaceAlarm,
                Html = true,
                Url = "http://www.google.com"
            };
            pclient.Push("Message Title", "Message text", options);
        }
    }
}
```

Please see example console application for more info

Converted from: https://github.com/danesparza/Pushover.NET