using EFSchools.Englishtown.Web;

namespace Dolphin.Services.Helpers
{
    public static class ResourceHelper
    {
        public static string GetTrans(Translator translator, string value)
        {
            var blurbId = int.Parse(value.Substring(ServiceConstant.BlurbPrefix.Length));
            return translator.GetTrans(blurbId, "en");
        }

        public static string GetMedia(Translator translator, string value)
        {
            var mediaResourceId = int.Parse(value.Substring(ServiceConstant.MediaPrefix.Length));
            return translator.GetMedia(mediaResourceId, "en");
        }
    }
}