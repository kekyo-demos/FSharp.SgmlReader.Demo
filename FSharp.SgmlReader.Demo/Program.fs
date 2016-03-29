open System.Net.Http
open System.Xml.Linq
open CenterCLR.Sgml

let asyncFetch (url: string) = async {
    let requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
    do requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0")
    use httpClient = new HttpClient()
    use! response = httpClient.SendAsync(requestMessage) |> Async.AwaitTask
    use! stream = response.Content.ReadAsStreamAsync() |> Async.AwaitTask
    return SgmlReader.Parse(stream)
}

[<EntryPoint>]
let main argv =
    let url = "http://www.aozora.gr.jp/index_pages/person81.html"
    let document = asyncFetch url |> Async.RunSynchronously
    printfn "%A" document
    0
