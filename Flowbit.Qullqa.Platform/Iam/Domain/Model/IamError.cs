namespace Flowbit.Qullqa.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    UserNotFound,
    EmailAlreadyTaken,
    InvalidCredentials,
    BusinessNotFound,
    RoleNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
