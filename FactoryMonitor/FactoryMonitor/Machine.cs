using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FactoryMonitor.Models;

public class Machine : INotifyPropertyChanged
{
    private string _Name = string.Empty;
    private string _Status = string.Empty;
    private double _Temperature;
    private double _OperationRate;

    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            OnPropertyChanged();
        }
    }

    public string Status
    {
        get => _Status;
        set
        {
            _Status = value;
            OnPropertyChanged();
        }
    }

    public double Temperature
    {
        get => _Temperature;
        set
        {
            _Temperature = value;
            OnPropertyChanged();
        }
    }

    public double OperationRate
    {
        get => _OperationRate;
        set
        {
            _OperationRate = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? Propertychanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        Propertychanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

