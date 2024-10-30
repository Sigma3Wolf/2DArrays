using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DArray {
    internal class Program {
        //https://www.bytehide.com/blog/2d-arrays-csharp#:~:text=Using%20Multidimensional%20Array%20Methods%20in%20C%23&text=GetLength(0)%20gets%20the%20length,through%20the%20maze%20of%20arrays!
        private static bool gblnDebug = true;      //<<<<<<<<<<<<<<<<<<<<<<<<< Change for WatchDog timer
        
        static void Main(string[] args) {
            //let's make a 2D array in Row and Column using GetLenght and one using Virtual Access
            //Goal: Calculate wich one is faster
            Stopwatch watch = new System.Diagnostics.Stopwatch();

            int xMult;
            if (gblnDebug) {
                //NE PAS METTRE PLUS DE 10 / DO NOT PUT MORE THEN 10
                xMult = 1;
            } else {
                //NE PAS MODIFIER CETTE LIGNE / DO NOT MODIFY THIS LINE
                xMult = 2500;
            }
            int lngMaxY = 2 * xMult;
            int lngMaxX = 3 * xMult;

            string strLine = "";
            string strDataX = "";
            int lngY = 0;
            int lngX = 0;
            int lngOldY = 0;
            int lngMaxLength = (lngMaxY * lngMaxX);

            //Initialize Array
            //string[,] T1 = new string[lngMaxY, lngMaxX] {
            //    {"1x1", "1x2", "1x3" },
            //    {"2x1", "2x2", "2x3" }
            //};
            //string[] T2 = new string[lngMaxY * lngMaxX] {"1x1", "1x2", "1x3", "2x1", "2x2", "2x3" };

            Console.WriteLine("WAIT FOR INITIALIZATION TO COMPLETE...");
            string[,] T1 = new string[lngMaxY, lngMaxX];
            string[] T2 = new string[lngMaxY * lngMaxX];
            string strData = "";
            for (lngY = 0; lngY < lngMaxY; lngY++) {
                for (lngX = 0; lngX < lngMaxX; lngX++) {
                    strData = (lngY + 1).ToString() + "x" + (lngX + 1).ToString();
                    T1[lngY, lngX] = strData;

                    int Indice = (lngY * lngMaxX) + lngX;
                    T2[Indice] = strData;
                }
            }
            //Initialization completed

            //Test Menu
            int lngChoix = -1;
            do {
                Console.Clear();
                Console.WriteLine("Menu");
                Console.WriteLine("1. Test 1");
                Console.WriteLine("2. Test 2");
                Console.WriteLine("3. Test 3");
                Console.WriteLine("Q. Quitter");
                Console.WriteLine("");

                lngChoix = -1;
                do {
                    ConsoleKeyInfo r = Console.ReadKey(true);
                    switch (r.Key) {
                        case ConsoleKey.D1:
                            lngChoix = 1;
                            break;

                        case ConsoleKey.D2:
                            lngChoix = 2;
                            break;

                        case ConsoleKey.D3:
                            lngChoix = 3;
                            break;

                        case ConsoleKey.Q:
                            lngChoix = 0;
                            break;
                    }
                } while (lngChoix == -1);

                if (lngChoix != 0) {
                    string strTestNo = "";
                    switch (lngChoix) {
                        case 1:
                            //1 execution Time: 
                            //Using GetLength
                            strTestNo = "1";
                            DebugX("Test #" + strTestNo);

                            watch.Restart();
                            for (lngY = 0; lngY < T1.GetLength(0); lngY++) {
                                strLine = "";
                                for (lngX = 0; lngX < T1.GetLength(1); lngX++) {
                                    // access T[j, i] here
                                    strDataX = T1[lngY, lngX];
                                    if (gblnDebug) {
                                        strLine = strLine + "[" + strDataX + "]";
                                    }
                                }
                                DebugX(strLine);
                            }
                            watch.Stop();

                            DebugX("");
                            break;

                        case 2:
                            //2 execution Time: 
                            //Using Virtual
                            strTestNo = "2";
                            lngOldY = 1;
                            strLine = "";
                            DebugX("Test #" + strTestNo);

                            //we need to abstract 0 as a position
                            watch.Restart();
                            for (int lngIndex = 1; lngIndex <= lngMaxLength; lngIndex++) {
                                lngY = ((lngIndex - 1) / lngMaxX) + 1;
                                lngX = lngIndex - ((lngY - 1) * lngMaxX);
                                if (gblnDebug) {
                                    if (lngOldY != lngY) {
                                        lngOldY = lngY;
                                        DebugX(strLine);
                                        strLine = "";
                                    }
                                }
                                strDataX = T1[lngY - 1, lngX - 1];
                                if (gblnDebug) {
                                    strLine = strLine + "[" + strDataX + "]";
                                }
                                //string strPosition = lngY.ToString() + "x" + lngX.ToString();
                                //Debug.Print(strPosition + "=" + strData);
                            }
                            watch.Stop();

                            DebugX(strLine);
                            DebugX("");
                            break;

                        case 3:
                            //3 execution Time: 
                            //Using Virtual with 1d array
                            strTestNo = "3";
                            lngOldY = 1;
                            strLine = "";
                            DebugX("Test #" + strTestNo);

                            //we need to abstract 0 as a position
                            watch.Restart();
                            for (int lngIndex = 1; lngIndex <= lngMaxLength; lngIndex++) {
                                if (gblnDebug) {
                                    lngY = ((lngIndex - 1) / lngMaxX) + 1;
                                    lngX = lngIndex - ((lngY - 1) * lngMaxX);
                                    if (lngOldY != lngY) {
                                        lngOldY = lngY;
                                        DebugX(strLine);
                                        strLine = "";
                                    }
                                }
                                strDataX = T2[lngIndex - 1];
                                if (gblnDebug) {
                                    strLine = strLine + "[" + strDataX + "]";
                                }
                                //string strPosition = lngY.ToString() + "x" + lngX.ToString();
                                //Debug.Print(strPosition + "=" + strData);
                            }
                            watch.Stop();

                            DebugX(strLine);
                            DebugX("");
                            break;
                    }

                    if (gblnDebug == false) {
                        Console.WriteLine(strTestNo + " execution Time: " + watch.ElapsedMilliseconds + "ms");
                    }
                    Console.ReadKey(true);
                }
            } while (lngChoix != 0);
        }

        private static void DebugX(string pstrData) {
            if (gblnDebug) {
                Console.WriteLine(pstrData);
            }
        }
    }
}
