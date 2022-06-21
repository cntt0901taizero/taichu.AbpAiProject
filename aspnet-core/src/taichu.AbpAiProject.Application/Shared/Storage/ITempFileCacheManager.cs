using System.Threading.Tasks;
using BaseApplication.Dtos;
using Volo.Abp.DependencyInjection;

namespace BaseApplication.Storage
{
    public interface ITempFileCacheManager: ITransientDependency
    {
         Task SetFileAsync(FileDto token, byte[] content);

         Task<byte[]> GetFileAsync(string token);
         Task<FileDto> GetFileDtoAsync(string token);
    }
}
