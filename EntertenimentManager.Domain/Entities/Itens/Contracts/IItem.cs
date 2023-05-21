using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens.Contracts
{
    public interface IItem
    {
        public string Title { get; }
        public string Genre { get; }
        public string UrlImage { get; }
        public int ReleaseYear { get; }
        public PersonalList BelongsTo { get; }


        public void Update()
        {
        }
    }
}
