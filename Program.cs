

using BigBattle.Game;

class Program
{
    static void Main()
    {
        while (Game.GameInstance.IsPlaying)
        {
            Game.GameInstance.Tick();
            Thread.Sleep(2000);
        }
    }
}
