using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices
{
    public class StudentService
    {
        private readonly DataContext _cont;
        public StudentService(DataContext context)
        {
            _cont = context;
        }
    }
}
