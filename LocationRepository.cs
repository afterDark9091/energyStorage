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
    [Dependency(typeof(ILocationRepository))]
    public class LocationRepository : BaseRepository<LocationEntity, int>, ILocationRepository
    {
        public async Task<ListQueryResult<LocationView>> GetLocation(QueryDataSource<LocationView> searchQuery, int languageRef)
        {
            var finalResult = new ListQueryResult<LocationView>
            {
                Entities = new List<LocationView>()
            };

            var queryResult = from l in GenericRepository.Query<LocationEntity>()
                              join ll in GenericRepository.Query<LocationLanguageEntity>()
                              on l.Key equals ll.LocationRef
                              join lt in GenericRepository.Query<LocationTypeEntity>()
                              on l.LocationTypeRef equals lt.Key
                              where ll.LanguageRef == languageRef
                              select new LocationView
                              {
                                  Key = l.Key,
                                  LocationTypeRef = l.LocationTypeRef,
                                  ParentRef = l.ParentRef,
                                  LevelNo = l.LevelNo,
                                  IsActive = l.IsActive,
                                  LocationRef = ll.LocationRef,
                                  _Name = ll._Name,
                                  Alias = lt.Alias
                              };

            var result = await queryResult.ToListQueryResultAsync(searchQuery);

            finalResult = result;

            return finalResult;

        }
    }
}
