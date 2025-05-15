using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Feedback
    {
        [Key]
        private int _feedbackID;

        [StringLength(50)]
        private string? _userID;

        [Required]
        [StringLength(200)]
        private string _content;

        [Required]
        private DateTime _createdAt = DateTime.Now;

        [Required]
        private bool _status = false;

        public Feedback(string content, string? userID = null)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
            _userID = userID;
        }

        public int FeedbackID
        {
            get => _feedbackID;
            set => _feedbackID = value;
        }

        public string? UserID
        {
            get => _userID;
            set => _userID = value;
        }

        public string Content
        {
            get => _content;
            set => _content = value ?? throw new ArgumentNullException(nameof(Content));
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        public bool Status
        {
            get => _status;
            set => _status = value;
        }

        [ForeignKey("UserID")]
        public virtual Customer? Customer { get; set; }

        public void MarkAsResolved()
        {
            _status = true;
        }
    }
}
