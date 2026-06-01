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
    public Machine? SelectedMachine { get; set; }

    public ICommand StartCommand { get; }
    public ICommand StopCommand  { get; }
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

        StartCommand = new RealyCommand(_ => ChangeStatus("RUN"));
        StopCommand  = new RealyCommand(_ => ChangeStatus("STOP"));
        ErrorCommand = new RealyCommand(_ => ChangeStatus("ERROR"));

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    private void ChangeStatus(string status)
    {
        if (SelectedMachine == null)
            return;

        SelectedMachine.Status = status;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        foreach (var machine in Machines)
        {
            if (machine.Status == "RUN")
            {
                machine.Temperature += _random.NextDouble() * 2 - 0.5;
                machine.OperationRate = _random.Next(80, 101);
            }
            else if(machine.Status == "STOP")
            {
                machine.Temperature -= _random.NextDouble();
                machine.OperationRate = 0;
            }
            else if(machine.Status == "ERROR")
            {
                machine.Temperature += _random.NextDouble() * 3;
                machine.OperationRate = _random.Next(0, 50);
            }   

            if(machine.Temperature > 100)
            {
                machine.Temperature = 20;
            }

            if(machine.Temperature > 100)
            {
                machine.Temperature = 100;
            }
        }
    }
}

