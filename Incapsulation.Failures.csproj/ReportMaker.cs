using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{
    public class ReportMaker
    {
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceId,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            var dateFailed = new DateTime(year, month, day);

            var devicesArr = new Device[failureTypes.Length];
            for (var i = 0; i < failureTypes.Length; i++)
                devicesArr[i] = new Device(deviceId[i], devices[i]["Name"].ToString(), (FailureType) failureTypes[i], new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]));

            return FindDevicesFailedBeforeDate(devicesArr, dateFailed);
        }
        public static List<string> FindDevicesFailedBeforeDate(Device[] devices, DateTime dateFailed)
        {
            return devices.Where(e => e.Date < dateFailed && (int)e.Failure % 2 == 0).Select(e => e.Name).ToList();
        }
    }
    public class Device
    { 
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public FailureType Failure { get; set; }
        public DateTime Date { get; set; }

        public Device(int deviceId, string name, FailureType failure, DateTime date)
        {
            DeviceId = deviceId;
            Name = name;
            Failure = failure;
            Date = date;
        }

    }
    public enum FailureType
    {
        unexpectedShutdown,/// 0 for unexpected shutdown, 
        shortNonResponding,/// 1 for short non-responding, 
        hardwareFailures, /// 2 for hardware failures, 
        connectionProblems/// 3 for connection problems
    }
}

