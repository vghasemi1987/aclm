//using System.Collections.Generic;
//using System.Threading.Tasks;
//using DomainContracts.Commons;
//using DomainContracts.DepartmentAggregate;
//using DomainEntities.DepartmentAggregate;

//namespace DomainApplication.Services.DepartmentAggregate
//{
//    public class DepartmentService : IDepartmentService
//    {
//        private readonly IRepository<Department> _repository;
//        private readonly IUnitOfWork _unitOfWork;

//        public DepartmentService(IRepository<Department> repository,
//            IUnitOfWork unitOfWork)
//        {
//            _repository = repository;
//            _unitOfWork = unitOfWork;
//        }

//        public IList<Department> GetAll()
//        {
//            return _repository.ListAll();
//        }

//        public Department FindById(short id)
//        {
//            return _repository.GetSingleBySpec(o => o.Id.Equals(id));
//        }

//        public async Task Save(Department model)
//        {
//            if (model.Id > 0)
//            {
//                //==== UPDATE ====
//                _repository.Update(model, o => o.Title);
//            }
//            else
//            {
//                //==== INSERT ====
//                _repository.Add(model);
//            }
//            await _unitOfWork.SaveAsync();
//        }

//        public async Task Delete(short id)
//        {
//            _repository.Delete(new Department { Id = id });
//            await _unitOfWork.SaveAsync();
//        }
//    }
//}
