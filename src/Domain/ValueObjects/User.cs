namespace Domain.ValueObjects
{
    public readonly struct User : IEquatable<User>
    {
        public int Id { get; }
        public string Name { get; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(User other) => this.Id == other.Id;

        public override string ToString() => this.Id.ToString();
    }
}
