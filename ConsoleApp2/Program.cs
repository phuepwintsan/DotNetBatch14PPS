using System.Data;
using DotNetBatch14PPS.ConsoleApp2.AdoDotNetExamples;
using DotNetBatch14PPS.ConsoleApp2.DapperExamples;
using DotNetBatch14PPS.ConsoleApp2.EFCoreExamples;
using Microsoft.Data.SqlClient;


AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Edit("2");
//adoDotNetExample.Create("BlogTitle 7", "BlogAuthor 7", "BlogContent 7");
//adoDotNetExample.Update("4", "B4");
//adoDotNetExample.Delete("6");

DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Edit("3");
//dapperExampEFle.Create("BlogTitle 8", "BlogAuthor 8", "BlogContent 8");
//dapperExample.Update("4", "BT");
//dapperExample.Delete("5");

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Read();
