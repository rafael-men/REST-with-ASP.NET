using System.Text;
using main.Hypermedia.Variables;
using main.VO;
using Microsoft.AspNetCore.Mvc;

namespace main.Hypermedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();

        protected override Task EnricherModel(PersonVO content, IUrlHelper helper)
        {
            var path = "v1/api/persons";
            string link = GetLink(content.Id, helper, path);

            content.Links.Add(new MyHypermediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new MyHypermediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new MyHypermediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new MyHypermediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int" // Verifique se esse valor "int" faz sentido aqui
            });

            return Task.CompletedTask;
        }

        private string GetLink(long id, IUrlHelper helper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(helper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
