﻿
using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Enumerators;
using EntertenimentManager.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.QueryTests
{
    [TestClass]
    public class CategoryQueriesTests
    {
        private readonly User _user = new("Fulano", "fulano@email.com", "hashpass", "image", new CategoryFactory());
        private readonly List<Category> _categories;
        private readonly int _existingUserId = 0;
        public CategoryQueriesTests()
        {
            _categories = new List<Category>
            {
                new Category("Movies", (int)EnumCategories.Movies, new List<PersonalList>())
            };
            _categories[0].AddOwner(_user);
        }

        [TestMethod]
        public void ShouldReturnResultWhenPassAExistingUserId()
        {
            var result = _categories.AsQueryable().Where(CategoryQueries.GetByUserId(_existingUserId));
            Assert.AreEqual(1, result.Count());
        }
    }
}
