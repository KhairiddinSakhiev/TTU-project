using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.PositionServices;

public class PositionService : IPositionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PositionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<Position>> GetPositions()
    {
        return await _context.Positions.ToListAsync();
    }

    public async Task<int> InsertPosition(PositionDto positionDto)
    {
        var mapped = _mapper.Map<Position>(positionDto);
        await _context.Positions.AddAsync(mapped);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdatePosition(PositionDto positionDto)
    {
        var finded = await _context.Positions.FindAsync(positionDto.Id);
        if (finded == null) return 0;
        finded.Name = positionDto.Name;
        finded.Enabled = positionDto.Enabled;
        finded.PositionType = positionDto.PositionType;
        finded.DepartmentId=positionDto.DepartmentId;
        return await _context.SaveChangesAsync();
    }

    public async Task<PositionDto> GetPositionById(int id)
    {
        var finded = await (from p in _context.Positions
                            where p.Id == id
                            select new PositionDto
                            {
                               Id=p.Id,
                               Name=p.Name,
                               Enabled=p.Enabled,
                               PositionType=p.PositionType,
                               DepartmentId=p.DepartmentId
                            }).FirstOrDefaultAsync();
        if (finded == null) return new PositionDto();
        return finded;
    }

    public async Task<int> DeletePosition(PositionDto dto)
    {
        var finded = await _context.Positions.FindAsync(dto.Id);
        if (finded == null) return 0;
        _context.Positions.Remove(finded);
        return await _context.SaveChangesAsync();
    }
}