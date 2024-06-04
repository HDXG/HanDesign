using Yarp.ReverseProxy.Configuration;

namespace HanDesign.Yarp.ReverseProxy
{
    public class ReverseProxyOptions
    {
        public const string Key = "ReverseProxyOptions";
        public ReverseProxyRoute[] Routes { get; set; }
        public ReverseProxyCluster[] Clusters { get; set; }
    }


    /// <summary>
    /// yarp配置文件中的route内容
    /// </summary>
    public class ReverseProxyRoute
    {
        public string RouteId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RouteClusterId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RouteMatchPath { get; set; }
        public RouteConfig GetRoteConfig()
        {
            return new RouteConfig()
            {
              RouteId = RouteId,
              ClusterId = RouteClusterId,
              Match=new RouteMatch()
              {
                  Path=RouteMatchPath
              }
            };
        }
        public RouteConfig GetSwaggerRouteConfig()
        {
            return new RouteConfig()
            {
                RouteId = RouteId + ".Swagger",
                ClusterId = RouteClusterId,
                Match = new RouteMatch()
                {
                    Path = $"/swagger/{RouteClusterId}/swagger.json"
                }
            };
        }
    }


    /// <summary>
    /// yarp配置文件中的  Clusters内容
    /// </summary>
    public class ReverseProxyCluster
    {
        public string ClusterId { get; set; }
        public string ClusterAddress { get; set; }
        public ClusterConfig GetClusterConfig()
        {
            return new ClusterConfig()
            {
                ClusterId = ClusterId,
                Destinations=new Dictionary<string, DestinationConfig>()
                {
                    { "destination1", new DestinationConfig(){ Address = ClusterAddress }}
                }
            };
        }
    }
}
