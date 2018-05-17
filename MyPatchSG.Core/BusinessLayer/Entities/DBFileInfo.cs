using System;
using MyPatchSG.SAL.DTO;

namespace MyPatchSG.BL.Entities
{
    public class DBFileInfo
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
        public Int64 FileSize { get; set; }
        public string Version { get; set; }
        public string PathHTTP { get; set; }
        public Int32 IsComplete { get; set; }

        public DBFileInfo()
        {
            this.FileName = "";
            this.FileType = "";
            this.FileSize = 0;
            this.Version = "";
            this.PathHTTP = "";
            this.IsComplete = 0;
        }

        public void ConvertFromFileInfoDto(FileInfoDto dto)
        {
            this.FileType = dto.file_Type;
            this.FileName = dto.file_Name;
            this.FileSize = dto.file_Size;
            this.Version = dto.version;
            this.PathHTTP = dto.path_HTTP;
            this.IsComplete = dto.isComplete;
        }

        public string GetFileName()
        {
            var filenameComponents = this.FileName.Split('.');
            return filenameComponents[0];
        }

        public string GetFileExtension()
        {
            var filenameComponents = this.FileName.Split('.');
            return filenameComponents[1].ToLower();
        }
    }
}
