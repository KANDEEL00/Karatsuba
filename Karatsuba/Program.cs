namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] X = { 9, 9, 9, 9, 9 };
            byte[] Y = { 9, 9, 9, 9, 9 };

            byte[] z = IntegerMultiply(X, Y, X.Length);//Add(X4, Y4)
            Array.Reverse(z);
            foreach (byte x in z)
                Console.Write(x + " ");


        }

        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            if (N == 1)
            {
                byte[] result = new byte[2];
                int bcase = 0;
                if (Y.Length * X.Length != 0)
                    bcase = (X[0] * Y[0]);
                result[0] = (byte)(bcase % 10);
                result[1] = (byte)(bcase / 10);
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