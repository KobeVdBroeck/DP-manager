using DP_manager_API.Entities;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using static System.Net.Mime.MediaTypeNames;

namespace DP_manager_API.Controllers;


public class StockController : GraphController
{
    [QueryRoot("stock")]
    public Plant RetrieveStock()
    {
        return new Plant()
        {
            
        };
    }
}
