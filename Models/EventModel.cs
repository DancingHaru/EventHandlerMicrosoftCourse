using System.ComponentModel.DataAnnotations;

namespace EventEase
{
    public class EventModel
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Date field is required.")]
        [DataType(DataType.Date, ErrorMessage = "The Date field is not a valid date.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The Location field is required.")]
        public string Location { get; set; } = string.Empty;

        public int Attendance {get; set;}
    }
}