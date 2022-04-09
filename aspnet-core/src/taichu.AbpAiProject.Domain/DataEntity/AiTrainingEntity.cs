using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace taichu.AbpAiProject.AiTraining
{
    [Table(name: "Db_Ai_Training")]
    public class AiTrainingEntity : Entity<long>
    {
        public string InputString { get; set; }
        public string OutputString { get; set; }
        public string FuncName { get; set; }
        public string Link { get; set; }
        public string Note { get; set; }
    }
}
