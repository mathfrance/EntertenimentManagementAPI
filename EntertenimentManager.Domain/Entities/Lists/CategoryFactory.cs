using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists.Contracts;
using EntertenimentManager.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntertenimentManager.Domain.Entities.Lists
{
    public interface IPersonalList { }

    public class CategoryFactory : ICategoryFactory
    {
        public IEnumerable<IPersonalList> Create(EnumCategories type)
        {
            if (type == EnumCategories.Movies)
            {
                var items = new List<Item>
                {
                    //new Movie()
                };
                var listas = new List<PersonalList>
                {
                    new("Filmes", items)
                };
                return listas;
            }
            else if (type == EnumCategories.Games)
            {
                var items = new List<Item>
                {
                    //new Game()
                };
                var listas = new List<PersonalList>
                {
                    new("Jogo", items)
                };
                return listas;
            }

            return null;
        }
    }
}