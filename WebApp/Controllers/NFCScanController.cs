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

namespace NFCSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NFCScanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NFCScanController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Todo
        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NFCScan>>> GetScans()
        {
            return await _context.NFCScans.ToListAsync();
        }


        // GET: api/Todo/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<NFCScan>> GetScan(long id)
        {
            var todoItem = await _context.NFCScans.FindAsync(id);
            //var student = await _context.Users.FirstOrDefaultAsync(x => x.UID == todoItem.UID);
            var restult = new {scan = todoItem/* , stud = student*/};
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(restult);
        }


        // POST: api/nfcscan/postscan
        // Posts scan to server database
        [HttpPost("[action]")]
        public async Task<ActionResult<NFCScan>> PostScan(NFCScan item)
        {   
            // Find related timetable element by deviceId, UID and time
            // Find classroom
            var classRoom = await _context.Devices.Where(d => d.DeviceId == item.DeviceId).FirstOrDefaultAsync();
            // Find student
            var stud = await _context.Users.Where(u => u.UID == item.UID).FirstOrDefaultAsync();
            // Find period
            var period = await _context.Periods
                .Where(
                    p => TimeSpan.Compare(p.PeriodStartTime, item.TimeStamp.TimeOfDay) == -1 &&
                    TimeSpan.Compare(p.PeriodEndTime, item.TimeStamp.TimeOfDay) == 1)
                .FirstOrDefaultAsync();
            // Find exact timetable
            if(stud != null && period != null && classRoom != null)
            {
            var timetable = await _context.Timetables
                .Where(
                    t => t.ClassroomId == classRoom.ClassroomId &&
                    t.StudentId == stud.Id &&
                    t.Date.Date == item.TimeStamp.Date && 
                    t.PeriodId == period.PeriodId)
                .FirstOrDefaultAsync();
            // Mark visit
            timetable.isVisited = true;
            
            _context.Timetables.Update(timetable);
            }
            _context.NFCScans.Add(item);
            await _context.SaveChangesAsync();
            //System.Diagnostics.Debug.WriteLine(item.ToString());

            return CreatedAtAction(nameof(GetScan), new { id = item.ScanId }, item);
        }

        // GET: api/Todo
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        // GET: api/nfcscan/getdeviceid/x
        // Gets device using realdevice id
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Device>> GetDeviceId(int id)
        {
            var device = await _context.Devices.Where(d => d.DeviceIdReal == id).FirstOrDefaultAsync();
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }


        // POST: api/nfcscan/postscan
        // Posts scan to server database
        [HttpPost("[action]")]
        public async Task<ActionResult<Device>> PostDevice(Device item)
        {   
            _context.Devices.Update(item);
            await _context.SaveChangesAsync();
            //System.Diagnostics.Debug.WriteLine(item.ToString());

            return CreatedAtAction(nameof(GetScan), new { id = item.DeviceIdReal }, item);
        }     
    }
}