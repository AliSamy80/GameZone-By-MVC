using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly Data.AppContext context;

        public DeviceService(Data.AppContext _context)
        {
            context = _context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return   context.Devices
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                    .OrderBy(d => d.Text)
                    .AsNoTracking()
                    .ToList();
        }        
    }
}
