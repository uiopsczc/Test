namespace CsCat
{
  public class TimeTable
  {
    public int day;
    public int hour;
    public int minute;
    public int second;

    public TimeTable(int day, int hour, int minute, int second)
    {
      this.day = day;
      this.hour = hour;
      this.minute = minute;
      this.second = second;
    }

    public string GetFormateString(string day_unit, string hour_unit, string minute_unit,
      string second_unit)
    {
      string content = "";
      if (this.day != 0)
        content += this.day + day_unit;
      if (!content.IsNullOrWhiteSpace() || this.hour != 0)
        content += this.hour + hour_unit;
      if (!content.IsNullOrWhiteSpace() || this.minute != 0)
        content += this.minute + minute_unit;
      if (!content.IsNullOrWhiteSpace() || this.second != 0)
        content += this.second + second_unit;
      return content;
    }

  }
}