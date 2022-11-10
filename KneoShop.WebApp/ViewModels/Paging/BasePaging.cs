namespace KenoShop.WebApp.ViewModels.Paging
{
    public class BasePaging<TEntity>
    {
        public BasePaging()
        {
            Page = 1;
            HowManyPagesShowAfterBefore = 5;
            TakeEntity = 10;
        }


        public int Page { get; set; }

        public int AllEntitiesCount { get; set; }

        public int PagesCount { get; set; }

        public int TakeEntity { get; set; }

        public int SkipEntity { get; set; }

        public int HowManyPagesShowAfterBefore { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public List<TEntity> Entities { get; set; }

        public void SetPaging(IQueryable<TEntity> queryable)
        {
            // 1 - calculate paging properties
            AllEntitiesCount = queryable.Count();

            PagesCount = (int)Math.Ceiling(AllEntitiesCount / (double)TakeEntity);

            SkipEntity = (Page - 1) * TakeEntity;

            StartPage = Page - HowManyPagesShowAfterBefore < 1 ? 1 : Page - HowManyPagesShowAfterBefore;

            EndPage = Page + HowManyPagesShowAfterBefore > PagesCount ? PagesCount : Page + HowManyPagesShowAfterBefore;

            // 2 - execute paging query
            Entities = queryable.Skip(SkipEntity).Take(TakeEntity).ToList();
        }
    }
}
