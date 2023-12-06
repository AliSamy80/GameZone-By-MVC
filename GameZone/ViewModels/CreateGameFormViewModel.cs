using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        [Required]
        [AllowedExtensions(FileSettings.AllowedExtensions)]  //<= 'AllowedExtensions'  is a custom attribute (Build Myself) : "FileSettings" is a class contains some static fields
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
