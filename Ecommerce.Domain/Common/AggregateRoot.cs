
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Common
{
    public abstract class AggregateRoot:Entity
    {
        private readonly List<DomainEvent> _events = [];

        [NotMapped]
        public IReadOnlyCollection<DomainEvent> Events => _events;

        protected void AddEvent(DomainEvent e)
        {
            _events.Add(e);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
