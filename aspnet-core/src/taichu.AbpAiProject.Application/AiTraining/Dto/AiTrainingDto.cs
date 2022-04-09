using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace taichu.AbpAiProject.AiTraining.Dto
{
    public class AiTrainingDto : EntityDto<long>
    {
        public string InputString { get; set; }
        public string OutputString { get; set; }
        public string FuncName { get; set; }
        public string Link { get; set; }
        public string Note { get; set; }
    }

    public class AiTrainingPagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto
    {
        
    }

}
