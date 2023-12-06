namespace GameZone.ViewModels
{
    public class GameFormViewModel
    {
        [Required, MaxLength(250)]
        public string Name { get; set; } = string.Empty;


        [Display(Name = "Category")]
        public int CategoryId { get; set; } //<= this is used for mapping
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required, Display(Name = "SupportedDevices")]
        public List<int> SelectedDevices { get; set; } = default!; // <= this is used for mapping
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required, MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
    }
}
