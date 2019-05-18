using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using Vinarish.Data;
using Vinarish.Models;

namespace Vinarish.Pages.StatCodes
{
    public class UploadDataModel : PageModel
    {
        private readonly Vinarish.Data.TCMMSContext _context;

        public UploadDataModel(Vinarish.Data.TCMMSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CatId"] = new SelectList(_context.Category, "Id", "CategoryName");
            return Page();
        }

        [BindProperty]
        public StatCode StatCode { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectToAction("Index");
            }
            List<StatCode> StatCodes = new List<StatCode>();

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Tip: To access the first worksheet, try index 1, not 0
                    int totalRows = worksheet.Dimension.Rows;


                    for (int i = 0; i < totalRows; i++)
                    {
                        StatCodes.Add(new StatCode
                        {
                            Code = ((object[,])(worksheet.Cells.Value))[i, 0].ToString(),
                            Text = ((object[,])(worksheet.Cells.Value))[i, 1].ToString(),
                            CatId = StatCode.CatId
                        });
                    }
                }
            }

            _context.StatCode.AddRange(StatCodes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}