using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }
        public string CurrentCover { get; set; }
        [AllowedExtensions(FileSettings.AllowedExtensions)]  //<= 'AllowedExtensions'  is a custom attribute (Build Myself) : "FileSettings" is a class contains some static fields
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
