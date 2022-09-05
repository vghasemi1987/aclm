﻿using System;

namespace DomainEntities.Commons
{
	public interface IAuditableEntity
	{
		DateTime CreatedDate { get; set; }

		int CreatedBy { get; set; }

		DateTime UpdatedDate { get; set; }

		int UpdatedBy { get; set; }
	}
}
