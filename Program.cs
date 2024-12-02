using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Var58_Task6
{
    internal class Program
    {
        struct Price
        {
            public string product_name;
            public string shop_name;
            public double product_price;

            public void GetProductString()
            {
                Console.WriteLine($"{product_name} доступен в {shop_name} за {product_price} у.е.");
            }
        }

        static void Main()
        {
            var catalog = new List<Price> { };

            //  формируем путь файла каталога
            //  спрашиваем имя файла
            Console.Write("Введите имя файла каталога с расширением, файл должен быть в корне: ");
            string file_name = Console.ReadLine();
            string file_path = $"..\\..\\{file_name}";

            //  проверяем существование файла
            //  если файл есть, идем парсить файл
            //  если файла нет, запрашиваем ручной ввод
            if (File.Exists(file_path))
            {
                Console.WriteLine("Файл каталога найден\n");
                catalog = GetFromFile(file_path);
            }
            else
            {
                Console.WriteLine("Файл каталога не найден\n");

                //  вызов ручного заполнения каталога
                AddByHand(catalog);
            }

            Console.WriteLine("Введите 'выход', чтобы прекратить поиск");

            //  обрабатываем бесконечный поиск
            string nom_name = "";
            while (nom_name.ToLower() != "выход")
            {
                Console.Write("Введите наименование из каталога, чтобы получить информацию: ");
                nom_name = Console.ReadLine();
                
                if (nom_name != "выход")
                    FindProductByName(nom_name, catalog);
            }            
        }

        static List<Price> GetFromFile(string file_path)
        {
            //  param:
            //  string file_path - путь к файлу
            //  return:
            //  List<Price> - список структур (каталог)

            //  список массивов с координатами из файла
            var catalog = new List<Price> { };

            //  переменная под разделитель, если разделитель вдруг изменится
            string splitter = ", ";

            try
            {
                //  через менеджер открываем файл на чтение
                using (StreamReader sr = new StreamReader(file_path))
                {
                    string line;

                    //  читаем строки файла
                    while ((line = sr.ReadLine()) != null)
                    {
                        //  получаем массив - будущий объект структуры
                        string[] line_split;
                        line_split = line.Split(new string[] { splitter }, StringSplitOptions.None);

                        //  создаём экземпляр структуры
                        var Product = new Price();
                        Product.product_name = line_split[0];
                        Product.shop_name = line_split[1];
                        Product.product_price = Convert.ToDouble(line_split[2]);

                        //  добавляем массив в список всех координат
                        catalog.Add(Product);
                    }
                }
            }
            //  обрабатываем выкидыши
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return catalog;
        }

        static void FindProductByName(string nom_name, List<Price> catalog)
        {
            //  param:
            //  string nom_name - искомое наименование
            //  List<Price> catalog - каталог

            //  флаг, существует ли товар
            bool IsExist = false;

            //  проверяем все экземепляры структуры на совпадение наименования
            foreach (var product in catalog)
            {
                if (product.product_name == nom_name)
                { 
                    product.GetProductString();
                    IsExist = true; 
                    break;
                }
            }

            //  если ничего не нашли
            if (!IsExist)
                Console.WriteLine($"Товар {nom_name} не существует");
        }

        static void AddByHand(List<Price> catalog)
        {
            //  param:
            //  List<Price> catalog - каталог

            //  создаем структуру и добавляем её в каталог
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine($"Товар {i+1}");

                var product = new Price();
                Console.Write("Наименование - ");
                product.product_name = Console.ReadLine();
                Console.Write("Магазин - ");
                product.shop_name = Console.ReadLine();
                Console.Write("Цена - ");

                try
                {
                    product.product_price = Convert.ToDouble(Console.ReadLine());
                }
                //  устанавливаем 0 цену, если ошибка ввода
                catch (Exception)
                {
                    product.product_price = 0;
                }

                catalog.Add(product);   
            }
        }
    }
}