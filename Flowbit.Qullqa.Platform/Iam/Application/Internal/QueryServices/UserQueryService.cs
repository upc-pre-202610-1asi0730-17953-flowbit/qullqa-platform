using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        => await userRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        => await userRepository.FindByEmailAsync(query.Email, cancellationToken);

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        => await userRepository.ListAsync(cancellationToken);
}
