namespace QAForum.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PostViewModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "User is required")]
        [DisplayName("User")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Thread is required")]
        [DisplayName("Thread")]
        public int ThreadId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [DisplayName("Title")]
        [StringLength(100)]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Post date and time is required")]
        [DisplayName("Posted")]
        public DateTime PostDateTime { get; set; }

        [Required(ErrorMessage = "Post body is required")]
        [DisplayName("Post")]
        [AllowHtml]
        public string PostBody { get; set; }
    }
}