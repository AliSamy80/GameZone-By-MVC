namespace GameZone.Controllers
{
    public class GamesController : Controller
    {

        private readonly ICategoriesService categoriesService; // service (1)

        private readonly IDeviceService deviceService;         // Service (2)

        private readonly IGamesService gamesService;           // Service (3)

        // Constructor :
        public GamesController(ICategoriesService _categoriesService,
            IDeviceService _deviceService,
            IGamesService _gamesService) 
        {
            categoriesService = _categoriesService;
            deviceService = _deviceService;
            gamesService = _gamesService;
        }

        public IActionResult Index()
        {
            var games = gamesService.GetAllGames();
            return View(games);
        }

        // page to display Details :
        public IActionResult Details(int id)
        {
            var game = gamesService.GetById(id);
            if(game is null)
                return NotFound();

            return View(game);
        }


        // Page To Create Game And Display it :
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = categoriesService.GetSelectList(),
                Devices = deviceService.GetSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {

                model.Categories = categoriesService.GetSelectList();
                model.Devices = deviceService.GetSelectList();
                return View(model);
            }

            // save game to d.b
            // save cover to server 
            await gamesService.Create(model);
            return RedirectToAction(nameof(Index));  // nameOf() => is better than because if i need to change name of action (index) , it alert me ;
        }

        // Edit Games
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = gamesService.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel editGameFormViewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = categoriesService.GetSelectList(),
                Devices = deviceService.GetSelectList(),
                CurrentCover = game.Cover
            };
            return View(editGameFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {

                model.Categories = categoriesService.GetSelectList();
                model.Devices = deviceService.GetSelectList();
                return View(model);
            }
            var game = await gamesService.Update(model);
            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = gamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
