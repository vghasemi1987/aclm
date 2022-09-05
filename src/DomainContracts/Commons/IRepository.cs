using DomainEntities.Commons;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DomainContracts.Commons
{
	public interface IRepository<T> where T : BaseEntity
	{
		//T GetById(int id);
		IList<T> ListAllByConditionAsync(Func<T, bool> predicate);
		T GetSingleBySpec(ISpecification<T> spec);
		IList<T> ListAll();
		IList<T> List(ISpecification<T> spec);
		T Add(T entity);
		void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
		void Delete(T entity);
		IDbContextTransaction BeginTransaction();
		void AddList(IEnumerable<T> entity);
		T GetSingleBySpec(Expression<Func<T, bool>> criteria);
	}
}