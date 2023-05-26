using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EntertenimentManager.Tests.QueryTests
{
    [TestClass]
    public class PersonalListQueriesTests
    {
        private readonly Category _category = new("Movies", (int)EnumCategories.Movies, new List<PersonalList>());
        private readonly List<PersonalList> _personalLists;
        private readonly int _existingCategoryId = 0;
        private readonly int _existingPersonalListId = 0;
        private readonly int _NotExistingPersonalListId = -1;
        public PersonalListQueriesTests()
        {
            _personalLists = new List<PersonalList>
            {
                new PersonalList("Assistido")
            };
            _personalLists[0].AddCategory(_category);
        }

        [TestMethod]
        public void ShouldReturnResultWhenPassACategoryId()
        {
            var result = _personalLists.AsQueryable().Where(PersonalListQueries.GetByCategoryId(_existingCategoryId));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ShouldReturnResultWhenPassAExistingPersonalListId()
        {
            var result = _personalLists.AsQueryable().Where(PersonalListQueries.GetById(_existingPersonalListId));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ShouldNotReturnResultWhenPassANotExistingPersonalListId()
        {
            var result = _personalLists.AsQueryable().Where(PersonalListQueries.GetById(_NotExistingPersonalListId));
            Assert.AreEqual(0, result.Count());
        }
    }
}
