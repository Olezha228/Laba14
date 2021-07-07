using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labar10;

namespace laba14
{
    class Program
    {
        static void Menu1()
        {
            Console.WriteLine("=============== Меню ===============");
            Console.WriteLine("1. Обновить стек.");
            Console.WriteLine("2. Вывести тех, чье имя длинне 10.");
            Console.WriteLine("3. Вывести тех, чей возраст больше 22.");
            Console.WriteLine("4. Вывести 'разность' возрастов.");
            Console.WriteLine("5. Вывести человека с максимальным возрастом.");
            Console.WriteLine("6. Группировать по возрасту.");
            Console.WriteLine("7. Вывести словарь.");
            Console.WriteLine("8. Выйти.");
        }

        static Dictionary<string, Stack<Person>> CreateDict()
        {
            Dictionary<string, Stack<Person>> dict = new Dictionary<string, Stack<Person>>();

            for (int i = 0; i < 5; i++)
            {
                var stack = new Stack<Person>();
                dict.Add($"Group{i + 1}", stack);
                int n = (new Random()).Next(4, 8);
                for (int k = 0; k < n; k++)
                {
                    if (k % 3 == 0)
                        stack.Push((Student)(new Student()).Init());
                    if (k % 3 == 1)
                        stack.Push((CorrespondenceStudent)(new CorrespondenceStudent()).Init());
                    else
                        stack.Push((Person)(new Person()).Init());
                }
            }

            return dict;
        }

        static void PrintDict(Dictionary<string, Stack<Person>> group)
        {
            foreach(var pair in group)
            {
                Console.WriteLine($"{pair.Key}");
                foreach (var val in pair.Value)
                    Console.WriteLine(val);
            }
        }


        static void NameLongerThanTen(Dictionary<string, Stack<Person>> diction)
        {
            var nameLonger4 = from pair in diction
                              from k in pair.Value
                              where k.name.Length > 10
                              orderby k
                              select k;

            var nameLonger = diction.SelectMany(x => x.Value)
                                    .Where(x => x.name.Length > 10)
                                    .OrderBy(x => x)
                                    .Select(x => x);

            foreach (var el in nameLonger)
                Console.WriteLine(el);
        }

        static void CountOlderThan22(Dictionary<string, Stack<Person>> diction)
        {
            var OlderThan22 = (from pair in diction
                               from k in pair.Value
                               where k.Age > 22
                               select k).Count<Person>();

            var OlderThan = diction.SelectMany(x => x.Value)
                                   .Where(x => x.Age > 22)
                                   .Select(x => x)
                                   .Count();

            Console.WriteLine(OlderThan22);
            Console.WriteLine(OlderThan);
        }

        static void DictDifference(Dictionary<string, Stack<Person>> diction)
        {
            Console.WriteLine("Второй словарь:");
            Dictionary<string, Stack<Person>> dict2= CreateDict();
            PrintDict(dict2);
            var dictDif = (from pair in diction
                           from p in pair.Value
                           select p.Age)
                           .Except
                           (from pair in dict2 
                            from s in pair.Value
                            select s.Age);

            foreach (var d in dictDif)
                Console.WriteLine(d);
        }

        static void AgregationMaxAged(Dictionary<string, Stack<Person>> diction)
        {
            var maxAged = (from pair in diction
                           from p in pair.Value
                           select p)
                           .Max();

            var maxAge = diction.SelectMany(x => x.Value)
                                .Select(x => x)
                                .Max();

            Console.WriteLine(maxAge);
            Console.WriteLine(maxAged);
        }

        static void Grouping(Dictionary<string, Stack<Person>> diction)
        {
            var gropingByAge = from pair in diction
                               from p in pair.Value
                               orderby p.name
                               group p by p.Age;

            foreach (IGrouping<int, Person> p in gropingByAge)
            {
                Console.WriteLine($"Возраст {p.Key}: ");
                foreach (var val in p)
                    Console.WriteLine(val);
            }

        }

        static void Main(string[] args)
        {
            #region
            Dictionary<string, Stack<Person>> dict = CreateDict();

            Console.WriteLine("------------------------------------------Имя");
            Grouping(dict);

            bool moveNext = false;
            while (!moveNext)
            {
                Menu1();
                int i = CheckInput.ParseInt("Введите выбранный пункт: ");
                switch (i)
                {
                    case 1:
                        dict = CreateDict();
                        break;
                    case 2:
                        NameLongerThanTen(dict);
                        break;
                    case 3:
                        CountOlderThan22(dict);
                        break;
                    case 4:
                        DictDifference(dict);
                        break;
                    case 5:
                        AgregationMaxAged(dict);
                        break;
                    case 6:
                        Grouping(dict);
                        break;
                    case 7:
                        PrintDict(dict);
                        break;
                    case 8:
                        moveNext = true;
                        break;
                    default:
                        Console.WriteLine("Такого пункта нет!");
                        break;
                }
            }
            #endregion

        }
    }
}
