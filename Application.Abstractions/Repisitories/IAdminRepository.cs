using Application.Models.Admins;

namespace Application.Abstractions.Repisitories;

public interface IAdminRepository
{
    Task<Admin?> FindAdminByAdminId(string adminId, string password);
}