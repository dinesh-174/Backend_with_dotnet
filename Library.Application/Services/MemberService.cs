using Library.Application.Interfaces;
using Library.Core.Entities;

namespace Library.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Member>> GetAllMembersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddMemberAsync(Member member)
    {
        await _repository.AddAsync(member);
    }

    public async Task UpdateMemberAsync(int id, Member member)
    {
        var existingMember = await _repository.GetByIdAsync(id);

        if (existingMember == null)
            throw new KeyNotFoundException("Member not found.");

        existingMember.FullName = member.FullName;
        existingMember.Email = member.Email;
        existingMember.PhoneNumber = member.PhoneNumber;
        existingMember.IsActive = member.IsActive;

        await _repository.UpdateAsync(existingMember);
    }

    public async Task DeleteMemberAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id);

        if (member == null)
            throw new KeyNotFoundException("Member not found.");

        await _repository.DeleteAsync(member);
    }
}