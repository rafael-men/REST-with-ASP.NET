using Microsoft.AspNetCore.Mvc;
using main.Hypermedia.Abstract;
using Microsoft.AspNetCore.Mvc.Filters;


namespace main.Hypermedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HypermediaFilterOptions _options;

        public HyperMediaFilter(HypermediaFilterOptions options)
        {
            _options = options;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult ok)
            {
                var enricher = _options.ContentResponseEnricherList.FirstOrDefault(x => x.CanEnrich(context));
                if(enricher != null) Task.FromResult(enricher.Enrich(context)); 
            }
        }
    }
}
