namespace CsCat
{
    public class ValueResult<V>
    {
        private readonly bool _isHasValue;
        private readonly V _value;

        public bool GetIsHasValue() => _isHasValue;
        public V GetValue() => _value;

        public ValueResult(bool isHasValue, V value)
        {
            this._isHasValue = isHasValue;
            this._value = value;
        }
    }
}