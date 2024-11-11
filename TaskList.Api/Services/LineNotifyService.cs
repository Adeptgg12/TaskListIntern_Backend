using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Api.Services;
public class LineNotifyService
{
    private readonly HttpClient _httpClient;
    private const string LineNotifyApiUrl = "https://notify-api.line.me/api/notify";
    private const string LineNotifyToken = "fKyDLagNT3rmGm623DJlbxphgmtrvDBCM3P2cB2Quks"; // นำ Access Token ที่ได้จาก LINE Notify มาใส่

    public LineNotifyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendLineNotificationAsync(string message)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("message", message)
        });

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {LineNotifyToken}");
        var response = await _httpClient.PostAsync(LineNotifyApiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to send LINE notification");
        }
    }
}