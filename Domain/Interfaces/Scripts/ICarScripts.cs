namespace Domain.Interfaces.Scripts
{
    public interface ICarScripts
    {
        public string InsertCarAsync { get; }
        public string SearchCarAsync { get; }
        public string DeleteCarAsync { get; }
        public string GetCarByIdAsync { get; }
        public string UpdateCarAsync { get; }
    }
}
