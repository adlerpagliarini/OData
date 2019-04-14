using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using OData.Domain;
using System;

namespace OData.Data
{
    public static class ODataEntitiesConfiguration
    {
        public static IEdmModel GetEdmModel(IServiceProvider provider)
        {
            var builder = new ODataConventionModelBuilder(provider);

            builder.EntitySet<Developer>(nameof(Developer)).EntityType
                   //.Expand(SelectExpandType.Automatic)
                   .Expand()
                   .Filter()
                   .Count()
                   .OrderBy()
                   .Page()
                   .Select();
                   // Enables Select for TaskToDo IF it isn't configured
                   // .HasMany(t => t.TasksToDo).Select();

            builder.EntitySet<TaskToDo>(nameof(TaskToDo)).EntityType
                   //.Expand(SelectExpandType.Automatic)
                   .Expand()
                   .Filter()
                   .Count()
                   .OrderBy()
                   .Page()
                   .Select();

            return builder.GetEdmModel();
        }
    }
}
