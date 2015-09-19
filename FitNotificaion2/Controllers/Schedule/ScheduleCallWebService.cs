using Quartz;

namespace FitNotificaion2.Controllers.Schedule
{
    public class ScheduleCallWebService : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ServiceNewPost task = new ServiceNewPost();
            string result = task.TimPostMoi();
        }
    }
}