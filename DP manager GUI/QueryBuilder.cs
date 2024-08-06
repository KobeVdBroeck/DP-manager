using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DP_manager
{
    public class QueryBuilder
    {
        string baseQuery;
        string afterKeyword;
        Dictionary<string, List<string>> arguments = new Dictionary<string, List<string>>();

        public QueryBuilder(string baseQuery, string afterKeyword) 
        { 
            this.baseQuery = baseQuery;
            this.afterKeyword = afterKeyword;
        }

        public void AddArgument(string to, string arg, bool removeExisting = false)
        {
            if(!arguments.ContainsKey(to))
            {
                arguments.Add(to, new List<string>());
            }
            else if(removeExisting)
                arguments[to].Clear();
                

            arguments[to].Add(arg);
        }

        public List<string> GetArguments(string arg)
        {
            return arguments[arg];
        }

        public void RemoveArgument(string arg)
        {
            arguments.Remove(arg);
        }

        public string BuildQuery()
        {
            StringBuilder sb = new StringBuilder(baseQuery);

            int index = baseQuery.IndexOf(afterKeyword);

            StringBuilder sb2 = new StringBuilder("(");
            foreach (var arg in arguments.Keys)
            {

                if (index == -1)
                    continue;

                int argCount = arguments[arg].Count;

                for (int i = 0; i < argCount; i++) 
                {
                    sb2.Append(arg + ": ");
                    sb2.Append(arguments[arg][i]);

                    if(i != argCount)
                        sb2.Append(", ");
                }

            }
            sb2.Append(")");
            sb.Insert(index + afterKeyword.Length, sb2.ToString());

            return sb.ToString();
        }
    }
}
