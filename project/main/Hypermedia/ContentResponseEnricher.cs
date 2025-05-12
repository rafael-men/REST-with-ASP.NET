using System.Collections.Concurrent;
using main.Hypermedia.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace main.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportHypermedia
    {

        public ContentResponseEnricher()
        {

        }
        public bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>);
        }

        protected abstract Task EnricherModel(T content, IUrlHelper helper);

        bool IResponseEnricher.CanEnrich(Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult ok)
            {
                return CanEnrich(ok.Value.GetType());
            }
            return false;
        }
        public async Task Enrich(ResultExecutingContext context)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(context);
            if (context.Result is OkObjectResult ok)
            {
                if (ok.Value is T model)
                {
                    await EnricherModel(model, urlHelper);
                }
                else if (ok.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) =>
                    {
                        EnricherModel(element, urlHelper);
                    });
                }
            }
            await Task.FromResult<object>(null);
        }
    }
}

