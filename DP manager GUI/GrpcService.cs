using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DP_manager.Form1;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DP_manager
{
    public static class GrpcService
    {
        static GraphQLHttpClient client;

        static GrpcService()
        {
            var endpoint = new Uri("https://localhost:7241/graphql");

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
