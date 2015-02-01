namespace BetterLife.WebUi.ControllersLogic.BookController
{
    public interface IFavoriteRepository
    {
        bool AddToFavorite(int bookId, string login);
    }
}