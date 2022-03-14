using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserService.Infrastructure.Context
{
    public class UserDbDesignFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbDesignFactory()
        {

        }

        public UserDbContext CreateDbContext(string[] args)
        {
            var conn = "Host=localhost;Port=5432;Database=UserDb;Username=admin1;Password=admin1";
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>().UseNpgsql(conn);
            return new UserDbContext(optionsBuilder.Options,new NoMediator());
        }
    }


    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return null;
        }

        public IAsyncEnumerable<object> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return null;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<TResponse>(default);
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(default);
        }
    }
}
