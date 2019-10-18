using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using MobilebACKEND.DAL;
using MobilebACKEND.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;

namespace MobilebACKEND.Controllers
{
    [MobileAppController]
    public class FoodController : TableController<FoodDTO>
    {
        private FoodContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new FoodContext();
            DomainManager = new FoodDomainManager(context, Request);
        }

        public IQueryable<FoodDTO> GetFoods()
        {
            IQueryable<FoodDTO> res = null;
            try
            {
                res = DomainManager.Query();
            }
            catch (Exception ex)
            {
                var rs = ex;
            }
            return res;
        }

        public SingleResult<FoodDTO> GetFood(string id)
        {
            return Lookup(id);
        }

        public async Task<FoodDTO> PatchFood(string id, Delta<FoodDTO> patch)
        {
            //var res = patch.GetEntity();
           // var fieldName = patch.GetDynamicMemberNames();
            return await DomainManager.UpdateAsync(id, patch);
        }

        public async Task<IHttpActionResult> PostFood([FromBody]FoodDTO item)
        {
            FoodDTO current = null;
            try
            {
                current = await DomainManager.InsertAsync(item);
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        public Task<FoodDTO> PostUndeleteFood(string id)
        {
            return UndeleteAsync(id);
        }

        public Task DeleteFood(string id)
        {
            return DeleteAsync(id);
        }
    }
}
