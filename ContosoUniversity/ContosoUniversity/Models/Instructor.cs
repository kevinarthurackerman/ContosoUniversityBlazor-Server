using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DelegateDecompiler;
using ContosoUniversity.Pages.Instructors;

namespace ContosoUniversity.Models
{
    public class Instructor : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Computed]
        public string FullName => LastName + ", " + FirstMidName;

        public ICollection<CourseAssignment> CourseAssignments { get; private set; } = new List<CourseAssignment>();
        public OfficeAssignment OfficeAssignment { get; private set; }

        public void Handle(CreateEdit.Command message)
        {
            UpdateDetails(message);

            UpdateInstructorCourses(message.Courses);
        }

        public void Handle(Delete.Command message) => OfficeAssignment = null;

        private void UpdateDetails(CreateEdit.Command message)
        {
            FirstMidName = message.FirstMidName;
            LastName = message.LastName;
            HireDate = message.HireDate.GetValueOrDefault();

            if (string.IsNullOrWhiteSpace(message.OfficeAssignmentLocation))
            {
                OfficeAssignment = null;
            }
            else if (OfficeAssignment == null)
            {
                OfficeAssignment = new OfficeAssignment
                {
                    Location = message.OfficeAssignmentLocation
                };
            }
            else
            {
                OfficeAssignment.Location = message.OfficeAssignmentLocation;
            }
        }

        private void UpdateInstructorCourses(IEnumerable<CreateEdit.Command.CourseData> courses)
        {
            var assignedCourseIds = courses
                .Where(x => x.Assigned).Select(x => x.Id)
                .ToArray();

            var removedCourses = CourseAssignments
                .Where(x => !assignedCourseIds.Contains(x.CourseId))
                .ToArray();

            var previouslyAssignedCourseIds = CourseAssignments
                .Select(x => x.CourseId)
                .ToArray();

            var addedCourses = assignedCourseIds
                .Where(x => !previouslyAssignedCourseIds.Contains(x))
                .Select(x => new CourseAssignment{ CourseId = x, InstructorId = Id })
                .ToArray();

            foreach (var removedCourse in removedCourses) CourseAssignments.Remove(removedCourse);

            foreach(var addedCourse in addedCourses) CourseAssignments.Add(addedCourse);
        }
    }
}