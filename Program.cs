using System;
using System.Data;
using System.IO;

namespace ImageExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            Services services = new Services();
            Console.WriteLine("Enter the Path for The Image Files : ");
            string path = Console.ReadLine();
            List<Patient> list = new List<Patient>();
            list = data.PatientDataService();

            foreach (var x in list.Select(i => i.OfficeID).Distinct())
            {
                if (!Directory.Exists(path + $"\\{x}"))
                {
                    Directory.CreateDirectory(path + $"\\{x}");
                }
                foreach (var p in list.Where(a => a.OfficeID == x).Select(j => j.PatientID))
                {
                    DataTable dataTable = new DataTable();
                    dataTable = data.ImageDataService(p);
                    if (!Directory.Exists(path + $"\\{x}\\{p}"))
                    {
                        Directory.CreateDirectory(path + $"\\{x}\\{p}");
                    }
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        string imageFilePath = path + $"\\{x}\\{p}";
                        if (!Directory.Exists(imageFilePath))
                        {
                            Directory.CreateDirectory(imageFilePath);
                        }
                        services.ImageService((byte[])dr["Image"], imageFilePath + $"\\{dr["Pac"]}~{dr["auxauto"]}");
                    }
                }
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}