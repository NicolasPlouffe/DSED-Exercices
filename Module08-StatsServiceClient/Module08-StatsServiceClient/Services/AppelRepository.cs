using Module08_StatsServiceClient.Models;
using System.Collections.Generic;

namespace Module08_StatsServiceClient.Services;

public class AppelRepository
{
    public List<AppelModel> Appels { get; } = new List<AppelModel>();
}