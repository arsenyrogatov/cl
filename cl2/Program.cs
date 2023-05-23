int[,] matrix1 = new int[3, 3] { 
    { 1, 2, 3 }, 
    { 4, 5, 6 }, 
    { 7, 8, 9 } 
};
Matrix m1 = new(matrix1);
m1.Print();
int[] result1 = m1.Spiral();
Console.WriteLine($"spiral1: {String.Join(" ", result1)}");

Console.WriteLine();

int[,] matrix2 = new int[3, 4] { 
    { 1, 2, 3, 4 }, 
    { 5, 6, 7, 8 }, 
    { 9, 10, 11, 12 } 
};
Matrix m2 = new(matrix2);
m2.Print();
int[] result2 = m2.Spiral();
Console.WriteLine($"spiral2: {String.Join(" ", result2)}");

Console.WriteLine();

Matrix m3 = new(4,3);
m3.Print();
int[] result3 = m3.Spiral();
Console.WriteLine($"spiral3: {String.Join(" ", result3)}");

Console.WriteLine();

Matrix m4 = new(10, 3);
m4.Print();
int[] result4 = m4.Spiral();
Console.WriteLine($"spiral4: {String.Join(" ", result4)}");

class Matrix //класс матрицы
{
    int[,] matrix;
    int rows, columns;

    public Matrix(int rows, int columns) //генерируем по количеству строк и столбцов
    {
        this.rows = rows;
        this.columns = columns;
        matrix = new int[rows, columns];
        Generate();
    }

    public Matrix(int[,] matrix) //копируем из исходной матрицы
    {
        rows = matrix.GetLength(0);
        columns = matrix.GetLength(1);

        this.matrix = new int[rows, columns];
        Array.Copy(matrix, this.matrix, rows * columns);
    }

    public void Generate()
    {
        int value = 1;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = value;
                value++;
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public int[] Spiral() //читаем спиралью
    {
        int resultCount = rows * columns;
        int[] result = new int[resultCount];

        int top = 0;
        int bottom = rows - 1;
        int left = 0;
        int right = columns - 1;

        int index = 0;

        while (top <= bottom && left <= right)
        {
            for (int row = top; row <= bottom; row++) //левый столбец вниз
            {
                result[index] = matrix[row, left];
                index++;
            }
            left++;

            for (int col = left; col <= right; col++) //нижний столбец вправо
            {
                result[index] = matrix[bottom, col];
                index++;
            }
            bottom--;

            if (top <= bottom && index < resultCount)
            {
                for (int row = bottom; row >= top; row--) //правый столбец вверх
                {
                    result[index] = matrix[row, right];
                    index++;
                }
                right--;
            }

            if (left <= right && index < resultCount) //верхний столбец влево
            {
                for (int col = right; col >= left; col--)
                {
                    result[index] = matrix[top, col];
                    index++;
                }
                top++;
            }
        }

        return result;
    }
}