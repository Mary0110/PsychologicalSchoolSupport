namespace PsychologicalSupportPlatform.Common.Config;

public class RedisConfig
{
    public int AbsoluteExpiration { get; set; }  //Minutes
    
    public int SlidingExpiration { get; set; }   //Minutes
}
