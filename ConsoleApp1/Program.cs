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
        /// <summary>
        /// 각각 소수값들을 출력하는 함수.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        static void PrintPrime(object sender, CallbackArg arg)
        {
            Console.Write((arg as PrimeCallBackArg).Prime_ + ", ");
        }

        static int Sum;

        /// <summary>
        /// 서브기능으로 모든 소수들의 값을 합치는 함수입니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        static void SumPrime(object sender, CallbackArg arg)
        {
            Sum += (arg as PrimeCallBackArg).Prime_;
        }

        /// <summary>
        /// 프로그램 메인
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            PrimeGenerator gen = new PrimeGenerator();

            /// 대리자함수로 등록하는 과정. 이벤트가 발생할때, 해당 함수가 실행될 것임.
            PrimeGenerator.PrimeDelegate callprint = PrintPrime;
            gen.AddDelegate(callprint);
            PrimeGenerator.PrimeDelegate callsum = SumPrime;
            gen.AddDelegate(callsum);

            // 사용자의 콘솔입력을 받아서 실행됨. 숫자가 아닌 잘못된 값을 받았을때 계속 Loop됨.
            //Console.WriteLine("범위값을 설정하여 주십시오.");
            string input = string.Empty;
            int num;
            while(int.TryParse(input, out num) == false) {
                Console.WriteLine("범위값을 설정하여 주십시오.");
                input = Console.ReadLine();
            }

            Console.WriteLine("0~{0} 사이의 소수들을 구합니다.",num);
            gen.Run(num);

            Console.WriteLine();
            Console.WriteLine("모든 소스들의 합은 {0} 입니다.", Sum);

            // SumPrime 대리자 함수등록를 해지합니다. 더이상 이 이벤트발생시, 해당함수가 실행되지 않습니다. PrintPrime 대리자는 아직 남아있어 그대로 작동합니다.
            gen.RemoveDelegate(callsum);
            gen.Run(99);
            Console.WriteLine();
            Console.Write("모든 소스들의 합은 ");
            Console.WriteLine("{0} 입니다.", Sum);

            // 아무키나 입력하게 되면, 프로그램을 종료합니다.
            Console.WriteLine();
            Console.WriteLine("프로그램을 끝냅니다.");
            Console.ReadLine();
        }
    }
}
