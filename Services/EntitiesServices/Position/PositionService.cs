using AutoMapper;
using Domain.EntitiesDto;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.Position;

public class PositionService : IPositionService
{
    private readonly DataContext _context;

    public PositionService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.Entities.Position>> GetPositions()
    {
        
        var get = await _context.Positions.ToListAsync();
        return get;
    }

    public Task<int> InsertPosition(PositionDto positionDto)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdatePosition(PositionDto positionDto)
    {
        throw new NotImplementedException();
    }

    public Task<PositionDto> GetPositionById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeletePosition(int id)
    {
        throw new NotImplementedException();
    }
}