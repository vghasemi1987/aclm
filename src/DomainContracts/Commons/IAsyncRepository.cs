﻿using DomainEntities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.Commons
{
	public interface IAsyncRepository<T> where T : BaseEntity
	{
		//Task<T> GetByIdAsync(int id);
		Task<IList<T>> ListAllAsync();
		Task<IList<T>> ListAsync(ISpecification<T> spec);
		Task AddListAsync(IEnumerable<T> entity);
	}
}
