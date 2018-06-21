using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzurePurchaseFAQ
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAnswers();
            Console.ReadLine();
        }

        /*
         * POST /knowledgebases/ee0f5e4a-ac42-4f91-9b47-8161b6c5a409/generateAnswer
         * Host: https://azurepurchasefaq.azurewebsites.net/qnamaker
         * Authorization: EndpointKey 4836aac3-9fcf-45ca-9295-d256a50216ec
         * Content-Type: application/json
         * {"question":"<Your question>"}
         */

        // Replace this with a valid host name.
        static readonly string host = "https://azurepurchasefaq.azurewebsites.net"; //"ENTER HOST HERE";

        // Replace this with a valid endpoint key. get your endpoint keys, call the GET /endpointkeys method.
        static readonly string endpoint_key = "4836aac3-9fcf-45ca-9295-d256a50216ec"; //"ENTER KEY HERE";

        // Replace this with a valid knowledge base ID.From POST /knowledgebases/{knowledge base ID} method.
        static readonly string kb = "ee0f5e4a-ac42-4f91-9b47-8161b6c5a409"; //"ENTER KB ID HERE";
        static readonly string service = "/qnamaker";
        static readonly string method = "/knowledgebases/" + kb + "/generateAnswer/";
        
        // Replace this with a valid question string you want to search.
        static readonly string question = @"{'question': 'Does Azure support IPv6?'}";

        async static Task<string> Post(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                return await client.SendAsync(request).Result.Content.ReadAsStringAsync();
            }
        }
        async static void GetAnswers()
        {
            var uri = host + service + method;
            Console.WriteLine("Calling Uri: " + uri);
            var response = await Post(uri, question);
            Console.WriteLine(response + "\n\nPress any key to continue.");
        }
    }
}
