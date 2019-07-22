using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace c_learn
{

    class queen
    {
        public int[] graphic = new int[8];//int[8] queen in each col
        public int value = 0;//collapse times
        public queen()//constructor initial new graphic and collapse times
        {
            this.newGraphic();
        }
        //initial new graphic
        //pre: none
        //post: initial graphic(int[8])
        public void newGraphic()
        {
            int i;
            Random ranNum = new Random(Guid.NewGuid().GetHashCode());
            for (i = 0; i < 8; i++)
            {
                this.graphic[i] = ranNum.Next(8);
            }
            this.collapse();
        }
        //count collapse times return to value
        //pre: none
        //post: collapse count(int)
        public int collapse()
        {
            int count = 0;
            int i, j;
            for (i = 0; i < 8; i++)
            {
                for (j = i + 1; j < 8; j++)
                {
                    if (this.graphic[j] == this.graphic[i]) count++;
                    if (Math.Abs(this.graphic[j] - this.graphic[i]) == (j - i)) count++;
                }
            }
            this.value = count;
            return count;
        }
        //cross dna between a and b
        //pre :queen a,queen b
        //post:queen child
        public queen cross(queen a, queen b)
        {
            queen Child1 = new queen();
            queen Child2 = new queen();
            int i;
            for (i = 0; i < 8; i++)
            {
                if(i<4){
                    Child2.graphic[i] = a.graphic[i];
                    Child1.graphic[i] = b.graphic[i];
                }
                else{
                    Child1.graphic[i] = a.graphic[i];
                    Child2.graphic[i] = b.graphic[i];
                }
            }
            if (Child1.collapse() > Child2.collapse())
            {
                return Child1;
            }
            else
            {
                return Child2;
            }
        }
        //copy b to this
        //pre: b(queen)
        //post: this = b
        public void equal(queen b)
        {
            int i;
            for (i = 0; i < 8; i++)
            {
                b.graphic[i] = this.graphic[i];
            }
            b.value = this.value;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            queen[] parent = new queen[50];
            int i, j, min = 0;
            int repreasent = 1;
            for (i = 0; i < 50; i++)
            {
                parent[i] = new queen();
            }
            Program program = new Program();
            for (i = 0; i < 200000; i++)
            {
                if(i%1000 == 0)program.death(parent);
                repreasent = program.generation(parent);
                if (parent[repreasent].value == 0) break;
            }
            if (parent[repreasent].value == 0) { min = repreasent; }
            else
            {
                for (i = 0; i < 50; i++)
                {
                    if (parent[min].value > parent[i].value)
                    {
                        min = i;
                    }
                }
            }
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if (j == 0) Console.Write("\n");
                    if (parent[min].graphic[i] == j)
                        Console.Write("1 ");
                    else
                        Console.Write("0 ");
                }
            }
            Console.WriteLine("\nanswer:{0}", parent[min].value);
        }
        // cross parent and generate child, 5 childs who has least value will death, 5 new childs replace the death 5
        int generation(queen[] p)
        {
            int end = 1;
            int i;
            queen[] childs = new queen[50];

            Random ranNum = new Random(Guid.NewGuid().GetHashCode());
            for (i = 0; i < 50; i++)
            {
                childs[i] = new queen();
                childs[i].cross(p[ranNum.Next(49)], p[ranNum.Next(49)]);
            }
            // 5 collapse the most will die

            for (i = 0; i < 50; i++)
            {
                childs[i].Equals(p[i]);
                if (childs[i].value == 0)
                {
                    end = i;
                }
            }
            return end;
        }
        void death(queen[] p)
        {
            int[] death = new int[5];
            int i, j;
            int tmp = 50, tmp2;
            for (i = 0; i < 50; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    if (tmp == 50 && p[death[j]].value < p[i].value)
                    {
                        tmp = j;
                        death[j] = i;
                    }
                    else if (tmp < 50)
                    {
                        tmp2 = j;
                        death[j] = tmp;
                        tmp = tmp2;
                    }
                }
            }
            for (i = 0; i < 5; i++)
            {
                p[death[i]].newGraphic();
            }
            return;
        }
    }
}
