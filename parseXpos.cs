using System;
using System.Collections.Generic;
using System.IO;

namespace parseXpos
{
    class parseXpos
    {
        static void Main(string[] args)
        {
            string XpoDir = getArg(args,"-XpoDir");
            string CombinedXpoFile = getArg(args,"-CombinedXpoFile");

            if (XpoDir == "" || CombinedXpoFile == "")
            {
                usage();
                return;
            }

            string objectType = "Unknown";
            string objectName = "Unknown";
            int fileNumber = 1;

            StreamReader reader = new StreamReader(CombinedXpoFile);
            StreamWriter writer = new StreamWriter(XpoDir + @"\" + objectType + "_" + objectName);
            
            string currentLine;
            List<string> fileLines = new List<string>();
            int listIndex = 0;

            //Read all the lines of the file into a List
            do
            {
                currentLine = reader.ReadLine();
                fileLines.Add(currentLine);
            }
            while (reader.Peek() != -1);

            reader.Close();

            List<string>.Enumerator listEnum = fileLines.GetEnumerator();

            while (listEnum.MoveNext())
            {
                currentLine = listEnum.Current;
                objectName = "Start";
                objectType = "Unknown";                

                //If the current line is the start or end of an element
                if (currentLine.StartsWith("***Element: "))
                {
                    if (currentLine.StartsWith("***Element: END"))
                    {                        
                        objectName = "End";
                    }
                    else
                    {                        
                        string nameLine = fileLines[listIndex + 6];
                        int nameIndex = nameLine.IndexOf("#");
                        objectName = nameLine.Substring(nameIndex + 1);
                    }

                    //Get the Type of object
                    objectType = ObjectType(currentLine);
                                        
                    fileNumber++;                    
                    writer.Close();
                    writer = new StreamWriter(XpoDir + @"\" + objectType + "_" + objectName);
                }

                writer.WriteLine(currentLine);
                listIndex++;
            }

            writer.Close();

        }

        static void usage()
        {
            Console.WriteLine("Usage: parseXpos.exe -XpoDir <output path> -CombinedXpoFile <input file.xpo>"); 
        }
            
        static string getArg(string[] args, string argName)
        {
            string value = "";

            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == argName)
                {
                    value = args[i + 1];
                }
            }

            return value;
        }

        static string ObjectType(string currentLine)
        {
            string objectType = "Unknown";

            // TODO: Replace this huge if/else statement with a better pattern

            if (currentLine.StartsWith("***Element: VIE"))
            {
                objectType = "View";
            }
            else if (currentLine.StartsWith("***Element: DBT"))
            {
                objectType = "Table";                
            }
            else if (currentLine.StartsWith("***Element: MAP"))
            {
                objectType = "Map";                
            }
            else if (currentLine.StartsWith("***Element: UTS"))
            {
                objectType = "EDT";                
            }
            else if (currentLine.StartsWith("***Element: UTR"))
            {
                objectType = "EDT";                
            }
            else if (currentLine.StartsWith("***Element: UTD"))
            {
                objectType = "EDT";                
            }
            else if (currentLine.StartsWith("***Element: UTE"))
            {
                objectType = "EDT";                
            }
            else if (currentLine.StartsWith("***Element: UTI"))
            {
                objectType = "EDT";                
            }
            else if (currentLine.StartsWith("***Element: UTW"))
            {
                objectType = "EDT";                
            }
            else if (currentLine.StartsWith("***Element: DBE"))
            {
                objectType = "Enum";                
            }
            else if (currentLine.StartsWith("***Element: TCL"))
            {
                objectType = "TableCollection";                
            }
            else if (currentLine.StartsWith("***Element: MCR"))
            {
                objectType = "Macro";                
            }
            else if (currentLine.StartsWith("***Element: CLS"))
            {
                objectType = "Class";                
            }
            else if (currentLine.StartsWith("***Element: FRM"))
            {
                objectType = "Form";               
            }
            else if (currentLine.StartsWith("***Element: RST"))
            {
                objectType = "SectionTemplate";                
            }
            else if (currentLine.StartsWith("***Element: RG"))
            {
                objectType = "Report";                
            }
            else if (currentLine.StartsWith("***Element: QUE"))
            {
                objectType = "Query";                
            }
            else if (currentLine.StartsWith("***Element: JOB"))
            {
                objectType = "Job";                
            }
            else if (currentLine.StartsWith("***Element: MNU"))
            {
                objectType = "Menu";                
            }
            else if (currentLine.StartsWith("***Element: FTM"))
            {
                objectType = "MenuItem";                
            }
            else if (currentLine.StartsWith("***Element: REF"))
            {
                objectType = "Reference";                
            }
            else if (currentLine.StartsWith("***Element: WFC"))
            {
                objectType = "WorkflowCategory";                
            }
            else if (currentLine.StartsWith("***Element: WFA"))
            {
                objectType = "WorkflowApproval";
            }
            else if (currentLine.StartsWith("***Element: WFL"))
            {
                objectType = "WorkflowTemplate";               
            }
            else if (currentLine.StartsWith("***Element: SVC"))
            {
                objectType = "Service";                
            }
            else if (currentLine.StartsWith("***Element: PRN"))
            {
                objectType = "Project";                
            }

            return objectType;
        }
    }
}
