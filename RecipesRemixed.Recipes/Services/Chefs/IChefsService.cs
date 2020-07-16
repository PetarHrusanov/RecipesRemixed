namespace RecipesRemixed.Recipes.Services.Chefs
{
    using System;
    using System.Threading.Tasks;
    using static RecipesRemixed.Recipes.Data.DataConstants;

    public interface IChefsService : IDataService<Chef>
    {
        Task<Chef> FindByUser(string userId);

        Task<int> GetIdByUser(string userId);

        Task<bool> HasCarAd(int dealerId, int carAdId);

        Task<bool> IsDealer(string userId);

        Task<DealerDetailsOutputModel> GetDetails(int id);

        Task<DealerOutputModel> GetDetailsByCarId(int carAdId);
    }
}
