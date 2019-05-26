using System.Net.Security;
using System.Collections.Immutable;
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

        // GET api/stats/getstudentstattotal/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<StudentStatDTO>> GetStudentStatTotal(string id)
        {
            StudentStatDTO stat = new StudentStatDTO();
            DateTime currentDate = DateTime.Now.Date;
            //Get all timetables till yesterday
            var timetable = await _context.Timetables.Where(
                x => x.StudentId == id
                && DateTime.Compare(x.Date.Date, currentDate) < 0)
                .ToListAsync();

            if (timetable.Count == 0)
            {
                return NotFound();
            }
            
            stat = GetCourseStat(timetable);

            return Ok(stat);
        }

        // GET api/stats/getstudentstat/{id}
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<List<StudentStatDTO>>> GetStudentStat(string id)
        {
            List<StudentStatDTO> stats = new List<StudentStatDTO>();
            StudentStatDTO stat = new StudentStatDTO();

            DateTime currentDate = DateTime.Now.Date;
            
            // Get all timetables till yesterday
            var timetable = await _context.Timetables.Where(
                x => x.StudentId == id
                && DateTime.Compare(x.Date.Date, currentDate) < 0)
                .ToListAsync();

            if (timetable.Count == 0)
            {
                return NotFound();
            }

            var lectureIds = timetable.GroupBy(x => x.CourseId).Select(g => g.First()).Select(a => a.CourseId).ToList();

            for(int i = 0; i < lectureIds.Count; i++)
            {
                StudentStatDTO tmpStud  = new StudentStatDTO();
                var courseTimetable = timetable.Where(c => c.CourseId == lectureIds[i]).ToList();
                tmpStud = GetCourseStat(courseTimetable);
                tmpStud.LectureID = lectureIds[i];
                tmpStud.LectureName = await _context.Courses.Where(c => c.CourseId == lectureIds[i]).Select(c => c.CourseName).FirstOrDefaultAsync();
                stats.Add(tmpStud);
            }

            return Ok(stats);
        }

        // GET api/stats/getgroupstat/{id}
        [HttpGet("[action]/{group}")]
        public async Task<ActionResult<List<StudentStatDTO>>> GetGroupStat(string group)
        {
            List<StudentStatDTO> stats = new List<StudentStatDTO>();
            StudentStatDTO stat = new StudentStatDTO();
            group = group.Replace("_","/");

            DateTime currentDate = DateTime.Now.Date;
            // Get all studenss in gruop
            var students = await _context.Users.Where(s => s.Group == group).Select(w => w.Id).ToListAsync();
            
            
            // Get all timetables till yesterday
            var timetable = await _context.Timetables.Where(
                x => students.Contains(x.StudentId)
                && DateTime.Compare(x.Date.Date, currentDate) < 0)
                .ToListAsync();

            if (timetable.Count == 0)
            {
                return NotFound();
            }

            var lectureIds = timetable.GroupBy(x => x.CourseId).Select(g => g.First()).Select(a => a.CourseId).ToList();

            for(int i = 0; i < lectureIds.Count; i++)
            {
                StudentStatDTO tmpStud  = new StudentStatDTO();
                var courseTimetable = timetable.Where(c => c.CourseId == lectureIds[i]).ToList();
                tmpStud = GetCourseStat(courseTimetable);
                tmpStud.LectureID = lectureIds[i];
                tmpStud.LectureName = await _context.Courses.Where(c => c.CourseId == lectureIds[i]).Select(c => c.CourseName).FirstOrDefaultAsync();
                stats.Add(tmpStud);
            }

            return Ok(stats);
        }

        public StudentStatDTO GetCourseStat(List<Timetable> timetable)
        {
            StudentStatDTO stat = new StudentStatDTO();
            DateTime currentDate = DateTime.Now.Date;

            var onlyTheory = timetable.Where(x => x.LectureType == Timetable.Types.Teorija).ToList();
            if (onlyTheory.Count != 0)
            {
                var visitedTheory = onlyTheory.Where(x => x.isVisited).ToList();
                stat.TotalTheory = onlyTheory.Count;
                stat.VisitedTheory = visitedTheory.Count;
                stat.AttendanceTheory = Math.Round((double)stat.VisitedTheory / stat.TotalTheory * 100, 1);
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
                stat.AttendancePractice = Math.Round((double)stat.VisitedPractice / stat.TotalPractice * 100, 1);
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
                stat.AttendanceLab = Math.Round((double)stat.VisitedLab / stat.TotalLab * 100,1);
            }
            else
            {
                stat.TotalLab = 0;
                stat.VisitedLab = 0;
                stat.AttendanceLab = 100;
            }
            return stat;
        }
    }
}
