namespace QAForum.Models.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ForumViewModel
    {
        public int ForumId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        [DisplayName("Title")]
        public string ForumTitle { get; set; }
    }
}