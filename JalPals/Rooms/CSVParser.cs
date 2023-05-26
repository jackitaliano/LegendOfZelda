using System.Collections.Generic;
using System.IO;

namespace JalPals.Rooms;

public class CSVParser
{
    public List<string[]> Parse(string filePath, char delimiter = ',')
    {
        List<string[]> result = new List<string[]>();

        using (var reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] fields = line.Split(delimiter);
                result.Add(fields);
            }
        }

        return result;
    }
}
