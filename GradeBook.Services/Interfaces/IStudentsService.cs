﻿using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<StudentDto> GetStudentAsync(int id);
        Task<int> CreateStudentAsync(StudentDto student);
        Task UpdateStudentAsync(StudentDto student);
        Task DeleteStudentAsync(int studentId);
    }
}