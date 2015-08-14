namespace BePositive
{
    using System;
    using System.Collections.Generic;

    public class BePositiveMain
    {
        public static void Main()
        {
            int countSequences = int.Parse(Console.ReadLine());
		
		    for (int i = 0; i < countSequences; i++)
            {
			    string[] input = Console.ReadLine().Trim().Split(' ');
			    var numbers = new List<int>();
			
			    for (int j = 0; j < input.Length; j++) 
                {
				    if (!input[j].Equals(string.Empty) ) 
                    {
					    int num = int.Parse(input[j]);
					    numbers.Add(num);	
				    }							
			    }
			
                string result = "";
			
			    for (int j = 0; j < numbers.Count; j++) 
                {				
				    int currentNum = numbers[j];
				
				    if (currentNum >= 0)
                    {
					    result +=string.Format("{0} ",
                            currentNum);
				    }
                    else 
                    {
                        if(j != numbers.Count - 1)
                        {
                            currentNum += numbers[j + 1];

                            if (currentNum >= 0)
                            {
                                result += string.Format("{0} ",
                                    currentNum);
                            }
                            j++;
                        }					
					    
                        
				    }
			    }

                if(!string.IsNullOrEmpty(result))
                {
                    Console.WriteLine(result);
                }
                else
                {
				    Console.WriteLine("(empty)");
			    } 			
		    }		
        }
    }
}
