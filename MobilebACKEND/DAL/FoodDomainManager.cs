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
    public class FoodDomainManager : MappedEntityDomainManager<FoodMobile, Food>
    {
        private readonly Mapper _mapper;

        public FoodDomainManager(FoodContext context, HttpRequestMessage request) : base(context, request)
        {
            _mapper = new Mapper(Startup.MapperConfiguration);
            Context = context;
        }
        public override IQueryable<FoodMobile> Query()
        {
            return this.Context.Set<Food>().ProjectTo<FoodMobile>(Startup.MapperConfiguration);
        }

        public async override Task<FoodMobile> InsertAsync(FoodMobile data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (data.Id == null)
            {
                data.Id = Guid.NewGuid().ToString("N");
            }

            Food model = _mapper.Map<FoodMobile, Food>(data);
            Context.Set<Food>().Add(model);
            try
            {
                await this.SubmitChangesAsync();
            }
            catch (Exception ex)
            {
                var er = ex;
            }

            return _mapper.Map<Food, FoodMobile>(model);
        }

        public async override Task<bool> DeleteAsync(string id)
        {
            Food foodToDelete = this.Context.Set<Food>().Where(f => f.Id == id).FirstOrDefault();
            if (foodToDelete != null)
            {
                this.Context.Entry(foodToDelete).State = EntityState.Detached;
                await this.SubmitChangesAsync();
                return true;
            }

            return false;
        }

        public override SingleResult<FoodMobile> Lookup(string id)
        {
            FoodMobile existingCustomerDTO = this.Context.Set<Food>().Where(f => f.Id == id)
                .ProjectTo<FoodMobile>(Startup.MapperConfiguration).FirstOrDefault();

            return SingleResult.Create((new List<FoodMobile>() { existingCustomerDTO }).AsQueryable());
        }

        public override async Task<FoodMobile> UpdateAsync(string id, Delta<FoodMobile> patch)
        {
            FoodMobile existingCustomerDTO = null;
            Food existingCustomer = null;
            try
            {
                existingCustomer = this.Context.Set<Food>().Include(f => f.Description).Where(f => f.Id == id).FirstOrDefault();
                if (existingCustomer == null)
                {
                    throw new HttpResponseException(this.Request.CreateNotFoundResponse());
                }

                existingCustomerDTO = _mapper.Map<Food, FoodMobile>(existingCustomer);
                patch.Patch(existingCustomerDTO);
                _mapper.Map(existingCustomerDTO, existingCustomer);

                await this.SubmitChangesAsync();
            }
            catch (Exception ex)
            {
                var rr = ex;
            }

            FoodMobile updatedCustomerDTO = _mapper.Map<Food, FoodMobile>(existingCustomer);

            return existingCustomerDTO;
        }
    }
}