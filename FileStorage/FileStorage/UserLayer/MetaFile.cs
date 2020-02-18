using System;

namespace FileStorage.UserLayer
{
    [Serializable]
    public class MetaFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public double Size { get; set; }
        public DateTime CreationDate { get; set; }
        public int DownloadsNumber { get; set; }
    }
}
