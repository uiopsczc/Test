using System.Collections;

namespace CsCat
{
  public class BuffCache
  {
    public float duration;
    public float remain_duration;
    public Unit source_unit;
    public SpellBase source_spell;
    public Hashtable arg_dict;

    public BuffCache(float duration, Unit source_unit, SpellBase source_spell, Hashtable arg_dict)
    {
      this.duration = duration;
      this.remain_duration = this.duration;
      this.source_unit = source_unit;
      this.source_spell = source_spell;
      this.arg_dict = arg_dict;
    }

  }
}



