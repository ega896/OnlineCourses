using System;

namespace Courses.Emails.Views
{
    public class ConfirmAccountEmailViewModel
    {
        public string Token { get; set; }

        public string Username { get; set; }

        public Guid UserId { get; set; }
    }
}
