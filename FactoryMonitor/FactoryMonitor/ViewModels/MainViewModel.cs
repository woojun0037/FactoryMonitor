using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FactoryMonitor.Models;

namespace FactoryMonitor.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Machine> Machines { get; set; }
    public Machine? SelectedMachine { get; set; }

    public MainViewModel()
    {
        Machines = new ObservableCollection<Machine>();

        Machines.Add(new Machine
        {
            Name = "설비 A",
            Status = "RUN",
            Temperature = 35.5,
            OperationRate = 95
        });

        Machines.Add(new Machine
        {
            Name = "설비 B",
            Status = "STOP",
            Temperature = 22.0,
            OperationRate = 0
        });

        Machines.Add(new Machine
        {
            Name = "설비 C",
            Status = "ERROR",
            Temperature = 80.2,
            OperationRate = 50
        });
    }
}

