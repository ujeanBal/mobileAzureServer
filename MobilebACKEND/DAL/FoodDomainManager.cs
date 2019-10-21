using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Azure.Mobile.Server;
using MobileAzureServerMVC;
using MobilebACKEND.Models;
using MobilebACKEND.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace MobilebACKEND.DAL
{
    public class FoodDomainManager : MappedEntityDomainManager<FoodDTO, Food>
    {
        private readonly Mapper _mapper;

        public FoodDomainManager(FoodContext context, HttpRequestMessage request) : base(context, request)
        {
            _mapper = new Mapper(Startup.MapperConfiguration);
            Context = context;
        }
        public override IQueryable<FoodDTO> Query()
        {
            return this.Context.Set<Food>().ProjectTo<FoodDTO>(Startup.MapperConfiguration);
        }

        public async override Task<FoodDTO> InsertAsync(FoodDTO data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (data.Id == null)
            {
                data.Id = Guid.NewGuid().ToString("N");
            }

            Food newFood = _mapper.Map<FoodDTO, Food>(data);
            Context.Set<Food>().Add(newFood);
            try
            {
                await this.SubmitChangesAsync();
            }
            catch (Exception ex)
            {
                var er = ex;
            }

            return _mapper.Map<Food, FoodDTO>(newFood);
        }

        public async override Task<bool> DeleteAsync(string id)
        {
            Food foodToDelete = this.Context.Set<Food>().Where(f => f.Id == id).Include(f=>f.Description).FirstOrDefault();

            if (foodToDelete != null)
            {
                this.Context.Entry(foodToDelete.Description).State = EntityState.Deleted;
                this.Context.Entry(foodToDelete).State = EntityState.Deleted;

                await this.SubmitChangesAsync();

                return true;
            }

            return false;
        }

        public override SingleResult<FoodDTO> Lookup(string id)
        {
            FoodDTO existingFoodDTO = this.Context.Set<Food>().Where(f => f.Id == id)
                .ProjectTo<FoodDTO>(Startup.MapperConfiguration).FirstOrDefault();

            return SingleResult.Create((new List<FoodDTO>() { existingFoodDTO }).AsQueryable());
        }

        public override async Task<FoodDTO> UpdateAsync(string id, Delta<FoodDTO> patch)
        {
            FoodDTO existingFoodDTO = null;
            Food existingFood = null;
            try
            {
                existingFood = this.Context.Set<Food>().Include(f => f.Description)
                    .Where(f => f.Id == id).FirstOrDefault();

                if (existingFood == null)
                {
                    throw new HttpResponseException(this.Request.CreateNotFoundResponse());
                }

                existingFoodDTO = _mapper.Map<Food, FoodDTO>(existingFood);
                patch.Patch(existingFoodDTO);
                _mapper.Map(existingFoodDTO, existingFood);

                await this.SubmitChangesAsync();
            }
            catch (Exception ex)
            {
                var rr = ex;
            }

            FoodDTO updatedFoodDTO = _mapper.Map<Food, FoodDTO>(existingFood);

            return updatedFoodDTO;
        }
    }
}