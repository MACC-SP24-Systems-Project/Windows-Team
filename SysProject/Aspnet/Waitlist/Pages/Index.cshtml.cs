using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Waitlist.EntityModels;

namespace Waitlist.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDatabaseService _databaseService;

        // properties for the index model
        #region Student Data
        public string StudentId { get; set; }
        [Required]
        [BindProperty(SupportsGet = false)]
        public string FirstName { get; set; }

        [BindProperty(SupportsGet = false)]
        public string LastName { get; set; }

        [BindProperty(SupportsGet = false)]
        public string Email { get; set; }

        #endregion

        #region Course Data

        public string CourseCode { get; set; }
        public string Term { get; set; }
        public string CourseTitle { get; set; }
        public string Instructor { get; set; }
        public string Location { get; set; }
        public decimal Credits { get; set; }
        public int PageSize { get; set; } = 15;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public List<CourseDatum> CourseData { get; set; }

        #endregion


        // constructor for the index model
        public IndexModel(ILogger<IndexModel> logger, IDatabaseService databaseService)
        {
            _logger = logger;
            // added for database context
            _databaseService = databaseService;
        }

        // method to be run when the submit button is clicked
        public async Task<IActionResult> OnPostAsync()
        {
            // check if the model state is valid
            if (!ModelState.IsValid)
            {
                // if not, return the page
                return Page();
            }

            // code to be run after the submit button is clicked
            var result = await _databaseService.AddStudentData(FirstName, LastName, Email);
            if (!result)
            {
                throw new ApplicationException("Error adding student data");
            }

            // return to the index page/redirect to another page
            return RedirectToPage("./Index");
        }

        public void OnGet()
        {
            CourseData = _databaseService.GetCourseData();


        }
    }
}
