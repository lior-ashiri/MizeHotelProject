using MizeHotelProject;
using MizeHotelProject.objects;
using MizeHotelProject.Storages.implementations;
using MizeHotelProject.Storages.interfaces;
using System.Text.Json;

// here I sinulated 3 runs. press any key after each run to continue simulation
await GetDataFromWebService();
Console.Clear();
await GetDataFromFileSystem();
Console.Clear();
await GetDataFromMemoryService();
Console.Clear();
Console.WriteLine("end of sinulation");
Console.ReadKey();
async Task GetDataFromMemoryService()
{
    WriteSinulationHeader("start sinulation of getting data only from memory service");
    var memoryService = new MemoryService<ExchangeRate>(1);
    var dataToSave = await GetDataFromTestHelperFile();
    var data = JsonSerializer.Deserialize<DataWithDate<ExchangeRate>>(dataToSave);
    await memoryService.Overrite(data.Value);
    await RunTest(memoryService);
}

void WriteSinulationHeader(string text)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ResetColor();
}

async Task GetDataFromWebService()
{

    WriteSinulationHeader("start sinulation of getting data only from web service");
    ClearDataFile();
    await RunTest();
}
async Task GetDataFromFileSystem()
{
    WriteSinulationHeader("start sinulation of getting data only from fileSystem service");
    await CopyDataFromDataSourceTestOnlyToData();
    await RunTest();
    

}
async Task RunTest(MemoryService<ExchangeRate>? possibleMemoryService = null)
{
    var chain = CreateChain(possibleMemoryService);
    var value = await chain.GetValue();
    var dataAsJson = JsonSerializer.Serialize(value);
    Console.WriteLine(dataAsJson);
    WriteSinulationHeader("end of sinulation");
    Console.ReadKey();
}

static async Task CopyDataFromDataSourceTestOnlyToData()
{
    ClearDataFile();
    string dataFromSource = await GetDataFromTestHelperFile();
    await File.WriteAllTextAsync(Consts.pathToJson, dataFromSource);
}
static void ClearDataFile()
{
    File.Delete(Consts.pathToJson);
}
static ChainResource<ExchangeRate> CreateChain(MemoryService<ExchangeRate>? possibleMemoryService = null)
{
    var memoryService = possibleMemoryService ?? new MemoryService<ExchangeRate>(1);
    var fileSystemService = new FileSystemService<ExchangeRate>(Consts.pathToJson, 4);
    var webService = new WebService<ExchangeRate>(Consts.pathToOpehExchange);
    var list = new List<IReadableStorage<ExchangeRate>>() { memoryService, fileSystemService, webService };
    var chain = new ChainResource<ExchangeRate>(list);
    return chain;
}

static async Task<string> GetDataFromTestHelperFile()
{
    return await File.ReadAllTextAsync(Consts.DataSourceToTestOnlyPath);
}