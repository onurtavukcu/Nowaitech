using System.Diagnostics.CodeAnalysis;

namespace NowaitechShared.Maybe
{
    public readonly struct Maybe<TValue> : IEquatable<Maybe<TValue>>
    {
        private sealed class MaybeValueWrapper : IDisposable
        {
            private bool _disposed;

            internal TValue _value;

            public MaybeValueWrapper(TValue value)
            {
                _value = value;
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _disposed = true;
                    TValue value = _value;
                    _value = default(TValue);
                    if ((object)value is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        private readonly MaybeValueWrapper _valueWrapper;

        public TValue MaybeValue
        {
            get
            {
                if (HasNoValue)
                {
                    throw new InvalidOperationException("Maybe<" + typeof(TValue).Name + "> construct has no value.");
                }

                return _valueWrapper._value;
            }
        }

        public static Maybe<TValue> None => default(Maybe<TValue>);

        public bool HasValue => _valueWrapper != null;

        public bool HasNoValue => !HasValue;

        private Maybe([AllowNull] TValue value)
        {
            _valueWrapper = ((value == null) ? null : new MaybeValueWrapper(value));
        }

        public static implicit operator Maybe<TValue>([AllowNull] TValue value)
        {
            if (value?.GetType() == typeof(Maybe<TValue>))
            {
                return (Maybe<TValue>)(object)value;
            }

            return new Maybe<TValue>(value);
        }

        public static Maybe<TValue> From([AllowNull] TValue obj)
        {
            return new Maybe<TValue>(obj);
        }

        public static bool operator ==([AllowNull] TValue value, Maybe<TValue> maybe)
        {
            if (value is Maybe<TValue>)
            {
                return maybe.Equals(value);
            }

            if (maybe.HasNoValue)
            {
                return false;
            }

            return maybe.MaybeValue.Equals(value);
        }

        public static bool operator !=([AllowNull] TValue value, Maybe<TValue> maybe)
        {
            return !(maybe == value);
        }

        public static bool operator ==(Maybe<TValue> maybe, [AllowNull] TValue value)
        {
            if (value is Maybe<TValue>)
            {
                return maybe.Equals(value);
            }

            if (maybe.HasNoValue)
            {
                return false;
            }

            return maybe.MaybeValue.Equals(value);
        }

        public static bool operator !=(Maybe<TValue> maybe, [AllowNull] TValue value)
        {
            return !(maybe == value);
        }

        public static bool operator ==(Maybe<TValue> first, Maybe<TValue> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Maybe<TValue> first, Maybe<TValue> second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != typeof(Maybe<TValue>))
            {
                if (obj is TValue value)
                {
                    obj = new Maybe<TValue>(value);
                }

                if (!(obj is Maybe<TValue>))
                {
                    return false;
                }
            }

            Maybe<TValue> other = (Maybe<TValue>)obj;
            return Equals(other);
        }

        public bool Equals(Maybe<TValue> other)
        {
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return _valueWrapper._value.Equals(other._valueWrapper._value);
        }

        public override int GetHashCode()
        {
            if (!HasNoValue)
            {
                return _valueWrapper._value.GetHashCode();
            }

            return 0;
        }

        public override string ToString()
        {
            if (!HasNoValue)
            {
                return MaybeValue.ToString();
            }

            return "<No value>";
        }
    }
}
