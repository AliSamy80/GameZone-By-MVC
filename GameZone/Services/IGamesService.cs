namespace GameZone.Services
{
    public interface IGamesService
    {
        IEnumerable<Game> GetAllGames();
        Game? GetById(int id);
        Task Create(CreateGameFormViewModel model);
        Task<Game?> Update(EditGameFormViewModel model);
        bool Delete(int id);
    }
}
