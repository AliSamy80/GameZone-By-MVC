﻿namespace GameZone.Attributes
{
    public class AllowedExtensions : ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtensions(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid
            (object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if(!isAllowed)
                {
                    return new ValidationResult($"Only {_allowedExtensions} Are Allowed!");
                }
            }
            return ValidationResult.Success;
        }

    }
}
