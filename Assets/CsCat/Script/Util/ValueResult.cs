namespace CsCat
{
    public class ValueResult<V>
    {
        private bool _isHasValue;
        private V _value;

        public bool GetIsHasValue() => _isHasValue;
        public V GetValue() => _value;

        public ValueResult(bool isHasValue, V value)
        {
            this._isHasValue = isHasValue;
            this._value = value;
        }
    }
}