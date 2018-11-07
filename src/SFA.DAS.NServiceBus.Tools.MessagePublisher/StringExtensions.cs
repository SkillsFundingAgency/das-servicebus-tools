using System.Net;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher
{
    public static class StringExtensions
    {
        public static string HtmlDecode(this string input)
        {
            return WebUtility.HtmlDecode(input);
        }
    }
}
