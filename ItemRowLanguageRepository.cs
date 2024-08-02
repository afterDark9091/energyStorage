using ICD.Base.Domain.Entity;
using ICD.Base.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ICD.Framework.Extensions;

namespace ICD.Base.Repository
{
    [Dependency(typeof(IItemRowLanguageRepository))]
    public class ItemRowLanguageRepository : BaseRepository<ItemRowLanguageEntity, int>, IItemRowLanguageRepository
    {
        public async Task<ListQueryResult<ItemRowLanguageEntity>> GetItemRowLanguagesAsync(QueryDataSource<ItemRowLanguageEntity> queryDataSource, int languageRef)
        {
            var result = new ListQueryResult<ItemRowLanguageEntity>
            {
                Entities = new List<ItemRowLanguageEntity>()
            };

            var queryResult = from irl in GenericRepository.Query<ItemRowLanguageEntity>()
                              where irl.LanguageRef == languageRef
                              select irl;

            result = await queryResult.ToListQueryResultAsync(queryDataSource);

            return result;
        }
    }
}
