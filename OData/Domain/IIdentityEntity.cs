using System.ComponentModel.DataAnnotations;

namespace OData.Domain
{
    public abstract class IdentityEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
