using System;

for (int i = 0; i < 10; i++){
    Console.WriteLine(i%5);
}

enum PhilosopherState { Eating, Thinking }
class Philosopher{
    
    public string Name{ get; set; }
    
    public PhilosopherState State{ get; set; }

    readonly int StarvationThreshold;

    public readonly ChopStick RightChopStick;
    public readonly ChopStick LeftChopStick;

    Random rand = new Random();

    int contThinkCOunt = 0;

    public Philosopher(ChopStick rightChopStick, ChopStick leftChopStick, string name, int starvationThreshold){
        RightChopStick = rightChopStick;
        LeftChopStick = leftChopStick;
        Name = name;
        State = PhilosopherState.Thinking;
        StarvationThreshold = starvationThreshold;
    }
}

enum ChopStickState{ Taken, onTable }
class ChopStick{
    public string ChopStickID{ get; set; }
    public ChopStickState State{ get; set; }
    public string TakenBy{ get; set; }
}
