using ServiceStack.DataAnnotations;

namespace Dolphin.Core.Domain.Course
{
    public class ImageResource
    {
        [Alias("ImageResource_Id")]
        public virtual int Id { get; set; }

        [Alias("DefaultUrl")]
        public virtual string Url { get; set; }
    }
}