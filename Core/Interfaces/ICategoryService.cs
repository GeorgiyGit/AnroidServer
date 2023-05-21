using Core.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        public Task Add(AddCategoryDTO categoryDTO);
        public Task Update(UpdateCategoryDTO categoryDTO);
        public Task Delete(int id);
        public Task<ICollection<CategoryDTO>> GetAll();
        public Task<CategoryDTO> GetById(int id);
    }
}
