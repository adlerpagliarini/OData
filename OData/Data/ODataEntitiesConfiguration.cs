using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
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
                   .Filter()
                   .OrderBy()
                   .Count()
                   .Page()
                   .Select()
                   .Expand();
                    // .Expand(SelectExpandType.Automatic)
                    // Enables Select for TaskToDo IF it isn't configured
                    // .HasMany(t => t.TasksToDo).Select();

            builder.EntitySet<TaskToDo>(nameof(TaskToDo)).EntityType
                   .Expand()
                   .Filter()
                   .OrderBy()
                   .Select();

            return builder.GetEdmModel();
        }
    }
}
