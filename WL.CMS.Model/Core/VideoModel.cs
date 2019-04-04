using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    public class VideoModel
    {
        public string VideoId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long ByteSize { get; set; }
        public long Duration { get; set; }
        public string VideoInfo { get; set; }
        public string Path { get; set; }
    }
}
