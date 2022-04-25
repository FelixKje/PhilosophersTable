using System;

for (int i = 0; i < 10; i++){
    Console.WriteLine(i%5);
}

enum PhilosopherState { Eating, Thinking }
class Philosopher{
    
    public string Name{ get; set; }
    
    public PhilosopherState State{ get; set; }

    readonly int StarvationThreshold;

    public readonly Chopstick RightChopstick;
    public readonly Chopstick LeftChopstick;

    Random rand = new Random();

    int contThinkCOunt = 0;

    public Philosopher(Chopstick rightChopstick, Chopstick leftChopstick, string name, int starvationThreshold){
        RightChopstick = rightChopstick;
        LeftChopstick = leftChopstick;
        Name = name;
        State = PhilosopherState.Thinking;
        StarvationThreshold = starvationThreshold;
    }

    private bool TakeChopstickInLeftHand() => LeftChopstick.Take(Name);

    private bool TakeChopstickInRightHand() => RightChopstick.Take(Name);
}

enum ChopstickState{ Taken, onTable }
class Chopstick{
    public string ChopStickID{ get; set; }
    public ChopstickState State{ get; set; }
    public string TakenBy{ get; set; }

    public bool Take(string takenBy){
        lock (this){
            if (this.State == ChopstickState.onTable){
                State = ChopstickState.Taken;
                TakenBy = takenBy;
                Console.WriteLine($"{ChopStickID} is taken by {TakenBy}");
                return true;
            }
            
            State = ChopstickState.Taken;
            return false;
        }
    }

    public void Put(){
        State = ChopstickState.onTable;
        Console.WriteLine($"{ChopStickID} is placed on the table by {TakenBy}");
        TakenBy = String.Empty;
    }
}
