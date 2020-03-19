using Microsoft.EntityFrameworkCore;
using SmartSchool.Core.Contracts;
using SmartSchool.Core.Entities;
using System.Linq;

namespace SmartSchool.Persistence
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private ApplicationDbContext _dbContext;

        public MeasurementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  void AddRange(Measurement[] measurements)
        {
            _dbContext.Measurements.AddRange(measurements);
        }

        public int GetCountLivingRoom()
        {
            return _dbContext.Measurements
                .Include(m => m.Sensor)
                .Where(m => m.Sensor.Location.Equals("livingroom") && m.Sensor.Name.Equals("temperature"))
                .Count();
        }
    }
}