namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = (int)Math.Pow(2, 15);
            byte[] X = new byte[N];//{ 9, 9, 9, 9, 9 };
            for (int i = 0; i < N; i++) X[i] = 9;

            //byte[] Y = { 9, 9, 9, 9, 9 };

            byte[] z = IntegerMultiply(X, X, N);//Add(X4, Y4)
                                                //Array.Reverse(z);
                                                //foreach (byte x in z)
                                                //Console.Write(x + " ");

            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");


        }

        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            if (X.Length < Y.Length)
            {
                byte[] Xtmp = new byte[N];
                for (int i = 0; i < N - 1; i++)
                {
                    Xtmp[i] = X[i];
                }
                Xtmp[N - 1] = 0;
                X = Xtmp;
            }

            if (X.Length > Y.Length)
            {
                byte[] Ytmp = new byte[N];
                for (int i = 0; i < N - 1; i++)
                {
                    Ytmp[i] = Y[i];
                }
                Ytmp[N - 1] = 0;
                Y = Ytmp;
            }
            if (N > 0)
            {
                byte[] result = new byte[2 * N];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        result[i + j] += (byte)(X[i] * Y[j]);
                        result[i + j + 1] += (byte)(result[i + j] / 10);
                        result[i + j] %= (byte)10;
                    }
                }
                return result;
            }

            int m = N / 2;
            byte[] B = new byte[m + N % 2];
            byte[] D = new byte[m + N % 2];
            if (N % 2 == 1)
            {
                N++;
                m = N / 2;
                for (int i = 0; i < m - 1; i++)
                {
                    B[i] = X[i + m];
                    D[i] = Y[i + m];
                }
                B[m - 1] = 0;
                D[m - 1] = 0;

            }
            else
            {
                B = X.Skip(m).ToArray();
                D = Y.Skip(m).ToArray();
            }
            byte[] A = X.Take(m).ToArray();
            byte[] C = Y.Take(m).ToArray();

            // recursively compute the three products
            byte[] M1 = IntegerMultiply(A, C, Math.Max(A.Length, C.Length));
            byte[] M2 = IntegerMultiply(B, D, Math.Max(B.Length, D.Length));
            byte[] khkh = Add(A, B);
            byte[] lklk = Add(C, D);
            byte[] Z = IntegerMultiply(khkh, lklk, Math.Max(khkh.Length, lklk.Length));
            //3 terms
            //1st term : 10^N*M2
            //byte[] term1 = AddZeros(M2, N - m);
            //int aaaa = 0;
            //if (A.Length != B.Length) aaaa = m;
            byte[] term1 = AddZeros(M2, N);
            //2nd term : 10^N/2 × (Z – M1 – M2) 
            byte[] subterm2 = Add(M1, M2);
            byte[] term2 = AddZeros(Subtract(Z, subterm2), m);
            //1st term : M1
            // add all the 3 terms
            byte[] res = Add(Add(term1, term2), M1);

            return res;


        }
        //byte sum = (byte)(x[i] + y[i] + carry);
        //result[i] = (byte) (sum % 10);
        //        carry = (byte) (sum / 10);

        private static byte[] Add(byte[] x, byte[] y)
        {

            int n = x.Length;
            int m = y.Length;
            int max;
            if (n > m)
                max = n;
            else
                max = m;
            byte[] z = new byte[max + 1];
            if (n > m)
            {
                for (int i = 0; i < m; i++)
                {
                    z[i] += (byte)(x[i] + y[i]);
                    z[i + 1] += (byte)(z[i] / 10);//carry
                    z[i] %= 10;//sum
                }
                for (int i = m; i < n; i++)
                {
                    z[i] += (byte)(x[i]);
                    z[i + 1] += (byte)(z[i] / 10);//carry
                    z[i] %= 10;//sum
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    z[i] += (byte)(x[i] + y[i]);
                    z[i + 1] += (byte)(z[i] / 10);//carry
                    z[i] %= 10;//sum
                }
                for (int i = n; i < m; i++)
                {
                    z[i] += (byte)(y[i]);
                    z[i + 1] += (byte)(z[i] / 10);//carry
                    z[i] %= 10;//sum
                }
            }

            if (z[n] == 1)
                return z;
            else
                return z.Take(n).ToArray();

        }
        private static byte[] Subtract(byte[] x, byte[] y)
        {
            int[] xInt = BytesToIntegers(x);
            int[] yInt = BytesToIntegers(y);
            int n = yInt.Length;
            int m = xInt.Length;
            byte[] z = new byte[m];
            int temp = 0;

            for (int i = 0; i < n; i++)
            {
                temp = (xInt[i] - yInt[i]);
                if (temp < 0)
                {
                    temp += 10;
                    xInt[i + 1]--;
                }
                z[i] = (byte)(temp);
            }
            for (int i = n; i < m; i++)
            {
                z[i] = (byte)xInt[i];
            }

            return z;
        }
        private static byte[] AddZeros(byte[] arr, int n)
        {
            byte[] result = new byte[arr.Length + n];
            for (int i = 0; i < n; i++)
            {
                result[i] = 0;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                result[n + i] = arr[i];
            }
            return result;
        }
        public static int[] BytesToIntegers(byte[] bytes)
        {
            int n = bytes.Length;
            int[] integers = new int[n];

            for (int i = 0; i < n; i++)
            {
                integers[i] = (int)(bytes[i]);
            }

            return integers;
        }
    }

}