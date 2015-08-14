using System;

namespace Task3
{
    class WalkInMatrica
    {
        static void Change(ref int dx, ref int dy)
        {

            int[] directionByX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] directionByY = { 1, 0, -1, -1, -1, 0, 1, 1 };

            int serialNumber = 0;

            for (int count = 0; count < 8; count++)
            {
                if (directionByX[count] == dx && directionByY[count] == dy) 
                { 
                    serialNumber = count;
                    break; 
                }
            }

            if (serialNumber == 7)
            { 
                dx = directionByX[0];
                dy = directionByY[0];
                return; 
            }

            dx = directionByX[serialNumber + 1];
            dy = directionByY[serialNumber + 1];
        }

        static bool CheckCell(int[,] arr, int x, int y)
        {
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (x + dirX[i] >= arr.GetLength(0) || x + dirX[i] < 0)
                {
                    dirX[i] = 0;
                }

                if (y + dirY[i] >= arr.GetLength(0) || y + dirY[i] < 0)
                {
                    dirY[i] = 0;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (arr[x + dirX[i], y + dirY[i]] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        static void FindCell(int[,] arr, out int x, out int y)
        {
            x = 0;
            y = 0;

            for (int i = 0; i < arr.GetLength(0); i++)
            {

                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (arr[i, j] == 0) 
                    {
                        x = i;
                        y = j;
                        return; 
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a posistive number in range [0 - 50] :");
            string input = Console.ReadLine();
            int number;

            while(!int.TryParse(input, out number))
            {
                if(number < 1 || number > 50)
                {
                    Console.WriteLine("Please enter a correct positive number!");
                }
            }

            int[,] matrix = new int[number, number];
            int step = number;
            int value = 1;
            int row = 0;
            int column = 0;
            int directionX = 1;
            int directionY = 1;

            while (true)
            { 
                //malko e kofti tova uslovie, no break-a raboti 100% : )
                matrix[row, column] = value;

                if (!CheckCell(matrix, row, column)) 
                { 
                    break; 
                } 
                // prekusvame ako sme se zadunili

                bool outOfBorders = row + directionX >= number
                    || row + directionX < 0
                    || column + directionY >= number
                    || column + directionY < 0
                    || matrix[row + directionX, column + directionY] != 0;

                if (outOfBorders)
                {
                    while ((row + directionX >= number 
                        || row + directionX < 0 
                        || column + directionY >= number 
                        || column + directionY < 0 
                        || matrix[row + directionX, column + directionY] != 0))
                    {
                        Change(ref directionX, ref directionY);
                    }
                }

                row += directionX;
                column += directionY;
                value++;
            }

            FindCell(matrix, out row, out column);

            if (row != 0 && column != 0)
            { 
                // taka go napravih, zashtoto funkciqta ne mi davashe da ne si definiram out parametrite

                directionX = 1;
                directionY = 1;

                while (true)                    
                {
                    //malko e kofti tova uslovie, no break-a raboti 100% : )

                    matrix[row, column] = value;

                    if (!CheckCell(matrix, row, column))
                    {
                        break;  // prekusvame ako sme se zadunili
                    }

                    bool outOfBorders = row + directionX >= number
                        || row + directionX < 0
                        || column + directionY >= number
                        || column + directionY < 0
                        || matrix[row + directionX, column + directionY] != 0;
                    if (outOfBorders)
                    {
                        while ((row + directionX >= number 
                            || row + directionX < 0 
                            || column + directionY >= number 
                            || column + directionY < 0 
                            || matrix[row + directionX, column + directionY] != 0))
                        {
                            Change(ref directionX, ref directionY);
                        }
                    }

                    row += directionX;
                    column += directionY;
                    value++;
                }
            }

            for (int printRow = 0; printRow < number; printRow++)
            {
                for (int prinColumn = 0; prinColumn < number; prinColumn++)
                {
                    Console.Write("{0,3}", matrix[printRow, prinColumn]);
                }

                Console.WriteLine();
            }
        }
    }
}
