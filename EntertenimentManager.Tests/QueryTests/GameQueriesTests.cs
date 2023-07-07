using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.QueryTests
{
    [TestClass]
    public class GameQueriesTests
    {
        private readonly List<Game> _games;
        private readonly int _existingGameId = 0;
        private readonly int _notExistingGameId = -1;
        private readonly int _existingPersonalListId = 0;
        private readonly List<int> _platformtIds;
        private readonly int _existingPlatformId = 0;

        public GameQueriesTests()
        {
            _games = new()
            {
                new(
                    "GTA V",
                    "Sandbox",
                    2013,
                    new PersonalList("Jogando"),
                    "RockStar Games",
                    "",
                    new List<Platform>()
                    {
                        new Platform("PlayStation 3"),
                        new Platform("X Box 360")
                    })
            };

            _platformtIds = new() { _existingPlatformId };
        }
        [TestMethod]
        public void ShouldReturnResultWhenPassAExistentPersonalListId()
        {
            var result = _games.AsQueryable().Where(GameQueries.GetByPersonalListId(_existingPersonalListId));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ShouldReturnAllPlatformsAExistentPlatformsId()
        {
            var result = _games.First().Platforms.AsQueryable().Where(GameQueries.GetAllPlatformsByIds(_platformtIds));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ShouldReturnResultWhenPassAExistingGameId()
        {
            var result = _games.AsQueryable().Where(GameQueries.GetById(_existingGameId));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ShouldNotReturnResultWhenPassANotExistingGameId()
        {
            var result = _games.AsQueryable().Where(GameQueries.GetById(_notExistingGameId));
            Assert.AreEqual(0, result.Count());
        }

    }
}
