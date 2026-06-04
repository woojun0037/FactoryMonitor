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
    private double _RunningSeconds;

    private int _ProductionCount;
    private int _TargetCount = 1000;

    private bool _isAlarmActive;

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
            OnPropertyChanged(nameof(StatusColor));
        }
    }

    public double Temperature
    {
        get => _Temperature;
        set
        {
            _Temperature = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AlarmColor));
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

    public double RunningSeconds
    {
        get => _RunningSeconds;
        set
        {
            _RunningSeconds = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(RunningTimeText));
        }
    }   

    public string RunningTimeText
    {
        get
        {
            TimeSpan time = TimeSpan.FromSeconds(RunningSeconds);
            return time.ToString(@"hh\:mm\:ss");
        }
    }

    public int ProductionCount
    {
        get => _ProductionCount;
        set 
        {
            _ProductionCount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AchievementRate));
            OnPropertyChanged(nameof(AchievementRateText));
        }
    }

    public int TargetCount
    {
        get => _TargetCount;
        set
        {
            _TargetCount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AchievementRate));
            OnPropertyChanged(nameof(AchievementRateText));
        }
    }

    public double AchievementRate
    {
        get
        {
            if (TargetCount == 0)
            {
                return 0;
            }
            return (double)ProductionCount / TargetCount * 100;
        }
    }

    public string AchievementRateText
    {
        get
        {
            return $"{AchievementRate:F1}%";
        }
    }

    public bool IsAlarm
    {
        get => Temperature >= 80;
    }

    public bool IsAlarmActive
    {
        get => _isAlarmActive;
        set
        {
            _isAlarmActive = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(AlarmColor));
        }
    }

    public string AlarmColor
    {
        get
        {
            return IsAlarm ? "Red" : "Transparent";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public string StatusColor
    {
        get
        {
            return Status switch
            {
                "RUN" => "LightGreen",
                "STOP" => "LightGray",
                "ERROR" => "LightCoral",
                _ => "White"
            };
        }
    }
}
