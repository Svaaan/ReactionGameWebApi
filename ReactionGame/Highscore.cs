using System;
namespace ReactionGame
{
    class Highscore
    {
        public string GameName {get; set;} = "ReactionGame";
        public string Name { get; set; }
        public long Time { get; set; }
        public DateTime ClickCreated {get; set;} = DateTime.Now;


        public override string ToString()
        {
            return Name + "  -  " + Time + "  -  " + ClickCreated;
        }
    }
}