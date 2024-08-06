using System.Formats.Asn1;
using System.Reflection;

namespace DP_manager_API.Models
{
    public class SortModel<T>
    {
        public string FieldName { get; set; }
        public string Direction { get; set; } = "asc";
    }
}
