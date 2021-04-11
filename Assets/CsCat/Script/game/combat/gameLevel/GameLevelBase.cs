namespace CsCat
{
  public partial class GameLevelBase : TickObject
  {
    private bool is_started;
    private bool is_finished;

    public override void Start()
    {
      base.Start();
      this.is_started = true;
    }

    public override bool IsCanUpdate()
    {
      return this.IsStarted() && !this.IsFinished() && base.IsCanUpdate();
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
    }

    public bool CheckWin()
    {
      return false;
    }

    public bool CheckLose()
    {
      return false;
    }

    public void SetIsFinished(bool is_finished)
    {
      this.is_finished = is_finished;
    }

    public bool IsStarted()
    {
      return this.is_started;
    }

    public bool IsFinished()
    {
      return this.is_finished;
    }
  }
}