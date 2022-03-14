using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagementDbDesignFactory : IDesignTimeDbContextFactory<UserManagementDbContext>
    {
        public UserManagementDbDesignFactory()
        {

        }

        public UserManagementDbContext CreateDbContext(string[] args)
        {
            var conn = "Host=localhost;Port=5433;Database=UserManagementDb;Username=admin;Password=admin";
            var optionsBuilder = new DbContextOptionsBuilder<UserManagementDbContext>().UseNpgsql(conn);
            return new UserManagementDbContext(optionsBuilder.Options,new NoMediator());
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
