using System;
namespace MyPatchSG.SAL.DTO
{
    public class FileInfoDto
    {
        public string file_Type { get; set; }
        public string file_Name { get; set; }
        public Int64 file_Size { get; set; }
        public string version { get; set; }
        public string path_HTTP { get; set; }
        public Int32 isComplete { get; set; }
        public string AppVersion { get; set; }
        public string AppURL { get; set; }
        public int FORCE_UPDATE { get; set; }
    }
}
