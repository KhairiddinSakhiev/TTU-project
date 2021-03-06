using Domain.Entities;
using Domain.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.NewsServices
{
    public interface INewsService
    {
        Task<List<News>> GetNewses();
        Task<NewsDto> GetNewsById(int Id);
        Task<int> Insert(NewsDto news);
        Task<int> Update(NewsDto news);
        Task<int> Delete(NewsDto news);
        Task<int> Delete(int Id);
    }
}
