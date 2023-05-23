using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.EntityTests
{
    [TestClass]
    public class CategoryFactoryTests
    {

        private readonly CategoryFactory _categoryFactory = new();
        private readonly EnumCategories _movieType = EnumCategories.Movies;
        private readonly EnumCategories _gameType = EnumCategories.Games;
        private readonly EnumCategories _notValidType = (EnumCategories)99;

        [TestMethod]
        public void ShouldReturnMoviePersonalListsWhenTypeIsMovies()
        {
            var result = _categoryFactory.Create(_movieType).ToList();
            Assert.IsInstanceOfType(result, typeof(IEnumerable<PersonalList>));
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Pra Assistir", result[0].Title);
            Assert.AreEqual("Assistido", result[1].Title);
        }

        [TestMethod]
        public void ShouldReturnGamePersonalListsWhenTypeIsGames()
        {
            var result = _categoryFactory.Create(_gameType).ToList();

            Assert.IsInstanceOfType(result, typeof(IEnumerable<PersonalList>));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Pra Jogar", result[0].Title);
            Assert.AreEqual("Jogando", result[1].Title);
            Assert.AreEqual("Jogado", result[2].Title);
        }

        [TestMethod]
        public void Create_ReturnsEmptyList_WhenTypeIsNotMoviesOrGames()
        {
            var result = _categoryFactory.Create(_notValidType);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<PersonalList>));
            Assert.IsFalse(result.Any());
        }
    }
}
