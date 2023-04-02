using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Application.Abstractions;
using TaskMaster.Application.Services;
using TaskMaster.Infrastructure.UnitsOfWork;

namespace TaskMaster.Application.MediatR
{
    /// <summary>
    /// Represents an abstract base class for handling requests using the MediatR library.
    /// Provides access to commonly used services like IUnitOfWork and IMapper.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being handled.</typeparam>
    /// <typeparam name="TResponse">The type of the response being returned.</typeparam>
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IMapper> _mapper;
        private readonly Lazy<IUserContext> _userContext;


        /// <summary>
        /// Gets the IUnitOfWork instance for the current request.
        /// </summary>
        protected IUnitOfWork UnitOfWork => _unitOfWork.Value;

        /// <summary>
        /// Gets the IMapper instance for the current request.
        /// </summary>
        protected IMapper Mapper => _mapper.Value;

        /// <summary>
        /// Gets the IMapper instance for the current request.
        /// </summary>
        protected Guid CurrentUserId => _userContext.Value.UserId;

        public BaseRequestHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = new Lazy<IUnitOfWork>(_serviceProvider.GetRequiredService<IUnitOfWork>);
            _mapper = new Lazy<IMapper>(_serviceProvider.GetRequiredService<IMapper>);
            _userContext = new Lazy<IUserContext>(_serviceProvider.GetRequiredService<IUserContext>);
        }

        /// <summary>
        /// Handles the request asynchronously and returns a response.
        /// </summary>
        /// <param name="request">The request to be handled.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response.</returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = await HandleAsync(request, cancellationToken);

            // TODO: Raise domain events, if needed

            return response;
        }

        /// <summary>
        /// Handles the request asynchronously and returns a response.
        /// </summary>
        /// <param name="request">The request to be handled.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response.</returns>
        protected abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
