using Dolphin.Services.Course;
using EFSchools.Englishtown.Web;

namespace Dolphin.Services.Helpers
{
    public static class ResourceHelper
    {
        public static string GetTrans(Translator translator, string value)
        {
            if (value.StartsWith(ServiceConstant.BlurbPrefix))
            {
                var blurbId = int.Parse(value.Substring(ServiceConstant.BlurbPrefix.Length));
                return translator.GetTrans(blurbId, "en");
            }
            return value;
        }

        public static string GetMedia(ICourseContentService courseContentService, string value)
        {
            if (value != null && value.StartsWith(ServiceConstant.MediaPrefix))
            {
                var mediaResourceId = int.Parse(value.Substring(ServiceConstant.MediaPrefix.Length));
                return courseContentService.GetMedia(mediaResourceId).Url;
            }
            return value;
        }
    }
}