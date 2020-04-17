using System;

namespace Dojodachi
{
    public class Dachi
    {
        public int Fullness { get; set; }
        public int Happiness { get; set; }
        public int Energy { get; set; }
        public int Meals { get; set; }
        public string Message { get; set; }

        public Dachi()
        {
            Fullness = 20;
            Happiness = 20;
            Energy = 50;
            Meals = 3;
        }
        public void Feed()
        {
            Meals -= 1;
            Random dachi = new Random();
            if(dachi.Next(1,4) != 1)
            {
                Fullness += dachi.Next(5,10);
            }
           
        }
        public void Play()
        {
            Energy -= 5;
            Random dachi = new Random();
            if(dachi.Next(1,4) !=1 )
            {
               Happiness += dachi.Next(5,10); 
            }
            
        }
        public void Work()
        {
            Energy -= 5;
            Random dachi = new Random();
            Meals += dachi.Next(1,3);
        }
        public void Sleep()
        {
            Energy += 15;
            Fullness -= 5;
            Happiness -= 5;
        }
    }
}