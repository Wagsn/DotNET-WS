using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WS.AspNetCore.Quartz.Controllers
{
    /// <summary>
    /// 定时任务控制器
    /// </summary>
    public class SchedulerController: Controller
    {
        private ISchedulerFactory SchedulerFactory { get; }
        private IScheduler Scheduler { get; set; }
        private ILogger<SchedulerController> Logger { get; }

        public SchedulerController(ISchedulerFactory schedulerFactory, ILogger<SchedulerController> logger)
        {
            SchedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 创建打印时间任务
        /// http://localhost:5000/scheduler/create?name=print&desc=pint%20task&cron=%2f5+*+*+*+*+%3f
        /// </summary>
        /// <param name="cron">CRON表达式</param>
        /// <param name="desc">描述</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create([FromQuery]string cron, DateTime? start = null, DateTime? end = null, [FromQuery]string name = "default", [FromQuery]string desc = "default")
        {
            Logger.LogInformation($"{nameof(cron)}: [{cron}], {nameof(name)}: [{name}], {nameof(desc)}: [{desc}]");
            //Console.WriteLine($"{nameof(desc)}: [{desc}], {nameof(cron)}: [{cron}]");
            // 调度器
            if(Scheduler == null) Scheduler = await SchedulerFactory.GetScheduler();
            await Scheduler.Start();
            // 这里打印的数据表示这是同一个Scheduler
            //Logger.LogInformation($"Scheduler.GetHashCode: {Scheduler.GetHashCode()}");

            // 触发器
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(cron)
                .StartAt(start ?? DateTime.Now)
                .EndAt(end ?? DateTime.MaxValue)
                .EndAt(end)
                //.WithIdentity(new TriggerKey(name+"-trigger") { Group = name+"-group" })
                .Build();

            // 任务器
            var jobDeatail = JobBuilder.Create<PrintJob>()
                //.WithDescription(desc)
                //.WithIdentity(name+"-job", name+"-group")
                .UsingJobData(nameof(name), name)
                .UsingJobData(nameof(desc), desc)
                .Build();

            await Scheduler.ScheduleJob(jobDeatail, trigger);

            return new JsonResult(new
            {
                Code = 200,
                Message = "定时打印时间任务创建成功"
            });
        }

        /// <summary>
        /// 创建GET回调定时任务（参数可以放在Query上）
        /// http://localhost:5000/scheduler/createCallback?cron=%2f5+*+*+*+*+%3f&callback=http%3A%2F%2Flocalhost%3A5000%2Fscheduler%2Fcallback
        /// </summary>
        /// <param name="cron">CRON表达式</param>
        /// <param name="callback">回调地址</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CreateCallback([FromQuery]string cron, [FromQuery]string callback = "http%3A%2F%2Flocalhost%3A5000%2Fscheduler%2Fcallback")
        {
            Logger.LogInformation($"{nameof(cron)}: [{cron}], {nameof(callback)}: [{callback}]");
            if (cron == null) return new JsonResult(new { Code = 400, Message = "CRON表达式不能为空" });
            // 调度器
            if (Scheduler == null) Scheduler = await SchedulerFactory.GetScheduler();
            await Scheduler.Start();

            // 触发器
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(cron)
                .Build();

            // 任务器
            var jobDeatail = JobBuilder.Create<CallbackJob>()
                .UsingJobData(nameof(callback), callback)
                .Build();

            await Scheduler.ScheduleJob(jobDeatail, trigger);

            return new JsonResult(new
            {
                Code = 200,
                Message = "定时回调任务创建成功"
            });
        }

        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            return Ok("OK");
        }


    }

    public class CallbackJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var callback = context.JobDetail.JobDataMap.GetString("callback");
            Console.WriteLine(DateTime.Now.ToString($"<{context.JobDetail.Key}> yyyy-MM-dd HH:mm:ss  callback: {callback}"));
            try
            {
                HttpClient httpClient = new HttpClient();
                var result = await httpClient.GetAsync(new Uri(callback)).Result.Content.ReadAsStringAsync();
                Console.WriteLine($"result: {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            //return Task.Run(() =>
            //{
               
            //});
        }
    }

    public class PrintJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine(DateTime.Now.ToString($"<{context.JobDetail.JobDataMap.GetString("name")}:{context.JobDetail.JobDataMap.GetString("desc")}> yyyy-MM-dd HH:mm:ss"));
            });
        }
    }
}
