
using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.QueryTests
{
    [TestClass]
    public class MovieQueriesTests
    {
        private readonly List<Movie> _movies;
        private readonly int _existingMovieId = 0;
        private readonly int _notExistingMovieId = -1;

        public MovieQueriesTests()
        {
            _movies = new()
            {
                new()
            };
        }

        [TestMethod]
        public void ShouldReturnResultWhenPassAExistingMovieId()
        {
            var result = _movies.AsQueryable().Where(MovieQueries.GetById(_existingMovieId));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ShouldNotReturnResultWhenPassANotExistingMovieId()
        {
            var result = _movies.AsQueryable().Where(MovieQueries.GetById(_notExistingMovieId));
            Assert.AreEqual(0, result.Count());
        }
    }
}
