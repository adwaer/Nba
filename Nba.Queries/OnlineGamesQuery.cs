using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Nba.Cqrs;
using Nba.Domain.Dto;

namespace Nba.Queries
{
    public class OnlineGamesQuery : IQuery<string, Task<OnlineGame[]>>
    {
        public async Task<OnlineGame[]> Execute(string url)
        {
            var gameNodes = GetNodes(url
                , "//*[@id=\"lineContainer\"]/table/tbody/tr[@class=\"trSegment\"]");

            return null;
        }

        private static HtmlNodeCollection GetNodes(string url, string xpath)
        {
            int tryCount = 1;
            HtmlNodeCollection gamesNodes;
            l1:
            try
            {
                var request = WebRequest.CreateHttp(url);
                var response = request.GetResponse();

                using (var gameStream = response.GetResponseStream())
                {
                    var doc = new HtmlDocument();
                    doc.Load(gameStream, true);

                    gamesNodes =
                        doc.DocumentNode.SelectNodes(xpath);
                }
            }
            catch
            {
                if (tryCount < 10)
                {
                    tryCount++;
                    Thread.Sleep(1000);
                    goto l1;
                }
                throw;
            }
            return gamesNodes;
        }
    }
}
