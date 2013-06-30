using Dolphin.Core.Caching;
using Dolphin.Core.Data;
using Dolphin.Core.Domain.Course;
using Dolphin.Services.Helpers;
using EFSchools.Englishtown.Web;
using System.Collections.Generic;
using System.Linq;

namespace Dolphin.Services.Course
{
    public class CourseContentService : ICourseContentService
    {
        private readonly IRepository<CourseUnit> _courseUnitRepository;
        private readonly IRepository<ImageResource> _imageResourceRepository;
        private readonly Translator _translator;
        private readonly ICacheManager _cacheManager;

        public CourseContentService(
            IRepository<CourseUnit> courseUnitRepository,
            IRepository<ImageResource> imageResourceRepository,
            Translator translator,
            ICacheManager cacheManager)
        {
            this._courseUnitRepository = courseUnitRepository;
            this._imageResourceRepository = imageResourceRepository;
            this._translator = translator;
            this._cacheManager = cacheManager;
        }

        public IList<CourseUnit> GetAllUnits()
        {
            const string key = "Dolphin.unit.all";

            return this._cacheManager.Get(key, () =>
            {
                var units = _courseUnitRepository.Table.Where(unit =>
                        unit.Name != null && unit.Name.StartsWith(ServiceConstant.BlurbPrefix) &&
                        unit.Description != null && unit.Description.StartsWith(ServiceConstant.BlurbPrefix) &&
                        unit.ImageUrl != null && unit.ImageUrl.StartsWith(ServiceConstant.MediaPrefix)
                    ).ToList();

                foreach (var unit in units)
                {
                    unit.Name = ResourceHelper.GetTrans(_translator, unit.Name);
                    unit.Description = ResourceHelper.GetTrans(_translator, unit.Description);
                    unit.ImageUrl = GetMedia(int.Parse(unit.ImageUrl.Substring(ServiceConstant.MediaPrefix.Length))).Url;
                }

                return units;
            });
        }

        public IList<ImageResource> GetAllMedias()
        {
            const string key = "Dolphin.media.all";
            return this._cacheManager.Get(key, () => this._imageResourceRepository.Table.ToList());
        }

        public ImageResource GetMedia(int mediaId)
        {
            return GetAllMedias().SingleOrDefault(imageResource => imageResource.Id == mediaId);
        }
    }
}