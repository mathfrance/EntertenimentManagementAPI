using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.SharedContext;
using System.Collections.Generic;

namespace EntertenimentManager.Domain.Entities.Itens
{
    public interface Item
    {
        public string Title { get; }
        public string Genre { get; }
        public string UrlImage { get; }
        public int ReleaseYear { get; }
        public PersonalList BelongsTo { get; }


        protected void Update(string title, string genre, int releaseYear, string urlImage, PersonalList belongsTo)
        {

        }
    }
}
