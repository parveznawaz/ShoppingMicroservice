namespace Ordering.Core.Entities.Base
{
    public abstract class EntityBase<T>: IEntityBase<T>
    {
        public virtual T Id { get; protected set; }
        private int? _requestedHashCode;

        public bool IsTransient() => Id.Equals(default(T));

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<T>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var item = (EntityBase<T>) obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            else
            {
                return item == this;
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                }

                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }
    }
}