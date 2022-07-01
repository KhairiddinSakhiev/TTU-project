using Domain.Entities;
using Domain.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.SliderServices
{
    public interface ISliderService
    {
        Task<List<Slider>> GetSliders();
        Task<SliderDto> GetSliderById(int Id);
        Task<int> Insert(SliderDto slider);
        Task<int> Update(SliderDto slider);
        Task<int> Delete(SliderDto slider);
        Task<int> Delete(int Id);
    }
}
