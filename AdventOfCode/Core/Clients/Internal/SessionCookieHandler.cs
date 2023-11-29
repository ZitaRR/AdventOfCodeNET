using AdventOfCodeNet.Cli.Providers;

namespace AdventOfCodeNet.Core.Clients.Internal;

internal sealed class SessionCookieHandler : DelegatingHandler
{
    public SessionCookieHandler()
        : base(new HttpClientHandler()) { }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        request.Headers.Add("Cookie", $"session={SessionCookieProvider.SessionCookie}");
        return base.SendAsync(request, ct);
    }
}
