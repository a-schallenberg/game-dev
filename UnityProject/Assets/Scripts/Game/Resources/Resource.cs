namespace Game.Resources {
	public class Resource {
		public readonly ResourceType Type;
		private         int          _limit;

		internal Resource(ResourceType type) {
			Type = type;
		}

		public int Amount { get; private set; }

		public int Limit {
			get { return _limit; }
			set {
				_limit = value;
				if (_limit < Amount) {
					ResourceHandler.UseResources(Type, Amount - _limit);
				}
			}
		} // ReSharper disable Unity.PerformanceAnalysis

		public bool Add(int amount) {
			if (Amount + amount <= _limit) {
				Amount += amount;
				return true;
			}

			Amount = _limit;
			return false;
		}

		public bool Use(int amount) {
			if (Amount - amount >= 0) {
				Amount -= amount;
				return true;
			}

			Amount = 0;
			return false;
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
}