// See https://aka.ms/new-console-template for more information
using UniAPI;
using UniAPI.Test;
using UniAPI.UniAPI;

Console.WriteLine("Hello, World!");


var api = new UniApiClient();

var config = new UniRequestConfig<Person,object>
{
    BaseUrl = "https://api.sirshoo.com/",
    OnError = Error,
    OnResult = Result
};

var result = api.Get(new UniRequest<Person,object>
{
    Protocol = ApiProtocol.Rest,
    PathOrQuery = "api/App/GetFoodList",
    Config = config
});

Console.WriteLine(result);
Console.ReadKey();

void Error(UniRequest<Person,object> v1,int v2, string v3)
{
    Console.WriteLine("Test");
}

void Result(UniResponse<Person> v1, int v2, string v3)
{
    Console.WriteLine("Test");
}