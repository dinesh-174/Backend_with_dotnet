using Library.Application.Interfaces;
using Library.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMembers()
    {
        var members = await _memberService.GetAllMembersAsync();

        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);

        if (member == null)
            return NotFound();

        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(Member member)
    {
        await _memberService.AddMemberAsync(member);

        return Ok(member);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, Member member)
    {
        await _memberService.UpdateMemberAsync(id, member);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        await _memberService.DeleteMemberAsync(id);

        return NoContent();
    }
}