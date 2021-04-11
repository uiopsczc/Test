using System;

namespace CsCat
{
  public partial class Unit
  {
    public AIBaseComp aiComp;

    public void RunAI(string ai_class_path)
    {
      Type ai_class = !ai_class_path.IsNullOrWhiteSpace() ? TypeUtil.GetType(ai_class_path) : typeof(AIBaseComp);
      if (this.aiComp != null)
        this.RemoveChild(this.aiComp.key);
      this.aiComp = this.AddChild(null, ai_class, this) as AIBaseComp;
    }
  }
}