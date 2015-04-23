using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHelper.DB;

namespace Application.Dto
{
    public class ComicDto
    {
        public int Id { get; set; }
        public string ComicName { get; set; }
        public string TitleName { get; set; }
        public string Description { get; set; }
        public string CoverImgPath { get; set; }
        public string LastVolumeName { get; set; }

        internal static List<ComicDto> GetAll()
        {
            return BaseDto.DtoRepository.GetList<ComicDto>(SimpleSqlCreater.Select<ComicDto>().ToString());
        }

        internal static ComicDto GetById(int id)
        {
            return BaseDto.DtoRepository.GetModel<ComicDto>(SimpleSqlCreater.Select<ComicDto>().Eq("Id", id.ToString()).ToString());
        }

        internal static List<ComicVolumeDto> GetVolumeById(int id)
        {
            return BaseDto.DtoRepository.GetList<ComicVolumeDto>(SimpleSqlCreater.Select<ComicVolumeDto>().Eq("ComicId", id.ToString()).ToString());
        }
    }
}
