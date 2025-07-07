namespace UserDataService
{
    public interface IUserDataService
    {
        int Level { get; set; }
        int EXP { get; set; }
        int Stage { get; set; }
        
        void Load();
        void Save();
    }
}