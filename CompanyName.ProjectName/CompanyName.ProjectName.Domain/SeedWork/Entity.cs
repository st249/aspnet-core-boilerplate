namespace CompanyName.ProjectName.Domain.SeedWork;

public abstract class Entity : IEntity
{
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public void SetLastModificationTime()
    {
        LastModificationTime = DateTime.Now;
    }
    protected Entity()
    {
        CreationTime = DateTime.Now;
    }
}

public abstract class Entity<TIdentity> : Entity where TIdentity : struct
{

    private TIdentity _Id;

    public virtual TIdentity Id
    {
        get => _Id;
        protected set { _Id = value; }
    }

    protected Entity() : base()
    {
    }

    public bool IsTransient()
    {
        return this.Id.Equals(default(TIdentity));
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Entity<TIdentity>))
            return false;
        if (Object.ReferenceEquals(this, obj))
            return true;
        if (this.GetType() != obj.GetType())
            return false;
        var item = (Entity<TIdentity>)obj;
        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return item.Id.Equals(this.Id);
    }

    public bool Equals(IEntity<TIdentity>? obj)
    {
        if (obj == null || !(obj is Entity<TIdentity>))
            return false;
        if (Object.ReferenceEquals(this, obj))
            return true;
        if (this.GetType() != obj.GetType())
            return false;
        var item = (Entity<TIdentity>)obj;
        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return item.Id.Equals(this.Id);
    }

    int? _requestedHashCode;

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();
    }

    public static bool operator ==(Entity<TIdentity> left, Entity<TIdentity> right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null));
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity<TIdentity> left, Entity<TIdentity> right)
    {
        return !(left == right);
    }
}


