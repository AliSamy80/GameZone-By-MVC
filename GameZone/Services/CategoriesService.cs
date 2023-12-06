namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly Data.AppContext context;

        public CategoriesService(Data.AppContext _context)
        {
            context = _context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
