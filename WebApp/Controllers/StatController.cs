using System;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFCSystem.Models;
using NFCSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NFCSystem.Models.Timetables;

namespace NFCSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/lectures/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<StudentStatDTO>> GetStudentStatsTotal(string id)
        {
            StudentStatDTO stat = new StudentStatDTO();
            DateTime currentDate = DateTime.Now.Date;
            //Get all timetables till yesterday
            var timetable = await _context.Timetables.Where(
                x => x.StudentId == id
                && DateTime.Compare(x.Date.Date, currentDate) < 0)
                .ToListAsync();

            if (timetable == null)
            {
                return NotFound();
            }

            var onlyTheory = timetable.Where(x => x.LectureType == Timetable.Types.Teorija).ToList();
            if (onlyTheory.Count != 0)
            {
                var visitedTheory = onlyTheory.Where(x => x.isVisited).ToList();
                stat.TotalTheory = onlyTheory.Count;
                stat.VisitedTheory = visitedTheory.Count;
                stat.AttendanceTheory = (int)((float)stat.VisitedTheory / stat.TotalTheory * 100);
            }
            else
            {
                stat.TotalTheory = 0;
                stat.VisitedTheory = 0;
                stat.AttendanceTheory = 100;
            }

            var onlyPractice = timetable.Where(x => x.LectureType == Timetable.Types.Pratybos).ToList();
            if (onlyPractice.Count != 0)
            {
                var visitedPractice = onlyPractice.Where(x => x.isVisited).ToList();
                stat.TotalPractice = onlyPractice.Count;
                stat.VisitedPractice = visitedPractice.Count;
                stat.AttendancePractice = (int)((float)stat.VisitedPractice / stat.TotalPractice * 100);
            }
            else
            {
                stat.TotalPractice = 0;
                stat.VisitedPractice = 0;
                stat.AttendancePractice = 100;
            }

            var onlyLab = timetable.Where(x => x.LectureType == Timetable.Types.Laborai).ToList();
            if (onlyLab.Count != 0)
            {
                var visitedLab = onlyLab.Where(x => x.isVisited).ToList();
                stat.TotalLab = onlyLab.Count;
                stat.VisitedLab = visitedLab.Count;
                stat.AttendanceLab = (int)((float)stat.VisitedLab / stat.TotalLab * 100);
            }
            else
            {
                stat.TotalLab = 0;
                stat.VisitedLab = 0;
                stat.AttendanceLab = 100;
            }



            List<StudentStatDTO> stats = new List<StudentStatDTO>();


            // foreach (var lecture in timetable)
            // {
            //     lectures.Add(new Lecture()
            //     {
            //         StudentId = id,
            //         Title = lecture.CourseId + " " + _context.Courses.FirstOrDefault(x => x.CourseId == lecture.CourseId).CourseName,
            //         Details = _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodStartTime + " - " + 
            //             _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodEndTime + "<br>" + 
            //             lecture.LectureType + "<br>" + _context.Classrooms.FirstOrDefault(x => x.ClassroomId == lecture.ClassroomId).ClassLocation + " r.-" + 
            //             _context.Classrooms.FirstOrDefault(x => x.ClassroomId == lecture.ClassroomId).ClassLabel,
            //         Start = Convert.ToDateTime(lecture.Date.ToString("yyyy-MM-dd") + "T" + _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodStartTime),
            //         Finish = Convert.ToDateTime(lecture.Date.ToString("yyyy-MM-dd") + "T" + _context.Periods.FirstOrDefault(x => x.PeriodId == lecture.PeriodId).PeriodEndTime),
            //         IsVisited = lecture.isVisited, 
            //     });
            // }
            return Ok(stat);
        }
    }
}
