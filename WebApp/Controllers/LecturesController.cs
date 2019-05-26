using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFCSystem.Models;
using NFCSystem.Models.Timetables;
using NFCSystem.Data;

namespace NFCSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/lectures/5
        [HttpGet("{id}")]
        public ActionResult<List<Lecture>> Get(string id)
        {
            var timetable = _context.Timetables.Where(x => x.StudentId == id).ToList();

            if (timetable == null)
            {
                return NotFound();
            }

            List<Lecture> lectures = new List<Lecture>();

            foreach (var lecture in timetable)
            {
                lectures.Add(new Lecture()
                {
                    StudentId = id,
                    Title = lecture.CourseId + " " + _context.Courses.FirstOrDefault(x => x.CourseId == lecture.CourseId).CourseName,
                    Details = _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodStartTime + " - " + 
                        _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodEndTime + "<br>" + 
                        lecture.LectureType + "<br>" + _context.Classrooms.FirstOrDefault(x => x.ClassroomId == lecture.ClassroomId).ClassLocation + " r.-" + 
                        _context.Classrooms.FirstOrDefault(x => x.ClassroomId == lecture.ClassroomId).ClassLabel,
                    Start = Convert.ToDateTime(lecture.Date.ToString("yyyy-MM-dd") + "T" + _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodStartTime),
                    Finish = Convert.ToDateTime(lecture.Date.ToString("yyyy-MM-dd") + "T" + _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodEndTime),
                    IsVisited = lecture.isVisited, 
                });
            }
            return lectures.OrderBy(l => l.Start).ToList();
        }


        // GET api/lectures/getclassrooms
        // Gets all classroms available currently
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Classroom>>> GetClassrooms()
        {
            var classrooms = await _context.Classrooms.ToListAsync();
            return classrooms;
        }
    }
}
