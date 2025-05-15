using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Computer
    {
        [Key]
        [StringLength(50)]
        private string _computerID;

        [StringLength(50)]
        private string? _userID;

        private DateTime? _startTime;
        private DateTime? _endTime;

        [Required]
        [StringLength(30)]
        private string _status;

        public Computer(string computerID, string status)
        {
            _computerID = computerID ?? throw new ArgumentNullException(nameof(computerID));
            _status = status ?? throw new ArgumentNullException(nameof(status));
        }

        public string ComputerID
        {
            get => _computerID;
            set => _computerID = value ?? throw new ArgumentNullException(nameof(ComputerID));
        }

        public string? UserID
        {
            get => _userID;
            set => _userID = value;
        }

        public DateTime? StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public DateTime? EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public string Status
        {
            get => _status;
            set => _status = value ?? throw new ArgumentNullException(nameof(Status));
        }

        [ForeignKey("UserID")]
        public virtual Users? User { get; set; }

        public void StartSession(string userID)
        {
            if (_status != "Available")
                throw new InvalidOperationException("Computer is not available.");
            _userID = userID;
            _startTime = DateTime.Now;
            _status = "InUse";
        }

        public void EndSession()
        {
            if (_status != "InUse")
                throw new InvalidOperationException("No active session.");
            _endTime = DateTime.Now;
            _userID = null;
            _status = "Available";
        }
    }

}
