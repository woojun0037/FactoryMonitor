using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

using FactoryMonitor.Models;
using FactoryMonitor.Commands;

namespace FactoryMonitor.ViewModels;

public class MainViewModel
{
    private readonly DispatcherTimer _timer;
    private readonly Random _random = new Random();

    public ObservableCollection<Machine> Machines { get; set; }
    public ObservableCollection<LogMessage> Logs { get; }
    public Machine? SelectedMachine { get; set; }

    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }
    public ICommand ErrorCommand { get; }

    public MainViewModel()
    {
        Machines = new ObservableCollection<Machine>
        {
            new Machine
            {
                Name = "설비 A",
                Status = "RUN",
                Temperature = 35.5,
                OperationRate = 95
            },
            new Machine
            {
                Name = "설비 B",
                Status = "STOP",
                Temperature = 22.0,
                OperationRate = 0
            },
            new Machine
            {
                Name = "설비 C",
                Status = "ERROR",
                Temperature = 80.2,
                OperationRate = 50
            }
        };

        Logs = new ObservableCollection<LogMessage>();

        StartCommand = new RelyCommand(_ => ChangeStatus("RUN"));
        StopCommand  = new RelyCommand(_ => ChangeStatus("STOP"));
        ErrorCommand = new RelyCommand(_ => ChangeStatus("ERROR"));


        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private void ChangeStatus(string status)
    {
        if (SelectedMachine == null)
        {
            return;
        }

        string oldStatus = SelectedMachine.Status;

        SelectedMachine.Status = status;

        AddLog($"{SelectedMachine.Name} : {oldStatus} -> {status}");
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        foreach (var machine in Machines)
        {
            if (machine.Status == "RUN")
            {
                machine.RunningSeconds += 1;

                // RUN 상태에서는 서서히 과열
                machine.Temperature += _random.NextDouble() * 1.5 + 0.2;
                machine.OperationRate = _random.Next(80, 101);
            }
            else if (machine.Status == "STOP")
            {
                // STOP 상태에서는 냉각
                machine.Temperature -= _random.NextDouble() * 3;
                machine.OperationRate = 0;
            }
            else if (machine.Status == "ERROR")
            {
                // ERROR 상태에서는 빠르게 과열
                machine.Temperature += _random.NextDouble() * 3;
                machine.OperationRate = _random.Next(0, 50);
            }

            if (machine.Temperature < 20)
            {
                machine.Temperature = 20;
            }

            if (machine.Temperature > 100)
            {
                machine.Temperature = 100;
            }
            CheckAlarms(machine);
        }
    }

    private void AddLog(string message)
    {
        Logs.Add(new LogMessage
        {
            Time = DateTime.Now,
            Message = message
        });
    }

    private void CheckAlarms(Machine machine)
    {
        if(machine.Temperature >= 80 && machine.IsAlarmActive == false)
        {
            machine.IsAlarmActive = true;
            AddLog($"[ALARM] {machine.Name} 온도 초과 : {machine.Temperature:F4}");
        }
        else if(machine.Temperature < 70 && machine.IsAlarmActive == true)
        {
            machine.IsAlarmActive = false;
            AddLog($"[ALARM] {machine.Name} 온도 정상 복귀 : {machine.Temperature:F4}");
        }
    }
}
