using Core.Entities;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Vet : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
