using System;
using System.ComponentModel.DataAnnotations;

namespace BaseApplication.Dtos
{
    public class FileDto
    {
        [Required]
        public string FileName { get; set; }

        public string FileType { get; set; }

        [Required]
        public string FileToken { get; set; }
        public byte[] FileBytes { get; set; }

        public FileDto()
        {

        }

        public FileDto(string fileName, string fileType)
        {
            FileName = fileName;
            FileType = fileType;
            FileToken = Guid.NewGuid().ToString("N");
        }
        public FileDto(string fileName, string fileType, bool isSetFileName = false)
        {
            FileName = fileName;
            FileType = fileType;
            FileToken = isSetFileName ? fileName : Guid.NewGuid().ToString("N");
        }
    }
}
