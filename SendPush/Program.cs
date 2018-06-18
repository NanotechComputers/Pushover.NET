using System;
using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.Configuration;
using PushoverClient;

namespace SendPush
{
    static class Program
    {
        private static IConfiguration Configuration { get; set; }
        private static ExitCode ReturnErrorCode { get; set; }

        static int Main(string[] args)
        {
            Configuration = Startup.Configure();

            var parserResult = Parser.Default.ParseArguments<Options>(args);
            parserResult.WithParsed(OnSuccessfulParse);
            parserResult.WithNotParsed(errs =>
            {
                var helpText = HelpText.AutoBuild(parserResult, h => HelpText.DefaultParsingErrorsHandler(parserResult, h), e => e);
                Console.WriteLine(helpText);
                ReturnErrorCode = ExitCode.Error;
            });

            return (int) ReturnErrorCode;
        }


        private static void OnSuccessfulParse(Options options)
        {
            //  Get the settings defaults
            var appKey = Configuration["appKey"];
            var userGroupKey = Configuration["userGroupKey"];

            //  If we didn't get the app key passed in, use the default:
            if (string.IsNullOrEmpty(options.From))
            {
                options.From = appKey;
            }

            //  If we didn't get the user key passed in, use the default:
            if (string.IsNullOrEmpty(options.User))
            {
                options.User = userGroupKey;
            }

            //  Make sure we have our required items:
            if (OptionsValid(options))
            {
                //  Send the message
                var pclient = new Pushover(options.From);
                pclient.Push(options.Title, options.Message, options.User, "", !bool.Parse(options.Plaintext));
                ReturnErrorCode = ExitCode.Success;
            }

            ReturnErrorCode = ExitCode.Error;
        }

        private static bool OptionsValid(Options options)
        {
            var retval = !string.IsNullOrEmpty(options.From) &&
                         !string.IsNullOrEmpty(options.User) &&
                         !string.IsNullOrEmpty(options.Title) &&
                         !string.IsNullOrEmpty(options.Message);

            return retval;
        }
    }

    internal enum ExitCode
    {
        Success = 0,
        Error = -1
    }
}