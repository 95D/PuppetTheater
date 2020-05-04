using System;
using System.Collections.Generic;

namespace Viento.PuppetTheater.Utility
{
    using CaseTuple = Tuple<Func<bool>, Func<object>>;
    public class BaseWhen<T>
    {
        protected readonly T real;
        protected bool isComplete = false;
        protected object answer = null;
        
        protected List<CaseTuple> cases = new List<CaseTuple>();
        
        protected BaseWhen(T real)
        {
            this.real = real;
        }

        public object End(Func<object> defaultCase = null)
        {
            foreach(var c in cases)
            {
                if(c.Item1())
                {
                    answer = c.Item2();
                    isComplete = true;
                    break;
                }
            }

            if (isComplete)
                return answer;
            else if (defaultCase != null)
                return defaultCase();
            else
                return null;
        }
    }

    public sealed class When<T> : BaseWhen<T>
    {
        private readonly Func<T, T, bool> equals = (e, r) => e.Equals(r);

        private When(T real) : base(real) { }

        private When(T real, Func<T, T, bool> equals) : base(real) 
        {
            this.equals = equals;
        }

        public When<T> Case(T expected, Func<object> result)
        {
            cases.Add(new CaseTuple(
                () => equals(expected, real), 
                result
                ));
            return this;
        }

        public When<T> CaseThenAs<CAST>(T expected, Func<CAST, object> result)
        {
            cases.Add(new CaseTuple(
                () => equals(expected, real),
                () => real.AsDoing(result)
                ));
            return this;
        }

        public When<T> CaseRun(T expected, Action result)
        {
            cases.Add(new CaseTuple(
                () => equals(expected, real),
                () =>
                {
                    result();
                    return null;
                }));
            return this;
        }

        public When<T> CaseThenAsRun<CAST>(T expected, Action<CAST> result)
        {
            cases.Add(new CaseTuple(
                () => equals(expected, real),
                () => real.AsDoing<CAST, object>(x =>
                {
                    result(x);
                    return null;
                })));
            return this;
        }

        public static When<T> Start(T real)
        {
            return new When<T>(real);
        }

        public static When<T> Start(T real, Func<T, T, bool> equals)
        {
            return new When<T>(real, equals);
        }
    }

    sealed class When : BaseWhen<object>
    {
        private When() : base(null) {}

        public When Case(Func<bool> condition, Func<object> result)
        {
            cases.Add(new CaseTuple(
                condition, result
                ));
            return this;
        }

        public When CaseRun(Func<bool> condition, Action result)
        {
            cases.Add(new CaseTuple(
                condition,
                () =>
                {
                    result();
                    return null;
                }));
            return this;
        }
    }
}
