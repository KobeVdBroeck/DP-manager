using GraphQL.Client.Http;
using GraphQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Client.Abstractions;

namespace DP_manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowQueryResult();
        }

        public async void ShowQueryResult()
        {
            var endpoint = new Uri("https://localhost:7241/graphql");

            // Configure the HttpClient
            var graphQLHttpClientOptions = new GraphQLHttpClientOptions
            {
                EndPoint = endpoint,
                // Optionally, set the HTTP request timeout or other settings
                
            };

            var client = new GraphQLHttpClient(graphQLHttpClientOptions, new NewtonsoftJsonSerializer());

            var request = new GraphQLRequest
            {
                Query = new GraphQLQuery("query { stock {  jaar  soortCode  pm} }") 

            };

            var response = await client.SendQueryAsync<ResponseData>(request);

            textBox1.Text = response.Data.Stock.SoortCode;
        }
        public class ResponseData
        {
            public Plant Stock { get; set; }
        }

    }
}
