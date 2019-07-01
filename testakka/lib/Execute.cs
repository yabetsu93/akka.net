using System;
using System.Threading.Tasks;

public class Execute 
{
    public async Task Run(Messages mess)
    {
        await Task.Run(() => {Console.WriteLine($"hello world {mess}");}); 
    }
}