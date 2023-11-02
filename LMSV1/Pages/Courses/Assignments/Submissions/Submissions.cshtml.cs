﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LMSV1.Data;
using LMSV1.Models;

namespace LMSV1.Pages.Courses.Assignments.Submissions
{
    public class IndexModel : PageModel
    {
        private readonly LMSV1.Data.LMSV1Context _context;

        public IndexModel(LMSV1.Data.LMSV1Context context)
        {
            _context = context;
        }

        public IList<Submission> Submission { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? cId)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_context.Submissions != null)
            {
                Submission = await _context.Submissions
                .Include(s => s.Assignment)
                .Include(s => s.User)
                .Where(s => s.AssignmentID == id).ToListAsync();

            }
            ViewData["assId"] = id;
            // Submission.First().Assignment.CourseID breaks if _context.Submissions is null,
            // had to switch to using CourseId parameter passed in from previous page
            ViewData["cId"] = cId;
            return Page();
        }
    }
}
