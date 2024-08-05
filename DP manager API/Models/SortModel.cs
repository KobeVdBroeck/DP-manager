using System.Formats.Asn1;
using System.Reflection;

namespace DP_manager_API.Models
{
    public class SortModel<T>
    {
        public string FieldName { get; set; }
        public SortDirection Direction { get; set; } = SortDirection.Ascending;

        public IComparer<T> BuildSortFunction()
        {
            if (string.IsNullOrWhiteSpace(FieldName))
                return Comparer<T>.Create((x, y) => 0);

            Func<T, object> getValue = (t) =>
            {
                PropertyInfo pinfo = typeof(T).GetProperty(FieldName);
                return pinfo.GetValue(t, null);
            };

            return Comparer<T>.Create((x, y) =>
            {
                var valX = getValue(x);
                var valY = getValue(y);

                if(valX is int && valY is int)
                    return (int)valY - (int)valX;

                return valX.ToString().CompareTo(valY);
            });
        }
    }
}
