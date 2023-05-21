using AutoMapper;
using Core.DTOs.CategoryDTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repository;
        private readonly IImageService imageService;
        private readonly IMapper mapper;
        public CategoryService(IRepository<Category> repository,
                               IImageService imageService,
                               IMapper mapper)
        {
            this.repository = repository;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        public async Task Add(AddCategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };

            if (categoryDTO.Image != null)
            {
                var image = await imageService.SaveUserAvatar(categoryDTO.Image);

                image.Category = category;
                category.Image = image;

                await imageService.SaveImageToDatabase(image);
            }

            await repository.AddAsync(category);
            await repository.SaveChangesAsync();
        }
        public async Task Update(UpdateCategoryDTO categoryDTO)
        {
            var category = (await repository.GetAsync(c => c.Id == categoryDTO.Id, includeProperties: $"{nameof(Category.Image)}")).FirstOrDefault();

            if (category == null) throw new HttpException(ErrorMessages.CategoryNotFound, HttpStatusCode.BadRequest);

            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;

            if (categoryDTO.IsImageChanged && categoryDTO.Image != null)
            {
                await imageService.RemoveUserAvatar(category.Image.Id);

                var image = await imageService.SaveUserAvatar(categoryDTO.Image);

                category.Image.FullName = image.FullName;
                category.Image.Name = image.Name;

                category.Image.IsEdited = true;

                await imageService.UpdateImageInDatabase(category.Image);
            }

            category.IsEdited = true;

            //repository.Update(category);
            await repository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = (await repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(Category.Image)}")).FirstOrDefault();

            if (category == null) throw new HttpException(ErrorMessages.CategoryNotFound, HttpStatusCode.BadRequest);

            category.IsDeleted = true;

            repository.Update(category);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<CategoryDTO>> GetAll()
        {
            return mapper.Map<ICollection<CategoryDTO>>(await repository.GetAsync(c=>c.IsDeleted==false,includeProperties: $"{nameof(Category.Image)}"));
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = (await repository.GetAsync(c => c.Id == id, includeProperties: $"{nameof(Category.Image)}")).FirstOrDefault();

            if (category == null) throw new HttpException(ErrorMessages.CategoryNotFound, HttpStatusCode.BadRequest);

            return mapper.Map<CategoryDTO>(category);
        }
    }
}
