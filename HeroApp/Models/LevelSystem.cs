namespace HeroApp.Models
{
    public class LevelSystem
    {
        public double MinXP { get; set; }
        public double MaxXP { get; set; }
        public double XPinPercent { get; set; }
        public int Level { get; set; }

        public LevelSystem(int xp)
        {
            Level = 0;
            var xpLevel = 100;
            
            while (xpLevel < xp)
            {
                MinXP = xpLevel;
                xpLevel *= 2;
                MaxXP = xpLevel;
                Level++;
            }
            if (MaxXP - MinXP != 0)
            {
                XPinPercent = (xp - MinXP) / (MaxXP - MinXP) * 100;
            }
            else
            {
                XPinPercent = 0;
            }
        }
    }
}