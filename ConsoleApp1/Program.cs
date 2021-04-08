using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Class나 namespace는 Pascal Case로
    /// </summary>
    class CallbackArg { } //콜백의 값을 담는 클래스의최상위 부모 클래스 역할

    class PrimeCallBackArg : CallbackArg
    {
        /// <summary>
        /// Class 내의 필드는 Getter, Setter가 들어간다면, Pascal Case에서 언더바(_)를 붙임.
        /// </summary>
        public int Prime_;

        public PrimeCallBackArg(int prime)
        {
            this.Prime_ = prime;
        }
    }

    class PrimeGenerator
    {
        public delegate void PrimeDelegate(object sender, CallbackArg arg);

        PrimeDelegate callbacks;

        public void AddDelegate(PrimeDelegate callback)
        {
            callbacks = Delegate.Combine(callbacks, callback) as PrimeDelegate;
        }

        public void RemoveDelegate(PrimeDelegate callback)
        {
            callbacks = Delegate.Remove(callbacks, callback) as PrimeDelegate;
        }

        public void Run(int limit)
        {
            for (int i = 2; i <= limit; i++)
            {
                if (IsPrime(i) == true && callbacks != null)
                {
                    callbacks(this, new PrimeCallBackArg(i));
                }
            }
        }

        private bool IsPrime(int candidate)
        {
            //짝수 일때,
            if ((candidate & 1) == 0)
            {
                return candidate == 2;
            }

            // 3에서 시작하는 홀수들을 검사함.
            for (int i = 3; (i * i) < candidate; i += 2)
            {
                //나누어 떨어지는 값이 있다면, 소수가 아님.
                if ((candidate % i) == 0) return false;
            }

            // 위의 조건들을 통과한 수들중에 1은 절대로 소수가 될수가 없기때문에 걸러줌.
            return candidate != 1;
        }

    }

    class Program
    {
        static void PrintPrime(object sender, CallbackArg arg)
        {
            Console.Write((arg as PrimeCallBackArg).Prime_ + ", ");
        }

        static int Sum;

        static void SumPrime(object sender, CallbackArg arg)
        {
            Sum += (arg as PrimeCallBackArg).Prime_;
        }

        static void Main(string[] args)
        {
            PrimeGenerator gen = new PrimeGenerator();

            PrimeGenerator.PrimeDelegate callprint = PrintPrime;
            gen.AddDelegate(callprint);

            PrimeGenerator.PrimeDelegate callsum = SumPrime;
            gen.AddDelegate(callsum);

            gen.Run(10);
            Console.WriteLine();
            Console.WriteLine(Sum);

            gen.RemoveDelegate(callsum);
            gen.Run(15);
        }
    }
}
