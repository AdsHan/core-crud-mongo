using CMO.Product.Domain.Enums;
using System;

namespace CMO.Product.Domain.DomainObjects
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public EntityStatusEnum Status { get; set; }
        public DateTime DateCreateAt { get; private set; }
        public DateTime? DateDeleteAt { get; private set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            DateCreateAt = DateTime.Now;
            Status = EntityStatusEnum.Active;
        }

        public void Delete()
        {
            if (Status == EntityStatusEnum.Active)
            {
                Status = EntityStatusEnum.Inactive;
                DateDeleteAt = DateTime.Now;
            }
        }
    }
}