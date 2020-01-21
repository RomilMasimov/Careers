using System.ComponentModel.DataAnnotations;

namespace Careers.Areas.SpecialistArea.ViewModels
{
    public class EditAboutViewModel
    {
        [MaxLength(500)]
        public string Text { get; set; }
    }
}
