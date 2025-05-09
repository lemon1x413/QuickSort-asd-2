namespace QuickSort_asd_2;


class Program
{
    static int compares = 0;

    static bool isSorted(int[] A)
    {
        for (int i = 0; i < A.Length - 1; i++)
        {
            if (A[i] > A[i + 1])
                return false;
        }
        return true;
    }
    static void QuickSort(int[] A, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(A, left, right);
            QuickSort(A, left, pivot - 1);
            QuickSort(A, pivot + 1, right);
        }
    }

    static int Partition(int[] A, int left, int right)
    {
        int x = A[right];
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            compares++;
            if (A[j] <= x)
            {
                i++;
                (A[i], A[j]) = (A[j], A[i]);
            }
        }

        (A[i + 1], A[right]) = (A[right], A[i + 1]);
        return i + 1;
    }

    static void QuickSortMedian(int[] A, int left, int right)
    {
        if (right - left + 1 <= 3)
        {
            ManualSort(A, left, right);
            return;
        }

        if (left < right)
        {
            int median = Median(A, left, right);
            (A[median], A[right]) = (A[right], A[median]);
            int pivot = Partition(A, left, right);
            QuickSortMedian(A, left, pivot - 1);
            QuickSortMedian(A, pivot + 1, right);
        }
    }

    static int Median(int[] A, int left, int right)
    {
        int a = A[left];
        int b = A[(left + right) / 2];
        int c = A[right];
        
        if ((a <= b && b <= c) || (c <= b && b <= a))
            return (left + right) / 2;
        if ((b <= a && a <= c) || (c <= a && a <= b))
            return left;
        return right;
    }

    static void ManualSort(int[] A, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int key = A[i];
            int j = i - 1;
            while (j >= left)
            {
                compares++;
                if (A[j] > key)
                {
                    A[j + 1] = A[j];
                    j--;
                }
                else
                {
                    break;
                }
            }
            
            A[j + 1] = key;
        }
    }

    static void QuickSort3Pivot(int[] A, int left, int right)
    {
        if (left >= right) return;
        if (right - left + 1 <= 3)
        {
            ManualSort(A, left, right);
            return;
        }

        (int p, int q, int r) = Partition3Pivot(A, left, right);
        QuickSort3Pivot(A, left, p - 1);
        QuickSort3Pivot(A, p + 1, q - 1);
        QuickSort3Pivot(A, q + 1, r - 1);
        QuickSort3Pivot(A, r + 1, right);
    }

    static (int p, int q, int r) Partition3Pivot(int[] A, int left, int right)
    {
        compares++;
        if (A[left] > A[right])
            (A[left], A[right]) = (A[right], A[left]);
        compares++;
        if (A[left + 1] > A[right])
            (A[left + 1], A[right]) = (A[right], A[left + 1]);
        compares++;
        if (A[left] > A[left + 1])
            (A[left], A[left + 1]) = (A[left + 1], A[left]);
        
        int a = left + 2, b = left + 2, c = right - 1, d = right - 1;
        int p = A[left], q = A[left + 1], r = A[right];
        while (b <= c)
        {
            
            while (A[b] < q && b <= c)
            {
                compares++;
                compares++;
                if (A[b] < p)
                {
                    (A[a], A[b]) = (A[b], A[a]);
                    a++;
                }

                b++;
            }
            while (A[c] > q && b <= c)
            {
                compares++;
                compares++;
                if (A[c] > r)
                {
                    (A[d], A[c]) = (A[c], A[d]);
                    d--;
                }

                c--;
            }

            compares++;
            if (b <= c)
            {
                compares++;
                if (A[b] > r)
                {
                    compares++;
                    if (A[c] < p)
                    {
                        (A[a], A[b]) = (A[b], A[a]);
                        (A[a], A[c]) = (A[c], A[a]);
                        a++;
                    }
                    else
                        (A[c], A[b]) = (A[b], A[c]);

                    (A[d], A[c]) = (A[c], A[d]);
                    d--;
                    c--;
                    b++;
                }
                else
                {
                    compares++;
                    if (A[c] < p)
                    {
                        
                        (A[a], A[b]) = (A[b], A[a]);
                        (A[a], A[c]) = (A[c], A[a]);
                        a++;
                    }
                    else
                        (A[c], A[b]) = (A[b], A[c]);

                    b++;
                    c--;
                }
            }
        }
        compares++;
        a--;
        b--;
        c++;
        d++;
        (A[left + 1], A[a]) = (A[a], A[left + 1]);
        (A[a], A[b]) = (A[b], A[a]);
        a--;
        (A[left], A[a]) = (A[a], A[left]);
        (A[right], A[d]) = (A[d], A[right]);
        return (a, b, d);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Enter input file name.");
        string fileName = Console.ReadLine();
        string[] line = File.ReadAllLines(fileName + ".txt");
        int[] A = new int[int.Parse(line[0])];
        int[] B = new int[int.Parse(line[0])];
        int[] C = new int[int.Parse(line[0])];
        for (int i = 1; i < line.Length; i++)
        {
            A[i - 1] = int.Parse(line[i]);
            B[i - 1] = int.Parse(line[i]);
            C[i - 1] = int.Parse(line[i]);
        }

        QuickSort(A, 0, A.Length - 1);
        int comparesDefault = compares;
        Console.WriteLine("QuickSort:" + compares);
        if (isSorted(A))
        {
            Console.WriteLine("Sorted");
        }
        else
        {
            foreach (var a in A)
            {
                Console.WriteLine(a);
            }
        }
        
        compares = 0;
        QuickSortMedian(B, 0, B.Length - 1);
        int comparesMedian = compares;
        Console.WriteLine("QuickSortMedian:" + compares);
        if (isSorted(B))
        {
            Console.WriteLine("Sorted");
        }
        else
        {
            foreach (var b in B)
            {
                Console.WriteLine(b);
            }
        }

        compares = 0;
        QuickSort3Pivot(C, 0, C.Length - 1);
        int compares3Pivot = compares;
        Console.WriteLine("QuickSort3Pivot:" + compares);
        if (isSorted(C))
        {
            Console.WriteLine("Sorted");
        }
        else
        {
            foreach (var c in C)
            {
                Console.WriteLine(c);
            }
        }

        using StreamWriter writer = new StreamWriter("output" + fileName + ".txt");
        writer.WriteLine(comparesDefault + " " + comparesMedian + " " + compares3Pivot);
    }
}