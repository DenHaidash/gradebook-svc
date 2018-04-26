using System.Threading.Tasks;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        
        [HttpGet("api/students/{studentId:int}/grades/statistic")]
        public async Task<IActionResult> GetStudentGradesStats(int studentId)
        {
            var stats = await _statisticService.GetStudentGradesStatsAsync(studentId);

            return Ok(stats);
        }
        
        [HttpGet("api/groups/{groupId:int}/students/grades/statistic")]
        public async Task<IActionResult> GetGroupGradesStats(int groupId)
        {
            var stats = await _statisticService.GetGroupGradesStatsAsync(groupId);

            return Ok(stats);
        }
        
        [HttpGet("api/groups/{groupId:int}/courses/{courseId:int}/students/grades/statistic")]
        public async Task<IActionResult> GetGroupCourseGradesStats(int groupId, int courseId)
        {
            var stats = await _statisticService.GetGroupGradesStatsAsync(groupId, courseId);

            return Ok(stats);
        }
    }
}