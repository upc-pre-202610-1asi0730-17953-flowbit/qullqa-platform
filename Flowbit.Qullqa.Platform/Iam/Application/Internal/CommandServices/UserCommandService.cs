using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Iam.Application.CommandServices;
using Qullqa.Platform.Iam.Application.Internal.OutboundServices;
using Qullqa.Platform.Iam.Domain.Model;
using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Model.Commands;
using Qullqa.Platform.Iam.Domain.Repositories;
using Qullqa.Platform.Shared.Application.Model;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);
        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(IamError.InvalidCredentials, "Invalid email or password.");

        var token = tokenService.GenerateToken(user);
        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result.Failure(IamError.EmailAlreadyTaken, $"Email '{command.Email}' is already taken.");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Email, hashedPassword, command.FirstName, command.LastName);
        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(IamError.OperationCancelled, "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            return Result.Failure(IamError.InternalServerError, "An internal server error occurred.");
        }
    }

    public async Task<Result<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.Id, cancellationToken);
        if (user == null) return Result<User>.Failure(IamError.UserNotFound, $"User with id {command.Id} not found.");

        user.UpdateProfile(command.FirstName, command.LastName).UpdateStatus(command.Status);
        if (command.RoleId.HasValue) user.AssignRole(command.RoleId.Value);
        if (command.BusinessId.HasValue) user.AssignBusiness(command.BusinessId.Value);

        userRepository.Update(user);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<User>.Success(user);
    }
}
