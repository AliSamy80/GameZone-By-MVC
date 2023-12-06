namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly Data.AppContext context;
        private readonly IWebHostEnvironment webHostEnvironment; // a place to save images on server
        private readonly string _imagesPath; // property to save the path of the image

        public GamesService(Data.AppContext _context , IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
            webHostEnvironment = _webHostEnvironment;
            _imagesPath = $"{webHostEnvironment.WebRootPath}/assets/Images/Games";
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
            return games;
        }

        public Game? GetById(int id)
        {
            return context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            //var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}"; // Holding Image (Name in Generated Id + Extension)
            //var path = Path.Combine(_imagesPath, coverName); // to combine (place where i save image , image)

            //using var stream = File.Create(path);
            //await model.Cover.CopyToAsync(stream);


            var coverName = await SaveCover(model.Cover);


            var game = new Game()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            context.Games.Add(game);
            context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);
                
            if (game == null)
            {
                return null;
            }
            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            var effectedRows = context.SaveChanges();   // savechanges() => it affected number of rows in database 
            if(effectedRows > 0)
            {
                if (hasNewCover) // to delete the old cover which i change it in edit mode
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, game.Cover);
                    File.Delete(cover);
                }

                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = context.Games.Find(id);
            if (game is null)
            {
                return isDeleted;
            }
            context.Remove(game);
            var effectedRows = context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile Cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}"; // Holding Image (Name in Generated Id + Extension)
            var path = Path.Combine(_imagesPath, coverName); // to combine (place where i save image , image)

            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return coverName;
        }
    }
}
