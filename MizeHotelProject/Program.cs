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
await chain.GetValue();
var valMem = await memoryService.GetValue();
if (valMem == null)
{
    var val = await fileSystemService.GetValue();
    Console.WriteLine(JsonSerializer.Serialize(val));
    await memoryService.Overrite(val);
    valMem = await memoryService.GetValue();
    Console.WriteLine(JsonSerializer.Serialize(valMem));
}
Console.ReadKey();