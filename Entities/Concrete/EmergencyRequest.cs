using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class EmergencyRequest : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
