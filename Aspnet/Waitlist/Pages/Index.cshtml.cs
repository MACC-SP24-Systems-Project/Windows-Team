using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace.Pages
{
    public class IndexModel : PageModel
    {
        // Properties for form fields
        [BindProperty]
        public string ClassType { get; set; }

        [BindProperty]
        public string CourseNumber { get; set; }

        [BindProperty]
        public string SectionNumber { get; set; }

        [BindProperty]
        public string Term { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public DateTime SubmitTime { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SubmitTime = DateTime.Now;

            // Prepare the data to be written to the CSV file
            var csvData = $"{ClassType},{CourseNumber},{SectionNumber},{Term},{FirstName},{LastName},{Email},{SubmitTime}{Environment.NewLine}";

            // Determine the path to the CSV file
            var filePath = Path.Combine("csv", "Results.csv");

            // Write the data to the CSV file
            await WriteToCsvAsync(filePath, csvData);

            // Redirect back to the index page
            return RedirectToPage();
        }

        private async Task WriteToCsvAsync(string filePath, string csvData)
        {
            // Ensure the directory exists
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Write data to the CSV file
            await using (var streamWriter = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                await streamWriter.WriteAsync(csvData);
            }
        }
    }
}
