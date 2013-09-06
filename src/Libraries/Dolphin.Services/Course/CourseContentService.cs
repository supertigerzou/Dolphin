using Dolphin.Core.Caching;
using Dolphin.Core.Data;
using Dolphin.Core.Domain.Course;
using Dolphin.Services.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Dolphin.Services.Course
{
    public class CourseContentService : ICourseContentService
    {
        private readonly IRepository<CourseUnit> _courseUnitRepository;
        private readonly IRepository<ImageResource> _imageResourceRepository;
        private readonly IRepository<TextResource> _textResourceRepository;
        private readonly ICacheManager _cacheManager;

        public CourseContentService(
            IRepository<CourseUnit> courseUnitRepository,
            IRepository<ImageResource> imageResourceRepository,
            IRepository<TextResource> textResourceRepository,
            ICacheManager cacheManager)
        {
            this._courseUnitRepository = courseUnitRepository;
            this._imageResourceRepository = imageResourceRepository;
            this._textResourceRepository = textResourceRepository;
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
                    unit.Name = ResourceHelper.GetTrans(this, unit.Name);
                    unit.Description = ResourceHelper.GetTrans(this, unit.Description);
                    unit.ImageUrl = GetMedia(int.Parse(unit.ImageUrl.Substring(ServiceConstant.MediaPrefix.Length))).Url;
                    foreach (var lesson in unit.Lessons)
                    {
                        lesson.Name = ResourceHelper.GetTrans(this, lesson.Name);
                        lesson.Description = ResourceHelper.GetTrans(this, lesson.Description);
                        lesson.ImageUrl = ResourceHelper.GetMedia(this, lesson.ImageUrl);
                    }
                }

                return units;
            });
        }

        public IList<TextResource> GetAllTexts()
        {
            const string key = "Dolphin.text.all";
            return this._cacheManager.Get(key, () => this._textResourceRepository.Table.ToList());
        }

        public IList<ImageResource> GetAllMedias()
        {
            const string key = "Dolphin.media.all";
            return this._cacheManager.Get(key, () => this._imageResourceRepository.Table.ToList());
        }

        public TextResource GetText(int textId)
        {
            return GetAllTexts().SingleOrDefault(textResource => textResource.Id == textId);
        }

        public ImageResource GetMedia(int mediaId)
        {
            return GetAllMedias().SingleOrDefault(imageResource => imageResource.Id == mediaId);
        }
    }
}