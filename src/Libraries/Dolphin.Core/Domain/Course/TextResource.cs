using ServiceStack.DataAnnotations;

namespace Dolphin.Core.Domain.Course
{
    public class TextResource
    {
        [Alias("TextResource_Id")]
        public virtual int Id { get; set; }

        [Alias("DefaultText")]
        public virtual string Text { get; set; }
    }
}