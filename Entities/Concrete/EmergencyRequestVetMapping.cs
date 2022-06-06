using Core.Entities;

namespace Entities.Concrete
{
    public class EmergencyRequestVetMapping : IEntity
    {
        public int Id { get; set; }
        public int VetId { get; set; }
        public int EmergencyRequestId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
