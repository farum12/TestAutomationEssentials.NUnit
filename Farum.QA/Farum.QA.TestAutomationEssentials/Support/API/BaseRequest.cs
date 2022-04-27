using RestSharp;

namespace Farum.QA.TestAutomationEssentials.Support.API
{
    /// <summary>
    /// Class intended for creation of Requests. 
    /// </summary>
    public class BaseRequest
    {
        protected RestClient client;
        protected RestRequest request;

        protected BaseRequest(string url, Method httpMethod)
        {
            client = new RestClient(url);
            client.Timeout = -1;
            request = new RestRequest(httpMethod);
            request.AddHeader("authorization", "SOMEAUTHORIZATION");
            request.AddHeader("Content-Type", "application/json");
        }

        protected void SetPayload(string json)
        {
            request.AddParameter("application/json", json, ParameterType.RequestBody);
        }

        protected IRestResponse SendRequest()
        {
            return client.Execute(request);
        }
    }
}
