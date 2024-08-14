﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager
{
    public abstract class ResourceController<TResponse, TEntity>
    {
        protected QueryBuilder getQueryBuilder;
        protected string updateQueryBuilder;
        protected List<MenuItem> menuItems;
        public List<MenuItem> MenuItems { get => menuItems; }

        public ResourceController()
        {
            menuItems = new List<MenuItem>();
        }

        public async Task<TResponse> GetEntries()
        {
            return await GrpcService.SendRequestAsync<TResponse>(getQueryBuilder.BuildQuery());
        }

        public void RemovePaging()
        {
            getQueryBuilder.RemoveArgument("page");
            getQueryBuilder.RemoveArgument("limit");
        }

        public void RemoveSort()
        {
            getQueryBuilder.RemoveArgument("sortModel");
        }

        public void SetPaging(int page, int limit)
        {
            getQueryBuilder.AddArgument("page", page.ToString(), true);
            getQueryBuilder.AddArgument("limit", limit.ToString(), true);
        }

        public void SetSort(string column, string direction)
        {
            getQueryBuilder.AddArgument("sortModel", "{ fieldName: \"" + column + "\", direction: \"" + direction + "\" }", true);
        }
    }
}