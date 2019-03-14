using System.ComponentModel.DataAnnotations;

namespace MediaShare.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}