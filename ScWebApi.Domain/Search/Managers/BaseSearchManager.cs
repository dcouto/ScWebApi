using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ScWebApi.Domain.Common.Extensions;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace ScWebApi.Domain.Search.Managers
{
    public class BaseSearchManager
    {
        protected void AddAncestorCondition<T>(ref Expression<Func<T, bool>> queryExpression, ID ancestorId)
            where T : SearchResultItem
        {
            if (ID.IsNullOrEmpty(ancestorId))
                return;

            queryExpression = queryExpression.And(i => i.Paths.Contains(ancestorId));
        }

        protected void AddTemplatesCondition<T>(ref Expression<Func<T, bool>> queryExpression,
            IEnumerable<string> templateIds) where T : SearchResultItem
        {
            if (templateIds == null || !templateIds.Any())
                return;

            var templateQuery = PredicateBuilder.False<T>();

            foreach (var templateId in templateIds)
            {
                var currentTemplateId = templateId;

                templateQuery = templateQuery.Or(i => i.TemplateId == ID.Parse(currentTemplateId));
            }

            queryExpression = queryExpression.And(queryExpression);
        }

        protected void AddKeywordsCondition<T>(ref Expression<Func<T, bool>> queryExpression, string keywords)
            where T : SearchResultItem
        {
            if (keywords.IsNotNullAndNotEmpty())
            {
                queryExpression = queryExpression.And(i => i.Content == keywords);
            }
        }
    }
}