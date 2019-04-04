using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    public class ContentModel
    {
        public string ContentId { get; set; }
        public CategoryModel RootCategory { get; set; }
        public List<CategoryModel> ParentCategory { get; set; }
        public string ContentAlias { get; set; }
        public string AliasPath { get; set; }
        public List<string> Keys { get; set; }
        public List<string> Tags { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Remark { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastEditTime { get; set; }
        public string MainPic { get; set; }

        public List<FileModel> Files { get; set; }
        public List<PicModel> Pics { get; set; }
        public List<VideoModel> Videos { get; set; }
        public List<LogModel> Logs { get; set; }
        public NeighbourModel Previous { get; set; }
        public NeighbourModel Next { get; set; }
        public SysInfoModel SysInfo { get; set; }
    }
}
