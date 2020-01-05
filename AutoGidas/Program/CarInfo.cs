﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace AutoGidas
{
    public class CarInfo
    {
        public string left;
        public string right;
        public int averagePrice;
        public int price;
        public string average;
        public int amount;
        public double coefficient;
        public string picURL;
        public string run_cmd(string filename)
        {
            string dir = Environment.CurrentDirectory;
            dir = Directory.GetParent(dir).Parent.FullName;

            string fileName = String.Format(@"url", Directory.GetCurrentDirectory());
            File.WriteAllText(fileName, filename);

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = dir + "/py/dist/core2.exe";
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script

                    string s = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")

                    string GetLine(string text, int lineNo)
                    {
                        string[] lines = text.Replace("\r", "").Split('\n');
                        return lines.Length >= lineNo ? lines[lineNo - 1] : null;
                    }


                    char[] delimiterChars = { '_' };
                    string line1 = GetLine(s, 1);
                    string line2 = GetLine(s, 2);
                    string[] line11 = line1.Split(delimiterChars);
                    string[] line12 = line2.Split(delimiterChars);
                    string len = line11.Length.ToString();
                    left = "";
                    right= "";
                    for (int i = 0; i < Convert.ToInt32(len) - 1; ++i)
                    {
                        left += line11[i] + ":" + "\n";
                        right += line12[i] + "\n";
                    }
                    averagePrice = Convert.ToInt32(GetLine(s, 3));
                    price = Convert.ToInt32(GetLine(s, 4));
                    average = GetLine(s, 5);
                    amount = Convert.ToInt32(GetLine(s, 6));
                    coefficient = double.Parse(GetLine(s, 7), CultureInfo.InvariantCulture);
                    picURL = GetLine(s, 8);
                    return s;

                }
            }
        }
    }
}