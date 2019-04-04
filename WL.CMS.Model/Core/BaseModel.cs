using System;

namespace WL.CMS.Model
{
    public class BaseModel
    {
        public int Disabled { get; set; }
        public int IsRecycle { get; set; }
        public int IsDel { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set;}
        public string Editor { get; set; }
        public DateTime EditTime { get; set; }
    }
}
