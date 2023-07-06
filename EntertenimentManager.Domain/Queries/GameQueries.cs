using System.Linq.Expressions;
using System;
using EntertenimentManager.Domain.Entities.Itens;
using System.Linq;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Queries
{
    public class GameQueries
    {
        public static Expression<Func<Game, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }
        public static Expression<Func<Game, bool>> GetByPersonalListId(int personalListId)
        {
            return x => x.BelongsTo.Id == personalListId;
        }
        public static Expression<Func<Platform, bool>> GetAllPlatformsByIds(IEnumerable<int> platformsId)
        {
            return platform => platformsId.Contains(platform.Id);
        }
    }
}
