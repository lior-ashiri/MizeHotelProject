using MizeHotelProject;
using MizeHotelProject.objects;
using MizeHotelProject.Storages.implementations;
using MizeHotelProject.Storages.interfaces;
using System.Text.Json;

Console.WriteLine("Hello, World!");
var memoryService = new MemoryService<ExchangeRate>(1);
var fileSystemService = new FileSystemService<ExchangeRate>(Consts.pathToJson,4);
var webService = new WebService<ExchangeRate>(Consts.pathToOpehExchange);
var List = new List<IReadableStorage<ExchangeRate>>() { memoryService, fileSystemService, webService };

var chain = new ChainResource<ExchangeRate>(List);
var value = await chain.GetValue();
Console.WriteLine(value.ToString());
Console.ReadKey();