﻿using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IStatisticService
    {
        Task<GradesStatsDto> GetStudentGradesStatsAsync(int studentId);
        Task<GradesStatsDto> GetGroupGradesStatsAsync(int groupId);
        Task<GradesStatsDto> GetGroupGradesStatsAsync(int groupId, int subjectId);
    }
}