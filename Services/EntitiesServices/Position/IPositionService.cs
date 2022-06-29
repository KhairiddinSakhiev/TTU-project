using Domain.EntitiesDto;

namespace Services.EntitiesServices.Position;

public interface IPositionService
{
    Task<List<Domain.Entities.Position>> GetPositions();
    Task<int> InsertPosition(PositionDto positionDto);
    Task<int> UpdatePosition(PositionDto positionDto);
    Task<PositionDto> GetPositionById(int id);
    Task<int> DeletePosition(int id);
}