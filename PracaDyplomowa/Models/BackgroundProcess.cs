using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class BackgroundProcess : BackgroundService
    {
        private IHubContext<Hubs.NotificationHub> _hubContext;

        public BackgroundProcess(IHubContext<Hubs.NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }


        public List<Reminder> ReminderList { get; set; }
        public int IntervalSec { get; } = 30;

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000 * IntervalSec, stoppingToken);
                backgroundReminder();
            }
        }

        private async void backgroundReminder()
        {
            ReminderList = Reminder.GetAllCurrentReminders();
            
            if (ReminderList == null)
                return;

            foreach(var reminder in ReminderList)
            {
                var parsed2Date = DateTime.Parse(reminder.CzasRozpoczecia);
                var timeLfet = parsed2Date.Subtract(DateTime.Now);
                var emailList = new List<string>();
                if(timeLfet.TotalSeconds <= IntervalSec && timeLfet.TotalSeconds >=0)
                {
                    var reminderRecipients = Reminder.getReminderRecipients(reminder.Id).email;
                    foreach(var recipient in reminderRecipients)
                        emailList.Add(recipient);
                  
                    wait4It((int)timeLfet.TotalMilliseconds, emailList, reminder.Tytul, reminder.Tresc);
                }
            }
        }

        private async Task wait4It(int timeMs, List<string> emailList , string title, string content)
        {
            foreach(var mail in emailList)
            Console.WriteLine($"Wait for User@ : {mail} // Time left [ms]: {timeMs}");
            
            await Task.Delay(timeMs);

            Email.sendMail(emailList, title, content);

            Console.WriteLine(emailList.Count + "Emails send");
        }
    }
}

