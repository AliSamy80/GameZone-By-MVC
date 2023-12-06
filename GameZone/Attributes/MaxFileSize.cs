namespace GameZone.Attributes
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid
            (object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
               
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Maximum Allowed Size is {_maxFileSize} bytes");
                }
            }
            return ValidationResult.Success;
        }
    }
}
