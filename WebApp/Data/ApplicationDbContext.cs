#define NEW_DB

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql;
using NFCSystem.Models;
using NFCSystem.Models.Timetables;
using Microsoft.Extensions.Identity.Core;


namespace NFCSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<NFCScan> NFCScans { get; set; }
        public DbSet<Device> Devices {get;set;}
        public DbSet<Period> Periods {get;set;}
        public DbSet<Course> Courses {get;set;}
        public DbSet<Classroom> Classrooms {get;set;}
        public DbSet<Timetable>  Timetables {get;set;}
        public DbSet<IdentityUserRole<string>> IdentityUserRoles {get;set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuration for Periods
            builder.Entity<Period>().HasKey(k => k.PeriodId);
            #if NEW_DB
            builder.Entity<Period>().HasData(new Period{PeriodId=1, PeriodStartTime=new TimeSpan(9,0,0), PeriodEndTime=new TimeSpan(10,30,0)});
            builder.Entity<Period>().HasData(new Period{PeriodId=2, PeriodStartTime=new TimeSpan(11,0,0), PeriodEndTime=new TimeSpan(12,30,0)});
            builder.Entity<Period>().HasData(new Period{PeriodId=3, PeriodStartTime=new TimeSpan(13,30,0), PeriodEndTime=new TimeSpan(15,00,0)});
            builder.Entity<Period>().HasData(new Period{PeriodId=4, PeriodStartTime=new TimeSpan(15,30,0), PeriodEndTime=new TimeSpan(17,00,0)});
            builder.Entity<Period>().HasData(new Period{PeriodId=5, PeriodStartTime=new TimeSpan(17,30,0), PeriodEndTime=new TimeSpan(19,00,0)});
            builder.Entity<Period>().HasData(new Period{PeriodId=6, PeriodStartTime=new TimeSpan(19,30,0), PeriodEndTime=new TimeSpan(21,00,0)});
            builder.Entity<Period>().HasData(new Period{PeriodId=7, PeriodStartTime=new TimeSpan(7,30,0), PeriodEndTime=new TimeSpan(8,59,0)});
            #endif

            // Configuration for Courses
            builder.Entity<Course>().HasKey(k => k.CourseId);
            #if NEW_DB
            builder.Entity<Course>().HasData(new Course{CourseId="PB0000",CourseName="Algoritmų analizė ir sudarymas"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0001",CourseName="Duomenų bazės"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0002",CourseName="Semestro projektas"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0003",CourseName="Programų sistemų inžinerija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0004",CourseName="Programavimo kalbų teorija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0005",CourseName="Anglų kalba"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0006",CourseName="Operacinės sistemos"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0007",CourseName="Aeordinamikos pagrindai"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0008",CourseName="Akademinis rašymas"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0009",CourseName="Anglų kalba B1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0010",CourseName="Anglų kalba B2"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0011",CourseName="Anglų kalba C1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0012",CourseName="Ispanų kalba B1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0013",CourseName="Ispanų kalba B2"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0014",CourseName="Ispanų kalba C1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0015",CourseName="Prancūzų kalba B1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0016",CourseName="Prancūzų kalba B2"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0017",CourseName="Prancūzų kalba C1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0018",CourseName="Vokiečių kalba B1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0019",CourseName="Rusų kalba B2"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0020",CourseName="Architektūros istorija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0021",CourseName="Architektūros filosofija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0022",CourseName="Architektūros teorija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0023",CourseName="CAD/CAE sistemos"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0024",CourseName="Biudžetų sudarymas"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0025",CourseName="Biopolimerai"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0026",CourseName="Biochemija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0027",CourseName="Aviaciniai varikliai"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0028",CourseName="Bakalauro baigiamasis projektas"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0029",CourseName="Baigiamoji praktika"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0030",CourseName="Bioinformatika"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0031",CourseName="Elektroninis verslas"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0032",CourseName="Elektros energetika"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0033",CourseName="Elektro grandinių analizė 1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0034",CourseName="Fizika 1"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0035",CourseName="Genų inžinerija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0036",CourseName="Grafų teorija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0037",CourseName="Gydomoji kosmetika"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0038",CourseName="Intelektikos pagrindai"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0039",CourseName="Inžinerinė grafika"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0040",CourseName="Fonologija"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0041",CourseName="Kompiuterinė grafika"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0042",CourseName="Karjeros kūrimas"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0043",CourseName="Komunikavimo pagrindai"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0044",CourseName="Kultūros paveldo apsauga"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0045",CourseName="Lyderystės pagrindai"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0046",CourseName="Mados raida"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0047",CourseName="Matematika 3"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0048",CourseName="Mechaniniai virpesiai"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0049",CourseName="Mechatronika"});
            builder.Entity<Course>().HasData(new Course{CourseId="PB0050",CourseName="Muzikos estetika"});
            #endif

            //Configuration for Classrooms
            builder.Entity<Classroom>().HasKey(k => k.ClassroomId);
            builder.Entity<Classroom>().HasMany<Device>(g => g.Devices).WithOne(s => s.Classroom).HasForeignKey(s => s.ClassroomId);
            #if NEW_DB
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=9999, ClassLabel="DummmyClassroom",ClassLocation="Moon, in a far galaxy away"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=1, ClassLabel="101",ClassLocation="XI"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=2, ClassLabel="102",ClassLocation="XI"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=3, ClassLabel="103",ClassLocation="XI"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=4, ClassLabel="518",ClassLocation="XI"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=5, ClassLabel="215",ClassLocation="XI"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=6, ClassLabel="150",ClassLocation="XII"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=7, ClassLabel="151",ClassLocation="XII"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=8, ClassLabel="501",ClassLocation="XI"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=9, ClassLabel="152",ClassLocation="XII"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=10, ClassLabel="153",ClassLocation="XII"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=11, ClassLabel="103AB",ClassLocation="IX"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=12, ClassLabel="103CD",ClassLocation="IX"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=13, ClassLabel="103KL",ClassLocation="IX"});
            builder.Entity<Classroom>().HasData(new Classroom{ClassroomId=14, ClassLabel="108",ClassLocation="B2"});
            #endif

            //Configuration for Device
            builder.Entity<Device>().HasKey(k => k.DeviceId);
            builder.Entity<Device>().Property(p => p.DeviceId).ValueGeneratedOnAdd();
            builder.Entity<Device>().Property(p => p.ClassroomId).HasDefaultValue(9999);
            //builder.Entity<Device>().HasOne<Classroom>(p => p.Classroom).WithMany(d => d.Devices).HasForeignKey<>(s => s.ClassroomId).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Device>().HasData(new Device{DeviceId=1,DeviceIdReal=1, PendingDeviceId=1, ClassroomId=3});
            builder.Entity<Device>().HasData(new Device{DeviceId=2,DeviceIdReal=2, PendingDeviceId=2, ClassroomId=5});
            builder.Entity<Device>().HasData(new Device{DeviceId=3,DeviceIdReal=3, PendingDeviceId=3, ClassroomId=13});
            builder.Entity<Device>().HasData(new Device{DeviceId=4,DeviceIdReal=4, PendingDeviceId=4, ClassroomId=9999});
            builder.Entity<Device>().HasData(new Device{DeviceId=5,DeviceIdReal=5, PendingDeviceId=5, ClassroomId=9999});
            builder.Entity<Device>().HasData(new Device{DeviceId=6,DeviceIdReal=6, PendingDeviceId=6, ClassroomId=9999});
            builder.Entity<Device>().HasData(new Device{DeviceId=7,DeviceIdReal=7, PendingDeviceId=7, ClassroomId=4});

            // Configuration for NFCScans
            builder.Entity<NFCScan>().HasKey(k => k.ScanId);
            builder.Entity<NFCScan>().Property(p => p.ScanId).ValueGeneratedOnAdd();

            // Configuration for Timetables
            builder.Entity<Timetable>().HasKey(k => k.TimetableId);
            builder.Entity<Timetable>().Property(p => p.isVisited);
            builder.Entity<Timetable>().HasOne<Course>(c => c.Course).WithMany(t => t.Timetables).HasForeignKey(k => k.CourseId);
            builder.Entity<Timetable>().HasOne<Period>(p => p.Period).WithMany(t => t.Timetables).HasForeignKey(k => k.PeriodId);
            builder.Entity<Timetable>().HasOne<Classroom>(c => c.Classroom).WithMany(t => t.Timetables).HasForeignKey(k => k.ClassroomId);
            builder.Entity<Timetable>().HasOne<ApplicationUser>(s => s.ApplicationUser).WithMany(t => t.Timetables).HasForeignKey(k => k.StudentId);
            builder.Entity<Timetable>().Property(p => p.LectureType).HasConversion<string>();
            builder.Entity<Timetable>().Property(p => p.Date).HasColumnType("Date");
            builder.Entity<Timetable>( e => 
            {
                // e.HasData(new Timetable{TimetableId=1, CourseId="PB0000", PeriodId=1, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=9999, Date=new DateTime(2019,04,30), isVisited=false});
                // e.HasData(new Timetable{TimetableId=2, CourseId="PB0001", PeriodId=2, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=3, Date=new DateTime(2019,04,30), isVisited=false});
                // e.HasData(new Timetable{TimetableId=3, CourseId="PB0002", PeriodId=3, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=5, Date=new DateTime(2019,04,30), isVisited=false});
                // e.HasData(new Timetable{TimetableId=4, CourseId="PB0002", PeriodId=4, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=3, Date=new DateTime(2019,04,30), isVisited=false});
                // e.HasData(new Timetable{TimetableId=5, CourseId="PB0001", PeriodId=5, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=3, Date=new DateTime(2019,04,30), isVisited=false});
                // e.HasData(new Timetable{TimetableId=6, CourseId="PB0005", PeriodId=1, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=5, Date=new DateTime(2019,04,26), isVisited=false});
                // e.HasData(new Timetable{TimetableId=7, CourseId="PB0004", PeriodId=5, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=4, Date=new DateTime(2019,04,28), isVisited=false});
                // e.HasData(new Timetable{TimetableId=8, CourseId="PB0005", PeriodId=5, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=13, Date=new DateTime(2019,04,27), isVisited=false});
                // e.HasData(new Timetable{TimetableId=9, CourseId="PB0001", PeriodId=5, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440001", ClassroomId=4, Date=new DateTime(2019,04,25), isVisited=false});
                // e.HasData(new Timetable{TimetableId=10, CourseId="PB0001", PeriodId=5, 
                //     StudentId="a18be9c0-aa65-4af8-bd17-00bd93440002", ClassroomId=5, Date=new DateTime(2019,04,29), isVisited=false});
            });

            // Configuration for Identity framework
            builder.Entity<ApplicationUser>(entity => {
                entity.Property(m => m.Email).HasMaxLength(127);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
                entity.Property(m => m.UserName).HasMaxLength(127);
            });

            // Configure Roles and its fields
            builder.Entity<IdentityRole>(entity => {
                entity.Property(m => m.Name).HasMaxLength(127);
                entity.Property(m => m.NormalizedName).HasMaxLength(127);
            });
            #if NEW_DB
            builder.Entity<IdentityRole>().HasData(new IdentityRole{Id="STUDENT", Name="STUDENT",NormalizedName="STUDENT"});
            builder.Entity<IdentityRole>().HasData(new IdentityRole{Id="ADMIN", Name="ADMIN",NormalizedName="ADMIN"});
            builder.Entity<IdentityRole>().HasData(new IdentityRole{Id="LECTURER", Name="LECTURER",NormalizedName="LECTURER"});
            #endif

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.ProviderKey).HasMaxLength(127);
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.RoleId).HasMaxLength(127);
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.Name).HasMaxLength(127);

            });

            // Create seed users
            var rnd = new Random();
            var hasher = new PasswordHasher<ApplicationUser>();

            for(int i = 1; i <= 100; i++)
            {
                string username = "varpav" + i.ToString("D3");
                string name = "Vardas" + i.ToString("D3");
                string surname = "Pavarde" + i.ToString("D3");; 
                string email = name + "." + surname + i.ToString("D3") + "@email.com";
                int uid = 100000000 + rnd.Next(1,99999999);
                string group = string.Format("IFF-{0}/{1}", rnd.Next(1,10), rnd.Next(1,10));
                string userId = "a18be9c0-aa65-4af8-bd17-00bd9344" + i.ToString("D4"); // Need 4 more chars

                builder.Entity<ApplicationUser>().HasData(new ApplicationUser
                {
                    Id = userId,
                    UserName = username,
                    NormalizedUserName = username.ToUpper(),
                    Name = name,
                    Surname = surname,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UID = uid,
                    Group = group,
                    StudentCode = "C" + i.ToString("D5"),
                    PasswordHash = hasher.HashPassword(null, "Admin123++"),
                });

                builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                {
                    RoleId = "STUDENT",
                    UserId = userId
                });
            }
            // Create time tables
            // time table id
            int ttid = 11;
            for(int i = 1; i <= 100; i++)
            {
                //student id
                string id = "";
                if(i < 100 && i >= 10)
                    id = string.Concat("0", i);
                else if(i < 10)
                    id = string.Concat("00", i);
                else 
                    id = i.ToString();
                string studId = string.Concat("a18be9c0-aa65-4af8-bd17-00bd93440", id);
                List<Tuple<DateTime, int>> takenDates = new List<Tuple<DateTime, int>>();
                //generate 5 unique lectures for a student
                for (int j = 0; j < 5; j++)
                {
                    //each lecture is gonna have 1-3 lecture types at random
                    int lectureTypeC = rnd.Next(1, 4);
                    string corseID = "";
                    int rndCourseId = rnd.Next(0, 51);
                    if (rndCourseId < 10)
                        corseID = string.Concat("PB000", rndCourseId.ToString());
                    if (rndCourseId >= 10)
                        corseID = string.Concat("PB00", rndCourseId.ToString());
                    int classID = rnd.Next(1, 15);
                    for (int h = 0; h < lectureTypeC; h++ )
                    {
                        int periodID = rnd.Next(1, 8);
                        DateTime periodDate = new DateTime(2019, 3, rnd.Next(4, 9));
                        Tuple<DateTime, int> datetime = new Tuple<DateTime, int>(periodDate, periodID);
                        //randomly have lectures once every 1 or 2 weeks
                        int increment = rnd.Next(1, 3);
                        //add lectures from march to june
                        for (int k = 0; k < 16; k += increment)
                        {
                            //if there weren't any lectures generated for this date and period
                            if(!takenDates.Contains(datetime))
                            {
                                builder.Entity<Timetable>( e => 
                                {
                                    e.HasData(new Timetable{TimetableId=ttid++, CourseId=corseID, PeriodId=periodID, 
                                    StudentId=studId, ClassroomId=classID, Date=periodDate, 
                                    isVisited=rnd.NextDouble() > 0.5, LectureType=(Timetable.Types)h});
                                });
                                takenDates.Add(datetime);
                            }
                            periodDate = periodDate.AddDays(7 * increment);
                        }
                    }
                    for (int h = 0; h < lectureTypeC; h++ )
                    {
                        int periodID = rnd.Next(1, 8);
                        DateTime periodDate = new DateTime(2019, 4, rnd.Next(1, 6));
                        Tuple<DateTime, int> datetime = new Tuple<DateTime, int>(periodDate, periodID);
                        //randomly have lectures once every 1 or 2 weeks
                        int increment = rnd.Next(1, 3);
                        //add lectures from march to june
                        for (int k = 0; k < 16; k += increment)
                        {
                            //if there weren't any lectures generated for this date and period
                            if(!takenDates.Contains(datetime))
                            {
                                builder.Entity<Timetable>( e => 
                                {
                                    e.HasData(new Timetable{TimetableId=ttid++, CourseId=corseID, PeriodId=periodID, 
                                    StudentId=studId, ClassroomId=classID, Date=periodDate, 
                                    isVisited=rnd.NextDouble() > 0.5, LectureType=(Timetable.Types)h});
                                });
                                takenDates.Add(datetime);
                            }
                            periodDate = periodDate.AddDays(7 * increment);
                        }
                    }
                    for (int h = 0; h < lectureTypeC; h++ )
                    {
                        int periodID = rnd.Next(1, 8);
                        DateTime periodDate = new DateTime(2019, 5, rnd.Next(6, 11));
                        Tuple<DateTime, int> datetime = new Tuple<DateTime, int>(periodDate, periodID);
                        //randomly have lectures once every 1 or 2 weeks
                        int increment = rnd.Next(1, 3);
                        //add lectures from march to june
                        for (int k = 0; k < 16; k += increment)
                        {
                            //if there weren't any lectures generated for this date and period
                            if(!takenDates.Contains(datetime))
                            {
                                builder.Entity<Timetable>( e => 
                                {
                                    e.HasData(new Timetable{TimetableId=ttid++, CourseId=corseID, PeriodId=periodID, 
                                    StudentId=studId, ClassroomId=classID, Date=periodDate, 
                                    isVisited=rnd.NextDouble() > 0.5, LectureType=(Timetable.Types)h});
                                });
                                takenDates.Add(datetime);
                            }
                            periodDate = periodDate.AddDays(7 * increment);
                        }
                    }
                }
            }

            base.OnModelCreating(builder);
        }
    }
}

