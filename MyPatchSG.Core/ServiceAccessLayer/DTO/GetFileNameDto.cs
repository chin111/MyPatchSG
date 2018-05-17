using System;
using System.Collections.Generic;

namespace MyPatchSG.SAL.DTO
{
    public class GetFileNameDto
    {
        public string userName { get; set; }
        public List<FileInfoDto> dbFileNames { get; set; }
    }
}
