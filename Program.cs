using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

Philosopher aristotle = new Philosopher(Table.Copper, Table.Platinum, "Aristotle", 4);
Philosopher sokratis = new Philosopher(Table.Platinum, Table.Gold, "Sokratis", 6);
Philosopher john = new Philosopher(Table.Gold, Table.Silver, "John", 7);
Philosopher augustine = new Philosopher(Table.Silver, Table.Wood, "Augustine", 5);
Philosopher thomas = new Philosopher(Table.Wood, Table.Copper, "Thomas", 4);

new Thread(aristotle.Think).Start();
new Thread(sokratis.Think).Start();
new Thread(john.Think).Start();
new Thread(augustine.Think).Start();
new Thread(thomas.Think).Start();

Console.ReadKey();


enum PhilosopherState { Eating, Thinking }
class Philosopher{
    
    string Name{ get; set; }
    
    PhilosopherState State{ get; set; }

    readonly int StarvationThreshold;

    int timesEaten = 0;

    readonly Chopstick RightChopstick;
    readonly Chopstick LeftChopstick;

    Random rand = new Random();

    int contThinkCount = 0;

    public Philosopher(Chopstick rightChopstick, Chopstick leftChopstick, string name, int starvationThreshold){
        RightChopstick = rightChopstick;
        LeftChopstick = leftChopstick;
        Name = name;
        State = PhilosopherState.Thinking;
        StarvationThreshold = starvationThreshold;
    }

    void Eat(){
        if (TakeChopstickInRightHand()){
            if (TakeChopstickInLeftHand()){
                Eating();
            }

            else{
                Thread.Sleep(rand.Next(100, 400));
                if (TakeChopstickInLeftHand()){
                    Eating();
                }
                else{
                    RightChopstick.Put();
                }
            }
        }
        else{
            if (TakeChopstickInLeftHand()){
                Thread.Sleep(rand.Next(100,400));
                if (TakeChopstickInRightHand()){
                    Eating();
                }
                else{
                    LeftChopstick.Put();
                }
            }
        }

        Think();
    }

    void Eating(){
        this.State = PhilosopherState.Eating;
        Console.WriteLine(
            $"(:::) {Name} is eating for the {++timesEaten}th time, with {RightChopstick.ChopStickID} and {LeftChopstick.ChopStickID}");
        Thread.Sleep(rand.Next(5000, 10000));
        
        contThinkCount = 0;
                
        RightChopstick.Put();
        LeftChopstick.Put();
    }

    public void Think(){
        this.State = PhilosopherState.Thinking;
        Console.WriteLine($"^^*^^ {Name} is thinking...on {Thread.CurrentThread.Priority.ToString()}");
        Thread.Sleep(rand.Next(2500, 20000));
        contThinkCount++;

        if (contThinkCount > StarvationThreshold){
            Console.WriteLine($":ooooooooo: {Name} is starving");
        }
        
        Eat();
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
                Console.WriteLine($"||| {ChopStickID} is taken by {TakenBy}");
                return true;
            }
            
            State = ChopstickState.Taken;
            return false;
        }
    }

    public void Put(){
        State = ChopstickState.onTable;
        Console.WriteLine($"||| {ChopStickID} is placed on the table by {TakenBy}");
        TakenBy = String.Empty;
    }

   
}
class Table{
    internal static Chopstick Platinum = new Chopstick()
        {ChopStickID = "Platinum Chopstick", State = ChopstickState.onTable};
    internal static Chopstick Gold = new Chopstick()
        {ChopStickID = "Gold Chopstick", State = ChopstickState.onTable};
    internal static Chopstick Silver = new Chopstick()
        {ChopStickID = "Silver Chopstick", State = ChopstickState.onTable};
    internal static Chopstick Wood = new Chopstick()
        {ChopStickID = "Wood Chopstick", State = ChopstickState.onTable};
    internal static Chopstick Copper = new Chopstick()
        {ChopStickID = "Copper Chopstick", State = ChopstickState.onTable};
}
