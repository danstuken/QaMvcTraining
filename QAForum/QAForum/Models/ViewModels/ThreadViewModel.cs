namespace QAForum.Models.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ThreadViewModel
    {
        public int ThreadId { get; set; }

        [Required(ErrorMessage = "Forum is required")]
        [DisplayName("Forum")]
        public int ForumId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        [DisplayName("Title")]
        public string ThreadTitle { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        [DisplayName("Owner")]
        public Guid OwnerId { get; set; }
    }
}