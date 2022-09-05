﻿namespace DomainEntities.Commons
{
	public interface IEntity<T>
	{
		T Id { get; set; }
	}
	public abstract class BaseEntity
	{

	}

	public abstract class Entity<T> : BaseEntity, IEntity<T>
	{
		public virtual T Id { get; set; }
		public bool RecordStatus { get; set; }
	}
}
