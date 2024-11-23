using System.Data;
using ConsoleApp3.AdoDotNetExamples;
using ConsoleApp3.DapperExamples;
using ConsoleApp3.EFCoreExamples;


//using ConsoleApp3.DapperExamples;
//using ConsoleApp3.EFCoreExamples;
using Microsoft.Data.SqlClient;

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Write("3");
//adoDotNetExample.Create("Blog9", "Blog9", "Blog9");
//adoDotNetExample.Update("4", "Blg9");
//adoDotNetExample.Delete("4");


DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Write("4");
//dapperExample.Create("Blog3","B3","B4");
//dapperExample.Update("5", "Bg5");

EFCoreExample efCoreExample = new EFCoreExample();
//efCoreExample.Read();
//efCoreExample.Edit("F101C1E9-0255-4679-8A42-40BECF98ECEE");
//efCoreExample.Create("b","b","b");
//efCoreExample.Update("F101C1E9-0255-4679-8A42-40BECF98ECEE", "cc", "dd", "ee");
efCoreExample.Delete("F101C1E9-0255-4679-8A42-40BECF98ECEE");


