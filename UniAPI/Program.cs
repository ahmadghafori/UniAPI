using UniAPI;
using UniAPI.Test;
using UniAPI.UniAPI;

Console.WriteLine("Hello, World!");


var api = new UniApiClient();

var config = new UniRequestConfig
{
    BaseUrl = "http://localhost:28268"
};

var result = await api.Get(new UniRequest<DocumentationLists, DocumentationList>
{
    Protocol = ApiProtocol.GraphQL,
    PathOrQuery = "/portal/graphql/",
    Config = config,
    Body = new DocumentationList()
});

Console.WriteLine(result);
Console.ReadKey();

public class DocumentationItem
{
    public string Name { get; set; } = "";
}

public class DocumentationData
{
    public List<DocumentationItem> Items { get; set; } = new();
}

public class DocumentationLists
{
    public DocumentationData Data { get; set; } = new();
    public string Message { get; set; } = "";
}

// Wrapper برای GraphQL
public class GraphQLDocumentationListResponse
{
    public DocumentationList Documentation_List { get; set; } = new();
}

public class DocumentationList
{
    // اگر input فیلد داشت، اینجا properties اضافه می‌شد
}