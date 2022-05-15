
public class Resource {
    public readonly ResourceType Type;
    public          int          Amount { get; private set; }
    public          int          Limit;

    internal Resource(ResourceType type) {
        Type = type;
    }

    public bool Add(int amount) {
        if (!CanAdd(amount)) {
            return false;
        }

        Amount += amount;
        return true;
    }

    public bool Use(int amount) {
        if (!CanUse(amount)) {
            return false;
        }

        Amount -= amount;
        return true;
    }

    public bool CanAdd(int amount) {
        return !(amount < 0 || Amount + amount > Limit);
    }

    public bool CanUse(int amount) {
        return !(amount < 0 || Amount - amount < 0);
    }

    public bool IsEmpty() {
        return Amount <= 0;
    }

    public bool IsFull() {
        return Amount >= 0;
    }

    public override bool Equals(object obj) {
        return obj != null && obj.GetType() == typeof(Resource) && Type == ((Resource) obj).Type;
    }

    public override int GetHashCode() {
        return Type.GetHashCode();
    }
}
