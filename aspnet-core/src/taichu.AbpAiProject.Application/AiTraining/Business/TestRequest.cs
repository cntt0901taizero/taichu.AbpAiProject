//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Volo.Abp.Domain.Repositories;

//namespace taichu.AbpAiProject.AiTraining.Business
//{
//    public class TestRequest : IRequest<bool>
//    {

//    }
//    public class TestHandlerRequest : IRequestHandler<TestRequest, bool>
//    {
//        private readonly IRepository<AiTrainingEntity, long> _repository;
//        public TestHandlerRequest(IRepository<AiTrainingEntity, long> repository)
//        {
//            _repository = repository;
//        }

//        public async Task<bool> Handle(TestRequest request, CancellationToken cancellationToken)
//        {
//            return true;
//        }
//    }
//}
