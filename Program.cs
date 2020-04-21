using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Art_of_battle.Model;

namespace Art_of_battle
{

    public interface Test
    {
        void Attack();
    }

    public class TestClass : Test
    {
        private string Name = "hELLO";

        public void SomeNewMethod()
        {
            Console.WriteLine("Try it out");
        }
        public void Attack()
        {
            SomeNewMethod();
            Console.WriteLine(Name);
        }
    }

    public class AnotherTestClass : Test
    {
        private string H = "Hshjdhsj";
        public void Attack()
        {
            Console.WriteLine(H);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Test>() {new TestClass(), new AnotherTestClass()};
            list[0].Attack();
            list[1].Attack();
        }
    }
}
