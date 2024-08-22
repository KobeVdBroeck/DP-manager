using System.Reflection;

namespace DP_manager_API.Models;

public class FilterModel<T>
{
    public string FieldName { get; set; }
    public string Filter { get; set; }

    public Func<T, bool> BuildFilterFunction()
    {
        if (string.IsNullOrWhiteSpace(FieldName) || string.IsNullOrWhiteSpace(Filter))
            return (t) => true;

        Func<T, bool> temp = (t) =>
        {
            PropertyInfo pinfo = typeof(T).GetProperty(FieldName);
            return pinfo.GetValue(t, null).ToString().Contains(Filter);
        };

        return temp;
    }
}
