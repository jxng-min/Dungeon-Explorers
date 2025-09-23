public interface IStageDataBase
{
    int Count { get; }
    int Current { get; set; }
    
    Stage GetStage(int stage_id);
}