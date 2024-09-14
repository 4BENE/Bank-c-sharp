using Application.Contracts.Admin;
using Application.Models.Admins;

namespace Application.Admins;

public class CurrentAdminManager : ICurrentAdminService
{
    public Admin? Admin { get; set; }
}