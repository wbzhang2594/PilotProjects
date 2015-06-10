using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanNuoTa1
{
    enum location
    {
        A = 0,
        B = 1,
        C = 2,
    }


    class Program
    {
        static Dictionary<location, Stack<int>> AllBlocks;
        static UInt64 Steps = 0;
        static void Main(string[] args)
        {
            const int totalPieces = 64;
            init(totalPieces);

            MoveOneBlock(totalPieces, location.A, location.C);
        }

        static void init(int totalPieces)
        {
            AllBlocks = new Dictionary<location, Stack<int>>();
            AllBlocks.Add(location.A, new Stack<int>());
            AllBlocks.Add(location.B, new Stack<int>());
            AllBlocks.Add(location.C, new Stack<int>());

            while (totalPieces > 0)
            {
                AllBlocks[location.A].Push(totalPieces--);
            }



        }

        static int MoveOnePiece(int ID, location FromLoc, location ToLoc)
        {
            //Console.WriteLine(string.Format("第{3}步: 移动大小为{0}的一块, 从{1}, 到{2}. ", ID, FromLoc.ToString(), ToLoc.ToString(), Steps));
            Steps++;
            int top = AllBlocks[FromLoc].Pop();
            AllBlocks[ToLoc].Push(top);

            return top;
        }

        static void MoveOneBlock(int Size, location FromLoc, location ToLoc)
        {
            //int Id_TheTopOf_Source = AllBlocks[FromLoc].Last();

            //if(Id_TheTopOf_Source!=1 ||AllBlocks[FromLoc].Count()<Size)
            //{
            //    throw new Exception("算法失败.");
            //}

            //Console.WriteLine(string.Format("移动大小为{0}一堆, 从{1}, 到{2}.", Size, FromLoc.ToString(), ToLoc.ToString()));

            if(Size > 0)
            {
                int nextSize = Size -1;
                location StartOfNext = FromLoc;
                location TiaoBan = FindTiaoBan(FromLoc, ToLoc);
                MoveOneBlock(nextSize, FromLoc, TiaoBan);

                MoveOnePiece(Size, FromLoc, ToLoc);

                MoveOneBlock(nextSize, TiaoBan, ToLoc);
            }

        }

        private static location FindTiaoBan(location FromLoc, location ToLoc)
        {
            //if(FromLoc == location.A && ToLoc==location.C
            //    ||FromLoc == location.C && ToLoc == location.A)
            //{
            //    return location.B;
            //}

            int SubTotal = (int)FromLoc + (int)ToLoc;
            int result = 3 - SubTotal;

            return (location)result;
        }
    }
}
