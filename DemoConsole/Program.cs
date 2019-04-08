using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsole
{
    class Program
    {
        static void Main01(string[] args)
        {

            //CallWebApi();

            var test = SingleTon.Instance;

            Console.ReadLine();
        }

        public static int Reverse(int x)
        {
            int abs = Math.Abs(x);
            string sign = x < 0 ? "-" : string.Empty;
            char[] chars = abs.ToString().ToCharArray();
            char[] resultChars = new char[chars.Length];

            int index = 0;
            while (index < chars.Length)
            {
                resultChars[index] = chars[chars.Length - index - 1];
                index++;
            }

            int result = 0;
            try
            {
                checked
                {
                    result = int.Parse($"{sign}{new string(resultChars)}");
                }
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public static int[] TwoSum(int[] nums, int target)
        {

            if (nums?.Length < 1)
            {
                throw new Exception("Can't find the solution, please check the 'nums' parameter.");
            }

            var result = new int[2] { 0, 0 };

            for (int i = 0; i < nums.Length - 1; i++)
            {
                result[0] = i;
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        result[1] = j;
                        return result;
                    }
                }
            }

            throw new Exception("Can't find the solution, please check the 'nums' parameter.");
        }

        public static void CallWebApi()
        {
            HttpClient client = new HttpClient();

            string url = "http://ndc.emdlz.com.cn/ImageCache/DownloadCenter/20160704/05%20TTT%20-BI%20reporting%200629.pptx";
            url = "http://ndc.emdlz.com.cn/wopi/files/20170105/0/20170105153114.pptx&access_token=06l hXK6zWTUi";
            //ASP.NET_SessionId=v1l2katb3qjywej3b3yga2ma; BCSI-CS-50cf319c8a3b1a7d=2; BCSI-CS-e7fbc31aa68e8857=2; .ASPXAUTH=1BC8FFED21FC859AF8AFEF42F48A10A18A70F098D2C54490046F4779313BF96FD0672A1E9B394A4021ADDF09FA1558739E3BF3BD0C435617BE829E4E9FE8B49E13414E944DBB2D82257006A4EA168351D317E3D4F69AEA70455B937F902B5FA54C0307AAC5EED3E2B25C38AA2E03D6A6336D1B74F42FF48338AD3AB02C08A647DFC7DACAB3A0533425DFE833F9E8262CCE4D943A15075D9422F372BC97C9B603D1C1C072C980AA9FEFB7C273AD55E3AC965728E39A665FC6C7D7D4F3C0C10E30B85E58E7DDA6209BFCCD431E401F1CAB


            client.DefaultRequestHeaders.Add("ASP.NET_SessionId", "v1l2katb3qjywej3b3yga2ma");
            client.DefaultRequestHeaders.Add("BCSI-CS-50cf319c8a3b1a7d", "2");
            client.DefaultRequestHeaders.Add("BCSI-CS-e7fbc31aa68e8857", "2");
            client.DefaultRequestHeaders.Add(".ASPXAUTH", "1BC8FFED21FC859AF8AFEF42F48A10A18A70F098D2C54490046F4779313BF96FD0672A1E9B394A4021ADDF09FA1558739E3BF3BD0C435617BE829E4E9FE8B49E13414E944DBB2D82257006A4EA168351D317E3D4F69AEA70455B937F902B5FA54C0307AAC5EED3E2B25C38AA2E03D6A6336D1B74F42FF48338AD3AB02C08A647DFC7DACAB3A0533425DFE833F9E8262CCE4D943A15075D9422F372BC97C9B603D1C1C072C980AA9FEFB7C273AD55E3AC965728E39A665FC6C7D7D4F3C0C10E30B85E58E7DDA6209BFCCD431E401F1CAB");
            //http://docview.emdlz.com.cn/p/PowerPointFrame.aspx?PowerPointView=ReadingView&Embed=1&WOPISrc=http://ndc.emdlz.com.cn/wopi/files/20170105/0/20170105153114.pptx&access_token=06l hXK6zWTUi&trainid=58&fileid=104&userid=12500001
            //HttpResponseMessage response = client.GetAsync("http://ndc.emdlz.com.cn/wopi/files/20170105/0/20170105153114.pptx&access_token=06l hXK6zWTUi").Result;
            HttpResponseMessage response = client.GetAsync(url).Result;


            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }


    //Definition for singly-linked list.
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

    public class Solution
    {
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var queue1 = new Queue<int>();
            var queue2 = new Queue<int>();
            var stack3 = new Stack<int>();

            ListNode listNode = l1;
            while (listNode != null)
            {
                queue1.Enqueue(listNode.val);
                listNode = listNode.next;
            }

            listNode = l2;
            while (listNode != null)
            {
                queue2.Enqueue(listNode.val);
                listNode = listNode.next;
            }

            int flag = 0;
            int len = Math.Min(queue1.Count, queue2.Count);
            for (int i = 0; i < len; i++)
            {
                int sum = queue1.Dequeue() + queue2.Dequeue();
                int value = sum + flag;
                if (value >= 10)
                {
                    value = value % 10;
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }

                stack3.Push(value);
            }

            if (queue1.Count != 0)
            {
                len = queue1.Count;
                for (int i = 0; i < len; i++)
                {
                    int value = queue1.Dequeue() + flag;
                    if (value >= 10)
                    {
                        value = value % 10;
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                    stack3.Push(value);
                }
            }

            if (queue2.Count > 0)
            {
                len = queue2.Count;
                for (int i = 0; i < len; i++)
                {
                    int value = queue2.Dequeue() + flag;
                    if (value >= 10)
                    {
                        value = value % 10;
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                    stack3.Push(value);
                }
            }

            if (flag == 1)
            {
                stack3.Push(flag);
            }

            ListNode next = new ListNode(stack3.Pop());
            ListNode result = next;
            len = stack3.Count;

            for (int i = 0; i < len; i++)
            {
                result = new ListNode(stack3.Pop()) { next = next };
                next = result;
            }

            return result;
        }

        public static bool IsPowerOfTwo(int n)
        {
            bool result = false;
            int counts = 0;
            while (n != 0 && n > 0)
            {
                if ((n & 1) == 1)
                {
                    counts++;
                }

                n = n >> 1;
                if (counts > 1)
                {
                    result = false;
                }
            }

            if (counts == 1)
            {
                result = true;
            }

            return result;
        }


        public static double ChampagneTower(int poured, int query_row, int query_glass)
        {
            if (query_glass > query_row)
            {
                throw new Exception("Please check the input!");
            }

            double result = 0;
            int pre_row_sum = (1 + query_row) * query_row / 2;

            if (poured <= pre_row_sum)
            {
                result = 0;
            }
            else if (poured >= pre_row_sum + query_row + 1)
            {
                result = 1;
            }
            else
            {
                double pre = (double)(poured - pre_row_sum) / (double)(query_row * 2);
                if (query_glass == 0 || query_glass == query_row)
                {
                    result = pre;
                }
                else
                {
                    result = pre * 2;
                }
            }

            return result;
        }

        public static string LongestSubstring(string input)
        {
            var result = new List<char>();
            var chars = new List<char>();
            int index = 0;

            while (index < input.Length)
            {
                if (!chars.Contains(input[index]))
                {
                    chars.Add(input[index]);
                }
                else
                {

                }

                index--;
            }

            return result.ToString();
        }
    }



    //public sealed class SingleTon
    //{
    //    public static readonly SingleTon Instance;

    //    static SingleTon()
    //    {
    //        Instance = new SingleTon();
    //    }
    //}


    public sealed class SingleTon
    {
        static SingleTon()
        {
            Console.WriteLine("static SingleTon");



            Instance = new SingleTon();
        }

        public SingleTon()

        {

            int[] array = { 1, 2, 3, 4 };
            Console.WriteLine("public SingleTon");
        }

        public static SingleTon Instance { get; private set; } = new SingleTon();
    }

    public abstract class BaseClass
    {
        public abstract BaseClass Creator();
    }

    public class ImplementA : BaseClass
    {
        public override BaseClass Creator()
        {
            return new ImplementA();
        }
    }

    public abstract class IFactory
    {
        public abstract BaseClass CreatorFactory();
    }

    public class AFactory : IFactory
    {
        public override BaseClass CreatorFactory()
        {
            var aImplement = new ImplementA();
            return aImplement.Creator();
        }
    }

    public class MainProcess
    {
        public MainProcess()
        {


        }
    }
}
