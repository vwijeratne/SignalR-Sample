using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRServiceHub
{
    public class DataEngine
    {
        private IHubContext _hubs;
        private readonly int _pollIntervalMillis;
        
        public DataEngine(int pollIntervalMillis)
        {
            _hubs = GlobalHost.ConnectionManager.GetHubContext<SignalRServer>();
            _pollIntervalMillis = pollIntervalMillis;
        }

        public async Task OnTimeMonitor()
        {
            List<ClockModel> timeCounterList = new List<ClockModel>()
            {
                new ClockModel() { CategoryName = "Hour", Value = DateTime.Now.Hour },
                new ClockModel() { CategoryName="Minute", Value = DateTime.Now.Minute },
                new ClockModel() { CategoryName = "Second", Value = DateTime.Now.Second },
                new ClockModel() { CategoryName = "Milisecond", Value = DateTime.Now.Millisecond  }

            };
            while (true)
            {
                await Task.Delay(_pollIntervalMillis);

                IList<ClockModel> clockModels = new List<ClockModel>();
                foreach (var performanceCounter in timeCounterList)
                {
                    try
                    {
                        switch (performanceCounter.CategoryName)
                        {
                            case "Hour":
                                clockModels.Add(new ClockModel
                                {
                                   CategoryName = performanceCounter.CategoryName,
                                   Value = DateTime.Now.Hour
                                });
                                break;
                            case "Minute":
                                clockModels.Add(new ClockModel
                                {
                                    CategoryName = performanceCounter.CategoryName,
                                    Value = DateTime.Now.Minute
                                });
                                break;
                            case "Second":
                                clockModels.Add(new ClockModel
                                {
                                    CategoryName = performanceCounter.CategoryName,
                                    Value = DateTime.Now.Second
                                });
                                break;
                            case "Milisecond":
                                clockModels.Add(new ClockModel
                                {
                                    CategoryName = performanceCounter.CategoryName,
                                    Value = DateTime.Now.Millisecond
                                });
                                break;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Trace.TraceError("Error");
                        Trace.TraceError(ex.Message);
                        Trace.TraceError(ex.StackTrace);
                    }
                }

                _hubs.Clients.All.AddMessage(clockModels);                
            }
        }


    }
}