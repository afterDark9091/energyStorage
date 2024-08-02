using ICD.Base.Domain.Entity;
using ICD.Base.Domain.View;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IItemGroupRepository))]
    public class ItemGroupRepository : BaseRepository<ItemGroupEntity, int>, IItemGroupRepository
    {
        public async Task<ListQueryResult<ItemGroupView>> GetItemGroupsByApplicationRef(QueryDataSource<ItemGroupView> searchQuery, int languageRef)
        {
            var result = new ListQueryResult<ItemGroupView>
            {
                Entities = new List<ItemGroupView>()
            };

            var queryResult = from ig in GenericRepository.Query<ItemGroupEntity>()
                              join igl in GenericRepository.Query<ItemGroupLanguageEntity>()
                              on ig.Key equals igl.ItemGroupRef
                              where igl.LanguageRef == languageRef
                              select new ItemGroupView
                              {
                                  Key = ig.Key,
                                  ApplicationRef = ig.ApplicationRef,
                                  Alias = ig.Alias,
                                  IsActive = ig.IsActive,
                                  _Title = igl._Title,
                                  ItemGroupRef = igl.ItemGroupRef
                              };

            result = await queryResult.ToListQueryResultAsync(searchQuery);

            return result;
        }
    }
}
