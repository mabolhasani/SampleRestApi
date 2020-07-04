using NHibernate.Proxy;
using System;

namespace SampleRestApi.Data.utils
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }

        public virtual DateTime CreateDate { get; protected set; }

        public virtual DateTime ModifiedDate { get; protected set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            return NHibernateProxyHelper.GetClassWithoutInitializingProxy(this);
        }

        public virtual void UpdateModifiedDate()
        {
            ModifiedDate = DateTime.UtcNow;
        }

        public virtual void UpdateCreateDate()
        {
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
