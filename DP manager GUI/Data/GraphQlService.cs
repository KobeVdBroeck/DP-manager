using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Threading.Tasks;

namespace DP_manager
{
    public static class GraphQlService
    {
        static GraphQLHttpClient client;

        public static bool Connected => client != null;
        public static string Address { set => InitClient(value); }

        static void InitClient(string address)
        {
            var endpoint = new Uri(address);

            var graphQLHttpClientOptions = new GraphQLHttpClientOptions
            {
                EndPoint = endpoint
            };

            client = new GraphQLHttpClient(graphQLHttpClientOptions, new NewtonsoftJsonSerializer());
        }

        public static async Task<T> SendRequestAsync<T>(string query)
        {
            var request = new GraphQLRequest { Query = new GraphQLQuery(query) };

            var response = await client.SendQueryAsync<T>(request);

            return response.Data;
        }
    }
}
