//using System.Collections.Generic;
//using System.Threading.Tasks;
//using DomainContracts.ApplicationSystemAggregate;
//using DomainContracts.Commons;
//using DomainEntities.ApplicationSystemAggregate;
//using KendoHelper;

//namespace DomainApplication.Services.ApplicationSystemAggregate
//{
//    public class ApplicationSystemService : IApplicationSystemService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IApplicationSystemRepository _systemRepository;
//        public ApplicationSystemService(IUnitOfWork unitOfWork,
//            IApplicationSystemRepository systemRepository)
//        {
//            _unitOfWork = unitOfWork;
//            _systemRepository = systemRepository;
//        }

//        public DataSourceResult GetAllAsync(DataSourceRequest request)
//        {
//            return _systemRepository.GetList(request);
//        }

//        public async Task<List<ApplicationSystemListDto>> GetDropDownList()
//        {
//            return await _systemRepository.GetDropDownList();
//        }

//        public Task<Policy> FindByIdAsync(int id)
//        {
//            return _systemRepository.FindByIdAsync(id);
//        }

//        public async Task SaveAsync(Policy model)
//        {
//            if (model.Id > 0)
//            {
//                //==== UPDATE ====
//                _systemRepository.Update(model, o=> o.Title, o=>o.DateOfContract,
//                    o => o.IpAddress,
//                    o => o.PublishDate,
//                    o=> o.CreatorDepartmentId,
//                    o => o.ApplicationSystemTypeId);
//            }
//            else
//            {
//                //==== INSERT ====
//                _systemRepository.Add(model);
//            }
//            await _unitOfWork.SaveAsync();
//        }

//        public async Task Remove(IEnumerable<int> ids)
//        {
//            foreach (var id in ids)
//            {
//                _systemRepository.Delete(new Policy { Id = id });
//            }

//            await _unitOfWork.SaveAsync();
//        }
//    }
//}