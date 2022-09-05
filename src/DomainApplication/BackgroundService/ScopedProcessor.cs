﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DomainApplication.BackgroundService
{
	public abstract class ScopedProcessor : BackgroundService
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;

		protected ScopedProcessor(IServiceScopeFactory serviceScopeFactory) : base()
		{
			_serviceScopeFactory = serviceScopeFactory;
		}

		protected override async Task Process()
		{
			using (var scope = _serviceScopeFactory.CreateScope())
			{
				await ProcessInScopeAsync(scope.ServiceProvider);
			}
		}

		protected abstract Task ProcessInScopeAsync(IServiceProvider serviceProvider);
	}
}
