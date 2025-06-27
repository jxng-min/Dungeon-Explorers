public interface ISettingService
{
    bool BGM { get; set; }
    float BGMRate { get; set; }
    bool SFX { get; set; }
    float SFXRate { get; set; }
    
    void Load();
    void Save();
}