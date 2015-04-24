using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model.Comic;
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
            return BaseDto.DtoRepository.GetList<ComicVolumeDto>(SimpleSqlCreater.Select<ComicVolume>().Eq("ComicId", id.ToString()).ToString());
        }

        internal static List<ComicPageDto> GetPagesByVolId(int volid)
        {
            return BaseDto.DtoRepository.GetList<ComicPageDto>(SimpleSqlCreater.Select<ComicPage>().Eq("VolumeId", volid.ToString()).ToString());
        }
    }

    public class ComicVolumeDto
    {
        public int Id { get; set; }
        public int ComicId { get; set; }
        public string VolumeName { get; set; }
        public int SortNo { get; set; }
    }

    public class ComicPageDto
    {
        public int ComicId { get; set; }
        public int VolumeId { get; set; }
        public int PageNumber { get; set; }
        public string PageImgPath { get; set; }
    }
}
