using ICD.Base.Domain.Entity;
using ICD.Base.Domain.View;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICD.Framework.Extensions;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IItemRowRepository))]
    public class ItemRowRepository : BaseRepository<ItemRowEntity, int>, IItemRowRepository
    {
        public async Task<ListQueryResult<ItemRowByKeyView>> GetItemRowByKeyAsync(QueryDataSource<ItemRowByKeyView> queryDataSource, int languageRef)
        {
            var result = new ListQueryResult<ItemRowByKeyView>
            {
                Entities = new List<ItemRowByKeyView>()
            };

            var queryResult = from ir in GenericRepository.Query<ItemRowEntity>()
                              join irl in GenericRepository.Query<ItemRowLanguageEntity>()
                              on ir.Key equals irl.ItemRowRef
                              where irl.LanguageRef == languageRef
                              select new ItemRowByKeyView
                              {
                                  Key = ir.Key,
                                  Alias = ir.Alias,
                                  IsActive = ir.IsActive,
                                  ItemGroupRef = ir.ItemGroupRef,
                                  Value = ir.Value,
                                  ItemRowLanguageKey = irl.Key,
                                  _Title = irl._Title
                              };
            result = await queryResult.ToListQueryResultAsync(queryDataSource);

            return result;
        }

        public async Task<ListQueryResult<ItemRowView>> GetItemRowsByItemGroupRef(QueryDataSource<ItemRowView> searchQuery, int languageRef)
        {
            var result = new ListQueryResult<ItemRowView>
            {
                Entities = new List<ItemRowView>()
            };

            var queryResult = from ir in GenericRepository.Query<ItemRowEntity>()
                              join irl in GenericRepository.Query<ItemRowLanguageEntity>()
                              on ir.Key equals irl.ItemRowRef
                              join ig in GenericRepository.Query<ItemGroupEntity>()
                              on ir.ItemGroupRef equals ig.Key
                              where irl.LanguageRef == languageRef
                              select new ItemRowView
                              {
                                  Key = ir.Key,
                                  Alias = ir.Alias,
                                  IsActive = ir.IsActive,
                                  ItemGroupRef = ir.ItemGroupRef,
                                  Value = ir.Value,
                                  ItemRowRef = irl.ItemRowRef,
                                  _Title = irl._Title,
                                  ApplicationRef = ig.ApplicationRef,
                                  ItemGroupAlias = ig.Alias,
                                  ItemGroupIsActive = ig.IsActive
                              };
            result = await queryResult.ToListQueryResultAsync(searchQuery);

            return result;
        }
    }
}
