namespace GameZone.Services
{
    public interface IDeviceService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
