using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.CMSModel.AppModel
{
    public class FileModel
    {
        public string FileId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long ByteSize { get; set; }
        public string Path { get; set; }
    }
}
