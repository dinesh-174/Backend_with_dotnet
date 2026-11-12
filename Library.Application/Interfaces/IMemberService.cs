using Library.Core.Entities;

namespace Library.Application.Interfaces;

public interface IMemberService
{
    Task<List<Member>> GetAllMembersAsync();

    Task<Member?> GetMemberByIdAsync(int id);

    Task AddMemberAsync(Member member);

    Task UpdateMemberAsync(int id, Member member);

    Task DeleteMemberAsync(int id);
}