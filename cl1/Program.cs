using System.Text;

string input = "aaabbcccdde";
string compressed = StringCompress.Compress(input);
Console.WriteLine($"Компрессия {input}: {compressed}");
string decompressed = StringCompress.Decompress(compressed);
Console.WriteLine($"Декомпрессия {compressed}: {decompressed}");

class StringCompress
{
    public static string Compress (string input) //компрессия 
    {
        StringBuilder compressedString = new();
        int count = 1;

        for (int i = 1; i <= input.Length; i++) //идем по входной строке
        {
            if (i < input.Length && input[i] == input[i-1]) //если текущая совпадает с предыдущей
            {
                count++; //увеличиваем счетчик
            }
            else //в обратном случае
            {
                compressedString.Append(input[i-1]); //записываем предыдущий символ
                if (count > 1)
                {
                    compressedString.Append(count); //и его количество
                }
                count = 1;
            }
        }
        return compressedString.ToString();
    }

    public static string Decompress(string input) //декомпрессия
    {
        StringBuilder decompressedString = new();
        int index = 0;

        while (index < input.Length) //идем по входной строке
        {
            char currentChar = input[index];
            decompressedString.Append(currentChar); //добавляем текущий символ
            index++; //увеличиваем индекс

             if (index < input.Length && char.IsDigit(input[index])) //если индекс не за пределами строки и текущий символ - число
            {
                int count = int.Parse(input[index].ToString()); //получаем число
                decompressedString.Append(new string(currentChar, count-1)); //добавляем currentChar (count-1) раз
                index++; //переходим к следующему символу
            }
        }
        return decompressedString.ToString();
    }
}