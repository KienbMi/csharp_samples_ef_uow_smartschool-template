using System;
using System.Collections.Generic;
using System.IO;
using Utils;
using SmartSchool.Core.Entities;

namespace SmartSchool.TestConsole
{
    public class ImportController
    {
        const string Filename = "measurements.csv";

        /// <summary>
        /// Liefert die Messwerte mit den dazugehörigen Sensoren
        /// </summary>
        public static IEnumerable<Measurement> ReadFromCsv()
        {
            string filePath = MyFile.GetFullNameInApplicationTree(Filename);
            List<Measurement> measurements = new List<Measurement>();
            Dictionary<string, Sensor> sensors = new Dictionary<string, Sensor>();
            int id = 1;

            if (File.Exists(filePath) == false)
            {
                throw new Exception("File does not exist");
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] data = line.Split(';');
                DateTime date;
                DateTime time;
                double value;

                if (data != null
                    && data.Length >= 4
                    && DateTime.TryParse(data[0], out date)
                    && DateTime.TryParse(data[1], out time)
                    && Double.TryParse(data[3], out value))
                {
                    string sensorName = data[2];
                    string[] location_type = sensorName.Split('_');
                    if (location_type.Length == 2)
                    {
                        string location = location_type[0];
                        string type = location_type[1];

                        Sensor sensor;
                        if (sensors.TryGetValue(sensorName, out sensor) == false)
                        {
                            sensor = new Sensor
                            {
                                Name = type,
                                Location = location,
                                Unit = "unknown"
                            };
                            sensors.Add(sensorName, sensor);
                        }

                        measurements.Add(new Measurement
                        {
                            Sensor = sensor,
                            Time = date.AddSeconds(time.TimeOfDay.TotalSeconds),
                            Value = value
                        });
                    }
                }
            }
            return measurements;
        }
    }
}
