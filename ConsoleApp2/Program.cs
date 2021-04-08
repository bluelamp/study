using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Mathematics
{
    /// <summary>
    /// 예약어 delegate로 함수형 대리자를 미리 정의한다.
    /// </summary>
    delegate int CalcDelegate(int x, int y);

    /// <summary>
    /// 정의된 양식의 함수를 대리자배열로 입력하기 위해 미리 초기화.
    /// </summary>
    CalcDelegate[] methods;

    static int Add(int x, int y) { return x + y; }
    static int Subtract(int x, int y) { return x - y; }
    static int Multiply(int x, int y) { return x * y; }
    static int Divide(int x, int y) { return x / y; }

    /// <summary>
    /// 생성자, 해당 클래스가 생성되자마자 실행됨.
    /// </summary>
    public Mathematics()
    {
        methods = new CalcDelegate[] { Mathematics.Add, Mathematics.Subtract, Mathematics.Multiply, Mathematics.Divide };
    }

    public void Calculate(char opCode, int operand1, int operand2)
    {
        switch (opCode)
        {
            case '+':
                Console.WriteLine("{0} + {1} = {2}", operand1, operand2, methods[0](operand1, operand2));
                break;
            case '-':
                Console.WriteLine("{0} - {1} = {2}", operand1, operand2, methods[1](operand1, operand2));
                break;
            case '*':
                Console.WriteLine("{0} X {1} = {2}", operand1, operand2, methods[2](operand1, operand2));
                break;
            case '/':
                Console.WriteLine("{0} / {1} = {2}", operand1, operand2, methods[3](operand1, operand2));
                break;
            default:
                Console.WriteLine("실행코드가 잘못되었습니다.");
                break;
        }
    }

}


namespace ConsoleApp2
{
    class Program
    {
        /// <summary>
        /// 대리자 형태 정의.
        /// </summary>
        delegate void WorkDelegate(char arg1, int arg2, int arg3);

        static void Main(string[] args)
        {
            Mathematics math = new Mathematics();
            // 대리자에 해당 메서드를 직접 전달한다.
            WorkDelegate work = math.Calculate;

            char OpCode = new char();
            Console.WriteLine("연산을 위해서 다음 부호를 입력해주세요. [\"+,-,*,/\"]");
            //정상적인 부호가 입력될때까지 Loop
            while (false == IsOpCode(Console.ReadKey(), ref OpCode)) {
                Console.WriteLine();
                Console.WriteLine("부호를 다시 입력해주세요. [\"+,-,*,/\"]. 당신이 입력한 값 {0}", OpCode);
            }
            Console.WriteLine();

            int arg2;
            Console.WriteLine("연산을 위한 첫번째 수를 입력해주세요.");
            while (false == int.TryParse(Console.ReadLine(), out arg2))
            {
                Console.WriteLine("연산을 위한 첫번째 수를 다시 입력해주세요. 당신이 입력한 값 {0}", arg2);
            }

            int arg3;
            Console.WriteLine("연산을 위한 두번째 수를 입력해주세요.");
            while (false == int.TryParse(Console.ReadLine(), out arg3))
            {
                Console.WriteLine("연산을 위한 두번째 수를 다시 입력해주세요. 당신이 입력한 값 {0}", arg3);
            }

            work(OpCode, arg2, arg3);

            Console.WriteLine("아무 입력이나 누르면 프로그램을 종료됩니다.");
            Console.ReadKey();
        }

        /// <summary>
        /// 사용자입력이 부호가 맞는지 확인하는 함수
        /// </summary>
        /// <param name="key">사용자의 입력값</param>
        /// <param name="output">사용자의 입력값에서 char 변환.</param>
        /// <returns>부호가 맞는지 아닌지 bool형으로 체크</returns>
        static bool IsOpCode(ConsoleKeyInfo key, ref char output)
        {
            output = key.KeyChar;
            bool result;
            switch (output)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
    }
}
