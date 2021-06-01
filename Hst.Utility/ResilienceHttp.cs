using Hst.Model.ViewModels;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Hst.Model.Common;

namespace Hst.Utility
{
    public static class ResilienceHttp
    {
        public static async Task<ResponseData<TResponse>> ToPostAPI<TRequest, TResponse>(this string url, TRequest request, string authToken = "")
        {
            IRestResponse response = await url.ToRestAPI(Method.POST.ToRestRequest(request, authToken));
            HandleErrorResponse(response);
            return JsonConvert.DeserializeObject<ResponseData<TResponse>>(response.Content);
        }


        /// <summary>
        /// To perform api operations with get method, global for whole applciation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public static async Task<ResponseData<T>> ToGetAPI<T>(this string url, string authToken = "")
        {
            IRestResponse response = await url.ToRestAPI(Method.GET.ToRestRequest(authToken));

            HandleErrorResponse(response);

            return JsonConvert.DeserializeObject<ResponseData<T>>(response.Content);
        }



        private static async Task<IRestResponse> ToRestAPI(this string url, IRestRequest restRequest)
        {
            var client = new RestClient(url);
            IRestResponse restResponse = await client.ExecuteAsync(restRequest);
            return restResponse;
        }

        private static IRestRequest ToRestRequest(this Method requestMethod, string authToken = "")
        {
            //make request
            var restRequest = new RestRequest(requestMethod);
            restRequest.RequestFormat = DataFormat.Json;

            //set request header
            if (!string.IsNullOrWhiteSpace(authToken))
            {
                restRequest.AddHeader("Authorization", authToken);
            }

            return restRequest;
        }


        private static IRestRequest ToRestRequest<TRequest>(this Method requestMethod, TRequest request, string authToken = "")
        {
            var restRequest = requestMethod.ToRestRequest(authToken);
            restRequest.JsonSerializer = NewtonsoftJsonSerializer.DefaultValue; // added for Date Format
            restRequest.AddBody(request);
            return restRequest;
        }

        private static void HandleErrorResponse(IRestResponse response)
        {
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                var exception = JsonConvert.DeserializeObject<CatchExceptionData>(response.Content);
                throw new MicroserviceException(exception.Message);
            }
        }

    }

    [Serializable]
    public class MicroserviceException : Exception
    {

        protected MicroserviceException(SerializationInfo serialization, StreamingContext streamContext)
           : base(serialization, streamContext)
        {

        }
        public MicroserviceException() : base() { }
        public MicroserviceException(string message) : base(message) { }
        public MicroserviceException(string message, Exception inner) : base(message, inner) { }

    }
    public class CatchExceptionData
    {
        public string Message { get; set; }
    }

    public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        private readonly Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentType
        {
            get { return "application/json"; } ////// Probably used for Serialization?
            set { value = "application / json"; } ////// earlier was like this : set {}
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new System.IO.StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer DefaultValue
        {
            get
            {
                return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }
    }
}
