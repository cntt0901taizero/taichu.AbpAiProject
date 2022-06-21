using Volo.Abp.Application.Dtos;

namespace BaseApplication.Dtos
{
    public class PagedFullRequestDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public bool IsGetTotal { get; set; }
    }
}
