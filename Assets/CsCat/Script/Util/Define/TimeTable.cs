using System.Text;

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

        public string GetFormatString(string dayUnit, string hourUnit, string minuteUnit,
            string secondUnit)
        {
            using (var scope = new StringBuilderScope())
            {
                if (this.day != 0)
                    scope.stringBuilder.Append(this.day + dayUnit);
                if (scope.stringBuilder.Length != 0 || this.hour != 0)
                    scope.stringBuilder.Append(this.hour + hourUnit);
                if (scope.stringBuilder.Length != 0 || this.minute != 0)
                    scope.stringBuilder.Append(this.minute + minuteUnit);
                if (scope.stringBuilder.Length != 0 || this.second != 0)
                    scope.stringBuilder.Append(this.second + secondUnit);
                var result = scope.stringBuilder.ToString();
                return result;
            }
        }
    }
}