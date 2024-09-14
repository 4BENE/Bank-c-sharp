namespace Application.Contracts.Admin;

public interface ICurrentAdminService
{
    Models.Admins.Admin? Admin { get; }
}