using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace discordScpSlLogger
{
    class Http
    {
        public static async Task<byte[]> Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.UploadValues(uri, pairs);
            }
        }
    }
}